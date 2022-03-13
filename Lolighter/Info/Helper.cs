using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Lolighter.Info
{
    internal static class Helper
    {
        /// <summary>
        /// Method to find Arc between two notes based on their cut direction
        /// </summary>
        /// <param name="notes">List of ColorNote</param>
        /// <returns>List of SliderData</returns>
        static public List<SliderData> FindArc(List<ColorNote> notes)
        {
            List<SliderData> sliderData = new();

            for (int i = 0; i < notes.Count - 1; i++)
            {
                if (notes[i + 1].direction == CutDirection.DOWN_LEFT || notes[i + 1].direction == CutDirection.UP_LEFT || notes[i + 1].direction == CutDirection.LEFT)
                {
                    
                    sliderData.Add(new(notes[i], notes[i + 1], 0.8f, 0.8f, SliderMidAnchorMode.CLOCKWISE));
                }
                else if (notes[i + 1].direction == CutDirection.DOWN_RIGHT || notes[i + 1].direction == CutDirection.UP_RIGHT || notes[i + 1].direction == CutDirection.RIGHT)
                {
                    sliderData.Add(new(notes[i], notes[i + 1], 0.8f, 0.8f, SliderMidAnchorMode.COUNTER_CLOCKWISE));
                }
                else
                {
                    sliderData.Add(new(notes[i], notes[i + 1], 0.8f, 0.8f, SliderMidAnchorMode.STRAIGHT));
                }
            }

            return sliderData;
        }

        /// <summary>
        /// Method to find specific pattern and remove the notes of the pattern from the main list of note
        /// </summary>
        /// <param name="notes">List of ColorNote</param>
        /// <returns>List of list of ColorNote (Pattern) and modified List of ColorNote</returns>
        static public (List<List<ColorNote>>, List<ColorNote>) FindPattern(List<ColorNote> notes)
        {
            // List of list to keep thing like sliders/stack/window/tower etc
            List<List<ColorNote>> patterns = new();

            // Stock pattern notes
            List<ColorNote> pattern = new();

            // To know if a pattern was found
            bool found = false;

            // Find all notes sliders/stack/window/tower
            for (int i = 0; i < notes.Count; i++)
            {
                if (i == notes.Count - 1)
                {
                    if (found)
                    {
                        pattern.Add(new(notes[i]));
                        notes.RemoveAt(i);
                        patterns.Add(new(pattern));
                        found = false;
                    }
                    break;
                }

                ColorNote now = notes[i];
                ColorNote next = notes[i + 1];

                if (next.beat - now.beat >= 0 && next.beat - now.beat < 0.1)
                {
                    if (!found)
                    {
                        pattern = new();
                        found = true;
                    }
                    pattern.Add(new(notes[i]));
                    notes.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (found)
                    {
                        pattern.Add(new(notes[i]));
                        notes.RemoveAt(i);
                        i--;
                        patterns.Add(new(pattern));
                    }

                    found = false;
                }
            }

            return (patterns, notes);
        }

        /// <summary>
        /// Method to check if last and next cut direction match
        /// </summary>
        /// <param name="dir">Next cut direction</param>
        /// <param name="cut">Last cut direction</param>
        /// <returns>Boolean</returns>
        static public bool FlowCheck(int dir, int cut)
        {
            List<int> UpCut = new() { 0, 4, 5 };
            List<int> DownCut = new() { 1, 6, 7 };
            List<int> IntoLeft = new() { 3, 5, 7 };
            List<int> IntoRight = new() { 2, 4, 6 };

            bool found = false;
            var hand = cut;

            if (DownCut.Contains(hand) && UpCut.Contains(dir))
            {
                found = true;
            }
            else if (UpCut.Contains(hand) && DownCut.Contains(dir))
            {
                found = true;
            }
            else if (hand == 2 && IntoLeft.Contains(dir))
            {
                found = true;
            }
            else if (hand == 3 && IntoRight.Contains(dir))
            {
                found = true;
            }
            else if (IntoLeft.Contains(hand) && dir == 2)
            {
                found = true;
            }
            else if (IntoRight.Contains(hand) && dir == 3)
            {
                found = true;
            }

            return found;
        }

        /// <summary>
        /// Method to check if last and multiple next cut direction match
        /// </summary>
        /// <param name="note">Possible next cut direction</param>
        /// <param name="cut">Last cut direction</param>
        /// <returns>Boolean and a Queue of ColorNote</returns>
        static public (bool, Queue<ColorNote>) FlowCheck(List<ColorNote> note, int cut)
        {
            List<int> UpCut = new() { 0, 4, 5 };
            List<int> DownCut = new() { 1, 6, 7 };
            List<int> IntoLeft = new() { 3, 5, 7 };
            List<int> IntoRight = new() { 2, 4, 6 };

            bool found = false;
            var hand = cut;
            int count = 0;

            for (var i = 0; i < note.Count; i++)
            {
                if (DownCut.Contains(hand) && UpCut.Contains(note[i].direction))
                {
                    count = i;
                    found = true;
                    break;
                }
                else if (UpCut.Contains(hand) && DownCut.Contains(note[i].direction))
                {
                    count = i;
                    found = true;
                    break;
                }
                else if (hand == 2 && IntoLeft.Contains(note[i].direction))
                {
                    count = i;
                    found = true;
                    break;
                }
                else if (hand == 3 && IntoRight.Contains(note[i].direction))
                {
                    count = i;
                    found = true;
                    break;
                }
                else if (IntoLeft.Contains(hand) && note[i].direction == 2)
                {
                    count = i;
                    found = true;
                    break;
                }
                else if (IntoRight.Contains(hand) && note[i].direction == 3)
                {
                    count = i;
                    found = true;
                    break;
                }
            }

            Queue<ColorNote> temp = new(note);
            for (int i = 0; i < count; i++)
            {
                temp.Enqueue(temp.Dequeue());
            }

            return (found, temp);
        }


        /// <summary>
        /// Method with two layer of depth to find out if the parity of the next direction match the last two
        /// </summary>
        /// <param name="type">Color of the note</param>
        /// <param name="now">Next cut direction</param>
        /// <param name="before">Last cut direction</param>
        /// <param name="beforeBefore">Before last cut direction</param>
        /// <returns></returns>
        static public bool ParityCheck(int type, int now, int before, int beforeBefore)
        {
            if (now == 8) // Any
            {
                return true;
            }

            // Also have to handle break in parity, hence the non-flow part.

            switch (beforeBefore)
            {
                case 0: // Up
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    if (type == 0) // Red
                                    {
                                        return true;
                                    }
                                    break;
                                case 5: // Up-Right
                                    if (type == 1) // Blue
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                    }
                    break;
                case 1: // Down
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                    }
                    break;
                case 2: // Left
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                                case 4: // Up-Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 2: // Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 4: // Up-Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                            }
                            break;
                    }
                    break;
                case 3: // Right
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 5: // Up-Right
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 3: // Right
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 5: // Up-Right
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                            }
                            break;
                    }
                    break;
                case 4: // Up-Left
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 4: // Up-Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 2: // Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                    }
                    break;
                case 5: // Up-Right
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 4: // Up-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 4: // Up-Left
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 3: // Right
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                    }
                    break;
                case 6: // Down-Left
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 2: // Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                    }
                    break;
                case 7: // Down-Right
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 7: // Down-Right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                                case 6: // Down-Left
                                    if (type == 1)
                                    {
                                        return true;
                                    }
                                    break;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 3: // right
                                    if (type == 0)
                                    {
                                        return true;
                                    }
                                    break;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                        case 8: // Any
                            switch (now)
                            {
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                    }
                    break;
                case 8: // Any
                    switch (before)
                    {
                        case 0: // Up
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 1: // Down
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 2: // Left
                            switch (now)
                            {
                                case 3: // Right
                                    return true;
                            }
                            break;
                        case 3: // Right
                            switch (now)
                            {
                                case 2: // Left
                                    return true;
                            }
                            break;
                        case 4: // Up-Left
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 7: // Down-Right
                                    return true;
                            }
                            break;
                        case 5: // Up-Right
                            switch (now)
                            {
                                case 1: // Down
                                    return true;
                                case 6: // Down-Left
                                    return true;
                            }
                            break;
                        case 6: // Down-Left
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 5: // Up-Right
                                    return true;
                            }
                            break;
                        case 7: // Down-Right
                            switch (now)
                            {
                                case 0: // Up
                                    return true;
                                case 4: // Up-Left
                                    return true;
                            }
                            break;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Method that return a specific double notes fitting the current context given
        /// </summary>
        /// <param name="left">Last red note</param>
        /// <param name="right">Last blue note</param>
        /// <returns>Two new ColorNote</returns>
        static public List<ColorNote> FindDouble(int left, int right)
        {
            List<ColorNote> found = new();
            ColorNote n;

            switch (left)
            {
                case 0:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 1:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 2:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 2, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 3, 0, 0, 3);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 0, 0, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 7);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 0, 0, 7);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 3:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 0, 0, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 0, 1, 0, 2);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 4:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 5:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 0, 0, 1);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 6:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 7:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 2, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 0, 1, 1, 2);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 1, 2, 0, 0);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
                case 8:
                    switch (right)
                    {
                        case 0:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 1);
                            found.Add(n);
                            break;
                        case 1:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 2:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 3);
                            found.Add(n);
                            break;
                        case 3:
                            n = new ColorNote(0, 0, 1, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 2, 0, 1, 6);
                            found.Add(n);
                            break;
                        case 4:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 1);
                            found.Add(n);
                            break;
                        case 5:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 1, 1, 1);
                            found.Add(n);
                            break;
                        case 6:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 7:
                            n = new ColorNote(0, 1, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 2, 1, 0);
                            found.Add(n);
                            break;
                        case 8:
                            n = new ColorNote(0, 0, 0, 0, 8);
                            found.Add(n);
                            n = new ColorNote(0, 3, 0, 1, 8);
                            found.Add(n);
                            break;
                    }
                    break;
            }

            return found;
        }

        /// <summary>
        /// Method to randomly generate a number between the minimum and maximum (excluded)
        /// If Maximum is Minimum, return Minimum
        /// </summary>
        /// <param name="Low">Minimum</param>
        /// <param name="High">Maximum - 1</param>
        /// <returns></returns>
        public static int RandNumber(int Low, int High)
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            int rnd = rndNum.Next(Low, High);

            return rnd;
        }

        internal static class EnvironmentEvent
        {
            public static List<int> LightEventType = new() { 0, 1, 2, 3, 4 };
            public static List<int> TrackRingEventType = new() { 8, 9 };
            public static List<int> LaserRotationEventType = new() { 12, 13 };
            public static List<int> AllEventType = new() { 0, 1, 2, 3, 4, 8, 9, 12, 13 };
        }

        /// <summary>
        /// Method to swap between Fade and On for EventLightValue
        /// </summary>
        /// <param name="value">Current EventLightValue</param>
        /// <returns>Swapped EventLightValue</returns>
        internal static int Swap(int value)
        {
            switch (value)
            {
                case EventLightValue.BLUE_FADE: return EventLightValue.BLUE_ON;
                case EventLightValue.RED_FADE: return EventLightValue.RED_ON;
                case EventLightValue.BLUE_ON: return EventLightValue.BLUE_FADE;
                case EventLightValue.RED_ON: return EventLightValue.RED_FADE;
                default: return 0;
            }
        }

        /// <summary>
        /// Method to inverse the current EventLightValue between Red and Blue
        /// </summary>
        /// <param name="value">Current EventLightValue</param>
        /// <returns>Inversed EventLightValue</returns>
        internal static int Inverse(int value)
        {
            if (value > EventLightValue.BLUE_FADE)
                return value - 4; //Turn to blue
            else
                return value + 4; //Turn to red
        }

        /// <summary>
        /// Method to randomise the element of a List
        /// </summary>
        /// <typeparam name="T">Object</typeparam>
        /// <param name="list">List</param>
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

        /// <summary>
        /// Method to find the possible Head of a chain
        /// </summary>
        /// <param name="note">Current note</param>
        /// <param name="next">Next note</param>
        /// <param name="direction">Cut direction</param>
        /// <returns>True = Current note win over the next one as head</returns>
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

        internal static class DistributionParamType
        {
            public const int WAVE = 1;
            public const int STEP = 2;
        }

        internal static class RotationDirection
        {
            public const int Automatic = 0;
            public const int Clockwise = 1;
            public const int Counterclockwise = 2;
        }

        internal static class Axis
        {
            public const int X = 0;
            public const int Y = 1;
        }

        internal static class EaseType
        {
            public const int None = -1;
            public const int Linear = 0;
            public const int InQuad = 1;
            public const int OutQuad = 2;
            public const int InOutQuad = 3;
        }

        internal static class TransitionType
        {
            public const int Instant = 0;
            public const int Interpolate = 1;
            public const int Extend = 2;
        }

        internal static class IndexFilterType
        {
            public const int Division = 1;
            public const int StepAndOffset = 2;
        }

        internal static class LaserType
        {
            // Further away from the center
            public const int LEFT_BOTTOM_VERTICAL = 0;
            public const int RIGHT_BOTTOM_VERTICAL = 1;
            public const int LEFT_TOP_VERTICAL = 2;
            public const int RIGHT_TOP_VERTICAL = 3;
            // Same as those above, but close to the center
            public const int LEFT_BOTTOM_CENTER_VERTICAL = 4;
            public const int RIGHT_BOTTOM_CENTER_VERTICAL = 5;
            public const int LEFT_TOP_CENTER_VERTICAL = 6;
            public const int RIGHT_TOP_CENTER_VERTICAL = 7;
            // Two horizontal layer on the left and two on the right
            public const int LEFT_BOTTOM_HORIZONTAL = 8;
            public const int RIGHT_BOTTOM_HORIZONTAL = 9;
            public const int LEFT_TOP_HORIZONTAL = 10;
            public const int RIGHT_TOP_HORIZONTAL = 11;
            // At the very back, point directly toward player
            public const int TOP_CENTER = 12;
            public const int BOTTOM_CENTER = 13;
            public const int LEFT_CENTER = 14;
            public const int RIGHT_CENTER = 15;
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
