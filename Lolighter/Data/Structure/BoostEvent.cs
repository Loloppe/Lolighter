using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class ColorBoostEventData
    {
        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }

        [JsonInclude]
        [JsonPropertyName("o")]
        public bool on { get; set; }

        public ColorBoostEventData(float beat, bool on)
        {
            this.beat = beat;
            this.on = on;
        }
    }
}
