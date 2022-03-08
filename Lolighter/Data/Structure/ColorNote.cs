using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class ColorNote
    {
        [JsonConstructor]
        public ColorNote(float b, int c, int x, int y, int d, int a)
        {
            beat = b;
            color = c;
            line = x;
            layer = y;
            direction = d;
            angle = a;
        }
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
        [JsonPropertyName("a")]
        public int angle { get; set; }
        [JsonInclude]
        [JsonPropertyName("c")]
        public int color { get; set; }
        [JsonInclude]
        [JsonPropertyName("d")]
        public int direction { get; set; }
    }
}
