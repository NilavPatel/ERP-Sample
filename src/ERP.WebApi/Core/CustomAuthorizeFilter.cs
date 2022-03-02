using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace ERP.WebApi.Core
{
    public class CustomAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _config;
        public CustomAuthorizeFilter(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var token = context.HttpContext.Request.Headers[HeaderNames.Authorization].FirstOrDefault()?.Split(" ").Last();
            if (token == null)
            {
                throw new ArgumentNullException("Authorization Is Not Passed In Header");
            }
            var claims = GetClaimsFromJwt(token);
            if (claims.Any())
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
                context.HttpContext.User = user;
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _config.GetValue<string>("JWTSecretKey");
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims;
        }
    }
}