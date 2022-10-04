using System.Security.Claims;
using ERP.Application.Core.Helpers;
using ERP.Domain.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace ERP.WebApi.Core
{
    public class CustomAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration _config;
        private readonly ILoggerService _loggerService;

        public CustomAuthorizeFilter(IConfiguration configuration,
        ILoggerService loggerService)
        {
            _config = configuration;
            _loggerService = loggerService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Adding try catch here as Authorization filter run first, 
            // and Exception filter is not able to handler exception here
            try
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
                var secretKey = _config.GetValue<string>("JWTSecretKey");
                var claims = JWTHelper.ValidateTokenWithLifeTime(token, secretKey);
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
            catch (Exception ex)
            {
                _loggerService.LogException(ex);
                context.Result = new UnauthorizedResult();
            }
        }
    }
}