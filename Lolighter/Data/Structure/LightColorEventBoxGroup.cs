using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class LightColorEventBoxGroup
    {

        [JsonInclude]
        [JsonPropertyName("b")]
        public float beat { get; set; } // When the group of lights start
        [JsonInclude]
        [JsonPropertyName("g")]
        public int groupId { get; set; }
        [JsonInclude]
        [JsonPropertyName("e")]
        public List<LightColorEventBox> eventBoxes { get; set; }

        public class LightColorEventBox
        {
            [JsonInclude]
            [JsonPropertyName("f")]
            public IndexFilter indexFilter { get; set; }
            [JsonInclude]
            [JsonPropertyName("w")]
            public float beatDistributionParam { get; set; }
            [JsonInclude]
            [JsonPropertyName("d")]
            public int beatDistributionParamType { get; set; }
            [JsonInclude]
            [JsonPropertyName("r")]
            public float brightnessDistributionParam { get; set; }
            [JsonInclude]
            [JsonPropertyName("t")]
            public int brightnessDistributionParamType { get; set; }
            [JsonInclude]
            [JsonPropertyName("e")]
            public List<LightColorBaseData> lightColorBaseDataList { get; set; }
            [JsonInclude]
            [JsonPropertyName("b")]
            public int brightnessDistributionShouldAffectFirstBaseEvent { get; set; } = 1;
        }

        public class IndexFilter
        {
            [JsonInclude]
            [JsonPropertyName("f")]
            public int type { get; set; }
            [JsonInclude]
            [JsonPropertyName("p")]
            public int param0 { get; set; }
            [JsonInclude]
            [JsonPropertyName("t")]
            public int param1 { get; set; }
            [JsonInclude]
            [JsonPropertyName("r")]
            public int reversed { get; set; }
        }

        public class LightColorBaseData
        {
            [JsonInclude]
            [JsonPropertyName("b")]
            public float beat { get; set; }
            [JsonInclude]
            [JsonPropertyName("i")]
            public int transitionType { get; set; }
            [JsonInclude]
            [JsonPropertyName("c")]
            public int colorType { get; set; }
            [JsonInclude]
            [JsonPropertyName("s")]
            public int brightness { get; set; }
            [JsonInclude]
            [JsonPropertyName("f")]
            public int strobeBeatFrequency { get; set; }
        }
    }
}
