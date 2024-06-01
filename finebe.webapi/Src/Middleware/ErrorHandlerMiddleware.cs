using System.Text;
using System.Text.Json;
using finebe.webapi.Src.Models.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace finebe.webapi.Src.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Capture the original response stream
            var originalResponseBodyStream = context.Response.Body;

            // Use a memory stream to intercept the response
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    // Call the next middleware in the pipeline
                    await _next(context);

                    // Check if the response has already started
                    if (!context.Response.HasStarted)
                    {
                        // Reset the memory stream position to the beginning
                        context.Response.Body.Seek(0, SeekOrigin.Begin);

                        // Read the response body
                        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

                        // Determine the status code and set the message accordingly
                        string message;
                        if (context.Response.StatusCode == 401)
                        {
                            message = "Not Authorized";
                        }
                        else if (context.Response.StatusCode == 403)
                        {
                            message = "Forbidden";
                        }
                        else if (context.Response.StatusCode == 404)
                        {
                            message = "Not Found";
                        }
                        else
                        {
                            message = "Success";
                        }

                        object responseData;
                        try
                        {
                            responseData = JsonSerializer.Deserialize<object>(body);
                        }
                        catch
                        {
                            responseData = body;
                        }

                        // Wrap the response in ApiResponse
                        var apiResponse = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300
                            ? ApiResponse<object>.Success(responseData, message)
                            : ApiResponse<object>.Fail(message);

                        // Serialize the ApiResponse
                        var jsonResponse = JsonSerializer.Serialize(apiResponse);

                        // Reset the memory stream position to the beginning
                        context.Response.Body.Seek(0, SeekOrigin.Begin);

                        // Write the ApiResponse to the response body
                        context.Response.ContentType = "application/json";
                        context.Response.ContentLength = Encoding.UTF8.GetByteCount(jsonResponse);
                        await context.Response.WriteAsync(jsonResponse);
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions by wrapping them in ApiResponse
                    if (!context.Response.HasStarted)
                    {
                        var errorMessages = BuildErrorMessages(ex);
                        var apiResponse = ApiResponse<string>.Fail(errorMessages);
                        var jsonResponse = JsonSerializer.Serialize(apiResponse);

                        // Ensure the response is in a correct state
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";

                        // Write the ApiResponse to the response body
                        await context.Response.WriteAsync(jsonResponse);
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    // Reset the response body to the original stream
                    context.Response.Body = originalResponseBodyStream;

                    if (!context.Response.HasStarted)
                    {
                        // Copy the contents of the new memory stream (which includes the ApiResponse) to the original stream
                        responseBody.Seek(0, SeekOrigin.Begin);
                        await responseBody.CopyToAsync(originalResponseBodyStream);
                    }
                }
            }
        }

        private List<string> BuildErrorMessages(Exception ex)
        {
            var errorMessages = new List<string>
            {
                $"{ex.GetType().Name}: {ex.Message}"
            };

            var innerException = ex.InnerException;
            while (innerException != null)
            {
                errorMessages.Add($"{innerException.GetType().Name}: {innerException.Message}");
                innerException = innerException.InnerException;
            }

            return errorMessages;
        }
    }
}
