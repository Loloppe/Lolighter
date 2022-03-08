using System;

namespace Lolighter.Data.Structure
{
    internal class DifficultyBeatmapSets
    {
        public string _beatmapCharacteristicName { get; set; } = "Standard";
        public DifficultyBeatmaps[] _difficultyBeatmaps { get; set; } = Array.Empty<DifficultyBeatmaps>();
    }

    public class DifficultyBeatmaps
    {
        public string _difficulty { get; set; } = "ExpertPlus";
        public int _difficultyRank { get; set; } = 9;
        public string _beatmapFilename { get; set; } = "ExpertPlusStandard.dat";
        public float _noteJumpMovementSpeed { get; set; } = 20;
        public float _noteJumpStartBeatOffset { get; set; } = 0;
        public CustomData? _customData { get; set; }
    }

    public class CustomData
    {
        public float _editorOffset { get; set; } = 0;
        public float _editorOldOffset { get; set; } = 0;
        public string _difficultyLabel { get; set; } = "ExpertPlus";
    }
}
