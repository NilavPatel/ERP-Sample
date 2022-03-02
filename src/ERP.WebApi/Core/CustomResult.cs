using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Core
{
    public class CustomResult
    {
        public CustomResult(bool isValid, string[]? successMessages, string[]? errorMessages, object? data)
        {
            IsValid = isValid;
            SuccessMessages = successMessages;
            ErrorMessages = errorMessages;
            Data = data;
        }
        public bool IsValid { get; set; }
        public string[]? SuccessMessages { get; set; }
        public string[]? ErrorMessages { get; set; }
        public object? Data { get; set; }
    }

    public class CustomActionResult : IActionResult
    {
        private CustomResult _result;

        public CustomActionResult(bool isValid, string[]? successMessages, string[]? errorMessages, object? data)
        {
            _result = new CustomResult(isValid, successMessages, errorMessages, data);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_result);
            return objectResult.ExecuteResultAsync(context);
        }
    }
}