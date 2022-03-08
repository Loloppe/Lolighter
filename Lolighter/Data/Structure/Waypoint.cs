using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class Waypoint
    {
        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
        [JsonInclude]
        [JsonPropertyName("x")]
        public int line { get; set; }
        [JsonInclude]
        [JsonPropertyName("y")]
        public int layer { get; set; }
        [JsonInclude]
        [JsonPropertyName("d")]
        public int direction { get; set; }
    }
}
