using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BaseSliderData
    {
		[JsonConstructor]
		public BaseSliderData(float beat, int color, int line, int layer, int direction, float tailBeat, int tailLine, int tailLayer)
        {
			this.beat = beat;
			this.color = color;
			this.line = line;
			this.layer = layer;
			this.direction = direction;
			this.tailBeat = tailBeat;
			this.tailLine = tailLine;
			this.tailLayer = tailLayer;
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
