using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Infrastructure
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly JsonSerializer _serializer = new();
        private readonly string _filePath;

        public JsonStringLocalizer(string culture)
        {
            _filePath = $"Resources/{Thread.CurrentThread.CurrentCulture.Name}.json";
            //_filePath = "Resources/ar.json";
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            if (!File.Exists(_filePath))
            {
                yield break;
            }

            using FileStream stream = new(_filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    var key = reader.Value as string;
                    reader.Read();
                    var value = _serializer.Deserialize<string>(reader);
                    yield return new LocalizedString(key, value);
                }
            }
        }

        private string GetString(string key)
        {
            if (File.Exists(_filePath))
            {
                return GetValueFromJSON(key, _filePath);
            }
            return string.Empty;
        }

        private string GetValueFromJSON(string propertyName, string filePath)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(filePath))
                return string.Empty;

            using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using StreamReader streamReader = new(stream);
            using JsonTextReader reader = new(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && (reader.Value as string).Trim() == propertyName.Trim())
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }

            return string.Empty;
        }
    }
}
