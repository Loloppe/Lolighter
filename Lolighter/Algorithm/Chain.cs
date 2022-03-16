using Lolighter.Data.Structure;
using Lolighter.Info;
using System.Collections.Generic;
using static Lolighter.Info.Enumerator;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class Chain
    {
        static public (List<BurstSliderData>, List<ColorNote>) Chains(List<ColorNote> noteTemp)
        {
            // List of newly created Chain
            List<BurstSliderData> burstSliders = new();

            // List to separate each note per type
            List<ColorNote> red = new();
            List<ColorNote> blue = new();

            // Size of the chain
            int size = 4;

            foreach (ColorNote note in noteTemp)
            {
                if (note.color == ColorType.BLUE)
                {
                    blue.Add(note);
                }
                else if (note.color == ColorType.RED)
                {
                    red.Add(note);
                }
            }

            // List of list to keep thing like sliders/stack/window/tower etc
            List<List<ColorNote>> patterns = new();
            List<List<ColorNote>> tempList = new();

            // Find pattern here
            (patterns, blue) = FindPattern(blue);
            (tempList, red) = FindPattern(red);
            patterns.AddRange(tempList);

            // Add back the notes together
            List<ColorNote> temp = new(blue);
            temp.AddRange(red);

            // Create chain for the remaining notes
            foreach (ColorNote now in temp)
            {
                size = Utils.RandNumber(4, 9);

                switch (now.layer) //Process the note into a sliders depending on layer, lane and cut direction manually. This is pretty Pepega.
                {
                    case Layer.BOTTOM:
                        switch (now.line)
                        {
                            case Line.LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 2, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 1, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 0, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 2, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 0, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 1, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 0, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 0, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 2, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 0, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 1, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 0, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 2, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 1, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.MIDDLE:
                        switch (now.line)
                        {
                            case Line.LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 0, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 0, size, 0.8f));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.TOP:
                        switch (now.line)
                        {
                            case Line.LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 0, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 1, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 0, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 2, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 1, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 0, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 0, 2, size, 0.8f));
                                        break;
                                    case CutDirection.RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 1, size, 0.8f));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 0, size, 0.8f));
                                        break;
                                    case CutDirection.LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 1, 2, size, 0.8f));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 2, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 2, 1, size, 0.8f));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        burstSliders.Add(new(now, now.beat + 0.125f, 3, 1, size, 0.8f));
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            // Turn all sliders/stack/window/tower into chain
            for (int i = 0; i < patterns.Count; i++)
            {
                List<bool> count = new();
                int head = 0;
                int tail = 0;
                int direction = 8;

                // Find general cut direction of the chain
                foreach (ColorNote note in patterns[i])
                {
                    if(direction == 8 && note.direction == CutDirection.ANY)
                    {
                        if (note.color == ColorType.BLUE)
                        {
                            int index = blue.FindIndex(x => x == note);
                            if(index > 0)
                            {
                                if (blue[index - 1].direction != CutDirection.ANY)
                                {
                                    direction = blue[index - 1].direction;
                                }
                            }
                        }
                        else
                        {
                            if (note.color == ColorType.RED)
                            {
                                int index = blue.FindIndex(x => x == note);
                                if(index > 0)
                                {
                                    if (red[index - 1].direction != CutDirection.ANY)
                                    {
                                        direction = red[index - 1].direction;
                                    }
                                }
                            }
                        }
                    }
                    else if(note.direction != CutDirection.ANY)
                    {
                        direction = note.direction;   
                    }
                }

                // Logic to find the Head and Tail, up to three notes
                for (int j = 0; j < patterns[i].Count - 1; j++)
                {
                    count.Add(PossibleHead(patterns[i][j], patterns[i][j + 1], direction));
                    if(j == patterns[i].Count - 1)
                    {
                        count.Add(PossibleHead(patterns[i][j + 1], patterns[i][j + 2], direction));
                    }
                }

                // Apply the proper note as Head and Tail of the chain
                if(count.Count >= 3)
                {
                    if (count[0] == true && count[1] == true && count[2] == false)
                    {
                        head = 0;
                        tail = 2;
                    }
                    else if (count[0] == true && count[1] == false && count[2] == false)
                    {
                        head = 0;
                        tail = 1;
                    }
                    else if (count[0] == true && count[1] == false && count[2] == true)
                    {
                        head = 2;
                        tail = 1;
                    }
                    else if (count[0] == false && count[1] == true && count[2] == true)
                    {
                        head = 1;
                        tail = 0;
                    }
                    else if (count[0] == false && count[1] == true && count[2] == false)
                    {
                        head = 1;
                        tail = 2;
                    }
                    else if (count[0] == false && count[1] == false && count[2] == true)
                    {
                        head = 2;
                        tail = 0;
                    }
                }
                else if(count[0])
                {
                    head = 0;
                    tail = 1;
                }
                else
                {
                    head = 1;
                    tail = 0;
                }

                // Add a note at the Head of the chain (or it will be empty)
                if (patterns[i][head].color == ColorType.BLUE)
                {
                    blue.Add(patterns[i][head]);
                }
                else if (patterns[i][head].color == ColorType.RED)
                {
                    red.Add(patterns[i][head]);
                }

                // Create the chain
                BurstSliderData newSlider = new(patterns[i][head], patterns[i][tail].beat, patterns[i][tail].line, patterns[i][tail].layer, size, 0.8f);
                burstSliders.Add(newSlider);
            }

            // Remake the list of notes (to trim out pattern)
            noteTemp = new(blue);
            noteTemp.AddRange(red);

            return (burstSliders, noteTemp);
        }
    }
}
