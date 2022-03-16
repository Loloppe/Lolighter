using Lolighter.Data.Structure;
using Lolighter.Info;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Enumerator;

namespace Lolighter.Algorithm
{
    internal static class Light
    {
        internal static (List<ColorBoostEventData>, List<BasicEventData>) CreateLight(List<ColorNote> Notes, List<BurstSliderData> Slider, bool NerfStrobes, bool BoostLight)
        {
            // Bunch of var to keep timing in check
            float last = new(); 
            float[] time = new float[4];
            int[] light = new int[3];
            float offset = Notes[0].beat;
            float firstNote = 0;
            float timer = 0;

            //Light counter, stop at maximum.
            int count; 

            //Set color start value.
            int color; 

            // For laser speed
            int currentSpeed = 3;

            // Rhythm check
            float lastSpeed = 0;

            // To not light up Double twice
            float nextDouble = 0;

            // Slider stuff
            bool firstSlider = false;
            float nextSlider = new();
            List<int> sliderLight = new() { 4, 1, 0 };
            int sliderIndex = 0;
            float sliderNoteCount = 0;
            bool wasSlider = false;

            // Pattern for specific rhythm
            List<int> pattern = new(Enumerable.Range(0, 5));
            int patternIndex = 0;
            int patternCount = 0;

            // The new events
            List<BasicEventData> eventTempo = new();
            List<ColorBoostEventData> boostEvent = new();

            // Is the section currently using Boost Event
            bool boost = true;
            float boostSwap = 8;
            float boostIncrement = 0;

            // If double notes lights are on
            bool doubleOn = false;

            // Make sure this is the right timing for color swap with Boost Event
            float ColorOffset = 0;
            float ColorSwap = 4;

            // To make sure that slider doesn't apply as double
            List<ColorNote> sliderTiming = new();

            // Order note, necessary if we're converting V3 bomb from notes
            Notes = Notes.OrderBy(o => o.beat).ToList();

            void ResetTimer() //Pretty much reset everything necessary.
            {
                color = EventLightValue.BLUE_FADE; //Blue Fade
                firstNote = Notes[0].beat;
                offset = firstNote;
                count = 0;
                for (int i = 0; i < 2; i++)
                {
                    time[i] = 0.0f;
                    light[i] = 0;
                }
                time[2] = 0.0f;
                time[3] = 0.0f;
            }

            void TimerDuration() //Check the checkpoint
            {
                timer = time[0];
                if (timer >= ColorOffset + ColorSwap + offset) //If the timer is above offset + ColorOffset + ColorSwap (From the interface), then it's time to change color.
                {
                    int swapTime = (int)((time[0] - time[1]) / ColorSwap) + 1; //We get the number of "beat" since the last time it entered here this way.
                    for (int i = 0; i < swapTime; i++) //For each time that it need to swap. (Dumb fix for a dumb method)
                    {
                        color = Utils.Inverse(color); //Swap color
                        offset += ColorSwap; //Offset incremented
                    }
                }
                if (timer >= ColorOffset +  boostSwap + boostIncrement) //If the timer is above offset + ColorOffset + ColorSwap (From the interface), then it's time to change color.
                {
                    int swapTime = (int)((time[0] - time[1]) / boostSwap) + 1; //We get the number of "beat" since the last time it entered here this way.
                    for (int i = 0; i < swapTime; i++) //For each time that it need to swap. (Dumb fix for a dumb method)
                    {
                        boostIncrement += boostSwap; //Offset incremented
                    }
                }
            }

            ResetTimer();

            bool found = false;

            // Find all sliders
            for(int i = 1; i < Notes.Count; i++)
            {
                // Between 1/8 and 0, same cut direction or dots
                if (Notes[i].beat - Notes[i - 1].beat <= 0.125 && Notes[i].beat - Notes[i - 1].beat > 0 && (Notes[i].direction == Notes[i - 1].direction || Notes[i].direction == 8 || Notes[i - 1].direction == 8))
                {
                    sliderTiming.Add(Notes[i - 1]);
                    found = true;
                }
                else if(found)
                {
                    sliderTiming.Add(Notes[i - 1]);
                    found = false;
                }
            }

            foreach (ColorNote note in Notes) //Process specific light (Side/Neon) using time.
            {
                float now = note.beat;
                time[0] = now;

                //Here we process Spin and Zoom
                if (now == firstNote && time[1] == 0.0D) //If we are processing the first note, add spin + zoom to it.
                {
                    eventTempo.Add(new BasicEventData(now, EventType.SPIN, 0, 1));
                    eventTempo.Add(new BasicEventData(now, EventType.ZOOM, 0, 1));
                }
                else if (now >= ColorOffset + ColorSwap + offset && now > firstNote) //If we are reaching the next threshold of the timer
                {
                    //Add a spin at timer.
                    eventTempo.Add(new BasicEventData(offset, EventType.SPIN, 0, 1));
                    if (count == 0) //Only add zoom every 2 spin.
                    {
                        eventTempo.Add(new BasicEventData(offset, EventType.ZOOM, 0, 1));
                        count = 1;
                    }
                    else
                    {
                        count--;
                    }
                }
                //If there's a quarter between two float parallel notes and timer didn't pass the check.
                else if (time[1] - time[2] == 0.25 && time[3] == time[2] && time[1] == now && timer < offset)
                {
                    eventTempo.Add(new BasicEventData(now, EventType.SPIN, 0, 1));
                }

                // Boost Event
                if(now >= ColorOffset + boostSwap + boostIncrement && now > firstNote && BoostLight)
                {
                    if(boost)
                    {
                        boostEvent.Add(new ColorBoostEventData(offset, false));
                        boost = false;
                    }
                    else
                    {
                        boostEvent.Add(new ColorBoostEventData(offset, true));
                        boost = true;
                    }
                }

                TimerDuration();

                if (!NerfStrobes && doubleOn) //Off event
                {
                    if (now - last >= 1)
                    {
                        eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.BACK, 0, 1));
                        eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.RING, 0, 1));
                        eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.SIDE, 0, 1));
                        eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.LEFT, 0, 1));
                        eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.RIGHT, 0, 1));
                    }
                    else
                    {
                        // Will be fused with some events, but we will sort that out later on.
                        eventTempo.Add(new BasicEventData(now, EventType.BACK, 0, 1));
                        eventTempo.Add(new BasicEventData(now, EventType.RING, 0, 1));
                        eventTempo.Add(new BasicEventData(now, EventType.SIDE, 0, 1));
                        eventTempo.Add(new BasicEventData(now, EventType.LEFT, 0, 1));
                        eventTempo.Add(new BasicEventData(now, EventType.RIGHT, 0, 1));
                    }

                    doubleOn = false;
                }

                //If not same note, same beat and not slider, apply once.
                if ((now == time[1] || (now - time[1] <= 0.02 && time[1] != time[2])) && (time[1] != 0.0D && now != last) && !sliderTiming.Exists(e => e.beat == now))
                {
                    eventTempo.Add(new BasicEventData(now, EventType.BACK, color, 1)); //Back Top Laser
                    eventTempo.Add(new BasicEventData(now, EventType.RING, color, 1)); //Track Ring Neons
                    eventTempo.Add(new BasicEventData(now, EventType.SIDE, color, 1)); //Side Light
                    eventTempo.Add(new BasicEventData(now, EventType.LEFT, color, 1)); //Left Laser
                    eventTempo.Add(new BasicEventData(now, EventType.RIGHT, color, 1)); //Right Laser

                    // Laser speed based on rhythm
                    if (time[0] - time[1] < 0.25)
                    {
                        currentSpeed = 7;
                    }
                    else if (time[0] - time[1] >= 0.25 && time[0] - time[1] < 0.5)
                    {
                        currentSpeed = 5;
                    }
                    else if (time[0] - time[1] >= 0.5 && time[0] - time[1] < 1)
                    {
                        currentSpeed = 3;
                    }
                    else
                    {
                        currentSpeed = 1;
                    }

                    eventTempo.Add(new BasicEventData(now + 0.01f, EventType.LEFT_ROT, currentSpeed, 1)); //Left Rotation
                    eventTempo.Add(new BasicEventData(now + 0.01f, EventType.RIGHT_ROT, currentSpeed, 1)); //Right Rotation

                    doubleOn = true;
                    last = now;
                }

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

            nextSlider = new();

            // Convert quick light color swap
            if (NerfStrobes)
            {
                float lastTimeTop = 100;
                float lastTimeNeon = 100;
                float lastTimeSide = 100;

                foreach (var x in eventTempo)
                {
                    if (x.eventType == EventType.BACK)
                    {
                        if (x.beat - lastTimeTop <= 1)
                        {
                            x.value = Utils.Swap(x.value);
                        }
                        lastTimeTop = x.beat;
                    }
                    else if (x.eventType == EventType.RING)
                    {
                        if (x.beat - lastTimeNeon <= 1)
                        {
                            x.value = Utils.Swap(x.value);
                        }
                        lastTimeNeon = x.beat;
                    }
                    else if (x.eventType == EventType.SIDE)
                    {
                        if (x.beat - lastTimeSide <= 1)
                        {
                            x.value = Utils.Swap(x.value);
                        }
                        lastTimeSide = x.beat;
                    }
                }
            }

            ResetTimer();

            foreach (ColorNote note in Notes) //Process all note using time.
            {
                time[0] = note.beat;

                TimerDuration();

                if (wasSlider)
                {
                    if (sliderNoteCount != 0)
                    {
                        sliderNoteCount--;

                        for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                        {
                            time[i] = time[i - 1];
                        }
                        continue;
                    }
                    else
                    {
                        wasSlider = false;
                    }
                }

                if (firstSlider)
                {
                    firstSlider = false;
                    continue;
                }

                // Find the next double
                if (time[0] >= nextDouble)
                {
                    for (int i = Notes.FindIndex(n => n == note); i < Notes.Count - 1; i++)
                    {
                        if(i != 0)
                        {
                            if (Notes[i].beat == Notes[i - 1].beat)
                            {
                                nextDouble = Notes[i].beat;
                                break;
                            }
                        }
                    }
                }

                // Find the next slider (1/8 minimum) or chain
                if (time[0] >= nextSlider)
                {
                    sliderNoteCount = 0;

                    for (int i = Notes.FindIndex(n => n == note); i < Notes.Count - 1; i++)
                    {
                        if(i != 0 && i < Notes.Count)
                        {
                            // Between 1/8 and 0, same cut direction or dots
                            if (Notes[i].beat - Notes[i - 1].beat <= 0.125 && Notes[i].beat - Notes[i - 1].beat > 0 && (Notes[i].direction == Notes[i - 1].direction || Notes[i].direction == 8))
                            {
                                // Search for the last note of the slider
                                if (sliderNoteCount == 0)
                                {
                                    // This is the first note of the slider
                                    nextSlider = Notes[i - 1].beat;
                                }
                                sliderNoteCount++;
                            }
                            else if (sliderNoteCount != 0)
                            {
                                break;
                            }
                        }
                    }
                }

                // It's the next slider or chain
                if ((nextSlider == note.beat) || Slider.Exists(o => o.beat == time[0]))
                {
                    // Take a light between neon, side or backlight and strobes it via On/Flash
                    if(sliderIndex == -1)
                    {
                        sliderIndex = 2;
                    }

                    // Place light
                    eventTempo.Add(new BasicEventData(time[0], sliderLight[sliderIndex], color - 2, 1));
                    eventTempo.Add(new BasicEventData(time[0] + 0.125f, sliderLight[sliderIndex], color - 1, 1));
                    eventTempo.Add(new BasicEventData(time[0] + 0.25f, sliderLight[sliderIndex], color - 2, 1));
                    eventTempo.Add(new BasicEventData(time[0] + 0.375f, sliderLight[sliderIndex], color - 1, 1));
                    eventTempo.Add(new BasicEventData(time[0] + 0.5f, sliderLight[sliderIndex], 0, 1));

                    sliderIndex--;

                    // Spin goes brrr
                    eventTempo.Add(new BasicEventData(time[0], EventType.SPIN, 0, 1));
                    for (int i = 0; i < 8; i++)
                    {
                        eventTempo.Add(new BasicEventData(time[0] + 0.5f - (0.5f / 8f * i), EventType.SPIN, 0, 1));
                    }

                    wasSlider = true;
                }
                // Not a double
                else if (time[0] != nextDouble)
                {
                    if (time[1] - time[2] >= lastSpeed + 0.02 || time[1] - time[2] <= lastSpeed - 0.02 || patternCount == 20) // New speed or 20 notes of the same pattern
                    {
                        int old = 0;
                        // New pattern
                        if (patternIndex != 0)
                        {
                            old = pattern[patternIndex - 1];
                        }
                        else
                        {
                            old = pattern[4];
                        }

                        do
                        {
                            pattern.Shuffle();
                        } while (pattern[0] == old);
                        patternIndex = 0;
                        patternCount = 0;
                    }

                    // Place the next light
                    eventTempo.Add(new BasicEventData(time[0], pattern[patternIndex], color, 1));

                    // Speed based on rhythm
                    if (time[0] - time[1] < 0.25)
                    {
                        currentSpeed = 7;
                    }
                    else if (time[0] - time[1] >= 0.25 && time[0] - time[1] < 0.5)
                    {
                        currentSpeed = 5;
                    }
                    else if (time[0] - time[1] >= 0.5 && time[0] - time[1] < 1)
                    {
                        currentSpeed = 3;
                    }
                    else
                    {
                        currentSpeed = 1;
                    }

                    // Add laser rotation if necessary
                    if (pattern[patternIndex] == 2)
                    {
                        eventTempo.Add(new BasicEventData(time[0] + 0.01f, EventType.LEFT_ROT, currentSpeed, 1));
                    }
                    else if (pattern[patternIndex] == 3)
                    {
                        eventTempo.Add(new BasicEventData(time[0] + 0.01f, EventType.RIGHT_ROT, currentSpeed, 1));
                    }

                    // Place off event
                    if (Notes[^1].beat != note.beat)
                    {
                        if (Notes[Notes.FindIndex(n => n == note) + 1].beat == nextDouble)
                        {
                            if (Notes[Notes.FindIndex(n => n == note) + 1].beat - time[0] <= 2)
                            {
                                float value = (Notes[Notes.FindIndex(n => n == note) + 1].beat - Notes[Notes.FindIndex(n => n == note)].beat) / 2;
                                eventTempo.Add(new BasicEventData(Notes[Notes.FindIndex(n => n == note)].beat + value, pattern[patternIndex], 0, 1));
                            }
                        }
                        else
                        {
                            eventTempo.Add(new BasicEventData(Notes[Notes.FindIndex(n => n == note) + 1].beat, pattern[patternIndex], 0, 1));
                        }
                    }

                    // Pattern have 5 notes in total (5 lights available)
                    if (patternIndex < 4)
                    {
                        patternIndex++;
                    }
                    else
                    {
                        patternIndex = 0;
                    }

                    patternCount++;
                    lastSpeed = time[0] - time[1];
                }

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

            eventTempo = eventTempo.OrderBy(o => o.beat).ToList();

            // Remove fused or move off event between
            eventTempo = RemoveFused(eventTempo);

            // Sort lights
            eventTempo = eventTempo.OrderBy(o => o.beat).ToList();
            boostEvent = boostEvent.OrderBy(o => o.beat).ToList();

            return (boostEvent, eventTempo);
        }

        static public List<BasicEventData> RemoveFused(List<BasicEventData> events)
        {
            float? closest = 0f;

            // Get all fused events of a specific type
            for (int i = 0; i < events.Count; i++)
            {
                BasicEventData e = events[i];

                BasicEventData? basicEventData = events.Find(o => o.eventType == e.eventType && (o.beat - e.beat >= -0.02 && o.beat - e.beat <= 0.02) && o != e);
                if (basicEventData != null)
                {
                    BasicEventData? basicEventData2 = events.Find(o => o.eventType == basicEventData.eventType && (o.beat - basicEventData.beat >= -0.02 && o.beat - basicEventData.beat <= 0.02) && o != basicEventData);

                    if(basicEventData2 != null)
                    {
                        BasicEventData? temp = events.FindLast(o => o.beat < e.beat && e.beat > closest && o.value != 0);

                        if (temp != null)
                        {
                            closest = temp.beat;

                            if (basicEventData2.value == EventLightValue.OFF)
                            {
                                // Move off event between fused note and last note
                                events[(events.FindIndex(o => o.beat == basicEventData2.beat && o.value == basicEventData2.value && o.eventType == basicEventData2.eventType))].beat = (float)(basicEventData2.beat - ((basicEventData2.beat - closest) / 2));
                            }
                            else
                            {
                                // Move off event between fused note and last note
                                if (basicEventData.value == EventLightValue.OFF || basicEventData.value == EventLightValue.BLUE_TRANSITION || basicEventData.value == EventLightValue.RED_TRANSITION)
                                {
                                    events[(events.FindIndex(o => o.beat == basicEventData.beat && o.value == basicEventData.value && o.eventType == basicEventData.eventType))].beat = (float)(basicEventData.beat - ((basicEventData.beat - closest) / 2));
                                }
                                else // Delete event
                                {
                                    events.RemoveAt(events.FindIndex(o => o.beat == basicEventData.beat && o.value == basicEventData.value && o.eventType == basicEventData.eventType));
                                }
                            }
                        }
                    }
                }
            }

            return events;
        }
    }
}
