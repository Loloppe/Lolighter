using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class ColorNote
    {
        [JsonConstructor]
        public ColorNote(float beat, int color, int line, int layer, int direction, int angle)
        {
            this.beat = beat;
            this.color = color;
            this.line = line;
            this.layer = layer;
            this.direction = direction;
            this.angle = angle;
        }

        public ColorNote(float beat, int color, int line, int layer, int direction)
        {
            this.beat = beat;
            this.color = color;
            this.line = line;
            this.layer = layer;
            this.direction = direction;
            this.angle = 0;
        }

        public ColorNote(ColorNote note)
        {
            this.beat = note.beat;
            this.color = note.color;
            this.line = note.line;
            this.layer = note.layer;
            this.direction = note.direction;
            this.angle = note.angle;
        }

        public ColorNote(BombNote bomb)
        {
            this.beat = bomb.beat;
            this.color = 3;
            this.line = bomb.line;
            this.layer = bomb.layer;
            this.direction = 0;
            this.angle = 0;
        }

        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
        [JsonInclude]
        [JsonPropertyName("c")]
        public int color { get; set; }
        [JsonInclude]
        [JsonPropertyName("x")]
        public int line { get; set; }
        [JsonInclude]
        [JsonPropertyName("y")]
        public int layer { get; set; }
        
        [JsonInclude]
        [JsonPropertyName("d")]
        public int direction { get; set; }
        [JsonInclude]
        [JsonPropertyName("a")]
        public int angle { get; set; }
    }
}
