using Lolighter.Data.Structure;
using System;

namespace Lolighter.Data
{
    internal class InfoData
    {
        public string _version { get; set; } = "3.0.0";
        public string _songName { get; set; } = "Song Name";
        public string _songSubName { get; set; } = "Sub Name";
        public string _songAuthorName { get; set; } = "Author Name";
        public string _levelAuthorName { get; set; } = "Mapper Name";
        public float _beatsPerMinute { get; set; } = 120f;
        public float _shuffle { get; set; } = 0f;
        public float _shufflePeriod { get; set; } = 0.5f;
        public float _previewStartTime { get; set; } = 0f;
        public float _previewDuration { get; set; } = 0f;
        public string _songFilename { get; set; } = "song.egg";
        public string _coverImageFilename { get; set; } = "cover.jpg";
        public string _environmentName { get; set; } = "DefaultEnvironment";
        public string _allDirectionsEnvironmentName { get; set; } = "GlassDesertEnvironment";
        public float _songTimeOffset { get; set; } = 0f;
        public CustomDataInfo? _customData { get; set; }
        public DifficultyBeatmapSets[] _difficultyBeatmapSets { get; set; } = Array.Empty<DifficultyBeatmapSets>();
    }
}
