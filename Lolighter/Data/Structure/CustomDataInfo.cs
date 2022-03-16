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
                _editors = new Editors("Lolighter", new());
            }
            this._editors = _editors;
        }

        [JsonInclude]
        [JsonPropertyName("_editors")]
        public Editors _editors { get; set; }

        internal class Editors
        {

            [JsonConstructor]
            public Editors(string _lastEditedBy, Lolighter lolighter)
            {
                _lastEditedBy = "Lolighter";
                this._lastEditedBy = _lastEditedBy;

                lolighter = new Lolighter();
                this.lolighter = lolighter;
            }

            [JsonInclude]
            [JsonPropertyName("_lastEditedBy")]
            public string _lastEditedBy { get; set; } = "Lolighter";
            [JsonInclude]
            [JsonPropertyName("Lolighter")]
            public Lolighter lolighter { get; set; } = new();
        }

        internal class Lolighter
        {
            [JsonConstructor]
            public Lolighter(string version = "1.0.5")
            {
                this.version = version;
            }

            [JsonInclude]
            [JsonPropertyName("version")]
            public string version { get; set; } = "1.0.5";
        }
    }
}
