using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;

public class JsonStringLocalizer : IStringLocalizer
{
    private readonly JObject _localizationData;

    public JsonStringLocalizer(string culture)
    {
       
        var filePath = Path.Combine("Resources", $"{culture}.json");
        Console.WriteLine($"Attempting to load file: {filePath}"); 
        if (File.Exists(filePath))
        {
            var jsonData = File.ReadAllText(filePath);
            _localizationData = JObject.Parse(jsonData);
        }
        else
        {
            Console.WriteLine($"File not found: {filePath}");
            _localizationData = new JObject();
        }
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = _localizationData[name]?.ToString() ?? name;
            return new LocalizedString(name, value);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var value = _localizationData[name]?.ToString() ?? name;
            return new LocalizedString(name, string.Format(value, arguments));
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _localizationData.Properties().Select(p => new LocalizedString(p.Name, p.Value.ToString()));
    }
}
