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
        // Var for the onset detection, lower = include more noise. Default: 1.5f
        public const float ONSET_SENSITIVITY = 1.3f;
        // Onset must be above that number to add a double note
        public const float DOUBLE_THRESHOLD = 0.2f;
        // Maximum speed for a note placement (in beat)
        public const double LIMITER = (1d / 8d);
        // Maximum speed for a double note placement (in beat)
        public const double DOUBLE_LIMITER = (1d / 4d);

        static public AudioAnalysis audioAnalysis = new();

        static public (List<ColorNote>, List<BurstSliderData>) GetMap(string audioPath, float bpm, float indistinguishableRange)
        {
            // New list of notes and chain, will be filled via PatternCreator.cs
            // notes is filled with fake notes with only beat from onset
            List<ColorNote> notes = new();
            List<BurstSliderData> chains = new();

            try
            {
                // 0 = Average between 0 and Max. 1 = Average between Min and Max
                // indistinguishableRange = Modify the number of set of samples to ignore after an onset
                AnalyseSong(audioPath, 1, indistinguishableRange);

                // List with all the onsets (even the one that we don't want)
                List<float> onsets = audioAnalysis.GetOnsets().ToList();

                int number = 0;
                double lastBeat = -1;
                double lastOnset = -1;


                bool wasDouble = false;

                // Convert SampleSize and SampleRate into time
                double milisec = audioAnalysis.GetTimePerSample();

                foreach (float onset in onsets)
                {
                    number++; 
                    // If it match, get timing and create a new note with it
                    if (onset >= 0.01)
                    {
                        double time = number * milisec; // Get the time for the current onset
                        double beat = (time * bpm / 60) - 0.125; // Convert the time into beat

                        if (beat - lastBeat >= LIMITER)
                        {
                            // Add a new timing
                            notes.Add(new((float)(beat), 0, 0, 0, 0, 0));

                            if (onset >= DOUBLE_THRESHOLD)
                            {
                                // Add another timing to create a double note on same beat
                                notes.Add(new((float)(beat), 0, 0, 0, 0, 0));
                                wasDouble = true;
                            }
                            else
                            {
                                // New timing wasn't a double
                                wasDouble = false;
                            }

                            lastBeat = beat;
                            lastOnset = onset;
                        }
                        else
                        {
                            if (onset > lastOnset) // Higher onset take priority
                            {
                                // Modify the timing of the last note
                                notes.Last().beat = (float)beat;
                                if(wasDouble)
                                {
                                    // Modify the timing of the last double note to match the current new timing
                                    notes[notes.Count - 2].beat = (float)beat;
                                }
                                else if(onset >= DOUBLE_THRESHOLD)
                                {
                                    // Add a new timing to create a double since last timing wasn't a double
                                    notes.Add(new((float)(beat), 0, 0, 0, 0, 0));
                                    wasDouble = true;
                                }

                                lastBeat = beat;
                                lastOnset = onset;
                            }
                        }
                    }
                }

                // Find double on same beat with note closer than DOUBLE_LIMITER before or after the double and remove a note of the double (to fix burst issue)
                for(int i = 2; i < notes.Count - 1; i++)
                {
                    if(notes[i - 1].beat == notes[i].beat && (notes[i - 1].beat - notes[i - 2].beat <= DOUBLE_LIMITER || notes[i + 1].beat - notes[i].beat <= DOUBLE_LIMITER))
                    {
                        notes.RemoveAt(i);
                        i--;
                    }
                }

                // Method to generate the map pattern
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
