using System.Net;
using System.Text;
using finebe.webapi.Src.Models.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace finebe.webapi.Src.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public CustomExceptionFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var response = new ApiResponse<object>
            {
                Success = false,
                Data = null,
                ErrorMessage = "An internal server error occurred.",
                Errors = new List<string> (){ $"{context.Exception.GetType().FullName} with message: {context.Exception.Message}" }
            };

            if (_env.IsDevelopment())
            {
                var exceptionDetails = GetExceptionDetails(context.Exception);
                response.Errors.Add(exceptionDetails);
            }

            context.Result = new JsonResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }

        private string GetExceptionDetails(Exception exception)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Exception Type: {exception.GetType().FullName}");
            sb.AppendLine($"Message: {exception.Message}");
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(exception.StackTrace);

            if (exception.InnerException != null)
            {
                sb.AppendLine();
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(GetExceptionDetails(exception.InnerException));
            }

            return sb.ToString();
        }
    }
}
