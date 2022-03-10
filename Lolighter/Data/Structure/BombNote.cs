using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BombNote
    {
        [JsonConstructor]
        public BombNote(float beat, int line, int layer)
        {
            this.beat = beat;
            this.line = line;
            this.layer = layer;
        }

        public BombNote(ColorNote note)
        {
            this.beat = note.beat;
            this.line = note.line;
            this.layer = note.layer;
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
    }
}
