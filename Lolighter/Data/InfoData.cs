using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lolighter.Data
{
    internal class InfoData
    {
        [JsonConstructor]
        public InfoData(string _version, string _songName, string _songSubName, string _songAuthorName, string _levelAuthorName, float _beatsPerMinute, float _shuffle,
            float _shufflePeriod, float _previewStartTime, float _previewDuration, string _songFilename, string _coverImageFilename, string _environmentName, string _allDirectionsEnvironmentName,
            float _songTimeOffset, CustomDataInfo _customData, List<DifficultyBeatmapSets> _difficultyBeatmapSets)
        {
            this._version = _version;
            this._songName = _songName;
            this._songSubName = _songSubName;
            this._songAuthorName = _songAuthorName;
            this._levelAuthorName = _levelAuthorName;
            this._beatsPerMinute = _beatsPerMinute;
            this._shuffle = _shuffle;
            this._shufflePeriod = _shufflePeriod;
            this._previewStartTime = _previewStartTime;
            this._previewDuration = _previewDuration;
            this._songFilename = _songFilename;
            this._coverImageFilename = _coverImageFilename;
            this._environmentName = _environmentName;
            this._allDirectionsEnvironmentName = _allDirectionsEnvironmentName;
            this._songTimeOffset = _songTimeOffset;
            if (this._customData == null)
            {
                this._customData = new(new("Lolighter", new()));
            }
            this._customData = _customData;
            this._difficultyBeatmapSets = _difficultyBeatmapSets;
        }

        internal InfoData()
        {
            if(this._customData == null)
            {
                this._customData = new(new("Lolighter", new()));
            }
        }

        [JsonInclude]
        [JsonPropertyName("_version")]
        public string _version { get; set; } = "3.0.0";
        [JsonInclude]
        [JsonPropertyName("_songName")]
        public string _songName { get; set; } = "Song Name";
        [JsonInclude]
        [JsonPropertyName("_songSubName")]
        public string _songSubName { get; set; } = "Sub Name";
        [JsonInclude]
        [JsonPropertyName("_songAuthorName")]
        public string _songAuthorName { get; set; } = "Author Name";
        [JsonInclude]
        [JsonPropertyName("_levelAuthorName")]
        public string _levelAuthorName { get; set; } = "Mapper Name";
        [JsonInclude]
        [JsonPropertyName("_beatsPerMinute")]
        public float _beatsPerMinute { get; set; } = 120f;
        [JsonInclude]
        [JsonPropertyName("_shuffle")]
        public float _shuffle { get; set; } = 0f;
        [JsonInclude]
        [JsonPropertyName("_shufflePeriod")]
        public float _shufflePeriod { get; set; } = 0.5f;
        [JsonInclude]
        [JsonPropertyName("_previewStartTime")]
        public float _previewStartTime { get; set; } = 0f;
        [JsonInclude]
        [JsonPropertyName("_previewDuration")]
        public float _previewDuration { get; set; } = 0f;
        [JsonInclude]
        [JsonPropertyName("_songFilename")]
        public string _songFilename { get; set; } = "song.ogg";
        [JsonInclude]
        [JsonPropertyName("_coverImageFilename")]
        public string _coverImageFilename { get; set; } = "cover.jpg";
        [JsonInclude]
        [JsonPropertyName("_environmentName")]
        public string _environmentName { get; set; } = "DefaultEnvironment";
        [JsonInclude]
        [JsonPropertyName("_allDirectionsEnvironmentName")]
        public string _allDirectionsEnvironmentName { get; set; } = "GlassDesertEnvironment";
        [JsonInclude]
        [JsonPropertyName("_songTimeOffset")]
        public float _songTimeOffset { get; set; } = 0f;
        [JsonInclude]
        [JsonPropertyName("_customData")]
        public CustomDataInfo _customData { get; set; }
        [JsonInclude]
        [JsonPropertyName("_difficultyBeatmapSets")]
        public List<DifficultyBeatmapSets> _difficultyBeatmapSets { get; set; } = new();
    }
}
