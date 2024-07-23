using AngularAuthApi.Middlewares;

namespace AngularAuthApi.Localization
{
    public static class LocalizationMiddlewareExtensions
    {

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
