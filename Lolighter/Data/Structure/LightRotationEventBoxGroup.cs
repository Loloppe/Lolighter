using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class LightRotationEventBoxGroup
    {
        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
    }
}
