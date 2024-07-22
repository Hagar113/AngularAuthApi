using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Globalization;
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

    if (context.Request.Query.TryGetValue("languageCode", out var languageCode))
    {
        try
        {
            culture = new CultureInfo(languageCode).Name;
        }
        catch (CultureNotFoundException)
        {
            culture = "en";
        }
    }

    CultureInfo.CurrentCulture = new CultureInfo(culture);
    CultureInfo.CurrentUICulture = new CultureInfo(culture);

    await _next(context);
}







    }
}
