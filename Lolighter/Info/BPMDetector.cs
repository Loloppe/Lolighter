// https://github.com/TheOsch/naudio-bpm/blob/master/NAudioBPM/NAudioBPM/BPMDetector.cs

using System;
using System.Collections.Generic;
using NAudio.Wave;
using NAudio.Dsp;
using System.IO;
using NAudio.Vorbis;

namespace Lolighter.Info
{
    struct BPMGroup
    {
        public int Count;
        public short Tempo;
    }

    class BPMDetector
    {
        private BPMGroup[] groups;

        public BPMGroup[] Groups
        {
            get
            {
                return groups;
            }
        }

        private int sampleRate;
        private int channels;

        private struct Peak
        {
            public int Position;
            public float Volume;
        }

        private Peak[] getPeaks(float[] data)
        {
            // What we're going to do here, is to divide up our audio into parts.

            // We will then identify, for each part, what the loudest sample is in that
            // part.

            // It's implied that that sample would represent the most likely 'beat'
            // within that part.

            // Each part is 0.5 seconds long

            // This will give us 60 'beats' - we will only take the loudest half of
            // those.

            // This will allow us to ignore breaks, and allow us to address tracks with
            // a BPM below 120.

            int partSize = sampleRate / 2;
            int parts = data.Length / channels / partSize;
            Peak[] peaks = new Peak[parts];

            for (int i = 0; i < parts; ++i)
            {
                Peak max = new Peak
                {
                    Position = -1,
                    Volume = 0.0F
                };
                for (int j = 0; j < partSize; ++j)
                {
                    float vol = 0.0F;
                    for (int k = 0; k < channels; ++k)
                    {
                        float v = data[i * channels * partSize + j * channels + k];
                        if (vol < v)
                        {
                            vol = v;
                        }
                    }
                    if (max.Position == -1 || max.Volume < vol)
                    {
                        max.Position = i * partSize + j;
                        max.Volume = vol;
                    }
                }
                peaks[i] = max;
            }

            // We then sort the peaks according to volume...

            Array.Sort(peaks, (x, y) => y.Volume.CompareTo(x.Volume));

            // ...take the loundest half of those...

            Array.Resize(ref peaks, peaks.Length / 2);

            // ...and re-sort it back based on position.

            Array.Sort(peaks, (x, y) => x.Position.CompareTo(y.Position));

            return peaks;
        }

        private BPMGroup[] getIntervals(Peak[] peaks)
        {
            // What we now do is get all of our peaks, and then measure the distance to
            // other peaks, to create intervals.  Then based on the distance between
            // those peaks (the distance of the intervals) we can calculate the BPM of
            // that particular interval.

            // The interval that is seen the most should have the BPM that corresponds
            // to the track itself.

            List<BPMGroup> groups = new List<BPMGroup>();

            for (int index = 0; index < peaks.Length; ++index)
            {
                Peak peak = peaks[index];
                for (int i = 1; index + i < peaks.Length && i < 10; ++i)
                {
                    float tempo = 60.0F * sampleRate / (peaks[index + i].Position - peak.Position);
                    while (tempo < 90.0F)
                    {
                        tempo *= 2.0F;
                    }
                    while (tempo > 180.0F)
                    {
                        tempo /= 2.0F;
                    }
                    BPMGroup group = new BPMGroup
                    {
                        Count = 1,
                        Tempo = (short)Math.Round(tempo)
                    };
                    int j;
                    for (j = 0; j < groups.Count && groups[j].Tempo != group.Tempo; ++j) { }
                    if (j < groups.Count)
                    {
                        group.Count = groups[j].Count + 1;
                        groups[j] = group;
                    }
                    else
                    {
                        groups.Add(group);
                    }
                }
            }
            return groups.ToArray();
        }

