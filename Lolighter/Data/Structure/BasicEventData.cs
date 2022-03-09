using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
	internal class BasicEventData
	{
		[JsonConstructor]
		public BasicEventData(float beat, int eventType, int value, float floatValue)
		{
			this.beat = beat;
			this.eventType = eventType;
			this.value = value;
			this.floatValue = floatValue;
		}

		[JsonInclude]
		[JsonPropertyName("b")]
		public float beat { get; set; }

		[JsonInclude]
		[JsonPropertyName("et")]
		public int eventType { get; set; }

		[JsonInclude]
		[JsonPropertyName("i")]
		public int value { get; set; }

		[JsonInclude]
		[JsonPropertyName("f")]
		public float floatValue { get; set; }
    }
}
