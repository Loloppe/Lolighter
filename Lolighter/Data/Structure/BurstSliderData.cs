using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BurstSliderData : BaseSliderData
    {
        [JsonConstructor]
        public BurstSliderData(float beat, int color, int line, int layer, int direction, float tailBeat, int tailLine, int tailLayer, int sliceCount, float squishAmount) : 
            base(beat, color, line, layer, direction, tailBeat, tailLine, tailLayer)
        {
            this.sliceCount = sliceCount;
            this.squishAmount = squishAmount;
        }

        public BurstSliderData(ColorNote note, float tailBeat, int tailLine, int tailLayer, int sliceCount, float squishAmount) : 
            base(note.beat, note.color, note.line, note.layer, note.direction, tailBeat, tailLine, tailLayer)
        {
            this.sliceCount = sliceCount;
            this.squishAmount = squishAmount;
        }

        [JsonInclude]
        [JsonPropertyName("sc")]
        public int sliceCount { get; set; }
        [JsonInclude]
        [JsonPropertyName("s")]
        public float squishAmount { get; set; }
    }
}