        public BPMDetector(string audioFile, int start = 0, int length = 0)
        {
            // Load the file
            if(Path.GetExtension(audioFile) == ".ogg" || Path.GetExtension(audioFile) == ".egg")
            {
                using (VorbisWaveReader reader = new VorbisWaveReader(audioFile))
                {
                    // Originally the sample rate was constant (44100), and the number of channels was 2. 
                    // Let's just in case take them from file's properties
                    sampleRate = reader.WaveFormat.SampleRate;
                    channels = reader.WaveFormat.Channels;

                    int bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
                    if (bytesPerSample == 0)
                    {
                        bytesPerSample = 2; // assume 16 bit
                    }

                    int sampleCount = (int)reader.Length / bytesPerSample;

                    // Read the wave data

                    start *= channels * sampleRate;
                    length *= channels * sampleRate;
                    if (start >= sampleCount)
                    {
                        groups = new BPMGroup[0];
                        return;
                    }
                    if (length == 0 || start + length >= sampleCount)
                    {
                        length = sampleCount - start;
                    }

                    length = (int)(length / channels) * channels;

                    ISampleProvider sampleReader = reader.ToSampleProvider();
                    float[] samples = new float[length];
                    sampleReader.Read(samples, start, length);

                    // Beats, or kicks, generally occur around the 100 to 150 hz range.
                    // Below this is often the bassline.  So let's focus just on that.

                    for (int ch = 0; ch < channels; ++ch)
                    {
                        // First a lowpass to remove most of the song.

                        BiQuadFilter lowpass = BiQuadFilter.LowPassFilter(sampleRate, 150.0F, 1.0F);

                        // Now a highpass to remove the bassline.

                        BiQuadFilter highpass = BiQuadFilter.HighPassFilter(sampleRate, 100.0F, 1.0F);

                        for (int i = ch; i < length; i += channels)
                        {
                            samples[i] = highpass.Transform(lowpass.Transform(samples[i]));
                        }
                    }

                    Peak[] peaks = getPeaks(samples);

                    BPMGroup[] allGroups = getIntervals(peaks);

                    Array.Sort(allGroups, (x, y) => y.Count.CompareTo(x.Count));

                    if (allGroups.Length > 5)
                    {
                        Array.Resize(ref allGroups, 5);
                    }

                    this.groups = allGroups;
                }
            }
            else
            {
                using (MediaFoundationReader reader = new MediaFoundationReader(audioFile))
                {
                    // Originally the sample rate was constant (44100), and the number of channels was 2. 
                    // Let's just in case take them from file's properties
                    sampleRate = reader.WaveFormat.SampleRate;
                    channels = reader.WaveFormat.Channels;

                    int bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
                    if (bytesPerSample == 0)
                    {
                        bytesPerSample = 2; // assume 16 bit
                    }

                    int sampleCount = (int)reader.Length / bytesPerSample;

                    // Read the wave data

                    start *= channels * sampleRate;
                    length *= channels * sampleRate;
                    if (start >= sampleCount)
                    {
                        groups = new BPMGroup[0];
                        return;
                    }
                    if (length == 0 || start + length >= sampleCount)
                    {
                        length = sampleCount - start;
                    }

                    length = (int)(length / channels) * channels;

                    ISampleProvider sampleReader = reader.ToSampleProvider();
                    float[] samples = new float[length];
                    sampleReader.Read(samples, start, length);

                    // Beats, or kicks, generally occur around the 100 to 150 hz range.
                    // Below this is often the bassline.  So let's focus just on that.

                    for (int ch = 0; ch < channels; ++ch)
                    {
                        // First a lowpass to remove most of the song.

                        BiQuadFilter lowpass = BiQuadFilter.LowPassFilter(sampleRate, 150.0F, 1.0F);

                        // Now a highpass to remove the bassline.

                        BiQuadFilter highpass = BiQuadFilter.HighPassFilter(sampleRate, 100.0F, 1.0F);

                        for (int i = ch; i < length; i += channels)
                        {
                            samples[i] = highpass.Transform(lowpass.Transform(samples[i]));
                        }
                    }

                    Peak[] peaks = getPeaks(samples);

                    BPMGroup[] allGroups = getIntervals(peaks);

                    Array.Sort(allGroups, (x, y) => y.Count.CompareTo(x.Count));

                    if (allGroups.Length > 5)
                    {
                        Array.Resize(ref allGroups, 5);
                    }

                    this.groups = allGroups;
                }
            }
        }
    }
}
