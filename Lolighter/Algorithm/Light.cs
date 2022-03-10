using Lolighter.Data.Structure;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    internal static class Light
    {
        internal static (List<ColorBoostEventData>, List<BasicEventData>) CreateLight(List<float> Timing, float ColorOffset, float ColorSwap, bool AllowBackStrobe, bool AllowNeonStrobe, bool AllowSideStrobe, bool AllowFade, bool AllowSpinZoom, bool NerfStrobes, bool BoostLight)
        {
            float last = new(); //Var to stop spin-stack and also used as time check.
            float[] time = new float[4]; //Now, before, before-before, before-before-before, in this order.
            //0.0D = Default value for float, similar to NULL for int.
            int[] light = new int[3]; //Now, before, before-before.
            int lastLight = 0;
            float offset;
            float firstNote = 0;
            float timer = 0; //Timer start on the first note.
            int count; //Light counter, stop at maximum.
            int maximum = 2; //Maximum number of light per same time.
            int color; //Set color start value.
            float lastCut = 0;
            int currentSpeed = 3;
            float lastSpeed = 0;
            float nextfloat = 0;
            bool firstSlider = false;
            float nextSlider = new();
            List<int> sliderLight = new() { 0, 1, 4 };
            int sliderIndex = 0;
            float sliderNoteCount = 0;
            bool wasSlider = false;
            List<int> pattern = new(Enumerable.Range(0, 5));
            int patternIndex = 0;
            int patternCount = 0;
            List<BasicEventData> eventTempo = new();
            List<ColorBoostEventData> boostEvent = new();
            bool boost = true;
            float boostSwap = 8;
            float boostIncrement = 0;

            void ResetTimer() //Pretty much reset everything necessary.
            {
                if (AllowFade)
                {
                    color = EventLightValue.BLUE_FADE; //Blue Fade
                }
                else
                {
                    color = EventLightValue.BLUE_ON; //Blue On
                }
                firstNote = Timing[0];
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
                        color = Inverse(color); //Swap color
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

            void CreateGenericLight(int speed) //Receive laser speed as var.
            {
                if (time[0] == time[1]) //Same beat
                {
                    if (count < maximum) //Maximum laser is 2
                    {
                        count++;
                    }
                }
                else
                {
                    count = 0; //Reset the count, we are moving forward (in time).
                    for (int i = 0; i < 2; i++)
                    {
                        if (light[i] != 0 && time[0] - time[1] <= 2.5) //TODO: Re-add as an option left/right laser strobe.
                        {
                            //eventTempo.Add(new BasicEvent((time[0] - (time[0] - time[1]) / 2), light[i], 0));
                        }
                        light[i] = 0;
                    }
                }

                if (count == maximum) //If count reach the maximum, we skip this.
                {
                    return;
                }

                if (light[0] != 0)
                {
                    light[1] = light[0];
                }

                if (lastLight == 2) //We swap between laser
                {
                    light[0] = 3;
                }
                else
                {
                    light[0] = 2;
                }

                switch (light[0]) //Add laser + speed
                {
                    case 2:
                        eventTempo.Add(new BasicEventData(time[0], EventType.LEFT, color, 1));
                        eventTempo.Add(new BasicEventData(time[0], EventType.LEFT_ROT, speed, 1));
                        break;
                    case 3:
                        eventTempo.Add(new BasicEventData(time[0], EventType.RIGHT, color, 1));
                        eventTempo.Add(new BasicEventData(time[0], EventType.RIGHT_ROT, speed, 1));
                        break;
                }

                lastLight = light[0];
            }

            ResetTimer();

            foreach (float note in Timing) //Process specific light (Side/Neon) using time.
            {
                float now = note;
                time[0] = now;

                //Here we process Spin and Zoom
                if (now == firstNote && time[1] == 0.0D && AllowSpinZoom) //If we are processing the first note, add spin + zoom to it.
                {
                    eventTempo.Add(new BasicEventData(now, EventType.SPIN, 0, 1));
                    eventTempo.Add(new BasicEventData(now, EventType.ZOOM, 0, 1));
                }
                else if (now >= ColorOffset + ColorSwap + offset && now > firstNote && AllowSpinZoom) //If we are reaching the next threshold of the timer
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
                else if (time[1] - time[2] == 0.25 && time[3] == time[2] && time[1] == now && timer < offset && AllowSpinZoom)
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

                if ((now == time[1] || (now - time[1] <= 0.02 && time[1] != time[2])) && (time[1] != 0.0D && now != last)) //If not same note, same beat, apply once.
                {
                    if (!NerfStrobes) //Off event
                    {
                        if (now - last >= 0.5)
                        {
                            if (AllowBackStrobe) //Back Top Laser
                            {
                                eventTempo.Add(new BasicEventData(now + 0.25f, EventType.BACK, 0, 1));
                            }
                            if (AllowNeonStrobe) //Neon Light
                            {
                                eventTempo.Add(new BasicEventData(now + 0.25f, EventType.RING, 0, 1));
                            }
                            if (AllowSideStrobe) //Side Light
                            {
                                eventTempo.Add(new BasicEventData(now + 0.25f, EventType.SIDE, 0, 1));
                            }
                        }
                        else
                        {
                            if (AllowBackStrobe) //Back Top Laser
                            {
                                eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.BACK, 0, 1));
                            }
                            if (AllowNeonStrobe) //Neon Light
                            {
                                eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.RING, 0, 1));
                            }
                            if (AllowSideStrobe) //Side Light
                            {
                                eventTempo.Add(new BasicEventData(now - (now - last) / 2, EventType.SIDE, 0, 1));
                            }
                        }
                    }

                    eventTempo.Add(new BasicEventData(now, EventType.BACK, color, 1)); //Back Top Laser
                    eventTempo.Add(new BasicEventData(now, EventType.RING, color, 1)); //Track Ring Neons
                    eventTempo.Add(new BasicEventData(now, EventType.SIDE, color, 1)); //Side Light

                    last = now;
                }

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

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
                            x.value = Swap(x.value);
                        }
                        lastTimeTop = x.beat;
                    }
                    else if (x.eventType == EventType.RING)
                    {
                        if (x.beat - lastTimeNeon <= 1)
                        {
                            x.value = Swap(x.value);
                        }
                        lastTimeNeon = x.beat;
                    }
                    else if (x.eventType == EventType.SIDE)
                    {
                        if (x.beat - lastTimeSide <= 1)
                        {
                            x.value = Swap(x.value);
                        }
                        lastTimeSide = x.beat;
                    }
                }
            }

            ResetTimer();

            foreach (float note in Timing) //Process all note using time.
            {
                time[0] = note;

                TimerDuration();

                if (wasSlider)
                {
                    if (sliderNoteCount != 0)
                    {
                        sliderNoteCount--;
                        lastCut = note; //For the spin check.

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

                if (time[2] == 0.0D) //No third note processed yet.
                {
                    if (time[1] == 0.0D) //No second note processed yet.
                    {
                        time[1] = time[0]; //Skip first note.
                        continue;
                    }
                    else //The second note is processed a very specific way.
                    {
                        if (!firstSlider)
                        {
                            eventTempo.Add(new BasicEventData(time[0], EventType.RIGHT, color, 1));
                            eventTempo.Add(new BasicEventData(0, EventType.RIGHT_ROT, 1, 1));
                            eventTempo.Add(new BasicEventData(time[1], EventType.LEFT, color, 1));
                            eventTempo.Add(new BasicEventData(0, EventType.LEFT_ROT, 1, 1));
                        }
                        time[2] = time[1];
                        time[1] = time[0];
                        continue;
                    }
                }

                if (firstSlider)
                {
                    firstSlider = false;
                    continue;
                }

                // Find the next float
                if (time[0] >= nextfloat)
                {
                    for (int i = Timing.FindIndex(n => n == note); i < Timing.Count - 1; i++)
                    {
                        if(i != 0)
                        {
                            if (Timing[i] == Timing[i - 1])
                            {
                                nextfloat = Timing[i];
                                break;
                            }
                        }
                    }
                }

                // Find the next slider (1/8 minimum)
                if (time[0] >= nextSlider)
                {
                    sliderNoteCount = 0;

                    for (int i = Timing.FindIndex(n => n == note); i < Timing.Count - 1; i++)
                    {
                        if(i != 0)
                        {
                            // Between 1/8 and 0, same cut direction or dots
                            if (Timing[i] - Timing[i - 1] <= 0.125 && Timing[i] - Timing[i - 1] > 0 && (Timing[i] == Timing[i - 1] || Timing[i] == 8))
                            {
                                // Search for the last note of the slider
                                if (sliderNoteCount == 0)
                                {
                                    // This is the first note of the slider
                                    nextSlider = Timing[i - 1];
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

                // Slider time
                if (nextSlider == note)
                {
                    // Take a light between neon, side or backlight and strobes it via On/Flash
                    if (sliderIndex == -1)
                    {
                        int old = sliderLight[sliderIndex + 1];

                        do
                        {
                            sliderLight.Shuffle();
                        } while (sliderLight[2] == old);

                        sliderIndex = 2;
                    }

                    // Place light
                    if (AllowFade)
                    {
                        eventTempo.Add(new BasicEventData(time[0], sliderLight[sliderIndex], color - 2, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.125f, sliderLight[sliderIndex], color - 1, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.25f, sliderLight[sliderIndex], color - 2, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.375f, sliderLight[sliderIndex], color - 1, 1));
                    }
                    else
                    {
                        eventTempo.Add(new BasicEventData(time[0], sliderLight[sliderIndex], color, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.125f, sliderLight[sliderIndex], color + 1, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.25f, sliderLight[sliderIndex], color, 1));
                        eventTempo.Add(new BasicEventData(time[0] + 0.375f, sliderLight[sliderIndex], color + 1, 1));
                    }
                    eventTempo.Add(new BasicEventData(time[0] + 0.5f, sliderLight[sliderIndex], 0, 1));

                    sliderIndex--;

                    // Spin goes brrr
                    if (AllowSpinZoom)
                    {
                        eventTempo.Add(new BasicEventData(time[0], EventType.SPIN, 0, 1));
                        for (int i = 0; i < 8; i++)
                        {
                            eventTempo.Add(new BasicEventData(time[0] + 0.5f - (0.5f / 8f * i), EventType.SPIN, 0, 1));
                        }
                    }

                    wasSlider = true;
                }
                // Not a float
                else if (time[0] != nextfloat)
                {
                    if (time[0] - time[1] >= lastSpeed + 0.02 || time[0] - time[1] <= lastSpeed - 0.02 || patternCount == 20) // New speed or 20 notes of the same pattern
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

                    if (pattern[patternIndex] == 2)
                    {
                        eventTempo.Add(new BasicEventData(time[0], EventType.LEFT, currentSpeed, 1));
                    }
                    else if (pattern[patternIndex] == 3)
                    {
                        eventTempo.Add(new BasicEventData(time[0], EventType.RIGHT, currentSpeed, 1));
                    }

                    // Place off event
                    if (Timing[^1] != note)
                    {
                        if (Timing[Timing.FindIndex(n => n == note) + 1] == nextfloat)
                        {
                            if (Timing[Timing.FindIndex(n => n == note) + 1] - time[0] <= 2)
                            {
                                float value = (Timing[Timing.FindIndex(n => n == note) + 1] - Timing[Timing.FindIndex(n => n == note)]) / 2;
                                eventTempo.Add(new BasicEventData(Timing[Timing.FindIndex(n => n == note)] + value, pattern[patternIndex], 0, 1));
                            }
                        }
                        else
                        {
                            eventTempo.Add(new BasicEventData(Timing[Timing.FindIndex(n => n == note) + 1], pattern[patternIndex], 0, 1));
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
                else if (time[0] - time[1] < 0.25) //Lower than fourth
                {
                    if (time[0] != last && time[0] != time[1] && note != 8 && note != lastCut && AllowSpinZoom && !NerfStrobes) //Spin
                    {
                        last = time[0];
                        eventTempo.Add(new BasicEventData(time[0], EventType.SPIN, 0, 1));
                        for (int i = 0; i < 8; i++)
                        {
                            eventTempo.Add(new BasicEventData(time[0] - ((time[0] - time[1]) / 8 * i), EventType.SPIN, 0, 1));
                        }
                    }

                    if (time[0] == time[1])
                    {
                        CreateGenericLight(currentSpeed);
                    }
                    else
                    {
                        CreateGenericLight(currentSpeed = 7);
                    }
                }
                else if (time[0] - time[1] >= 0.25 && time[0] - time[1] < 0.5) //Quarter to half
                {
                    CreateGenericLight(currentSpeed = 5);
                }
                else if (time[0] - time[1] >= 0.5 && time[0] - time[1] < 1) //Half to 1
                {

                    CreateGenericLight(currentSpeed = 3);
                }
                else if (time[0] - time[1] >= 1) //1 and above
                {
                    CreateGenericLight(currentSpeed = 1);
                }

                lastCut = note; //For the spin check.

                for (int i = 3; i > 0; i--) //Keep the timing of up to three notes before.
                {
                    time[i] = time[i - 1];
                }
            }

            // Sort lights
            eventTempo = eventTempo.OrderBy(o => o.beat).ToList();

            // Remove fused
            for (int i = 1; i < eventTempo.Count - 1; i++)
            {
                // Very close to eachother
                if (eventTempo.Any(e => e.beat == eventTempo[i].beat && e.eventType == eventTempo[i].eventType && e != eventTempo[i]))
                {
                    // Off event
                    if (eventTempo[i].value == 0 || eventTempo[i].value == 4)
                    {
                        eventTempo.Remove(eventTempo[i]);
                        i--;
                    }
                }
            }

            return (boostEvent, eventTempo);
        }
    }
}
