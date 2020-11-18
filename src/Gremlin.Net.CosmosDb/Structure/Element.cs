using Newtonsoft.Json;

namespace Gremlin.Net.CosmosDb.Structure
{
    /// <summary>
    /// Base class for data elements within a graph
    /// </summary>
    public abstract class Element
    {
        /// <summary>
        /// Reduces memory allocation for default serializer settings
        /// </summary>
        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings();
        /// <summary>
        /// Reduces memory allocation for default serializer
        /// </summary>
        private static readonly JsonSerializer DefaultJsonSerializer = JsonSerializer.Create(DefaultJsonSerializerSettings);

        /// <summary>
        /// Creates a JsonSerializer with the given settings
        /// </summary>
        /// <param name="serializerSettings"></param>
        /// <returns></returns>
        /// <remarks>If the settings are null, returns the default serializer</remarks>
        protected JsonSerializer CreateJsonSerializer(JsonSerializerSettings serializerSettings)
        {
	        return serializerSettings == null ? DefaultJsonSerializer : JsonSerializer.Create(serializerSettings);
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty(PropertyNames.Id, NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty(PropertyNames.Label)]
        public virtual string Label { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        protected internal Element()
        {
        }
    }
}