using System.Net;
using finebe.webapi.Src.Models.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace finebe.webapi.Src.Filters;

public class CustomResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            // Check if the response is a model validation error
            if (objectResult.StatusCode == (int)HttpStatusCode.BadRequest &&
                objectResult.Value is ValidationProblemDetails validationProblemDetails)
            {
                var validationResponse = new ApiResponse<object>
                {
                    Success = false,
                    Data = new
                    {
                        Type = validationProblemDetails.Type,
                        Title = validationProblemDetails.Title,
                        Status = validationProblemDetails.Status,
                        Errors = validationProblemDetails.Errors,
                        TraceId = validationProblemDetails.Extensions["traceId"]
                    },
                    ErrorMessage = null,
                    Errors = validationProblemDetails.Errors.SelectMany(e => e.Value).ToList()
                };

                context.Result = new JsonResult(validationResponse)
                {
                    StatusCode = objectResult.StatusCode
                };
                return;
            }

            // Handle other responses
            if (!(objectResult.Value is ApiResponse<object>))
            {
                var response = new ApiResponse<object>
                {
                    Success = objectResult.StatusCode >= 200 && objectResult.StatusCode < 300,
                    Data = objectResult.Value,
                    ErrorMessage = objectResult.StatusCode >= 400 ? "An error occurred" : null,
                    Errors = null
                };

                if (objectResult.Value is ProblemDetails problemDetails)
                {
                    response.ErrorMessage = problemDetails.Title;
                    response.Errors = new List<string> { problemDetails.Detail };
                }

                context.Result = new JsonResult(response)
                {
                    StatusCode = objectResult.StatusCode
                };
            }
        }
    }

    public void OnResultExecuted(ResultExecutedContext context) { }
}

