using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    internal class CustomDataInfo
    {
        [JsonConstructor]
        public CustomDataInfo(Editors _editors)
        {
            if(_editors == null)
            {
                _editors = new Editors();
            }
            this._editors = _editors;
        }

        [JsonInclude]
        [JsonPropertyName("_editors")]
        public Editors _editors { get; set; }

        internal class Editors
        {

            [JsonConstructor]
            public Editors(string _lastEditedBy = "Lolighter 3.0.0")
            {
                this._lastEditedBy = _lastEditedBy;
            }

            [JsonInclude]
            [JsonPropertyName("_lastEditedBy")]
            public string _lastEditedBy { get; set; } = "Lolighter 3.0.0";
        }
    }
}
