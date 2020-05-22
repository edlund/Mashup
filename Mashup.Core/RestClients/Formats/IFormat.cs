
namespace Mashup.Core.RestClients.Formats
{
    /**
     * Interface for RestClient content formats.
     */
    public interface IFormat
    {
        public string MediaType { get; }

        public T Deserialize<T>(string value);

        public string Serialize<T>(T value);
    }
}
