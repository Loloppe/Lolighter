using Lolighter.Data.Structure;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class Chain
    {
        static public (List<BurstSliderData>, List<ColorNote>) Chains(List<ColorNote> noteTemp, int size = 4)
        {
            List<BurstSliderData> burstSliders = new();

            List<ColorNote> red = new();
            List<ColorNote> blue = new();

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

            List<List<ColorNote>> others = new();
            List<ColorNote> other = new();
            bool found = false;

            // Find all blue sliders/stack/window/tower
            for (int i = 0; i < blue.Count; i++)
            {
                if (i == blue.Count - 1)
                {
                    if (found)
                    {
                        other.Add(new(blue[i]));
                        blue.RemoveAt(i);
                        others.Add(new(other));
                        found = false;
                    }
                    break;
                }
                ColorNote now = blue[i];
                ColorNote next = blue[i + 1];

                if (next.beat - now.beat >= 0 && next.beat - now.beat < 0.1)
                {
                    if (!found)
                    {
                        other = new();
                        found = true;
                    }
                    other.Add(new(blue[i]));
                    blue.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (found)
                    {
                        other.Add(new(blue[i]));
                        blue.RemoveAt(i);
                        i--;
                        others.Add(new(other));
                    }

                    found = false;
                }
            }

            // find all red sliders/stack/window/tower
            for (int i = 0; i < red.Count; i++)
            {
                if (i == red.Count - 1)
                {
                    if (found)
                    {
                        other.Add(new(red[i]));
                        red.RemoveAt(i);
                        others.Add(new(other));
                    }
                    break;
                }
                ColorNote now = red[i];
                ColorNote next = red[i + 1];

                if (next.beat - now.beat >= 0 && next.beat - now.beat < 0.1)
                {
                    if (!found)
                    {
                        other = new();
                        found = true;
                    }
                    other.Add(new(red[i]));
                    red.RemoveAt(i);
                    i--;
                }
                else
                {
                    if (found)
                    {
                        other.Add(new(red[i]));
                        red.RemoveAt(i);
                        i--;
                        others.Add(new(other));
                    }

                    found = false;
                }
            }

            List<ColorNote> temp = new(blue);
            temp.AddRange(red);

            // Create chain for the remaining notes
            foreach (ColorNote now in temp)
            {
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
            for (int i = 0; i < others.Count; i++)
            {
                List<bool> count = new();
                int head = 0;
                int tail = 0;
                int direction = 8;

                // Find general cut direction
                foreach (ColorNote note in others[i])
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

                // Logic only handle up to 3 notes
                for (int j = 0; j < others[i].Count - 1; j++)
                {
                    count.Add(PossibleHead(others[i][j], others[i][j + 1], direction));
                    if(j == others[i].Count - 1)
                    {
                        count.Add(PossibleHead(others[i][j + 1], others[i][j + 2], direction));
                    }
                }

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

                if (others[i][head].color == ColorType.BLUE)
                {
                    blue.Add(others[i][head]);
                }
                else if (others[i][head].color == ColorType.RED)
                {
                    red.Add(others[i][head]);
                }

                BurstSliderData newSlider = new(others[i][head], others[i][tail].beat, others[i][tail].line, others[i][tail].layer, size, 0.8f);
                burstSliders.Add(newSlider);
            }

            noteTemp = new(blue);
            noteTemp.AddRange(red);

            return (burstSliders, noteTemp);
        }
    }
}
