using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Lolighter.Data.V2
{
    internal class OldDifficultyData
    {
        [JsonConstructor]
        public OldDifficultyData(string _version, List<Events> _events, List<Notes> _notes, List<Obstacles> _obstacles, CustomData _customData, List<Waypoints> _waypoints)
        {
            if (_version == null)
            {
                _version = "2.0.0";
            }
            this._version = _version;
            if (_events == null)
            {
                _events = new();
            }
            this._events = _events;
            if (_notes == null)
            {
                _notes = new();
            }
            this._notes = _notes;
            if (_obstacles == null)
            {
                _obstacles = new();
            }
            this._obstacles = _obstacles;
            if (_customData == null)
            {
                _customData = new(0, new(), new());
            }
            this._customData = _customData;
            if (_waypoints == null)
            {
                _waypoints = new();
            }
            this._waypoints = _waypoints;
        }

        public OldDifficultyData(DifficultyData diff)
        {
            if (diff.version == null)
            {
                diff.version = "2.0.0";
            }
            this._version = diff.version;

            List<Events> events = new();
            foreach(var ev in diff.basicBeatmapEvents)
            {
                events.Add(new(ev));
            }
            this._events = events;

            List<Notes> notes = new();
            foreach (var nt in diff.colorNotes)
            {
                notes.Add(new(nt));
            }
            foreach (var bomb in diff.bombNotes)
            {
                notes.Add(new(bomb));
            }

            notes = notes.OrderBy(o => o._time).ToList();

            this._notes = notes;

            List<Obstacles> obs = new();
            foreach (var obst in diff.obstacles)
            {
                obs.Add(new(obst));
            }
            this._obstacles = obs;

            List<Waypoints> w = new();
            foreach (var way in diff.waypoints)
            {
                w.Add(new(way));
            }
            this._waypoints = w;

            CustomData custom = new(diff.customData, diff.bpmEvents);
            this._customData = custom;
        }

        public string _version { get; set; } = "2.0.0";
        public List<Events> _events { get; set; } = new();
        public List<Notes> _notes { get; set; } = new();
        public List<Obstacles> _obstacles { get; set; } = new();
        public CustomData _customData { get; set; }
        public List<Waypoints> _waypoints { get; set; } = new();
    }

    internal class CustomData
    {
        [JsonConstructor]
        public CustomData(float _time, List<BPMChange> _BPMChanges, List<Bookmarks> _bookmarks)
        {
            this._time = _time;
            this._BPMChanges = _BPMChanges;
            this._bookmarks = _bookmarks;
        }

        public CustomData(CustomDataDifficulty custom, List<BPMChangeEvent> bpm)
        {
            this._time = custom.t;
            List<BPMChange> c = new List<BPMChange>();
            foreach(var change in bpm)
            {
                c.Add(new(change));
                
            }
            this._BPMChanges = c;

            List<Bookmarks> b = new List<Bookmarks>();
            foreach (var book in custom.bm)
            {
                b.Add(new(book));

            }
            this._bookmarks = b;
        }

        public float _time { get; set; }
        public List<BPMChange> _BPMChanges { get; set; } = new();
        public List<Bookmarks> _bookmarks { get; set; } = new();
    }

    internal class BPMChange
    {
        [JsonConstructor]
        public BPMChange(float _time, float _BPM, int _beatsPerBar, int _metronomeOffset)
        {
            this._time = _time;
            this._BPM = _BPM;
            this._beatsPerBar = _beatsPerBar;
            this._metronomeOffset = _metronomeOffset;
        }

        public BPMChange(BPMChangeEvent bpm)
        {
            this._time = bpm.beat;
            this._BPM = bpm.bpm;
            this._beatsPerBar = 4;
            this._metronomeOffset = 0;
        }

        public float _time { get; set; }
        public float _BPM { get; set; }
        public int _beatsPerBar { get; set; }
        public int _metronomeOffset { get; set; }
    }

    internal class Bookmarks
    {
        [JsonConstructor]
        public Bookmarks(float _time, string _name, float[] _color)
        {
            this._time = _time;
            this._name = _name;
            this._color = _color;
        }

        public Bookmarks(Bookmark wp)
        {
            this._time = wp.b;
            this._name = wp.n;
            this._color = wp.c;
        }

        public float _time { get; set; }
        public string _name { get; set; } = String.Empty;
        public float[] _color { get; set; } = new float[4] { 1, 1, 1, 1 };
    }

    internal class Waypoints
    {
        [JsonConstructor]
        public Waypoints(float _time, int _lineIndex, int _lineLayer, int _offsetDirection)
        {
            this._time = _time;
            this._lineIndex = _lineIndex;
            this._lineLayer = _lineLayer;
            this._offsetDirection = _offsetDirection;
        }

        public Waypoints(Waypoint wp)
        {
            this._time = wp.beat;
            this._lineIndex = wp.line;
            this._lineLayer = wp.layer;
            this._offsetDirection = wp.direction;
        }

        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _lineLayer { get; set; }
        public int _offsetDirection { get; set; }
    }

    internal class Events
    {
        [JsonConstructor]
        public Events(float _time, int _type, int _value)
        {
            this._time = _time;
            this._type = _type;
            this._value = _value;
        }

        public Events(BasicEventData eventData)
        {
            this._time = eventData.beat;
            this._type = eventData.eventType;
            this._value = eventData.value;
        }

        public float _time { get; set; }
        public int _type { get; set; }
        public int _value { get; set; }
    }

    internal class Notes
    {
        [JsonConstructor]
        public Notes(float _time, int _lineIndex, int _lineLayer, int _type, int _cutDirection)
        {
            this._time = _time;
            this._lineIndex = _lineIndex;
            this._lineLayer = _lineLayer;
            this._type= _type;
            this._cutDirection = _cutDirection;
        }

        public Notes(ColorNote note)
        {
            this._time = note.beat;
            this._lineIndex = note.line;
            this._lineLayer = note.layer;
            this._type = note.color;
            this._cutDirection = note.direction;
        }

        public Notes(BombNote note)
        {
            this._time = note.beat;
            this._lineIndex = note.line;
            this._lineLayer = note.layer;
            this._type = 3;
            this._cutDirection = 0;
        }

        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _lineLayer { get; set; }
        public int _type { get; set; }
        public int _cutDirection { get; set; }
    }

    internal class Obstacles
    {
        [JsonConstructor]
        public Obstacles(float _time, int _lineIndex, int _type, float _duration, int _width)
        {
            this._time = _time;
            this._lineIndex = _lineIndex;
            this._type = _type;
            this._duration = _duration;
            this._width = _width;
        }

        public Obstacles(Obstacle obs)
        {
            this._time = obs.beat;
            this._lineIndex = obs.index;
            if(obs.height == 5)
            {
                this._type = 0;
            }
            else
            {
                this._type = 1;
            }
            this._duration = obs.duration;
            this._width = obs.width;
        }

        public float _time { get; set; }
        public int _lineIndex { get; set; }
        public int _type { get; set; }
        public float _duration { get; set; }
        public int _width { get; set; }
    }
}
