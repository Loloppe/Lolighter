using Lolighter.Data.Structure;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class Sliders
    {
        static public List<ColorNote> MakeSliders(List<ColorNote> noteTemp, double Limiter, bool IsLimited)
        {
            List<ColorNote> newNote = new();
            List<ColorNote> toRemove = new();
            ColorNote now;
            ColorNote lastNote;
            bool wrong = false;

            for (int i = noteTemp.Count - 1; i > 0; i--) // We try to find if the current note is part of a slider or can be slidified.
            {
                now = noteTemp[i];

                if (now.direction == CutDirection.ANY) //If ANY, skip
                {
                    continue;
                }

                wrong = false; //If wrong, skip

                foreach (ColorNote temp in noteTemp) //For each note
                {
                    if (now == temp) continue; // Same note
                    else if (now.beat == temp.beat && now.color == temp.color && now.direction == temp.direction && !IsLimited) break; // Loloppe
                    else if (((now.beat - temp.beat < Limiter * 2 && now.beat - temp.beat > 0) || (temp.beat - now.beat < Limiter * 2 && temp.beat - now.beat > 0)) && temp.color == now.color)
                    {
                        // Already a slider, tower, stack or issue with mapping?
                        wrong = true;
                        break;
                    }
                    else if (temp.beat == now.beat && temp.color == now.color && now != temp)
                    {
                        // Already a slider, tower, stack or issue with mapping?
                        wrong = true;
                        break;
                    }
                    else if (now.beat == temp.beat && now.color != temp.color && now.direction == temp.direction && (now.direction == CutDirection.UP_LEFT || now.direction == CutDirection.UP_RIGHT))
                    {
                        // Diagonal double
                        wrong = true;
                        break;
                    }
                    else if (now.beat == temp.beat && temp.color != now.color && ((temp.line == Line.LEFT && now.line == Line.MIDDLE_LEFT) || (temp.line == Line.RIGHT && now.line == Line.MIDDLE_RIGHT)))
                    {
                        // Collision issue
                        wrong = true;
                        break;
                    }
                }

                if (wrong) //If wrong, then skip
                {
                    continue;
                }

                switch (now.layer) //Process the note into a sliders depending on layer, lane and cut direction manually. This is pretty Pepega.
                {
                    case Layer.BOTTOM:
                        switch (now.line)
                        {
                            case Line.LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        if (now.color == ColorType.RED)
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.TOP))
                                        {
                                            now.layer = Layer.TOP;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_LEFT))
                                        {
                                            now.line = Line.MIDDLE_LEFT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_LEFT))
                                        {
                                            now.line = Line.MIDDLE_LEFT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_LEFT))
                                        {
                                            now.line = Line.MIDDLE_LEFT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.LEFT && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            now.line = Line.LEFT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.RIGHT && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            now.line = Line.RIGHT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        if (now.color == ColorType.RED)
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.TOP))
                                        {
                                            now.layer = Layer.TOP;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_RIGHT))
                                        {
                                            now.line = Line.MIDDLE_RIGHT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_RIGHT))
                                        {
                                            now.line = Line.MIDDLE_RIGHT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_RIGHT))
                                        {
                                            now.line = Line.MIDDLE_RIGHT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
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
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
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
                                    case CutDirection.UP:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.BOTTOM))
                                        {
                                            now.layer = Layer.BOTTOM;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        if (now.color == ColorType.RED)
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_LEFT))
                                        {
                                            now.line = Line.MIDDLE_LEFT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.LEFT && x.layer == Layer.MIDDLE))
                                        {
                                            now.line = Line.LEFT;
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.MIDDLE, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.RIGHT && x.layer == Layer.MIDDLE))
                                        {
                                            now.line = Line.RIGHT;
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (now.direction)
                                {
                                    case CutDirection.UP:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.BOTTOM))
                                        {
                                            now.layer = Layer.BOTTOM;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        if (now.color == ColorType.BLUE)
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(now.beat + 0.0625f, now.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_RIGHT, Layer.TOP, CutDirection.ANY));
                                        break;
                                    case CutDirection.RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.line == Line.MIDDLE_RIGHT))
                                        {
                                            now.line = Line.MIDDLE_RIGHT;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (!noteTemp.Any(x => x != now && x.beat == now.beat && x.layer == Layer.MIDDLE))
                                        {
                                            now.layer = Layer.MIDDLE;
                                            newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.TOP, CutDirection.ANY));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.MIDDLE_LEFT, Layer.BOTTOM, CutDirection.ANY));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(now.beat + 0.03125f, now.color, Line.RIGHT, Layer.MIDDLE, CutDirection.ANY));
                                        break;
                                }
                                break;
                        }
                        break;
                }

                lastNote = now;
            }

            noteTemp.RemoveAll(item => toRemove.Contains(item));
            newNote.AddRange(noteTemp);
            List<ColorNote> sorted = newNote.OrderBy(o => o.beat).ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                if (sorted.Any(x => x != sorted[i] && x.line == sorted[i].line && x.layer == sorted[i].layer && x.beat == sorted[i].beat))
                {
                    sorted.Remove(sorted[i]);
                    i--;
                }
            }

            return sorted;
        }
    }
}
