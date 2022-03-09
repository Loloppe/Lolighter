using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BaseSliderData
    {
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
		[JsonPropertyName("tb")]
		public float tailBeat { get; set; }

		[JsonInclude]
		[JsonPropertyName("tx")]
		public int tailLine { get; set; }

		[JsonInclude]
		[JsonPropertyName("ty")]
		public int tailLayer { get; set; }
	}
}
