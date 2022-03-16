using Lolighter.Data.Structure;
using Lolighter.Info;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Enumerator;

namespace Lolighter.Algorithm
{
    class Bomb
    {
        public static List<BombNote> CreateBomb(List<ColorNote> noteTemp)
        {
            // List of newly generated BombNote
            List<BombNote> newBomb = new();
            // Note currently being worked on
            ColorNote n;

            for (int i = 0; i < noteTemp.Count(); i++) //For each note
            {
                n = noteTemp[i];

                if (n.direction == CutDirection.ANY) //Skip Any cut direction
                {
                    continue;
                }

                switch (n.layer) //Each layer, index and cut direction are handled manually.
                {
                    case Layer.BOTTOM:
                        switch (n.line)
                        {
                            case Line.LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
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
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.BOTTOM));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        }
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
                                        }
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.BOTTOM));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
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
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.RIGHT:
                                        if (Utils.RandNumber(0, 2) == 0)
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
                                        }
                                        else
                                        {
                                            newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.BOTTOM));
                                        }
                                        break;
                                    case CutDirection.UP_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
                                        break;
                                    case CutDirection.UP_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN_LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN_RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.BOTTOM));
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
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_LEFT, Layer.TOP));
                                        break;
                                    case CutDirection.LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.MIDDLE));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_LEFT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.LEFT, Layer.TOP));
                                        break;
                                }
                                break;
                            case Line.MIDDLE_RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.TOP));
                                        break;
                                }
                                break;
                            case Line.RIGHT:
                                switch (n.direction)
                                {
                                    case CutDirection.UP:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        break;
                                    case CutDirection.DOWN:
                                        newBomb.Add(new BombNote(n.beat, Line.MIDDLE_RIGHT, Layer.TOP));
                                        break;
                                    case CutDirection.LEFT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                    case CutDirection.RIGHT:
                                        newBomb.Add(new BombNote(n.beat, Line.RIGHT, Layer.MIDDLE));
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            // Some bomb end up fused with notes
            List<ColorNote> compare = new(noteTemp);

            foreach (var bomb in newBomb)
            {
                compare.Add(new(bomb));
            }

            compare = compare.OrderBy(o => o.beat).ToList();

            //Dumb method to remove bomb that conflict with a note.
            for (int i = compare.Count() - 5; i > 4; i--) 
            {
                if (compare[i].color == 3)
                {
                    if (compare[i].beat - compare[i - 1].beat <= 0.25 && compare[i].layer == compare[i - 1].layer && compare[i].line == compare[i - 1].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i].beat - compare[i - 2].beat <= 0.25 && compare[i].layer == compare[i - 2].layer && compare[i].line == compare[i - 2].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i].beat - compare[i - 3].beat <= 0.25 && compare[i].layer == compare[i - 3].layer && compare[i].line == compare[i - 3].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i].beat - compare[i - 4].beat <= 0.25 && compare[i].layer == compare[i - 4].layer && compare[i].line == compare[i - 4].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i + 1].beat - compare[i].beat <= 0.25 && compare[i].layer == compare[i + 1].layer && compare[i].line == compare[i + 1].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i + 2].beat - compare[i].beat <= 0.25 && compare[i].layer == compare[i + 2].layer && compare[i].line == compare[i + 2].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i + 3].beat - compare[i].beat <= 0.25 && compare[i].layer == compare[i + 3].layer && compare[i].line == compare[i + 3].line)
                    {
                        compare.Remove(compare[i]);
                    }
                    else if (compare[i + 4].beat - compare[i].beat <= 0.25 && compare[i].layer == compare[i + 4].layer && compare[i].line == compare[i + 4].line)
                    {
                        compare.Remove(compare[i]);
                    }
                }
            }

            // Clear and add what's left
            newBomb.Clear();

            foreach (var bomb in compare)
            {
                if(bomb.color == 3)
                {
                    newBomb.Add(new(bomb));
                }
            }

            return newBomb;
        }
    }
}
