using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class BurstSliderData : BaseSliderData
    {
        [JsonInclude]
        [JsonPropertyName("sc")]
        public int sliceCount { get; set; }
        [JsonInclude]
        [JsonPropertyName("s")]
        public float squishAmount { get; set; }
    }
}
