using Lolighter.Data.Structure;
using System.Collections.Generic;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class Invert
    {
        static public List<ColorNote> MakeInvert(List<ColorNote> noteTemp, double Limiter, bool IsLimited)
        {
            // Current note
            ColorNote n;

            // Found something that we don't want to invert
            bool found;

            for (int i = noteTemp.Count - 1; i > -1; i--) // Reverse-order
            {
                n = noteTemp[i];

                found = false;

                foreach (ColorNote temp in noteTemp)
                {
                    if (n.beat == temp.beat && n.color == temp.color && n.direction == temp.direction && !IsLimited)
                    {
                        //Loloppe notes
                        break;
                    }
                    if (((n.beat - temp.beat < Limiter && n.beat - temp.beat > 0) || (temp.beat - n.beat < Limiter && temp.beat - n.beat > 0)) && temp.color == n.color)
                    {
                        found = true;
                        break;
                    }
                    else if (temp.beat == n.beat && temp.color == n.color && n != temp)
                    {
                        found = true;
                        break;
                    }
                    else if (temp.beat == n.beat && temp.color != n.color && n != temp && (temp.line == n.line || temp.layer == n.layer))
                    {
                        found = true;
                        break;
                    }
                }

                if (found) //If found, then skip
                {
                    continue;
                }

                switch (n.direction) //Based on the cut direction, change the layer of the note.
                {
                    case CutDirection.UP:
                        n.layer = Layer.BOTTOM;
                        break;
                    case CutDirection.DOWN:
                        n.layer = Layer.TOP;
                        break;
                    case CutDirection.LEFT:
                        n.line = Line.RIGHT;
                        break;
                    case CutDirection.RIGHT:
                        n.line = Line.LEFT;
                        break;
                    case CutDirection.UP_LEFT:
                        n.layer = Layer.BOTTOM;
                        break;
                    case CutDirection.UP_RIGHT:
                        n.layer = Layer.BOTTOM;
                        break;
                    case CutDirection.DOWN_LEFT:
                        n.layer = Layer.TOP;
                        break;
                    case CutDirection.DOWN_RIGHT:
                        n.layer = Layer.TOP;
                        break;
                    case CutDirection.ANY:
                        break;
                }
            }

            return noteTemp;
        }
    }
}
