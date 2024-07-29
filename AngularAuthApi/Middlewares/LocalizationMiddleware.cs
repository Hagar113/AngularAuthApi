using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Models.DTOs.BaseRequest;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularAuthApi.Middlewares
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer _localizer;

        public LocalizationMiddleware(RequestDelegate next, IStringLocalizer<LocalizationMiddleware> localizer)
        {
            _next = next;
            _localizer = localizer;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var culture = "en"; // Default culture

            // Enable buffering  ..  read the request body
            context.Request.EnableBuffering();

            // Read the request body as a stream
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset the request body 

                // Deserialize the body to BaseRequestHeader
                BaseRequestHeader requestHeader = null;
                try
                {
                    requestHeader = JsonSerializer.Deserialize<BaseRequestHeader>(body);
                }
                catch (JsonException)
                {
                    // Handle deserialization error (if any)
                }

                // Determine culture from languageCode
                if (!string.IsNullOrWhiteSpace(requestHeader?.languageCode))
                {
                    try
                    {
                        culture = new CultureInfo(requestHeader.languageCode).Name;
                    }
                    catch (CultureNotFoundException)
                    {
                        culture = "en"; // Default to English if the culture is not found
                    }
                }
            }

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            await _next(context);
        }
    }
}
