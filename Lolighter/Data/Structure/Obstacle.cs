using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class Obstacle
    {
        [JsonConstructor]
        public Obstacle(float beat, int index, int layer, float duration, int width, int height)
        {
            this.beat = beat;
            this.index = index;
            this.layer = layer;
            this.duration = duration;
            this.width = width;
            this.height = height;
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
        [JsonInclude]
        [JsonPropertyName("d")]
        public float duration { get; set; }
        [JsonInclude]
        [JsonPropertyName("w")]
        public int width { get; set; }
        [JsonInclude]
        [JsonPropertyName("h")]
        public int height { get; set; }
    }
}
