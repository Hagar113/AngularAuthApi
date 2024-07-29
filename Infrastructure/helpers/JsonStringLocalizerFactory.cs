
using Infrastructure;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;

namespace Infrastructure.helpers
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            // Create an instance of JsonStringLocalizer with the current culture
            var culture = CultureInfo.CurrentUICulture.Name;
            return new JsonStringLocalizer(culture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            // Create an instance of JsonStringLocalizer with the current culture
            var culture = CultureInfo.CurrentUICulture.Name;
            return new JsonStringLocalizer(culture);
        }
    }
}
