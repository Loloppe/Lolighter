using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
	internal class BasicEventData
	{
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

		public BasicEventData(float b, int et, int v, int f)
        {
			beat = b;
			eventType = et;
			value = v;
			floatValue = f;
        }
    }
}
