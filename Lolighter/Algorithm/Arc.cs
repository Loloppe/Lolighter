using Lolighter.Data.Structure;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    internal class Arc
    {
        static public List<SliderData> CreateArc(List<ColorNote> notes)
        {
            List<SliderData> arc = new();
            List<ColorNote> blue = new();
            List<ColorNote> red = new();

            notes = notes.OrderBy(o => o.beat).ToList();

            foreach (ColorNote note in notes)
            {
                if (note.color == ColorType.RED)
                {
                    red.Add(note);
                }
                else if (note.color == ColorType.BLUE)
                {
                    blue.Add(note);
                }
            }

            for(int i = 0; i < blue.Count - 1; i++)
            {
                if(blue[i + 1].direction == CutDirection.DOWN_LEFT || blue[i + 1].direction == CutDirection.UP_LEFT || blue[i + 1].direction == CutDirection.LEFT)
                {
                    arc.Add(new(blue[i], blue[i + 1], 0.8f, 0.8f, 1));
                }
                else if (blue[i + 1].direction == CutDirection.DOWN_RIGHT || blue[i + 1].direction == CutDirection.UP_RIGHT || blue[i + 1].direction == CutDirection.RIGHT)
                {
                    arc.Add(new(blue[i], blue[i + 1], 0.8f, 0.8f, 2));
                }
                else
                {
                    arc.Add(new(blue[i], blue[i + 1], 0.8f, 0.8f, 0));
                }
            }

            for (int i = 0; i < red.Count - 1; i++)
            {
                if (red[i + 1].direction == CutDirection.DOWN_LEFT || red[i + 1].direction == CutDirection.UP_LEFT || red[i + 1].direction == CutDirection.LEFT)
                {
                    arc.Add(new(red[i], red[i + 1], 0.8f, 0.8f, 1));
                }
                else if (red[i + 1].direction == CutDirection.DOWN_RIGHT || red[i + 1].direction == CutDirection.UP_RIGHT || red[i + 1].direction == CutDirection.RIGHT)
                {
                    arc.Add(new(red[i], red[i + 1], 0.8f, 0.8f, 2));
                }
                else
                {
                    arc.Add(new(red[i], red[i + 1], 0.8f, 0.8f, 0));
                }
            }

            arc = arc.OrderBy(o => o.beat).ToList();

            return arc;
        }
    }
}
