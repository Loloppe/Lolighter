using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BombNote
    {
        public BombNote(float beat, int index, int layer)
        {
            this.beat = beat;
            this.index = index;
            this.layer = layer;
        }
        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
        [JsonInclude]
        [JsonPropertyName("x")]
        public int index { get; set; }
        [JsonInclude]
        [JsonPropertyName("y")]
        public int layer { get; set; }
    }
}
