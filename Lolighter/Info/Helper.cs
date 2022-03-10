using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lolighter.Info
{
    internal static class Helper
    {
        internal static class EnvironmentEvent
        {
            public static List<int> LightEventType = new() { 0, 1, 2, 3, 4 };
            public static List<int> TrackRingEventType = new() { 8, 9 };
            public static List<int> LaserRotationEventType = new() { 12, 13 };
            public static List<int> AllEventType = new() { 0, 1, 2, 3, 4, 8, 9, 12, 13 };
        }

        internal static int Swap(int temp) //Fade -> On, On -> Fade
        {
            switch (temp)
            {
                case EventLightValue.BLUE_FADE: return EventLightValue.BLUE_ON;
                case EventLightValue.RED_FADE: return EventLightValue.RED_ON;
                case EventLightValue.BLUE_ON: return EventLightValue.BLUE_FADE;
                case EventLightValue.RED_ON: return EventLightValue.RED_FADE;
                default: return 0;
            }
        }

        internal static int Inverse(int temp) //Red -> Blue, Blue -> Red
        {
            if (temp > EventLightValue.BLUE_FADE)
                return temp - 4; //Turn to blue
            else
                return temp + 4; //Turn to red
        }

        internal static void Shuffle<T>(this IList<T> list)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do rng.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        internal static bool PossibleHead(ColorNote note, ColorNote next, int direction)
        {
            if (note.direction == CutDirection.ANY && next.direction == CutDirection.ANY && direction != 8)
            {
                if (next.line > note.line && (direction == 3 || direction == 5 || direction == 7))
                {
                    return true;
                }
                if (next.line < note.line && (direction == 2 || direction == 4 || direction == 6))
                {
                    return true;
                }
                if (next.layer > note.layer && (direction == 0 || direction == 4 || direction == 5))
                {
                    return true;
                }
                if (next.layer < note.layer && (direction == 1 || direction == 6 || direction == 7))
                {
                    return true;
                }
            }
            else if (note.direction == CutDirection.DOWN || note.direction == CutDirection.DOWN_LEFT || note.direction == CutDirection.DOWN_RIGHT)
            {
                if (next.direction == CutDirection.ANY || next.layer < note.layer)
                {
                    return true;
                }
            }
            if (note.direction == CutDirection.UP || note.direction == CutDirection.UP_LEFT || note.direction == CutDirection.UP_RIGHT)
            {
                if (next.direction == CutDirection.ANY || next.layer > note.layer)
                {
                    return true;
                }
            }
            if (note.direction == CutDirection.LEFT || note.direction == CutDirection.UP_LEFT || note.direction == CutDirection.DOWN_LEFT)
            {
                if (next.direction == CutDirection.ANY || next.line < note.line)
                {
                    return true;
                }
            }
            if (note.direction == CutDirection.RIGHT || note.direction == CutDirection.UP_RIGHT || note.direction == CutDirection.DOWN_RIGHT)
            {
                if (next.direction == CutDirection.ANY || next.line > note.line)
                {
                    return true;
                }
            }

            return false;
        }

        internal static class Line
        {
            public const int LEFT = 0;
            public const int MIDDLE_LEFT = 1;
            public const int MIDDLE_RIGHT = 2;
            public const int RIGHT = 3;
        }

        internal static class Layer
        {
            public const int BOTTOM = 0;
            public const int MIDDLE = 1;
            public const int TOP = 2;
        }

        internal static class CutDirection
        {
            public const int UP = 0;
            public const int DOWN = 1;
            public const int LEFT = 2;
            public const int RIGHT = 3;
            public const int UP_LEFT = 4;
            public const int UP_RIGHT = 5;
            public const int DOWN_LEFT = 6;
            public const int DOWN_RIGHT = 7;
            public const int ANY = 8;
        }

        internal static class ColorType
        {
            public const int RED = 0;
            public const int BLUE = 1;
        }

        internal static class ObstacleType
        {
            public const int WALL = 0;
            public const int CEILING = 1;
        }

        internal static class EventType
        {
            public const int BACK = 0;
            public const int RING = 1;
            public const int LEFT = 2;
            public const int RIGHT = 3;
            public const int SIDE = 4;
            public const int SPIN = 8;
            public const int ZOOM = 9;
            public const int LEFT_ROT = 12;
            public const int RIGHT_ROT = 13;
        }

        internal static class EventLightValue
        {
            public const int OFF = 0;
            public const int BLUE_ON = 1;
            public const int BLUE_FLASH = 2;
            public const int BLUE_FADE = 3;
            public const int BLUE_TRANSITION = 4;
            public const int RED_ON = 5;
            public const int RED_FLASH = 6;
            public const int RED_FADE = 7;
            public const int RED_TRANSITION = 8;
        }

        internal static class SliderMidAnchorMode
        {
            public const int STRAIGHT = 0;
            public const int CLOCKWISE = 1;
            public const int COUNTER_CLOCKWISE = 2;
        }
    }
}
