using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AngularAuthApi.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            // Enable buffering to read the request body multiple times
            request.EnableBuffering();
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Position = 0;

            // Keep the original response stream to copy the modified response back
            var originalBodyStream = response.Body;
            using var newBodyStream = new MemoryStream();
            response.Body = newBodyStream;

            // Execute the next middleware in the pipeline
            await _next(context);

            // Read the response body from the temporary stream
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            // Log the request and response
            await LogRequestResponseAsync(request, requestBody, response, responseBody);

            // Copy the new body stream to the original stream
            await newBodyStream.CopyToAsync(originalBodyStream);
        }

        private async Task LogRequestResponseAsync(HttpRequest request, string requestBody, HttpResponse response, string responseBody)
        {
            var logFilePath = GetLogFilePath();
            var logEntry = $"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}\n" + // إضافة الطابع الزمني
                           $"Request: {request.Method} {request.Path}{request.QueryString}\n" +
                           $"Request Body:\n{requestBody}\n" +
                           $"Response Status: {response.StatusCode}\n" +
                           $"Response Body:\n{responseBody}\n" +
                           $"----------------------------------------------------\n";

            await File.AppendAllTextAsync(logFilePath, logEntry);
        }

        private string GetLogFilePath()
        {
            var today = DateTime.Now.ToString("yyyyMMdd");
            var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "log");

            if (!Directory.Exists(logFolderPath))
            {
                Directory.CreateDirectory(logFolderPath);
            }

            return Path.Combine(logFolderPath, $"{today}.txt");
        }
    }
}
