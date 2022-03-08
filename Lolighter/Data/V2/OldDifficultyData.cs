using System;

namespace Lolighter.Data.V2
{
    public class OldDifficultyData
    {
        public string _version { get; set; } = "2.0.0";
        public Events[] _events { get; set; } = Array.Empty<Events>();
        public Notes[] _notes { get; set; } = Array.Empty<Notes>();
        public Obstacles[] _obstacles { get; set; } = Array.Empty<Obstacles>();
        public CustomData _customData { get; set; } = new();
        public Waypoints[]? _waypoints { get; set; }
    }

    public class CustomData
    {
        public float _time { get; set; }
        public BPMChange[] _BPMChanges { get; set; } = Array.Empty<BPMChange>();
        public Bookmarks[] _bookmarks { get; set; } = Array.Empty<Bookmarks>();
    }

    public class BPMChange
    {
        public float _time { get; set; }
        public float _BPM { get; set; }
        public int _beatsPerBar { get; set; }
        public int _metronomeOffset { get; set; }
    }

    public class Bookmarks
    {
        public float _time { get; set; }
        public string _name { get; set; } = String.Empty;
        public float[] _color { get; set; } = new float[4] { 1, 1, 1, 1 };
    }

    public class Waypoints
    {
        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _lineLayer { get; set; }
        public int _offsetDirection { get; set; }
    }

    public class Events
    {
        public float _time { get; set; }
        public int _type { get; set; }
        public int _value { get; set; }
    }

    public class Notes
    {
        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _lineLayer { get; set; }
        public int _type { get; set; }
        public int _cutDirection { get; set; }
    }

    public class Obstacles
    {
        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _type { get; set; }
        public float _duration { get; set; }
        public int _width { get; set; }
    }
}
