using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class Obstacle
    {
        public Obstacle(float t, int i, int l, float d, int w, int h)
        {
            beat = t;
            index = i;
            layer = l;
            duration = d;
            width = w;
            height = h;
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
