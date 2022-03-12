using Lolighter.Data.Structure;
using Lolighter.Onset_Detection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Lolighter.Algorithm
{
    internal class Onset
    {
        public const float ONSET_SENSITIVITY = 1f;
        public const float DOUBLE_THRESHOLD = 0.2f;
        public const double LIMITER = (1d / 8d);
        static public readonly List<double> SNAP = new(){ 0.125d, 0.16666666d, 0.25d, 0.33333333d, 0.5d, 1d };

        static public AudioAnalysis audioAnalysis = new();

        static public (List<ColorNote>, List<BurstSliderData>) GetMap(string audioPath, float bpm, float indistinguishableRange)
        {
            List<ColorNote> notes = new();
            List<BurstSliderData> chains = new();

            try
            {
                AnalyseSong(audioPath, 1, indistinguishableRange);

                List<float> onsets = audioAnalysis.GetOnsets().ToList();
                int number = 0;
                double lastBeat = -1;
                double lastOnset = -1;
                bool wasDouble = false;
                double milisec = audioAnalysis.GetTimePerSample() - 0.000015;

                // Get all onsets
                foreach (float onset in onsets)
                {
                    number++; 
                    // If it match, get timing and create a new note with it
                    if (onset >= 0.01)
                    {
                        double time = number * milisec;
                        double beat = ((time * bpm) / 60) - 0.625;

                        double temp = Math.Floor(beat);
                        double nearest = SNAP.MinBy(x => Math.Abs(x - (beat - temp)));
                        beat = temp + nearest;

                        if (beat - lastBeat >= LIMITER)
                        {
                            notes.Add(new((float)(beat), 0, 0, 0, 0, 0));

                            if (onset >= DOUBLE_THRESHOLD)
                            {
                                notes.Add(new((float)(beat), 0, 0, 0, 0, 0));
                                wasDouble = true;
                            }
                            else
                            {
                                wasDouble = false;
                            }

                            lastBeat = beat;
                            lastOnset = onset;
                        }
                        else
                        {
                            if (onset > lastOnset) // Higher take priority
                            {
                                notes.Last().beat = (float)beat;
                                if(wasDouble)
                                {
                                    notes[notes.Count - 2].beat = (float)beat;
                                }
                                else if(onset >= DOUBLE_THRESHOLD)
                                {
                                    notes.Add(new((float)(beat), 0, 0, 0, 0, 0));
                                    wasDouble = true;
                                }

                                lastBeat = beat;
                                lastOnset = onset;
                            }
                        }
                    }
                }

                for(int i = 1; i < notes.Count - 1; i++)
                {
                    if(notes[i - 1].beat == notes[i].beat && notes[i + 1].beat - notes[i].beat <= 0.125)
                    {
                        // Gallops, we remove a note.
                        notes.RemoveAt(i);
                        i--;
                    }
                }

                (notes, chains) = PatternCreator.Create(notes, "RandomStream", false, false, true, true);
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR: Reading music file");
            }
            finally
            {
                audioAnalysis.DisposeAudioAnalysis();
            }

            return (notes, chains);
        }

        static void AnalyseSong(string filePath, int type, float indistinguishableRange = 0.01f)
        {
            // Load the audio file from the given file path
            audioAnalysis.LoadAudioFromFile(filePath);
            // Find the onsets
            audioAnalysis.DetectOnsets(ONSET_SENSITIVITY, indistinguishableRange);
            // Normalize the intensity of the onsets
            audioAnalysis.NormalizeOnsets(type);
        }
    }
}
