using Newtonsoft.Json;

namespace Infrastructure.helpers
{
    public static class Deserialization
    {
        public static T Deserialize<T>(string json) where T : class
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonSerializationException ex)
            {
                // Handle JSON serialization exceptions
                Console.WriteLine("JSON Serialization Error: " + ex.Message);
                return null;
            }
        }
    }
}
