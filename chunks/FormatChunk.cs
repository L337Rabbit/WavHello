using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using com.okitoki.wavhello.utils;

namespace com.okitoki.wavhello.chunks
{
    public class FormatChunk : Chunk
    {

        public short AudioFormat { get; set; }

        public short NumChannels { get; set; }

        public int SampleRate { get; set; }

        public int BitRate { get; set; }

        public short BlockAlign { get; set; }

        public short BitsPerSample { get; set; }

        public short ExtraParamsSize { get; set; }

        public byte[] ExtraParamsData { get; set; }

        public static FormatChunk Read(FileStream fs, string chunkID, int chunkSize)
        {
            FormatChunk chunk = new FormatChunk();
            chunk.ChunkID = chunkID;
            chunk.ChunkSize = chunkSize;

            chunk.AudioFormat = BitConverter.ToInt16(BinaryFileUtils.Read(fs, 2));
            chunk.NumChannels = BitConverter.ToInt16(BinaryFileUtils.Read(fs, 2));
            chunk.SampleRate = BitConverter.ToInt32(BinaryFileUtils.Read(fs, 4));
            chunk.BitRate = BitConverter.ToInt32(BinaryFileUtils.Read(fs, 4));
            chunk.BlockAlign = BitConverter.ToInt16(BinaryFileUtils.Read(fs, 2));
            chunk.BitsPerSample = BitConverter.ToInt16(BinaryFileUtils.Read(fs, 2));

            if (chunk.AudioFormat == 1) { return chunk; }

            /*
            //This was based on specifications found on a certain website. It seems to not be the case
            //that this is always included in files. 
            chunk.ExtraParamsSize = BitConverter.ToInt16(BinaryFileUtils.Read(fs, 2));
            chunk.ExtraParamsData = BinaryFileUtils.Read(fs, chunk.ExtraParamsSize);*/

            return chunk;
        }

        public static void Write(BinaryWriter bw, FormatChunk chunk)
        {
            bw.Write(System.Text.Encoding.UTF8.GetBytes(chunk.ChunkID));
            bw.Write(chunk.ChunkSize);
            bw.Write(chunk.AudioFormat);
            bw.Write(chunk.NumChannels);
            bw.Write(chunk.SampleRate);
            bw.Write(chunk.BitRate);
            bw.Write(chunk.BlockAlign);
            bw.Write(chunk.BitsPerSample);

            /*
            //This was based on specifications found on a certain website. It seems to not be the case
            //that this is always included in files.
            if (chunk.AudioFormat != 1)
            {
                bw.Write(chunk.ExtraParamsSize);

                if (chunk.ExtraParamsSize > 0)
                {
                    bw.Write(chunk.ExtraParamsData);
                }
            }*/
        }

        public override void Write(BinaryWriter bw)
        {
            FormatChunk.Write(bw, this);
        }

        public override string ToString()
        {
            return "{\n" +
                "\tCHUNK ID: " + ChunkID + "\n" +
                "\tCHUNK SIZE: " + ChunkSize + "\n" +
                "\tAudio Format: " + AudioFormat + "\n" +
                "\tChannels: " + NumChannels + "\n" +
                "\tSample Rate: " + SampleRate + "\n" +
                "\tBit Rate: " + BitRate + "\n" +
                "\tBlock Align: " + BlockAlign + "\n" +
                "\tBits Per Sample: " + BitsPerSample + "\n" +
                "}";
        }
    }
}
