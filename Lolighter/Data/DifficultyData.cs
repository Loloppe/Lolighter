using Lolighter.Data.Structure;
using Lolighter.Data.V2;
using System;
using System.Collections.Generic;
using static Lolighter.Info.Helper;

namespace Lolighter.Data
{
    internal class DifficultyData
    {
        public DifficultyData(OldDifficultyData oldDiffData)
        {
            this.colorNotes = ConvertOldNoteToNew(oldDiffData._notes);
            this.bombNotes = ConvertOldBombToNew(oldDiffData._notes);
            this.basicBeatmapEvents = ConvertOldEventToNew(oldDiffData._events);
            this.obstacles = ConvertOldObstaclesToNew(oldDiffData._obstacles);
            this.bpmEvents = ConvertOldBPMtoNew(oldDiffData._customData._BPMChanges);

            CustomDataDifficulty customDataDiff = new CustomDataDifficulty();
            customDataDiff.t = oldDiffData._customData._time;
            customDataDiff.bm = ConvertOldBookmarktoNew(oldDiffData._customData._bookmarks);
            this.customData = customDataDiff;
        }

        public static BPMChangeEvent[] ConvertOldBPMtoNew(BPMChange[] bpm)
        {
            List<BPMChangeEvent> newBPM = new();
            for (int i = 0; i < bpm.Length; i++)
            {
                newBPM.Add(new(bpm[i]._time, bpm[i]._BPM));
            }
            return newBPM.ToArray();
        }

        public static Bookmark[] ConvertOldBookmarktoNew(Bookmarks[] book)
        {
            List<Bookmark> newBookmarks = new();
            for (int i = 0; i < book.Length; i++)
            {
                newBookmarks.Add(new(book[i]._time, book[i]._name, book[i]._color));
            }
            return newBookmarks.ToArray();
        }

        public static ColorNote[] ConvertOldNoteToNew(Notes[] notes)
        {
            List<ColorNote> colorNotes = new();
            for(int i = 0; i < notes.Length; i++)
            {
                if(notes[i]._type == 0 || notes[i]._type == 1)
                {
                    colorNotes.Add(new(notes[i]._time, notes[i]._type, notes[i]._lineIndex, notes[i]._lineLayer, notes[i]._cutDirection, 0));
                }
            }
            return colorNotes.ToArray();
        }

        public static BombNote[] ConvertOldBombToNew(Notes[] notes)
        {
            List<BombNote> bombNotes = new();
            for (int i = 0; i < notes.Length; i++)
            {
                if (notes[i]._type == 3)
                {
                    bombNotes.Add(new(notes[i]._time, notes[i]._lineIndex, notes[i]._lineLayer));
                }
            }
            return bombNotes.ToArray();
        }

        public static BasicEventData[] ConvertOldEventToNew(Events[] events)
        {
            List<BasicEventData> basicEvents = new();
            for (int i = 0; i < events.Length; i++)
            {
                basicEvents.Add(new(events[i]._time, events[i]._type, events[i]._value, 1));
            }
            return basicEvents.ToArray();
        }

        //Not sure about that one
        public static Obstacle[] ConvertOldObstaclesToNew(Obstacles[] obstacles)
        {
            List<Obstacle> obstacle = new();
            for (int i = 0; i < obstacles.Length; i++)
            {
                if(obstacles[i]._type == 0) // Full
                {
                    obstacle.Add(new(obstacles[i]._time, obstacles[i]._lineIndex, 0, obstacles[i]._duration, obstacles[i]._width, 3));
                }
                else if (obstacles[i]._type == 1) // Crouch
                {
                    obstacle.Add(new(obstacles[i]._time, obstacles[i]._lineIndex, 2, obstacles[i]._duration, obstacles[i]._width, 1));
                }
            }
            return obstacle.ToArray();
        }

        public string version { get; set; } = "3.0.0";
        public BPMChangeEvent[] bpmEvents { get; set; } = Array.Empty<BPMChangeEvent>();// Variable BPM
        public RotationEvent[] rotationEvents { get; set; } = Array.Empty<RotationEvent>(); // 90/360 map rotation
        public ColorNote[] colorNotes { get; set; } = Array.Empty<ColorNote>(); // Note
        public BombNote[] bombNotes { get; set; } = Array.Empty<BombNote>(); // Bomb
        public Obstacle[] obstacles { get; set; } = Array.Empty<Obstacle>(); // Wall
        public BaseSliderData[] sliders { get; set; } = Array.Empty<BaseSliderData>(); // New line
        public SliderData[] burstSliders { get; set; } = Array.Empty<SliderData>(); // New slider
        public Waypoint[] waypoints { get; set; } = Array.Empty<Waypoint>(); // BTS
        public BasicEventData[] basicBeatmapEvents { get; set; } = Array.Empty<BasicEventData>(); // Light
        public ColorBoostEventData[] colorBoostBeatmapEvents { get; set; } = Array.Empty<ColorBoostEventData>(); // Boost (switch color palette)
        public LightColorEventBoxGroup[] lightColorEventBoxGroups { get; set; } = Array.Empty<LightColorEventBoxGroup>(); // ??
        public LightRotationEventBoxGroup[] lightRotationEventBoxGroups { get; set; } = Array.Empty<LightRotationEventBoxGroup>(); // ??
        public BasicEventTypesWithKeywords[] basicEventTypesWithKeywords { get; set; } = Array.Empty<BasicEventTypesWithKeywords>(); // BTS
        public bool useNormalEventsAsCompatibleEvents { get; set; } = true; // Allow older map to use BasicEvent as rotationEvents/BoostEvent, etc.
        public CustomDataDifficulty customData { get; set; } = new(); // For external tools
    }
}
