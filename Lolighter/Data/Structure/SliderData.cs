using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
	internal class SliderData : BaseSliderData
	{
		[JsonConstructor]
        public SliderData(float beat, int color, int line, int layer, int direction, float tailBeat, int tailLine, int tailLayer,
			float headControlPointLengthMultiplier, float tailControlPointLengthMultiplier, int tailCutDirection, int midAnchorMode) : 
			base(beat, color, line, layer, direction, tailBeat, tailLine, tailLayer)
        {
			this.headControlPointLengthMultiplier = headControlPointLengthMultiplier;
			this.tailControlPointLengthMultiplier = tailControlPointLengthMultiplier;
			this.tailCutDirection = tailCutDirection;
			this.midAnchorMode = midAnchorMode;
        }

        [JsonInclude]
		[JsonPropertyName("mu")]
		public float headControlPointLengthMultiplier { get; set; }

		[JsonInclude]
		[JsonPropertyName("tmu")]
		public float tailControlPointLengthMultiplier { get; set; }

		[JsonInclude]
		[JsonPropertyName("tc")]
		public int tailCutDirection { get; set; }

		[JsonInclude]
		[JsonPropertyName("m")]
		public int midAnchorMode { get; set; }
	}
}
