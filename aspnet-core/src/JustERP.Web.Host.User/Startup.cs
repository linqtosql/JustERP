using Abp.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace JustERP.Web.Host.User
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "localhost";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAbp(); //Initializes ABP framework.
            app.UseCors(DefaultCorsPolicyName); //Enable CORS!

            app.UseStaticFiles();

            app.UseAuthentication();
            //app.UseJwtTokenMiddleware();

#if FEATURE_SIGNALR
//Integrate to OWIN
            app.UseAppBuilder(ConfigureOwinServices);
#endif

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "defaultWithArea",
                    template: "{area}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            //// Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUI(options =>
            {
                options.InjectOnCompleteJavaScript("/swagger/ui/abp.js");
                options.InjectOnCompleteJavaScript("/swagger/ui/on-complete.js");
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "JustERP API V1");
            });
        }
    }
}
