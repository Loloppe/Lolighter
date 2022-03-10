using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class DoubleDirectional
    {
        static public (List<ColorNote>, List<BurstSliderData>) Emilia(List<ColorNote> noteTemp, bool IsLimited, bool removeSide)
        {
            List<BurstSliderData> burstSliders = new();

            List<ColorNote> red = new();
            List<ColorNote> blue = new();

            for (int i = 0; i < noteTemp.Count; i++)
            {
                ColorNote note = noteTemp[i];

                switch (note.direction)
                {
                    case CutDirection.UP: if (IsLimited) { noteTemp.Remove(note); i--; } else { note.direction = CutDirection.DOWN; } break;
                    case CutDirection.UP_LEFT: if (IsLimited) { noteTemp.Remove(note); i--; } else { note.direction = CutDirection.DOWN_LEFT; } break;
                    case CutDirection.UP_RIGHT: if (IsLimited) { noteTemp.Remove(note); i--; } else { note.direction = CutDirection.DOWN_RIGHT; } break;
                    case CutDirection.LEFT: if (removeSide) { noteTemp.Remove(note); i--; } else { note.direction = CutDirection.DOWN_LEFT; } break;
                    case CutDirection.RIGHT: if (removeSide) { noteTemp.Remove(note); i--; } else { note.direction = CutDirection.DOWN_RIGHT; } break;
                }
            }

            foreach (ColorNote note in noteTemp)
            {
                if(note.color == ColorType.BLUE)
                {
                    blue.Add(note);
                }
                else if (note.color == ColorType.RED)
                {
                    red.Add(note);
                }
            }

            List<List<ColorNote>> sliders = new();
            List<ColorNote> slider = new();
            bool found = false;

            // Find all blue sliders
            for (int i = 0; i < blue.Count; i++)
            {
                if (i == blue.Count - 1)
                {
                    if (found)
                    {
                        slider.Add(new(blue[i]));
                        blue.RemoveAt(i);
                        sliders.Add(new(slider));
                    }
                    break;
                }
                ColorNote now = blue[i];
                ColorNote next = blue[i + 1];

                if (next.beat - now.beat > 0 && next.beat - now.beat < 0.1)
                {
                    if (!found)
                    {
                        slider = new();
                        found = true;
                    }
                    slider.Add(new(blue[i]));
                    blue.RemoveAt(i);
                    i--;
                }
                else if (next.beat - now.beat == 0 && next.direction == now.direction) // Stack/Window/Tower
                {
                    if (next.line > now.line && next.layer < now.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        int temp = next.layer;
                        next.layer = now.layer;
                        now.layer = temp;
                    }
                    else if (next.line > now.line && next.layer == now.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        if (next.layer != 2)
                        {
                            next.layer++;
                        }
                        else
                        {
                            now.layer--;
                        }
                    }
                    else if (now.line > next.line && now.layer < next.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        int temp = now.layer;
                        now.layer = next.layer;
                        next.layer = temp;
                    }
                    else if (now.line > next.line && now.layer == next.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        if (now.layer != 2)
                        {
                            now.layer++;
                        }
                        else
                        {
                            next.layer--;
                        }
                    }
                    else if (next.line > now.line && next.layer > now.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        int temp = next.layer;
                        next.layer = now.layer;
                        now.layer = temp;
                    }
                    else if (next.line > now.line && next.layer == now.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        if (next.layer != 0)
                        {
                            next.layer--;
                        }
                        else
                        {
                            now.layer++;
                        }
                    }
                    else if (now.line > next.line && now.layer < next.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        int temp = now.layer;
                        now.layer = next.layer;
                        next.layer = temp;
                    }
                    else if (now.line > next.line && now.layer == next.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        if (now.layer != 0)
                        {
                            now.layer--;
                        }
                        else
                        {
                            next.layer++;
                        }
                    }
                }
                else
                {
                    if (found)
                    {
                        slider.Add(new(blue[i]));
                        blue.RemoveAt(i);
                        i--;
                        sliders.Add(new(slider));
                    }

                    found = false;
                }
            }

            // Find all red sliders
            for (int i = 0; i < red.Count; i++)
            {
                if (i == red.Count - 1)
                {
                    if (found)
                    {
                        slider.Add(new(red[i]));
                        red.RemoveAt(i);
                        sliders.Add(new(slider));
                    }
                    break;
                }
                ColorNote now = red[i];
                ColorNote next = red[i + 1];

                if (next.beat - now.beat > 0 && next.beat - now.beat < 0.1)
                {
                    if (!found)
                    {
                        slider = new();
                        found = true;
                    }
                    slider.Add(new(red[i]));
                    red.RemoveAt(i);
                    i--;
                }
                else if(next.beat - now.beat == 0 && next.direction == now.direction) // Stack/Window/Tower
                {
                    if(next.line > now.line && next.layer < now.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        int temp = next.layer;
                        next.layer = now.layer;
                        now.layer = temp;
                    }
                    else if(next.line > now.line && next.layer == now.layer &&  now.direction == CutDirection.DOWN_LEFT)
                    {
                        if(next.layer != 2)
                        {
                            next.layer++;
                        }
                        else
                        {
                            now.layer--;
                        }
                    }
                    else if (now.line > next.line && now.layer < next.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        int temp = now.layer;
                        now.layer = next.layer;
                        next.layer = temp;
                    }
                    else if (now.line > next.line && now.layer == next.layer && now.direction == CutDirection.DOWN_LEFT)
                    {
                        if (now.layer != 2)
                        {
                            now.layer++;
                        }
                        else
                        {
                            next.layer--;
                        }
                    }
                    else if (next.line > now.line && next.layer > now.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        int temp = next.layer;
                        next.layer = now.layer;
                        now.layer = temp;
                    }
                    else if (next.line > now.line && next.layer == now.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        if (next.layer != 0)
                        {
                            next.layer--;
                        }
                        else
                        {
                            now.layer++;
                        }
                    }
                    else if (now.line > next.line && now.layer < next.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        int temp = now.layer;
                        now.layer = next.layer;
                        next.layer = temp;
                    }
                    else if (now.line > next.line && now.layer == next.layer && now.direction == CutDirection.DOWN_RIGHT)
                    {
                        if (now.layer != 0)
                        {
                            now.layer--;
                        }
                        else
                        {
                            next.layer++;
                        }
                    }
                }
                else
                {
                    if (found)
                    {
                        slider.Add(new(red[i]));
                        red.RemoveAt(i);
                        i--;
                        sliders.Add(new(slider));
                    }

                    found = false;
                }
            }

            // Fix sliders
            for (int i = 0; i < sliders.Count; i++)
            {
                if (sliders[i].First().color == ColorType.BLUE)
                {
                    blue.Add(sliders[i].First());
                }
                else if (sliders[i].First().color == ColorType.RED)
                {
                    red.Add(sliders[i].First());
                }

                if(sliders[i].Last().layer > sliders[i].First().layer)
                {
                    int temp = sliders[i].Last().layer;
                    sliders[i].Last().layer = sliders[i].First().layer;
                    sliders[i].First().layer = temp;
                }
                if(sliders[i].Last().line > sliders[i].First().line && sliders[i].First().direction == CutDirection.DOWN_LEFT)
                {
                    int temp = sliders[i].Last().line;
                    sliders[i].Last().line = sliders[i].First().line;
                    sliders[i].First().line = temp;
                }
                if (sliders[i].Last().line < sliders[i].First().line && sliders[i].First().direction == CutDirection.DOWN_RIGHT)
                {
                    int temp = sliders[i].Last().line;
                    sliders[i].Last().line = sliders[i].First().line;
                    sliders[i].First().line = temp;
                }

                int size = 4;
                BurstSliderData newSlider = new(sliders[i].First(), sliders[i].Last().beat, sliders[i].Last().line, sliders[i].Last().layer, size, 0.8f);
                burstSliders.Add(newSlider);
            }

            noteTemp = new(blue);
            noteTemp.AddRange(red);

            noteTemp = noteTemp.OrderBy(x => x.beat).ToList();

            return (noteTemp, burstSliders);
        }
    }
}
