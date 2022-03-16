using Lolighter.Data.Structure;
using Lolighter.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Enumerator;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class Loloppe
    {
        static public List<ColorNote> LoloppeGen(List<ColorNote> noteTemp)
        {
            // List to keep the new notes generated
            List<ColorNote> newNote = new();

            // Current note
            ColorNote n;

            for (int i = 0; i < noteTemp.Count; i++) //For each note -> Big Pepega
            {
                n = noteTemp[i];

                if (n.direction == CutDirection.ANY) //If Any direction, skip
                {
                    continue;
                }

                switch (n.layer) //Process each note using their layer, lane and cut direction manually.
                {
                    case Layer.BOTTOM:
                        switch (n.line)
                        {
                            case Line.LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.MIDDLE:
                        switch (n.line)
                        {
                            case Line.LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        }
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.BOTTOM, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        }
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        }
                                        else
                                        {
                                            newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.BOTTOM, n.direction));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.BOTTOM, n.direction));
                                        break;
                                }
                                break;
                        }
                        break;
                    case Layer.TOP:
                        switch (n.line)
                        {
                            case Line.LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_LEFT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.LEFT, Layer.TOP, n.direction));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.TOP, n.direction));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.DOWN:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.MIDDLE_RIGHT, Layer.TOP, n.direction));
                                        break;
                                    case CutDirection.LEFT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                    case CutDirection.RIGHT:
                                        newNote.Add(new ColorNote(n.beat, n.color, Line.RIGHT, Layer.MIDDLE, n.direction));
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            newNote = newNote.OrderBy(o => o.beat).ToList();

            //Here we remove notes that ended up stacked because of the silly algorithm.
            for (int i = newNote.Count - 1; i > -1; i--) // Reverse-order
            {
                foreach (var note in noteTemp)
                {
                    // Same line, same layer, between 0 and 1/8 of a beat (excluded)
                    if (newNote[i].beat - note.beat < 0.125 && newNote[i].beat - note.beat >= 0 && newNote[i].layer == note.layer && newNote[i].line == note.line)
                    {
                        newNote.Remove(newNote[i]);
                        break;
                    }
                }
            }

            newNote.AddRange(noteTemp);

            return newNote.OrderBy(o => o.beat).ToList();
        }
    }
}
