using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BPMChangeEvent
    {
        public BPMChangeEvent(float beat, float bpm)
        {
            this.beat = beat;
            this.bpm = bpm;
        }

        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
        [JsonInclude]
        [JsonPropertyName("m")]
        public float bpm { get; set; }
    }
}
