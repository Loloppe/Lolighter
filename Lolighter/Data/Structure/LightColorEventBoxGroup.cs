using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class LightColorEventBoxGroup
    {
        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
    }
}
