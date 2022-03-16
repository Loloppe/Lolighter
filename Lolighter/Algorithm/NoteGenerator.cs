using Lolighter.Data.Structure;
using Lolighter.Info;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Enumerator;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    internal class NoteGenerator
    {
        /// <summary>
        /// Method to generate new ColorNote and BurstSliderData from timings (in beat).
        /// This will create a new Beat Saber map from scratch (minus the timings).
        /// </summary>
        /// <param name="timings">Time (in beat) to generate the note</param>
        /// <param name="bpm">Main BPM of the song</param>
        /// <param name="limiter">Allow backhanded when off</param>
        /// <returns>Notes and Chains (for now)</returns>
        static public (List<ColorNote>, List<BurstSliderData>) AutoMapper(List<float> timings, float bpm, bool limiter)
        {
            // Our main list where we will store the generated Notes and Chains.
            List<ColorNote> notes = new();
            List<BurstSliderData> chains = new();

            // Keep the player wrist rotation via direction.
            // LEFT SIDE
            // Upper limit (tech): 0 (Up), lower limit (tech): 2 (Left)
            // Upper limit: 3 (Right), lower limit: 6 (Down-Left)
            // RIGHT SIDE (vertical mirror)
            // Upper limit (tech): 0 (Up), lower limit (tech): 3 (Right)
            // Upper limit: 2 (Left), lower limit: 7 (Down-Right)
            int leftDirection = 1;
            int rightDirection = 1;
            // The last swing. Upswing = 0, Downswing = 1
            int leftSwing = 1;
            int rightSwing = 1;

            // To know which hand (color). 0 is red, 1 is blue
            int hand = 1;

            // The current direction being selected for the next note.
            int direction = -1;

            // The expected speed, used to choose between tech or normal type of flow (in beat). 1+ beat = extreme, 0.5 - 1 beat = tech, 0.5- = normal.
            // Based 
            float speed;
            float lastLeft = 0;
            float lastRight = 0;

            // Selected line and layer
            int line = 0;
            int layer = 0;

            // Select all directions
            foreach (float timing in timings)
            {
                // For simplicity sake, the first two notes will start with specific value.
                // First note will be a blue down and second note will be a red down, in the bottom middle.
                if(notes.Count == 0)
                {
                    ColorNote n = new(timing, 1, 2, 0, 1);
                    notes.Add(n);
                    lastRight = timing;
                    continue;
                }
                else if(notes.Count == 1)
                {
                    ColorNote n = new(timing, 0, 1, 0, 1);
                    notes.Add(n);
                    lastLeft = timing;
                    continue;
                }

                // Direction are separated for each hand and each timing in step of 2.
                if (hand == 0) // Red
                {
                    // Get the current expected speed
                    speed = timing - lastLeft;
                    // If the BPM is above 250, we want to start restricting the speed
                    if(bpm >= 250)
                    {
                        speed = 250 / bpm * speed;
                    }

                    direction = NextDirection(leftDirection, leftSwing, hand, speed, limiter);

                    // We track the data for the next note
                    if(leftSwing == 0)
                    {
                        leftSwing = 1;
                    }
                    else if(leftSwing == 1)
                    {
                        leftSwing = 0;
                    }
                    leftDirection = direction;
                    lastLeft = timing;
                }
                else if(hand == 1) // Blue
                {
                    // Get the current expected speed
                    speed = timing - lastRight;
                    // If the BPM is above 250, we want to start restricting the speed
                    if (bpm >= 250)
                    {
                        speed = 250 / bpm * speed;
                    }

                    direction = NextDirection(rightDirection, rightSwing, hand, speed, limiter);

                    // We track the data for the next note
                    if (rightSwing == 0)
                    {
                        rightSwing = 1;
                    }
                    else if (rightSwing == 1)
                    {
                        rightSwing = 0;
                    }
                    rightDirection = direction;
                    lastRight = timing;
                }

                // Create the note and add it to the list
                if(hand == 1)
                {
                    ColorNote note = new(timing, hand, 2, 0, direction);
                    notes.Add(note);
                    hand = 0; // Switch hand for the next note
                }
                else
                {
                    ColorNote note = new(timing, hand, 1, 0, direction);
                    notes.Add(note);
                    hand = 1; // Switch hand for the next note
                }
            }

            // Select all lines and layers (should probably be done together)
            for(int i = 2; i < notes.Count; i++)
            {
                if (notes[i].color == 0)
                {
                    (line, layer) = PlacementCheck(notes[i].direction, 0, notes[i - 1], notes[i - 2]);
                }
                else if(notes[i].color == 1)
                {
                    (line, layer) = PlacementCheck(notes[i].direction, 1, notes[i - 1], notes[i - 2]);
                }

                notes[i].line = line;
                notes[i].layer = layer;

                if (notes[i].beat - notes[i - 1].beat >= -0.02 && notes[i].beat - notes[i - 1].beat <= 0.02)
                {
                    if(notes[i].color == 0)
                    {
                        (notes[i], notes[i - 1]) = FixDoublePlacement(notes[i], notes[i - 1]);
                    }
                    else if(notes[i].color == 1)
                    {
                        (notes[i - 1], notes[i]) = FixDoublePlacement(notes[i - 1], notes[i]);
                    }
                }
            }

            // We're done
            return (notes, chains);
        }
    }
}
