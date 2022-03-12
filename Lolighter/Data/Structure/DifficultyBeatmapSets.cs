using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lolighter.Data.Structure
{
    

    internal class DifficultyBeatmapSets
    {
        [JsonConstructor]
        public DifficultyBeatmapSets(string _beatmapCharacteristicName, List<DifficultyBeatmaps> _difficultyBeatmaps)
        {
            if(_beatmapCharacteristicName == null)
            {
                _beatmapCharacteristicName = "Standard";
            }
            this._beatmapCharacteristicName = _beatmapCharacteristicName;
            if(_difficultyBeatmaps.Count == 0)
            {
                _difficultyBeatmaps.Add(new("ExpertPlus", 9, "ExpertPlusStandard.dat", 20, 0, new(0, 0, "ExpertPlus", null, null, null, null, null, null, null)));
            }
            this._difficultyBeatmaps = _difficultyBeatmaps;
        }

        [JsonInclude]
        [JsonPropertyName("_beatmapCharacteristicName")]
        public string _beatmapCharacteristicName { get; set; } = "Standard";
        [JsonInclude]
        [JsonPropertyName("_difficultyBeatmaps")]
        public List<DifficultyBeatmaps> _difficultyBeatmaps { get; set; } = new();
    }

    internal class DifficultyBeatmaps
    {
        [JsonConstructor]
        public DifficultyBeatmaps(string _difficulty, int _difficultyRank, string _beatmapFilename, float _noteJumpMovementSpeed, float _noteJumpStartBeatOffset, CustomData _customData)
        {
            if(_difficulty == null)
            {
                _difficulty = "ExpertPlus";
            }
            this._difficulty = _difficulty;
            if(_difficultyRank == 0)
            {
                _difficultyRank = 9;
            }
            this._difficultyRank = _difficultyRank;
            if (_beatmapFilename == null)
            {
                _beatmapFilename = "ExpertPlusStandard.dat";
            }
            this._beatmapFilename = _beatmapFilename;
            if (_noteJumpMovementSpeed == 0)
            {
                _noteJumpMovementSpeed = 20;
            }
            this._noteJumpMovementSpeed = _noteJumpMovementSpeed;
            if (_noteJumpStartBeatOffset == 0)
            {
                _noteJumpStartBeatOffset = 0;
            }
            this._noteJumpStartBeatOffset = _noteJumpStartBeatOffset;
            if (_customData == null)
            {
                _customData = new(0, 0, "ExpertPlus", null, null, null, null, null, null, null);
            }
            this._customData = _customData;
        }

        [JsonInclude]
        [JsonPropertyName("_difficulty")]
        public string _difficulty { get; set; } = "ExpertPlus";
        [JsonInclude]
        [JsonPropertyName("_difficultyRank")]
        public int _difficultyRank { get; set; } = 9;
        [JsonInclude]
        [JsonPropertyName("_beatmapFilename")]
        public string _beatmapFilename { get; set; } = "ExpertPlusStandard.dat";
        [JsonInclude]
        [JsonPropertyName("_noteJumpMovementSpeed")]
        public float _noteJumpMovementSpeed { get; set; } = 20;
        [JsonInclude]
        [JsonPropertyName("_noteJumpStartBeatOffset")]
        public float _noteJumpStartBeatOffset { get; set; } = 0;
        [JsonInclude]
        [JsonPropertyName("_customData")]
        public CustomData? _customData { get; set; }
    }

    internal class CustomData
    {
        [JsonConstructor]
        public CustomData(float _editorOffset, float _editorOldOffset, string _difficultyLabel, RGB _colorLeft, RGB _colorRight, RGB _envColorLeft, RGB _envColorRight
            , RGB _envColorLeftBoost, RGB _envColorRightBoost, RGB _obstacleColor)
        {
            this._editorOffset = _editorOffset;
            this._editorOldOffset = _editorOldOffset;
            if(_difficultyLabel == null)
            {
                _difficultyLabel = "ExpertPlus";
            }
            this._difficultyLabel = _difficultyLabel;
            if (_colorLeft == null)
            {
                _colorLeft = new(0.7529412f, 0.1882353f, 0.1882353f);
            }
            this._colorLeft = _colorLeft;
            if (_colorRight == null)
            {
                _colorRight = new(0.1254902f, 0.3921569f, 0.6588235f);
            }
            this._colorRight = _colorRight;
            if (_envColorLeft == null)
            {
                _envColorLeft = new(0.7529412f, 0.1882353f, 0.1882353f);
            }
            this._envColorLeft = _envColorLeft;
            if (_envColorRight == null)
            {
                _envColorRight = new(0.1254902f, 0.3921569f, 0.6588235f);
            }
            this._envColorRight = _envColorRight;
            if (_envColorLeftBoost == null)
            {
                _envColorLeftBoost = new(0.8f, 0.9098f, 0.8f);
            }
            this._envColorLeftBoost = _envColorLeftBoost;
            if (_envColorRightBoost == null)
            {
                _envColorRightBoost = new(0.89019f, 0.396078f, 0.756862f);
            }
            this._envColorRightBoost = _envColorRightBoost;
            if (_obstacleColor == null)
            {
                _obstacleColor = new(1, 0, 0);
            }
            this._obstacleColor = _obstacleColor;
        }

        [JsonInclude]
        [JsonPropertyName("_editorOffset")]
        public float _editorOffset { get; set; } = 0;
        [JsonInclude]
        [JsonPropertyName("_editorOldOffset")]
        public float _editorOldOffset { get; set; } = 0;
        [JsonInclude]
        [JsonPropertyName("_difficultyLabel")]
        public string _difficultyLabel { get; set; } = "ExpertPlus";
        [JsonInclude]
        [JsonPropertyName("_colorLeft")]
        public RGB _colorLeft { get; set; } = new(0.7529412f, 0.1882353f, 0.1882353f);
        [JsonInclude]
        [JsonPropertyName("_colorRight")]
        public RGB  _colorRight { get; set; } = new(0.1254902f, 0.3921569f, 0.6588235f);
        [JsonInclude]
        [JsonPropertyName("_envColorLeft")]
        public RGB  _envColorLeft { get; set; } = new(0.7529412f, 0.1882353f, 0.1882353f);
        [JsonInclude]
        [JsonPropertyName("_envColorRight")]
        public RGB  _envColorRight { get; set; } = new(0.1254902f, 0.3921569f, 0.6588235f);
        [JsonInclude]
        [JsonPropertyName("_envColorLeftBoost")]
        public RGB  _envColorLeftBoost { get; set; } = new(0.8f, 0.9098f, 0.8f);
        [JsonInclude]
        [JsonPropertyName("_envColorRightBoost")]
        public RGB  _envColorRightBoost { get; set; } = new(0.89019f, 0.396078f, 0.756862f);
        [JsonInclude]
        [JsonPropertyName("_obstacleColor")]
        public RGB  _obstacleColor { get; set; } = new(1, 0, 0);
    }

    internal class RGB
    {
        [JsonConstructor]
        public RGB(float red = 1, float green = 0, float blue = 0)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        [JsonInclude]
        [JsonPropertyName("r")]
        public float red { get; set; } = 1;
        [JsonInclude]
        [JsonPropertyName("g")]
        public float green { get; set; } = 0;
        [JsonInclude]
        [JsonPropertyName("b")]
        public float blue { get; set; } = 0;
    }
}
