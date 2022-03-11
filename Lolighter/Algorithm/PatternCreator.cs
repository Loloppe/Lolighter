using Lolighter.Data.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Lolighter.Info;
using System.Text.Json;
using static Lolighter.Info.Helper;

namespace Lolighter.Algorithm
{
    class PatternCreator
    {
        public static (List<ColorNote>, List<BurstSliderData>) Create(List<ColorNote> notes, string pattern = "RandomStream", bool BaseDouble = false, bool AllowOneHanded = false, bool DoubleHitboxFix = true)
        {
            double SlowSpeed = 0.4;
            double ParitySpeed = 10;
            string PatternToUse = "Pack";

            // Last direction
            int leftHand = 0;
            int rightHand = 0;
            bool foundBlue = false;
            bool foundRed = false;
            bool modified = false;

            List<_Pattern> patterns = new List<_Pattern>();
            List<_Pattern> slowPatterns = new List<_Pattern>();
            string defaultPack = "{\"_pattern\":[{\"name\":\"Wave\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Spiral\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"On-Top\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":4}]},{\"name\":\"Woosh\",\"_notes\":[{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":4},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Middle Stream\",\"_notes\":[{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Ex-Diagonal\",\"_notes\":[{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":4}]},{\"name\":\"Diagonal\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":4}]},{\"name\":\"Right Stream\",\"_notes\":[{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Left Stream\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]}]}";
            string techPack = "{\"_pattern\":[{\"name\":\"I dunno\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":3},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":4},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"I dunno 2\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":2},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Diagonal\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Diagonal 2\",\"_notes\":[{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]},{\"name\":\"Some Inverted\",\"_notes\":[{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":6},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":7},{\"_time\":0,\"_lineIndex\":3,\"_lineLayer\":1,\"_type\":1,\"_cutDirection\":5},{\"_time\":0,\"_lineIndex\":0,\"_lineLayer\":1,\"_type\":0,\"_cutDirection\":4}]},{\"name\":\"Generic\",\"_notes\":[{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":0,\"_type\":1,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":0,\"_type\":0,\"_cutDirection\":1},{\"_time\":0,\"_lineIndex\":2,\"_lineLayer\":2,\"_type\":1,\"_cutDirection\":0},{\"_time\":0,\"_lineIndex\":1,\"_lineLayer\":2,\"_type\":0,\"_cutDirection\":0}]}]}";


            // Nb of attempt to get a pattern that fit, if > 100000, forcefully stop the process.
            Pack pack = JsonSerializer.Deserialize<Pack>(techPack);
            slowPatterns.AddRange(pack._pattern);

            pack = JsonSerializer.Deserialize<Pack>(defaultPack);
            patterns.AddRange(pack._pattern);

            int attempt = 0;
            double first = notes[0].beat;
            double second = 0;
            string lastPattern = "";
            string newPattern = "";
            bool parityPass = false;
            int rand = 0;
            if (notes[1].beat != notes[0].beat)
            {
                second = notes[1].beat;
            }
            // Current slow section
            bool slow = false;
            // To know the placement order of notes.
            List<int> noteOrder = new List<int>();
            // Note to be used from pattern
            Queue<ColorNote> blueNote = new Queue<ColorNote>();
            Queue<ColorNote> redNote = new Queue<ColorNote>();
            // To convert to queue
            List<ColorNote> patternList = new List<ColorNote>();
            // ColorNote-Last direction
            int leftHand2 = 1;
            int rightHand2 = 1;
            // Last note used
            ColorNote preceding = new(0, 0, 0, 0, 0);

            ColorNote n;

            // Need to remove sliders/stacks/windows/towers/etc.
            List<BurstSliderData> burstSliders = new();

            List<ColorNote> red = new();
            List<ColorNote> blue = new();

            foreach (ColorNote note in notes)
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
                        i--;
                        others.Add(new(other));
                    }

                    found = false;
                }
            }

            notes = new(blue);
            notes.AddRange(red);

            notes = notes.OrderBy(x => x.beat).ToList();

            //For each notes
            for (int i = 0; i < notes.Count() - 2; i++)
            {
                n = notes[i];

                if (BaseDouble) // Forcefully put Double and skip if BaseDouble is true
                {
                    if (notes[i + 1].beat - notes[i].beat <= 0.01 && notes[i + 1].beat - notes[i].beat >= -0.01) // This will be a double
                    {
                        List<ColorNote> l = FindDouble(leftHand, rightHand); // Find the right double
                        l[0].beat = notes[i].beat;
                        l[1].beat = notes[i + 1].beat;
                        // Apply the double
                        notes[i] = new ColorNote(l[0]);
                        notes[i + 1] = new ColorNote(l[1]);
                        // Skip notes
                        leftHand2 = leftHand;
                        rightHand2 = rightHand;
                        leftHand = l[0].direction;
                        rightHand = l[1].direction;
                        i++;
                        modified = true;
                        preceding = new ColorNote(notes[i]);
                        continue;
                    }
                }

                // If the pattern is done or the speed got faster than slow (ignore double)
                if (modified || noteOrder.Count == 0 || (noteOrder.Count % 2 == 0 && slow && notes[i + 1].beat - notes[i].beat < SlowSpeed && notes[i + 1].beat - notes[i].beat >= 0.01))
                {
                    // Infinite loop until a pattern that fit the condition is met.
                    do
                    {
                        // Need to find a new pattern.
                        foundBlue = false;
                        foundRed = false;
                        slow = false;
                        modified = false;
                        attempt++;
                        // Clear/create a new loop.
                        blueNote.Clear();
                        redNote.Clear();
                        noteOrder.Clear();

                        if (attempt >= 100000)
                        {
                            MessageBox.Show("Can't find a proper pattern, closing.");
                            Process.GetCurrentProcess().Kill();
                        }

                        // Slow speed
                        if (notes[i + 1].beat - notes[i].beat >= SlowSpeed || (notes[i + 1].beat - notes[i].beat > -0.01 && notes[i + 1].beat - notes[i].beat <= 0.01 && notes[i + 2].beat - notes[i + 1].beat >= SlowSpeed))
                        {
                            if (PatternToUse == "Pack")
                            {
                                do
                                {
                                    rand = RandNumber(0, slowPatterns.Count);
                                    newPattern = slowPatterns.ElementAt(rand).name;
                                } while (lastPattern == newPattern);
                                foreach (var no in slowPatterns.ElementAt(rand)._notes)
                                {
                                    ColorNote not = new ColorNote(no);
                                    if (not.color == 0)
                                    {
                                        noteOrder.Add(0);
                                        redNote.Enqueue(not);
                                    }
                                    else if (not.color == 1)
                                    {
                                        noteOrder.Add(1);
                                        blueNote.Enqueue(not);
                                    }
                                }
                            }
                            else
                            {
                                patternList = Pattern.GetNewPattern(pattern, 999);
                                foreach (var no in patternList)
                                {
                                    ColorNote not = new ColorNote(no);
                                    if (not.color == 0)
                                    {
                                        noteOrder.Add(0);
                                        redNote.Enqueue(not);
                                    }
                                    else if (not.color == 1)
                                    {
                                        noteOrder.Add(1);
                                        blueNote.Enqueue(not);
                                    }
                                }
                            }
                            slow = true;
                        }
                        else // Fast speed
                        {
                            if (PatternToUse == "Pack")
                            {
                                do
                                {
                                    rand = RandNumber(0, patterns.Count);
                                    newPattern = patterns.ElementAt(rand).name;
                                } while (lastPattern == newPattern);
                                foreach (var no in patterns.ElementAt(rand)._notes)
                                {
                                    ColorNote not = new ColorNote(no);
                                    if (not.color == 0)
                                    {
                                        noteOrder.Add(0);
                                        redNote.Enqueue(not);
                                    }
                                    else if (not.color == 1)
                                    {
                                        noteOrder.Add(1);
                                        blueNote.Enqueue(not);
                                    }
                                }
                            }
                            else
                            {
                                patternList = Pattern.GetNewPattern(pattern, 999);
                                foreach (var no in patternList)
                                {
                                    ColorNote not = new ColorNote(no);
                                    if (not.color == 0)
                                    {
                                        noteOrder.Add(0);
                                        redNote.Enqueue(not);
                                    }
                                    else if (not.color == 1)
                                    {
                                        noteOrder.Add(1);
                                        blueNote.Enqueue(not);
                                    }
                                }
                            }
                        }

                        // Here we check the flow before parity
                        if (!(notes[i + 1].beat - notes[i].beat < 0.02 && notes[i + 1].beat - notes[i].beat > -0.02))
                        {
                            // We ignore double for this
                            if (ParitySpeed > notes[i + 1].beat - notes[i].beat)
                            {
                                (foundRed, redNote) = FlowCheck(redNote.ToList(), leftHand);
                                (foundBlue, blueNote) = FlowCheck(blueNote.ToList(), rightHand);
                            }
                            else // Skip flow check
                            {
                                parityPass = true;
                                if (blueNote.Any())
                                {
                                    foundBlue = true;
                                }
                                if (redNote.Any())
                                {
                                    foundRed = true;
                                }
                            }
                        }
                        else // It's a double
                        {
                            if (ParitySpeed > notes[i + 2].beat - notes[i + 1].beat)
                            {
                                (foundRed, redNote) = FlowCheck(redNote.ToList(), leftHand);
                                (foundBlue, blueNote) = FlowCheck(blueNote.ToList(), rightHand);
                            }
                            else // Skip flow check
                            {
                                parityPass = true;
                                if (blueNote.Any())
                                {
                                    foundBlue = true;
                                }
                                if (redNote.Any())
                                {
                                    foundRed = true;
                                }
                            }
                        }

                        // DD Up is wack
                        if (parityPass)
                        {
                            if ((blueNote.First().direction == 0 && rightHand == 0) || (blueNote.First().direction == 4 && rightHand == 4) || (blueNote.First().direction == 5 && rightHand == 5) || (redNote.First().direction == 0 && leftHand == 0) || (redNote.First().direction == 4 && leftHand == 4) || (redNote.First().direction == 5 && leftHand == 5))
                            {
                                foundBlue = false;
                                foundRed = false;
                            }
                            else
                            {
                                parityPass = false;
                            }
                        }

                        // We only check parity if it's faster than X beat (for lower diff)
                        if (foundRed && foundBlue)
                        {
                            // We ignore double for this
                            if (!(notes[i + 1].beat - notes[i].beat < 0.02 && notes[i + 1].beat - notes[i].beat > -0.02))
                            {
                                if (ParitySpeed > notes[i + 1].beat - notes[i].beat)
                                {
                                    if (!ParityCheck(redNote.Peek().color, redNote.Peek().direction, leftHand, leftHand2))
                                    {
                                        foundRed = false;
                                    }
                                    if (!ParityCheck(blueNote.Peek().color, blueNote.Peek().direction, rightHand, rightHand2))
                                    {
                                        foundBlue = false;
                                    }
                                }
                            }
                            else // It's a double
                            {
                                if (ParitySpeed > notes[i + 2].beat - notes[i + 1].beat)
                                {
                                    if (!ParityCheck(redNote.Peek().color, redNote.Peek().direction, leftHand, leftHand2))
                                    {
                                        foundRed = false;
                                    }
                                    if (!ParityCheck(blueNote.Peek().color, blueNote.Peek().direction, rightHand, rightHand2))
                                    {
                                        foundBlue = false;
                                    }
                                }
                            }
                        }


                        // For patterns with only one color
                        if (((foundRed) || (foundBlue)) && AllowOneHanded)
                        {
                            break;
                        }
                    } while (!foundBlue || !foundRed);
                }

                if (i != 0) // Check for double issue
                {
                    if (notes[i].beat - notes[i - 1].beat <= 0.01 && notes[i].beat - notes[i - 1].beat >= -0.01) // This will be a double
                    {
                        if (notes[i - 1].color == 0 && noteOrder.First() == 0) // Both are going to be red, we don't want that.
                        {
                            if (noteOrder.Any(x => x == 1)) // There's blue left in the list
                            {
                                noteOrder.Remove(1);
                                noteOrder.Insert(0, 1);
                            }
                            else // No more blue, we need to get a new pattern.
                            {
                                noteOrder.Clear();
                                i--;
                                continue;
                            }
                        }
                        else if (notes[i - 1].color == 1 && noteOrder.First() == 1) // Both are going to be blue, we don't want that.
                        {
                            if (noteOrder.Any(x => x == 0)) // There's red left in the list
                            {
                                noteOrder.Remove(0);
                                noteOrder.Insert(0, 0);
                            }
                            else // No more red, we need to get a new pattern.
                            {
                                noteOrder.Clear();
                                i--;
                                continue;
                            }
                        }
                    }
                }

                // Get the current note type
                int noteType = noteOrder.First();
                noteOrder.Remove(noteOrder.First());

                // Create the note
                if (noteType == 0)
                {
                    ColorNote note = redNote.Dequeue();
                    n.line = note.line;
                    n.layer = note.layer;
                    n.direction = note.direction;
                    n.color = note.color;
                }
                else if (noteType == 1)
                {
                    ColorNote note = blueNote.Dequeue();
                    n.line = note.line;
                    n.layer = note.layer;
                    n.direction = note.direction;
                    n.color = note.color;
                }

                // Check for fused notes issue and fix hitbox issue
                if (n.beat - preceding.beat >= -0.02 && n.beat - preceding.beat <= 0.02)
                {
                    // Attempt to fix fused notes
                    if (preceding.line == n.line && preceding.layer == n.layer)
                    {
                        if (n.color == 0 && n.line != 0)
                        {
                            n.line--;
                        }
                        else if (n.color == 1 && n.line != 3)
                        {
                            n.line++;
                        }
                        else if (preceding.color == 0 && preceding.line != 0)
                        {
                            preceding.line--;
                        }
                        else if (preceding.color == 1 && preceding.line != 3)
                        {
                            preceding.line++;
                        }
                    }

                    // We only try to fix if the user decide to.
                    if (DoubleHitboxFix && !BaseDouble && i > 1)
                    {

                        // Attempt to fix vision issue
                        if ((n.line == 1 || n.line == 2) && n.layer == 1)
                        {
                            n.layer++;
                        }
                        else if ((preceding.line == 1 || preceding.line == 2) && preceding.layer == 1)
                        {
                            preceding.layer++;
                        }

                        // No pickles top row
                        if (((n.color == 0 && n.line > preceding.line) || (n.color == 1 && n.line < preceding.line)) && n.layer > 0)
                        {
                            int temp = n.line;
                            n.line = preceding.line;
                            preceding.line = temp;
                        }

                        // We lower a note if it fit
                        if (n.layer == 2 && preceding.layer == 2)
                        {
                            if (n.direction == 1 && preceding.direction == 1)
                            {
                                n.layer = 0;
                                preceding.layer = 0;
                            }
                            else if (n.direction == 6 || n.direction == 7)
                            {
                                n.layer = 0;
                            }
                            else if (preceding.direction == 6 || preceding.direction == 7)
                            {
                                preceding.layer = 0;
                            }
                        }
                        // We bring up a note if it fit
                        else if (n.layer == 0 && preceding.layer == 0)
                        {
                            if (n.direction == 0 && preceding.direction == 0)
                            {
                                n.layer = 2;
                                preceding.layer = 2;
                            }
                            if (n.direction == 4 || n.direction == 5)
                            {
                                n.layer = 2;
                            }
                            else if (preceding.direction == 4 || preceding.direction == 5)
                            {
                                preceding.layer = 2;
                            }
                        }
                        // Note side by side
                        if ((n.line == preceding.line - 1 || n.line == preceding.line + 1) && n.layer == preceding.layer && (n.direction > 1 || preceding.direction > 1))
                        {
                            // If the one before wasn't left or right
                            if (notes[i - 2].direction != 2 && notes[i - 2].direction != 3 && notes[i - 3].direction != 2 && notes[i - 3].direction != 3)
                            {
                                if (n.direction == 6 || n.direction == 7) // Modify to down
                                {
                                    n.direction = 1;
                                }
                                else if (n.direction == 4 || n.direction == 5) // Modify to up
                                {
                                    n.direction = 0;
                                }
                                if (preceding.direction == 6 || preceding.direction == 7)
                                {
                                    preceding.direction = 1; // Modify to down
                                    if (preceding.color == 0) // Need to update for parity
                                    {
                                        leftHand = 1;
                                    }
                                    else if (preceding.color == 1)
                                    {
                                        rightHand = 1;
                                    }
                                }
                                else if (preceding.direction == 4 || preceding.direction == 5)
                                {
                                    preceding.direction = 0; // Modify to up
                                    if (preceding.color == 0) // Need to update for parity
                                    {
                                        leftHand = 0;
                                    }
                                    else if (preceding.color == 1)
                                    {
                                        rightHand = 0;
                                    }
                                }
                            }
                            else // We separate them
                            {
                                if (n.line < preceding.line && n.line != 0)
                                {
                                    n.line--;
                                }
                                else if (n.line > preceding.line && n.line != 3)
                                {
                                    n.line++;
                                }
                                else if (preceding.line < n.line && preceding.line != 0)
                                {
                                    preceding.line--;
                                }
                                else if (preceding.line > n.line && preceding.line != 3)
                                {
                                    preceding.line++;
                                }
                            }
                        }
                        // Close together diagonally
                        if (((n.layer == preceding.layer - 1) || (n.layer == preceding.layer + 1)) && ((n.line == preceding.line - 1) || (n.line == preceding.line + 1)))
                        {
                            // Some can be skipped
                            if (n.direction == preceding.direction && ((n.direction == 4 && n.line < 2 && n.layer > 0 && preceding.layer > 0) || (n.direction == 5 && n.line > 1 && n.layer > 0 && preceding.layer > 0) || (n.direction == 6 && n.line < 2 && n.layer < 2 && preceding.layer < 2) || (n.direction == 7 && n.line > 1 && n.layer < 2 && preceding.layer < 2) || n.direction < 4))
                            {
                                // Skip
                            }
                            else
                            {
                                if (n.line == 0 || n.line == 3)
                                {
                                    if (n.direction == 0 || n.direction == 4 || n.direction == 5)
                                    {
                                        n.layer = 2;
                                        preceding.layer = 0;
                                    }
                                    else
                                    {
                                        if (n.direction != 2 && n.direction != 3)
                                        {
                                            n.layer = 0;
                                        }
                                        if (preceding.direction == 0 || preceding.direction == 4 || preceding.direction == 5)
                                        {
                                            preceding.layer = 2;
                                        }
                                        else
                                        {
                                            preceding.layer = 0;
                                        }
                                    }
                                }
                                else if (preceding.line == 0 || preceding.line == 3)
                                {
                                    if (preceding.direction == 0 || preceding.direction == 4 || preceding.direction == 5)
                                    {
                                        preceding.layer = 2;
                                        n.layer = 0;
                                    }
                                    else
                                    {
                                        if (preceding.direction != 2 && preceding.direction != 3)
                                        {
                                            preceding.layer = 0;
                                        }
                                        if (n.direction == 0 || n.direction == 4 || n.direction == 5)
                                        {
                                            n.layer = 2;
                                        }
                                        else
                                        {
                                            n.layer = 0;
                                        }
                                    }
                                }
                            }
                        }
                        // On top of eachother with a down or up.
                        if ((n.direction < 2 || preceding.direction < 2) && n.line == preceding.line)
                        {
                            if (n.color == 0 && n.line != 0)
                            {
                                n.line--;
                            }
                            else if (n.color == 1 && n.line != 3)
                            {
                                n.line++;
                            }
                        }
                        // Better flow with diagonal cross
                        if (n.color == 0 && n.layer == 0 && n.line < 2 && preceding.layer == 2 && preceding.line > 1)
                        {
                            if (n.direction == 7 && preceding.direction == 6)
                            {
                                n.line = 2;
                                preceding.line = 1;
                            }
                        }
                        else if (n.color == 1 && n.layer == 2 && n.line > 1 && preceding.layer == 0 && preceding.line < 2)
                        {
                            if (n.direction == 6 && preceding.direction == 7)
                            {
                                n.line = 1;
                                preceding.line = 2;
                            }
                        }
                    }
                }

                // Down into side doesn't flow, not sure why this is happening, so we fix here.
                if (i > 2)
                {
                    if ((ParitySpeed > notes[i].beat - notes[i - 2].beat && ParitySpeed > notes[i - 1].beat - notes[i - 3].beat))
                    {
                        if ((n.direction == 2 || n.direction == 3) && notes[i - 2].direction == 1)
                        {
                            if (n.direction == 2)
                            {
                                n.direction = 4;
                            }
                            if (n.direction == 3)
                            {
                                n.direction = 5;
                            }
                        }

                        // Make the flow a little smoother.
                        if (n.color == 0 && notes[i - 2].line > n.line && notes[i - 2].line > 1)
                        {
                            if (n.direction == 7)
                            {
                                n.direction = 1;
                            }
                            else if (n.direction == 5)
                            {
                                n.direction = 0;
                            }
                        }
                        else if (n.color == 1 && notes[i - 2].line < n.line && notes[i - 2].line < 2)
                        {
                            if (n.direction == 6)
                            {
                                n.direction = 1;
                            }
                            else if (n.direction == 4)
                            {
                                n.direction = 0;
                            }
                        }
                    }
                }

                // Always start the map on a bottom row down.
                if (n.beat == first || n.beat == second)
                {
                    n.direction = 1;
                    if (n.color == 0)
                    {
                        n.line = 1;
                    }
                    else
                    {
                        n.line = 2;
                    }
                    n.layer = 0;
                }

                // We add the note here
                notes[i] = new ColorNote(n);
                if (i > 0)
                {
                    notes[i - 1] = new ColorNote(preceding);
                }

                // Follow up the flow per hand
                if (n.color == 0)
                {
                    leftHand2 = leftHand;
                    leftHand = n.direction;
                }
                else if (n.color == 1)
                {
                    rightHand2 = rightHand;
                    rightHand = n.direction;
                }

                attempt = 0;
                preceding = new ColorNote(notes[i]);
                lastPattern = newPattern;
            }

            // Set notes by time order.
            notes = notes.OrderBy(note => note.beat).ToList();

            if (notes.Any() && notes.Count > 3) // Fix the last 2 notes
            {
                notes[notes.Count - 1].color = notes[notes.Count - 3].color;
                notes[notes.Count - 2].color = notes[notes.Count - 4].color;
                notes[notes.Count - 1].direction = 8;
                notes[notes.Count - 2].direction = 8;
                notes[notes.Count - 1].layer = 0;
                notes[notes.Count - 2].layer = 0;
                if (notes[notes.Count - 1].color == 0)
                {
                    notes[notes.Count - 1].line = 1;
                    notes[notes.Count - 2].line = 2;
                }
                else
                {
                    notes[notes.Count - 1].line = 2;
                    notes[notes.Count - 2].line = 1;
                }
            }

            // Set notes by time order.
            return (notes = notes.OrderBy(note => note.beat).ToList(), burstSliders);
        }

        #region Utils

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
            for(int i = 0; i < count; i++)
            {
                temp.Enqueue(temp.Dequeue());
            }

            return (found, temp);
        }

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

        static public List<ColorNote> FindDouble(int left, int right)
        {
            List<ColorNote> found = new List<ColorNote>();
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

        public static int RandNumber(int Low, int High)
        {
            Random rndNum = new Random(int.Parse(Guid.NewGuid().ToString().Substring(0, 8), System.Globalization.NumberStyles.HexNumber));

            int rnd = rndNum.Next(Low, High);

            return rnd;
        }

        #endregion
    }
}
