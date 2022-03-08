using System.Collections.Generic;
using System.Text.Json.Serialization;
using static Lolighter.Info.Helper;

namespace Lolighter.Data.Structure
{
    internal class BasicEventTypesWithKeywords
    {
		public class BasicEventTypesForKeyword
		{
			[JsonInclude]
			[JsonPropertyName("k")]
			public string keyword;

			[JsonInclude]
			[JsonPropertyName("e")]
			public int eventType;

			public BasicEventTypesForKeyword(string k, int e)
			{
				keyword = k;
				eventType = e;
			}
		}

		[JsonInclude]
		[JsonPropertyName("d")]
		public List<BasicEventTypesForKeyword> d;

		public BasicEventTypesWithKeywords(List<BasicEventTypesForKeyword> data)
		{
			d = data;
		}
	}
}
