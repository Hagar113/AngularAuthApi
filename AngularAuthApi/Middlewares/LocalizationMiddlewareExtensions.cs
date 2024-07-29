using Microsoft.AspNetCore.Builder;


namespace AngularAuthApi.Middlewares
{
    public static class LocalizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LocalizationMiddleware>();
        }
    }
}
