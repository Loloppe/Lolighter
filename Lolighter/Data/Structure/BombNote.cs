using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BombNote
    {
        public BombNote(float time, int lineIndex, int lineLayer)
        {
            beat = time;
            index = lineIndex;
            layer = lineLayer;
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
