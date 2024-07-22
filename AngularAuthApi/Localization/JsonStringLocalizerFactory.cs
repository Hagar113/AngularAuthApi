using Microsoft.Extensions.Localization;
using System.Globalization;

namespace AngularAuthApi.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            return new JsonStringLocalizer(culture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var culture = CultureInfo.CurrentUICulture.Name;
            return new JsonStringLocalizer(culture);
        }
    }
}
