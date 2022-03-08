using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BaseSliderData
    {
		[JsonInclude]
		[JsonPropertyName("b")]
		public float beat;

		[JsonInclude]
		[JsonPropertyName("c")]
		public int color;

		[JsonInclude]
		[JsonPropertyName("x")]
		public int line;

		[JsonInclude]
		[JsonPropertyName("y")]
		public int layer;

		[JsonInclude]
		[JsonPropertyName("d")]
		public int direction;

		[JsonInclude]
		[JsonPropertyName("tb")]
		public float tailBeat;

		[JsonInclude]
		[JsonPropertyName("tx")]
		public int tailLine;

		[JsonInclude]
		[JsonPropertyName("ty")]
		public int tailLayer;
	}
}
