using System.Collections.Generic;
using System.Linq;
using Lolighter.Data.Structure;
using Lolighter.Info;
using static Lolighter.Info.Enumerator;

namespace Lolighter.Algorithm
{
    internal class DownLight
    {
        internal static List<BasicEventData> Down(List<BasicEventData> light, double speed, double spamSpeed, double onSpeed)
        {
            // Turns all long strobes into pulse (alternate between fade and on)
            // Remove fast off
            // Automatically set on Backlight during long period of nothing
            // Remove spin/zoom spam

            // Sort the list
            light.Sort((x, y) => x.beat.CompareTo(y.beat));

            // Sort each of them per type
            Dictionary<int, List<BasicEventData>> mapEvents = new(9);
            foreach (var type in EnvironmentEvent.AllEventType)
            {
                mapEvents.Add(type, new List<BasicEventData>(light.Where(x => x.eventType == type)));
            }

            // Send them to the algorithm
            foreach (var type in EnvironmentEvent.LightEventType)
            {
                mapEvents[type] = Mod(mapEvents[type], speed);
            }

            // Spin/Zoom, we want to remove spam
            foreach (var type in EnvironmentEvent.TrackRingEventType)
            {
                mapEvents[type] = Spam(mapEvents[type], spamSpeed);
            }

            // Put back together the list
            light = new List<BasicEventData>();
            foreach (var type in EnvironmentEvent.AllEventType)
            {
                light.AddRange(mapEvents[type]);
            }

            // Turn On an Event if no light for a while.
            light = On(light, onSpeed);

            // Sort the list
            light.Sort((x, y) => x.beat.CompareTo(y.beat));

            return light;
        }

        static List<BasicEventData> On(List<BasicEventData> light, double onSpeed)
        {
            for (int i = light.Count - 1; i > 0; i--)
            {
                BasicEventData previous = light[i - 1];
                BasicEventData now = light[i];

                // If no light for a long duration, we turn on something.
                if (now.beat - previous.beat >= onSpeed)
                {
                    if (previous.value < 4)
                    {
                        previous.value = EventLightValue.BLUE_ON;
                    }
                    else
                    {
                        previous.value = EventLightValue.RED_ON;
                    }
                }
            }

            return light;
        }

        static List<BasicEventData> Spam(List<BasicEventData> light, double spamSpeed)
        {
            for (int i = light.Count - 1; i > 0; i--)
            {
                BasicEventData previous = light[i - 1];
                BasicEventData now = light[i];

                // We remove spam under that speed
                if (now.beat - previous.beat <= spamSpeed)
                {
                    light.Remove(now);
                }
            }

            return light;
        }

        static List<BasicEventData> Mod(List<BasicEventData> light, double speed)
        {
            for (int i = light.Count - 1; i > 0; i--)
            {
                BasicEventData previous = light[i - 1];
                BasicEventData now = light[i];

                // The light are pretty close
                if (now.beat - previous.beat <= speed)
                {
                    // One of them is an Off event
                    if (now.value == 4 || now.value == 0)
                    {
                        light.Remove(now);
                    }
                    else if (previous.value == 4 || previous.value == 0)
                    {
                        light.Remove(previous);
                    }
                }
            }

            // Now with fast stuff removed.
            for (int i = 1; i < light.Count; i++)
            {
                BasicEventData previous = light[i - 1];
                BasicEventData now = light[i];

                // Swap light between Fade and On if they are close.
                if (now.beat - previous.beat <= speed && now.value == previous.value)
                {
                    if (now.value == EventLightValue.BLUE_FADE || now.value == EventLightValue.RED_FADE || now.value == EventLightValue.BLUE_ON || now.value == EventLightValue.RED_ON)
                    {
                        now.value = Utils.Swap(now.value);
                    }
                }
            }

            return light;
        }
    }
}
