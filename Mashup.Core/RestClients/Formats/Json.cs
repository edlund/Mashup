using System.Text.Json;

namespace Mashup.Core.RestClients.Formats
{
    /**
     * Wrap System.Text.Json.JsonSerializer.
     */
    public class Json : IFormat
    {
        public string MediaType => "application/json";

        public T Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value);
        }

        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
