using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BPMChangeEvent
    {
        public BPMChangeEvent(float time, float b)
        {
            beat = time;
            bpm = b;
        }

        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; }
        [JsonInclude]
        [JsonPropertyName("m")]
        public float bpm { get; set; }
    }
}
