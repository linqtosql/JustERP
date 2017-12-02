using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using Abp.UI;
using JustERP.Authentication.External;
using JustERP.Authentication.JwtBearer;
using JustERP.Authorization.Users;
using JustERP.Core.User;
using JustERP.Models.TokenAuth;
using JustERP.Web.Core.User.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustERP.Web.Core.User.Controllers
{
    [Route("api/[controller]/[action]")]
    public class TokenAuthController : JustERPControllerBase
    {
        private readonly UserLogInManager _logInManager;
        private readonly ITenantCache _tenantCache;
        private readonly TokenAuthConfiguration _configuration;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;
        private readonly IExternalAuthManager _externalAuthManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public TokenAuthController(
            UserLogInManager logInManager,
            ITenantCache tenantCache,
            TokenAuthConfiguration configuration,
            IExternalAuthConfiguration externalAuthConfiguration,
            IExternalAuthManager externalAuthManager,
            UserRegistrationManager userRegistrationManager)
        {
            _logInManager = logInManager;
            _tenantCache = tenantCache;
            _configuration = configuration;
            _externalAuthConfiguration = externalAuthConfiguration;
            _externalAuthManager = externalAuthManager;
            _userRegistrationManager = userRegistrationManager;
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Authenticate([FromBody] UserAuthenticateModel model)
        {
            var loginResult = await GetLoginResultAsync(
                model.Phone,
                model.PhoneCode,
                GetTenancyNameOrNull()
            );

            return CreateAuthenticateResultModel(loginResult);
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> Register([FromBody] UserAuthenticateModel model)
        {
            var loginResult = await _logInManager.RegisterAsync(model.Phone, model.PhoneCode);

            return CreateAuthenticateResultModel(loginResult);
        }

        private AuthenticateResultModel CreateAuthenticateResultModel(UserLoginResult loginResult)
        {
            var accessToken = CreateAccessToken(CreateJwtClaims(loginResult.Identity));

            return new AuthenticateResultModel
            {
                AccessToken = accessToken,
                EncryptedAccessToken = GetEncrpyedAccessToken(accessToken),
                ExpireInSeconds = (int) _configuration.Expiration.TotalSeconds,
                UserId = loginResult.UserId
            };
        }

        [HttpPost]
        public async Task<AuthenticateResultModel> RegisterOrAuthenticate([FromBody] UserAuthenticateModel model)
        {
            var loginResult = await _logInManager.LoginAsync(model.Phone, model.PhoneCode);
            if (loginResult.Result != AbpLoginResultType.Success)
            {
                loginResult = await _logInManager.RegisterAsync(model.Phone, model.PhoneCode);
            }

            return CreateAuthenticateResultModel(loginResult);
        }


        [HttpGet]
        public List<ExternalLoginProviderInfoModel> GetExternalAuthenticationProviders()
        {
            return ObjectMapper.Map<List<ExternalLoginProviderInfoModel>>(_externalAuthConfiguration.Providers);
        }

        private async Task<Authorization.Users.User> RegisterExternalUserAsync(ExternalAuthUserInfo externalUser)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                externalUser.Name,
                externalUser.Surname,
                externalUser.EmailAddress,
                externalUser.EmailAddress,
                Authorization.Users.User.CreateRandomPassword(),
                true
            );

            user.Logins = new List<UserLogin>
            {
                new UserLogin
                {
                    LoginProvider = externalUser.Provider,
                    ProviderKey = externalUser.ProviderKey,
                    TenantId = user.TenantId
                }
            };

            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private async Task<ExternalAuthUserInfo> GetExternalUserInfo(ExternalAuthenticateModel model)
        {
            var userInfo = await _externalAuthManager.GetUserInfo(model.AuthProvider, model.ProviderAccessCode);
            if (userInfo.ProviderKey != model.ProviderKey)
            {
                throw new UserFriendlyException(L("CouldNotValidateExternalUser"));
            }

            return userInfo;
        }

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private async Task<UserLoginResult> GetLoginResultAsync(string phone, string phoneCode, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(phone, phoneCode);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw new UserFriendlyException();
            }
        }

        private string CreateAccessToken(IEnumerable<Claim> claims, TimeSpan? expiration = null)
        {
            var now = DateTime.UtcNow;

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(expiration ?? _configuration.Expiration),
                signingCredentials: _configuration.SigningCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        private static List<Claim> CreateJwtClaims(ClaimsIdentity identity)
        {
            var claims = identity.Claims.ToList();
            var nameIdClaim = claims.First(c => c.Type == ClaimTypes.NameIdentifier);

            // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, nameIdClaim.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            });

            return claims;
        }

        private string GetEncrpyedAccessToken(string accessToken)
        {
            return SimpleStringCipher.Instance.Encrypt(accessToken, AppConsts.DefaultPassPhrase);
        }
    }
}
