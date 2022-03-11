using Lolighter.Data.Structure;
using Lolighter.Data.V2;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lolighter.Data
{
    internal class DifficultyData
    {
        [JsonConstructor]
        public DifficultyData(string version, List<BPMChangeEvent> bpmEvents, List<RotationEvent> rotationEvents, List<ColorNote> colorNotes,
            List<BombNote> bombNotes, List<Obstacle> obstacles, List<SliderData>  sliders, List<BurstSliderData>  burstSliders, List<Waypoint>  waypoints,
            List<BasicEventData>  basicBeatmapEvents, List<ColorBoostEventData>  colorBoostBeatmapEvents, List<LightColorEventBoxGroup>  lightColorEventBoxGroups,
            List<LightRotationEventBoxGroup>  lightRotationEventBoxGroups, BasicEventTypesWithKeywords basicEventTypesWithKeywords, bool useNormalEventsAsCompatibleEvents,
            CustomDataDifficulty customData)
        {
            if (version == null)
            {
                version = "3.0.0";
            }
            this.version = version;
            if (bpmEvents == null)
            {
                bpmEvents = new();
            }
            this.bpmEvents = bpmEvents;
            if (rotationEvents == null)
            {
                rotationEvents = new();
            }
            this.rotationEvents = rotationEvents;
            if (colorNotes == null)
            {
                colorNotes = new();
            }
            this.colorNotes = colorNotes;
            if (bombNotes == null)
            {
                bombNotes = new();
            }
            this.bombNotes = bombNotes;
            if (obstacles == null)
            {
                obstacles = new();
            }
            this.obstacles = obstacles;
            if (sliders == null)
            {
                sliders = new();
            }
            this.sliders = sliders;
            if (burstSliders == null)
            {
                burstSliders = new();
            }
            this.burstSliders = burstSliders;
            if (waypoints == null)
            {
                waypoints = new();
            }
            this.waypoints = waypoints;
            if (basicBeatmapEvents == null)
            {
                basicBeatmapEvents = new();
            }
            this.basicBeatmapEvents = basicBeatmapEvents;
            if (colorBoostBeatmapEvents == null)
            {
                colorBoostBeatmapEvents = new();
            }
            this.colorBoostBeatmapEvents = colorBoostBeatmapEvents;
            if (lightColorEventBoxGroups == null)
            {
                lightColorEventBoxGroups = new();
            }
            this.lightColorEventBoxGroups = lightColorEventBoxGroups;
            if (lightRotationEventBoxGroups == null)
            {
                lightRotationEventBoxGroups = new();
            }
            this.lightRotationEventBoxGroups = lightRotationEventBoxGroups;
            this.basicEventTypesWithKeywords = basicEventTypesWithKeywords;
            this.useNormalEventsAsCompatibleEvents = useNormalEventsAsCompatibleEvents;
            this.customData = customData;
        }

        [JsonInclude]
        public string version { get; set; } = "3.0.0";
        [JsonInclude]
        public List<BPMChangeEvent> bpmEvents { get; set; } // Variable BPM
        [JsonInclude]
        public List<RotationEvent>  rotationEvents { get; set; } // 90/360 map rotation
        [JsonInclude]
        public List<ColorNote> colorNotes { get; set; } // Note
        [JsonInclude]
        public List<BombNote>  bombNotes { get; set; } // Bomb
        [JsonInclude]
        public List<Obstacle>  obstacles { get; set; } // Wall
        [JsonInclude]
        public List<SliderData>  sliders { get; set; } // New slider
        [JsonInclude]
        public List<BurstSliderData>  burstSliders { get; set; }// New burst slider
        [JsonInclude]
        public List<Waypoint>  waypoints { get; set; }  // BTS
        [JsonInclude]
        public List<BasicEventData>  basicBeatmapEvents { get; set; } // Light
        [JsonInclude]
        public List<ColorBoostEventData>  colorBoostBeatmapEvents { get; set; } // Boost (switch color palette)
        [JsonInclude]
        public List<LightColorEventBoxGroup>  lightColorEventBoxGroups { get; set; }  // New light stuff
        [JsonInclude]
        public List<LightRotationEventBoxGroup>  lightRotationEventBoxGroups { get; set; } // New light stuff
        [JsonInclude]
        public BasicEventTypesWithKeywords basicEventTypesWithKeywords { get; set; } = new(new()); // BTS
        [JsonInclude]
        public bool useNormalEventsAsCompatibleEvents { get; set; } = true; // Allow older map to use BasicEvent as rotationEvents/BoostEvent, etc.
        [JsonInclude]
        public CustomDataDifficulty customData { get; set; } = new(); // For external tools

        public DifficultyData(OldDifficultyData oldDiffData)
        {
            colorNotes = ConvertOldNoteToNew(oldDiffData._notes);
            bombNotes = ConvertOldBombToNew(oldDiffData._notes);
            basicBeatmapEvents = ConvertOldEventToNew(oldDiffData._events);
            obstacles = ConvertOldObstaclesToNew(oldDiffData._obstacles);
            bpmEvents = ConvertOldBPMtoNew(oldDiffData._customData._BPMChanges);

            CustomDataDifficulty customDataDiff = new();
            customDataDiff.t = oldDiffData._customData._time;
            customDataDiff.bm = ConvertOldBookmarktoNew(oldDiffData._customData._bookmarks);
            customData = customDataDiff;

            if (version == null)
            {
                var version = "3.0.0";
                this.version = version;
            }
            if (bpmEvents == null)
            {
                var bpmEvents = new List<BPMChangeEvent>();
                this.bpmEvents = bpmEvents;
            }
            ;
            if (rotationEvents == null)
            {
                var rotationEvents = new List<RotationEvent>();
                this.rotationEvents = rotationEvents;
            }
            if (colorNotes == null)
            {
                var colorNotes = new List<ColorNote>();
                this.colorNotes = colorNotes;
            }
            if (bombNotes == null)
            {
                var bombNotes = new List<BombNote>();
                this.bombNotes = bombNotes;
            }
            if (obstacles == null)
            {
                var obstacles = new List<Obstacle>();
                this.obstacles = obstacles;
            }
            if (sliders == null)
            {
                var sliders = new List<SliderData>();
                this.sliders = sliders;
            }
            if (burstSliders == null)
            {
                var burstSliders = new List<BurstSliderData>();
                this.burstSliders = burstSliders;
            }
            if (waypoints == null)
            {
                var waypoints = new List<Waypoint>();
                this.waypoints = waypoints;
            }
            if (basicBeatmapEvents == null)
            {
                var basicBeatmapEvents = new List<BasicEventData>();
                this.basicBeatmapEvents = basicBeatmapEvents;
            }
            if (colorBoostBeatmapEvents == null)
            {
                var colorBoostBeatmapEvents = new List<ColorBoostEventData>();
                this.colorBoostBeatmapEvents = colorBoostBeatmapEvents;
            }
            if (lightColorEventBoxGroups == null)
            {
                var lightColorEventBoxGroups = new List<LightColorEventBoxGroup>();
                this.lightColorEventBoxGroups = lightColorEventBoxGroups;
            }
            if (lightRotationEventBoxGroups == null)
            {
                var lightRotationEventBoxGroups = new List<LightRotationEventBoxGroup>();
                this.lightRotationEventBoxGroups = lightRotationEventBoxGroups;
            }
        }

        public static List<BPMChangeEvent> ConvertOldBPMtoNew(List<BPMChange> bpm)
        {
            List<BPMChangeEvent> newBPM = new();
            for (int i = 0; i < bpm.Count; i++)
            {
                newBPM.Add(new(bpm[i]._time, bpm[i]._BPM));
            }
            return newBPM;
        }

        public static List<Bookmark> ConvertOldBookmarktoNew(List<Bookmarks> book)
        {
            List<Bookmark> newBookmarks = new();
            for (int i = 0; i < book.Count; i++)
            {
                newBookmarks.Add(new(book[i]._time, book[i]._name, book[i]._color));
            }
            return newBookmarks;
        }

        public static List<ColorNote> ConvertOldNoteToNew(List<Notes> notes)
        {
            List<ColorNote> colorNotes = new();
            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i]._type == 0 || notes[i]._type == 1)
                {
                    colorNotes.Add(new(notes[i]._time, notes[i]._type, notes[i]._lineIndex, notes[i]._lineLayer, notes[i]._cutDirection, 0));
                }
            }
            return colorNotes;
        }

        public static List<BombNote> ConvertOldBombToNew(List<Notes> notes)
        {
            List<BombNote> bombNotes = new();
            for (int i = 0; i < notes.Count; i++)
            {
                if (notes[i]._type == 3)
                {
                    bombNotes.Add(new(notes[i]._time, notes[i]._lineIndex, notes[i]._lineLayer));
                }
            }
            return bombNotes;
        }

        public static List<BasicEventData> ConvertOldEventToNew(List<Events> events)
        {
            List<BasicEventData> basicEvents = new();
            for (int i = 0; i < events.Count; i++)
            {
                basicEvents.Add(new(events[i]._time, events[i]._type, events[i]._value, 1));
            }
            return basicEvents;
        }

        public static List<Obstacle> ConvertOldObstaclesToNew(List<Obstacles> obstacles)
        {
            List<Obstacle> obstacle = new();
            for (int i = 0; i < obstacles.Count; i++)
            {
                if (obstacles[i]._type == 0) // Full
                {
                    obstacle.Add(new(obstacles[i]._time, obstacles[i]._lineIndex, 0, obstacles[i]._duration, obstacles[i]._width, 5));
                }
                else if (obstacles[i]._type == 1) // Crouch
                {
                    obstacle.Add(new(obstacles[i]._time, obstacles[i]._lineIndex, 2, obstacles[i]._duration, obstacles[i]._width, 3));
                }
            }
            return obstacle;
        }
    }
}
