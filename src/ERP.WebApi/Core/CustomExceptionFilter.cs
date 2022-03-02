using ERP.Application.Core.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.WebApi.Core
{
    public class CustomExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly ILoggerService _loggerService;
        public CustomExceptionFilter(ILoggerService loggerService)
        {
            _loggerService = loggerService;
        }

        public void OnException(ExceptionContext context)
        {
            _loggerService.LogException(context.Exception);

            var errors = new string[]
            {
                context.Exception.Message
            };
            context.Result = new CustomActionResult(false, null, errors, null);
        }
    }
}