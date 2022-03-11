using System.Collections.Generic;
using System.Text.Json.Serialization;
using static Lolighter.Data.Structure.LightColorEventBoxGroup;

namespace Lolighter.Data.Structure
{
    internal class LightRotationEventBoxGroup
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
            [JsonPropertyName("s")]
            public float rotationDistributionParam { get; set; }
            [JsonInclude]
            [JsonPropertyName("t")]
            public int rotationDistributionParamType { get; set; }
            [JsonInclude]
            [JsonPropertyName("a")]
            public int axis { get; set; }
            [JsonInclude]
            [JsonPropertyName("r")]
            public int flipRotation { get; set; } = 1;
            [JsonInclude]
            [JsonPropertyName("b")]
            public int rotationDistributionShouldAffectFirstBaseEvent { get; set; } = 1;
            [JsonInclude]
            [JsonPropertyName("l")]
            public List<LightRotationBaseData> lightRotationBaseDataList { get; set; }
        }

        public class LightRotationBaseData
        {
            [JsonInclude]
            [JsonPropertyName("b")]
            public float beat { get; set; }
            [JsonInclude]
            [JsonPropertyName("p")]
            public int usePreviousEventRotationValue { get; set; } = 1;
            [JsonInclude]
            [JsonPropertyName("e")]
            public int easeType { get; set; }
            [JsonInclude]
            [JsonPropertyName("l")]
            public int loopsCount { get; set; }
            [JsonInclude]
            [JsonPropertyName("r")]
            public float rotation { get; set; }
            [JsonInclude]
            [JsonPropertyName("o")]
            public int rotationDirection { get; set; }
        }
    }
}
