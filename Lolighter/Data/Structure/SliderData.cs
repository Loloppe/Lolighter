using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
	internal class SliderData : BaseSliderData
	{
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
