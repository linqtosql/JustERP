using System;
using Abp.AspNetCore;
using Abp.Castle.Logging.Log4Net;
using Castle.Facilities.Logging;
using JustERP.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Linq;
using System.Reflection;
using Abp.Extensions;
using Abp.Json;
using Abp.Timing;
using JustERP.Authentication.JwtBearer;
using JustERP.Configuration;
using JustERP.SignalR.Hub;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;

#if FEATURE_SIGNALR
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using JustERP.Owin;
using Abp.Owin;
#endif

namespace JustERP.Web.Host.User
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IHostingEnvironment env)
        {
            _appConfiguration = env.GetAppConfiguration();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddMvc(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory(DefaultCorsPolicyName));
            });
            services.PostConfigure<MvcJsonOptions>(options =>
            {
                options.SerializerSettings.ContractResolver = new CustomContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);
            //Configure CORS for angular2 UI
            services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    //App:CorsOrigins in appsettings.json can contain more than one address with splitted by comma.
                    builder
                        .WithOrigins(_appConfiguration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            //Swagger - Enable this line and the related lines in Configure method to enable swagger UI
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "JustERP API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new ApiKeyScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
            });

            //add signalR
            services.AddSignalR();

            //Configure Abp and Dependency Injection
            return services.AddAbp<JustERPWebHostUserModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig("log4net.config")
                );
            });
        }

        class CustomContractResolver : AbpContractResolver
        {
            
            protected override void ModifyProperty(MemberInfo member, JsonProperty property)
            {
                if (property.PropertyType != typeof(DateTime) && property.PropertyType != typeof(DateTime?))
                {
                    return;
                }

                property.Converter = new AbpDateTimeConverter()
                {
                    DateTimeFormat = "yyyy/MM/dd HH:mm:ss"
                };
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Clock.Provider = ClockProviders.Unspecified;

            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); //Initializes ABP framework.
            app.UseCors(DefaultCorsPolicyName); //Enable CORS!

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

#if FEATURE_SIGNALR
//Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            app.UseSignalR(routes =>
            {
                routes.MapHub<ExpertChatHub>("/chat");
            });

            // letsencrypt router
            //app.UseRouter(r =>
            //{
            //    r.MapGet(".well-known/acme-challenge/{id}", async (request, response, routeData) =>
            //    {
            //        var id = routeData.Values["id"] as string;
            //        var file = Path.Combine(env.WebRootPath, ".well-known", "acme-challenge", id);
            //        await response.SendFileAsync(file);
            //    });
            //});

            app.UseMvc(routes =>
            {
                // disabled when none application services
                //routes.MapRoute(
                //    name: "defaultWithArea",
                //    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            //// Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                //options.InjectOnCompleteJavaScript("/swagger/ui/abp.js");
                //options.InjectOnCompleteJavaScript("/swagger/ui/on-complete.js");

                options.SwaggerEndpoint("/swagger/v1/swagger.json", "JustERP API V1");
            });

        }

#if FEATURE_SIGNALR
        private static void ConfigureOwinServices(IAppBuilder app)
        {
            app.Properties["host.AppName"] = "JustERP";

            app.UseAbp();
            
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
#endif
    }
}
