// https://github.com/tmokmss/Osu2Saber/blob/master/Osu2Saber/Model/Mp3toOggConverter.cs

using NAudio.Wave;
using OggVorbisEncoder;
using System;
using System.IO;

namespace Lolighter.Info
{
    internal class MP3toOGG
    {
        public static string ConvertToOgg(string srcPath, string outputDir)
        {
            var ext = Path.GetExtension(srcPath);
            switch (ext)
            {
                case ".mp3":
                    return ConvertMp3toOgg(srcPath, outputDir);
                default:
                    return "";
            }

        }

        public static string ConvertMp3toOgg(string mp3Path, string outputDir)
        {
            var oggName = Path.GetFileNameWithoutExtension(mp3Path) + ".ogg";
            try
            {
                using (var mp3 = new Mp3FileReader(mp3Path))
                using (var wav = WaveFormatConversionStream.CreatePcmStream(mp3))
                using (var ogg = new FileStream(Path.Combine(outputDir, oggName), FileMode.Create, FileAccess.Write))
                {
                    const int SampleSize = 4096;

                    // Here's a reference for the code below: 
                    //   https://github.com/SteveLillis/.NET-Ogg-Vorbis-Encoder/blob/master/OggVorbisEncoder.Example/Encoder.cs
                    var info = VorbisInfo.InitVariableBitRate(2, wav.WaveFormat.SampleRate, 0.5f);

                    // set up our packet->stream encoder
                    var serial = new Random().Next();
                    var oggStream = new OggStream(serial);

                    // =========================================================
                    // HEADER
                    // =========================================================
                    // Vorbis streams begin with three headers; the initial header (with
                    // most of the codec setup parameters) which is mandated by the Ogg
                    // bitstream spec.  The second header holds any comment fields.  The
                    // third header holds the bitstream codebook.

                    var comments = new Comments();
                    // comments.AddTag("ARTIST", "TEST");

                    var infoPacket = HeaderPacketBuilder.BuildInfoPacket(info);
                    var commentsPacket = HeaderPacketBuilder.BuildCommentsPacket(comments);
                    var booksPacket = HeaderPacketBuilder.BuildBooksPacket(info);

                    oggStream.PacketIn(infoPacket);
                    oggStream.PacketIn(commentsPacket);
                    oggStream.PacketIn(booksPacket);

                    // Flush to force audio data onto its own page per the spec
                    OggPage page;
                    while (oggStream.PageOut(out page, true))
                    {
                        ogg.Write(page.Header, 0, page.Header.Length);
                        ogg.Write(page.Body, 0, page.Body.Length);
                    }

                    // =========================================================
                    // BODY (Audio Data)
                    // =========================================================
                    var processingState = ProcessingState.Create(info);

                    var buffer = new float[info.Channels][];
                    buffer[0] = new float[SampleSize];
                    buffer[1] = new float[SampleSize];

                    var readbuffer = new byte[SampleSize * 4];
                    while (!oggStream.Finished)
                    {
                        var bytes = wav.Read(readbuffer, 0, readbuffer.Length);

                        if (bytes == 0)
                        {
                            processingState.WriteEndOfStream();
                        }
                        else
                        {
                            var samples = bytes / 4;

                            for (var i = 0; i < samples; i++)
                            {
                                // uninterleave samples
                                buffer[0][i] = (short)((readbuffer[i * 4 + 1] << 8) | (0x00ff & readbuffer[i * 4])) / 32768f;
                                buffer[1][i] = (short)((readbuffer[i * 4 + 3] << 8) | (0x00ff & readbuffer[i * 4 + 2])) / 32768f;
                            }

                            processingState.WriteData(buffer, samples);
                        }

                        while (!oggStream.Finished
                               && processingState.PacketOut(out OggPacket packet))
                        {
                            oggStream.PacketIn(packet);

                            while (!oggStream.Finished
                                   && oggStream.PageOut(out page, false))
                            {
                                ogg.Write(page.Header, 0, page.Header.Length);
                                ogg.Write(page.Body, 0, page.Body.Length);
                            }
                        }
                    }
                }
            }
            catch (NAudio.MmException e)
            {
                Console.WriteLine(e);
                // Just retry
                return ConvertMp3toOgg(mp3Path, outputDir);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Error while converting to " + oggName);
                return oggName;
            }
            return oggName;
        }
    }
}
