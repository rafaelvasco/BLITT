using System;
using System.IO;
using BLITTEngine.Numerics;

namespace BLITTEngine.Foundation
{
    class Bitmap
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int PixelCount { get; private set; }
        public byte[] PixelData { get; private set; }


        public Bitmap(byte[] pixelData, int width, int height)
        {
            SetPixels(pixelData, width, height);
        }

        internal void SetPixels(byte[] pixelData, int width, int height)
        {
            this.PixelData = pixelData;
            this.Width = width;
            this.Height = height;
            this.PixelCount = width * height;
        }
    }

    class DeflateDecoder
    {
        private static readonly byte[] lengthBits;
        private static readonly ushort[] lengthBase;
        private static readonly byte[] distBits;
        private static readonly ushort[] distBase;

        private static readonly byte[] clcidx =
        {
            16, 17, 18, 0, 8, 7, 9, 6,
            10, 5, 11, 4, 12, 3, 13, 2,
            14, 1, 15
        };

        static DeflateDecoder()
        {
            void Build(ref byte[] bits, ref ushort[] bas, int delta, int first)
            {
                bits = new byte[30];
                bas = new ushort[30];

                for (int i = 0; i < 30 - delta; ++i)
                    bits[i + delta] = (byte) (i / delta);

                for (int sum = first, i = 0; i < 30; ++i)
                {
                    bas[i] = (ushort) sum;
                    sum += 1 << bits[i];
                }
            }

            Build(ref lengthBits, ref lengthBase, 4, 3);
            Build(ref distBits, ref distBase, 2, 1);

            lengthBits[28] = 0;
            lengthBase[28] = 258;
        }

        private byte[] source;
        private int sourceInd;
        private byte[] dest;
        private int destCount;
        private int tag;
        private int bitCount;
        private int curLen;
        private int lzOff;
        private readonly ushort[] lTable = new ushort[16];
        private readonly ushort[] lTrans = new ushort[288];
        private readonly ushort[] dTable = new ushort[16];
        private readonly ushort[] dTrans = new ushort[288];
        private readonly ushort[] offs = new ushort[16];
        private readonly byte[] lengths = new byte[320];

        public byte[] Decode(byte[] _source)
        {
            byte[] results = null;
            Decode(_source, 0, ref results);
            return results;
        }

        public byte[] Decode(byte[] _source, int sourceIndex)
        {
            byte[] results = null;
            Decode(_source, sourceIndex, ref results);
            return results;
        }

        public void Decode(byte[] _source, ref byte[] results)
        {
            Decode(_source, 0, ref results);
        }

        public void Decode(byte[] _source, int sourceIndex, ref byte[] results)
        {
            this.source = _source;
            sourceInd = sourceIndex;

            if (dest == null)
            {
                dest = new byte[Calc.NextPowerOfTwo(_source.Length)];
            }

            else if (_source.Length > dest.Length)
            {
                Array.Resize(ref dest, Calc.NextPowerOfTwo(_source.Length));
            }

            destCount = 0;
            bitCount = 0;
            curLen = 0;

            var res = false;

            while (!res)
            {
                res = Uncompress();
            }

            if (results == null)
            {
                results = new byte[destCount];
            }
            else if (destCount > results.Length)
            {
                Array.Resize(ref results, destCount);
            }

            Buffer.BlockCopy(dest, 0, results, 0, destCount);
        }

        private void BuildFixedTrees()
        {
            for (int i = 0; i < 7; ++i)
                lTable[i] = 0;

            lTable[7] = 24;
            lTable[8] = 152;
            lTable[9] = 112;

            for (int i = 0; i < 24; ++i)
            {
                lTrans[i] = (ushort) (256 + i);
            }

            for (ushort i = 0; i < 144; ++i)
            {
                lTrans[24 + i] = i;
            }

            for (int i = 0; i < 8; ++i)
            {
                lTrans[24 + 144 + i] = (ushort) (280 + i);
            }

            for (int i = 0; i < 112; ++i)
            {
                lTrans[24 + 144 + 8 + i] = (ushort) (144 + i);
            }

            for (int i = 0; i < 5; ++i)
            {
                dTable[i] = 0;
            }

            dTable[5] = 32;

            for (ushort i = 0; i < 32; ++i)
            {
                dTrans[i] = i;
            }
        }

        private void BuildTree(ushort[] table, ushort[] trans, byte[] lens, int lensInd, int num)
        {
            for (int i = 0; i < 16; ++i)
            {
                table[i] = 0;
            }


            for (int i = 0; i < num; ++i)
            {
                table[lens[lensInd + i]]++;
            }

            table[0] = 0;

            for (ushort sum = 0, i = 0; i < 16; ++i)
            {
                offs[i] = sum;
                sum += table[i];
            }

            for (ushort i = 0; i < num; ++i)
            {
                if (lens[lensInd + i] > 0)
                {
                    trans[offs[lens[lensInd + i]]++] = i;
                }
            }
        }

        private void Put(byte c)
        {
            if (destCount == dest.Length)
            {
                Array.Resize(ref dest, destCount * 2);
            }

            dest[destCount++] = c;
        }

        private int GetBit()
        {
            if ((bitCount--) == 0)
            {
                tag = source[sourceInd++];
                bitCount = 7;
            }

            int bit = tag & 0x01;
            tag >>= 1;

            return bit;
        }

        private int ReadBits(int num, int baseVal)
        {
            int val = 0;

            if (num != 0)
            {
                int limit = 1 << num;

                for (int mask = 1; mask < limit; mask *= 2)
                {
                    if (GetBit() == 1)
                    {
                        val += mask;
                    }
                }
            }

            return baseVal + val;
        }

        private int DecodeSymbol(ushort[] table, ushort[] trans)
        {
            int sum = 0;
            int cur = 0;
            int len = 0;

            do
            {
                cur = 2 * cur + GetBit();
                ++len;
                sum += table[len];
                cur -= table[len];
            } while (cur >= 0);

            return trans[sum + cur];
        }

        private void DecodeTrees()
        {
            int hlit = ReadBits(5, 257);
            int hdist = ReadBits(5, 1);
            int hclen = ReadBits(4, 4);

            for (int i = 0; i < 19; ++i)
            {
                lengths[i] = 0;
            }

            for (int i = 0; i < hclen; ++i)
            {
                int clen = ReadBits(3, 0);
                lengths[clcidx[i]] = (byte) clen;
            }

            BuildTree(lTable, lTrans, lengths, 0, 19);

            for (int num = 0; num < hlit + hdist;)
            {
                int sym = DecodeSymbol(lTable, lTrans);

                switch (sym)
                {
                    case 16:
                    {
                        byte prev = lengths[num - 1];

                        for (int length = ReadBits(2, 3); length > 0; --length)
                        {
                            lengths[num++] = prev;
                        }
                    }
                        break;
                    case 17:
                    {
                        for (int length = ReadBits(3, 3); length > 0; --length)
                        {
                            lengths[num++] = 0;
                        }
                    }
                        break;
                    case 18:
                    {
                        for (int length = ReadBits(7, 11); length > 0; --length)
                        {
                            lengths[num++] = 0;
                        }
                    }
                        break;
                    default:
                    {
                        lengths[num++] = (byte) sym;
                    }
                        break;
                }
            }

            BuildTree(lTable, lTrans, lengths, 0, hlit);
            BuildTree(dTable, dTrans, lengths, hlit, hdist);
        }

        private bool InflateBlockData()
        {
            if (curLen == 0)
            {
                int sym = DecodeSymbol(lTable, lTrans);

                if (sym < 256)
                {
                    Put((byte) sym);
                    return false;
                }

                if (sym == 256)
                {
                    return true;
                }


                sym -= 257;
                curLen = ReadBits(lengthBits[sym], lengthBase[sym]);

                int dist = DecodeSymbol(dTable, dTrans);
                lzOff = -ReadBits(distBits[dist], distBase[dist]);
            }

            Put(dest[destCount + lzOff]);
            curLen--;

            return false;
        }

        private bool InflateUncompressedBlock()
        {
            if (curLen == 0)
            {
                int length = source[sourceInd++] + 256 * source[sourceInd++];
                int invlength = source[sourceInd++] + 256 * source[sourceInd++];

                if (length != (~invlength & 0x0000ffff))
                    throw new Exception("Data error.");

                curLen = length + 1;
                bitCount = 0;
            }

            --curLen;
            if (curLen == 0)
                return true;

            Put(source[sourceInd++]);
            return false;
        }

        private bool Uncompress()
        {
            int bType = -1;
            int bFinal = 0;
            bool res;

            while (true)
            {
                if (bType == -1)
                {
                    bFinal = GetBit();
                    bType = ReadBits(2, 0);

                    if (bType == 1)
                        BuildFixedTrees();
                    else if (bType == 2)
                        DecodeTrees();
                }

                switch (bType)
                {
                    case 0:
                        res = InflateUncompressedBlock();
                        break;
                    case 1:
                    case 2:
                        res = InflateBlockData();
                        break;
                    default: throw new Exception("Data error.");
                }

                if (res)
                {
                    if (bFinal == 0)
                        bType = -1;
                    else
                        return true;
                }
            }
        }
    }

    enum ChunkType : uint
    {
        IHDR = ((uint) 'I' << 24) + ((uint) 'H' << 16) + ((uint) 'D' << 8) + 'R',
        IDAT = ((uint) 'I' << 24) + ((uint) 'D' << 16) + ((uint) 'A' << 8) + 'T',
        IEND = ((uint) 'I' << 24) + ((uint) 'E' << 16) + ((uint) 'N' << 8) + 'D',
    }

    enum FilterType : byte
    {
        None = 0,
        Sub = 1,
        Up = 2,
        Avg = 3,
        Paeth = 4,
        AvgFirst,
        PaethFirst
    }

    class PngDecoder
    {
        private static readonly byte[] signature = {0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a};

        private DeflateDecoder inflater;
        private byte[] compressed;
        private int compressedSize;
        private byte[] filtered;
        private int width;
        private int height;
        private int bitDepth;
        private int colorType;
        private int compressionMethod;
        private int filterMethod;
        private int interlaceMethod;


        public void Decode(byte[] source, out Bitmap bitmap)
        {
            //Parse the PNG file to get all our compressed bytes
            ParsePng(source);

            //Make sure the ZLIB data is in good form
            CheckZLibHeader();

            //Inflate the compressed bytes
            if (inflater == null)
            {
                inflater = new DeflateDecoder();
            }

            filtered = inflater.Decode(compressed, 2);

            Unfilter(out var pixelData);

            bitmap = new Bitmap(pixelData, width, height);
        }

        private void ParsePng(byte[] source)
        {
            compressedSize = 0;

            int ind = 0;

            uint ReadInt()
            {
                return ((uint) source[ind++] << 24) | ((uint) source[ind++] << 16) | ((uint) source[ind++] << 8) |
                       (uint) source[ind++];
            }

            //Parse the PNG signature
            for (int i = 0; i < signature.Length; ++i)
            {
                if (source[ind++] != signature[i])
                {
                    throw new Exception("invalid PNG signature");
                }
            }

            //Read header chunk
            var chunkLength = (int) ReadInt();
            var chunkType = (ChunkType) ReadInt();
            if (chunkType != ChunkType.IHDR)
                throw new Exception("IHDR chunk is not first");

            //Read the size
            width = (int) ReadInt();
            height = (int) ReadInt();

            //Read the bit depth
            bitDepth = source[ind++];

            if (bitDepth != 8)
            {
                throw new Exception("bit depth not supported: " + bitDepth);
            }

            //Read the color type
            colorType = source[ind++];
            if (colorType != 2 && colorType != 6)
            {
                throw new Exception("color type not supported: " + colorType);
            }

            //Read the compression method
            compressionMethod = source[ind++];
            if (compressionMethod != 0)
            {
                throw new Exception("compression method not supported: " + compressionMethod);
            }

            //Read the filter method
            filterMethod = source[ind++];
            if (filterMethod != 0)
            {
                throw new Exception("filter method not supported: " + filterMethod);
            }

            //Read the interlace method
            interlaceMethod = source[ind++];
            if (interlaceMethod != 0)
            {
                throw new Exception("interlace method not supported: " + interlaceMethod);
            }

            //Skip the CRC
            ind += 4;

            //Scan ahead to find out the compressed size
            int prevInd = ind;
            int compLen = 0;
            chunkLength = (int) ReadInt();
            chunkType = (ChunkType) ReadInt();

            while (chunkType != ChunkType.IEND)
            {
                if (chunkType == ChunkType.IDAT)
                {
                    compLen += chunkLength;
                }

                ind += chunkLength + 4;
                chunkLength = (int) ReadInt();
                chunkType = (ChunkType) ReadInt();
            }

            ind = prevInd;

            //Make sure the stream ends on an IEND block
            if (chunkType != ChunkType.IEND)
            {
                throw new Exception("PNG must end with IEND chunk");
            }


            //Make sure we actually had data
            if (compLen <= 0)
            {
                throw new Exception("no IDAT data to decode");
            }


            //Make sure the compressed array is large enough
            if (compressed == null)
            {
                compressed = new byte[compLen];
            }
            else if (compressed.Length < compLen)
            {
                Array.Resize(ref compressed, compLen);
            }

            //Read all the IDAT chunks
            chunkLength = (int) ReadInt();
            chunkType = (ChunkType) ReadInt();

            while (chunkType != ChunkType.IEND)
            {
                if (chunkType == ChunkType.IDAT)
                {
                    Buffer.BlockCopy(source, ind, compressed, compressedSize, chunkLength);
                    compressedSize += chunkLength;
                }

                ind += chunkLength + 4;
                chunkLength = (int) ReadInt();
                chunkType = (ChunkType) ReadInt();
            }
        }

        private void CheckZLibHeader()
        {
            byte cmf = compressed[0];
            byte flg = compressed[1];

            //Check checksum
            if (((256 * cmf + flg) % 31) != 0)
            {
                throw new Exception("invalid checksum");
            }

            //Check method
            if ((cmf & 0x0f) != 8)
            {
                throw new Exception("invalid compression method: " + (cmf & 0x0f));
            }

            //Check window size 
            if ((cmf >> 4) > 7)
            {
                throw new Exception("invalid window size: " + (cmf >> 4));
            }

            //Make sure there's no preset dictionary
            if ((flg & 0x20) != 0)
            {
                throw new Exception("preset dictionary not allowed");
            }
        }

        private static byte ByteCast(int val)
        {
            return (byte) (val & 255);
        }

        private static int Paeth(int a, int b, int c)
        {
            int p = a + b - c;
            int pa = Math.Abs(p - a);
            int pb = Math.Abs(p - b);
            int pc = Math.Abs(p - c);

            if (pa <= pb && pa <= pc)
            {
                return a;
            }

            if (pb <= pc)
            {
                return b;
            }

            return c;
        }

        private unsafe void Unfilter(out byte[] pixels)
        {
            int bpp = colorType == 2 ? 3 : 4;
            const int alphaBpp = 4;
            int bdiff = alphaBpp - bpp;

            int byteCount = width * height * bpp;

            pixels = new Byte[byteCount];

            //If we're loading a RGB image with no alpha channel, load with full opacity
            if (bpp < alphaBpp)
            {
                for (int i = 0; i < byteCount; ++i)
                {
                    pixels[i] = 255;
                    pixels[i + 1] = 255;
                    pixels[i + 2] = 255;
                }
            }

            fixed (byte* cur = &pixels[0])
            {
                var raw = filtered;
                int rawi = 0;
                int curi = 0;
                int prei = curi - (width * 4);

                //For each scanline
                for (int y = 0; y < height; ++y)
                {
                    var filter = (FilterType) raw[rawi++];

                    //Use special filters for the first scanline
                    if (y == 0)
                    {
                        switch (filter)
                        {
                            case FilterType.Up:
                                filter = FilterType.None;
                                break;
                            case FilterType.Avg:
                                filter = FilterType.AvgFirst;
                                break;
                            case FilterType.Paeth:
                                filter = FilterType.PaethFirst;
                                break;
                        }
                    }

                    //Handle the first pixel on the scanline
                    switch (filter)
                    {
                        case FilterType.None:
                        case FilterType.Sub:
                        case FilterType.AvgFirst:
                        case FilterType.PaethFirst:
                            for (int i = 0; i < bpp; ++i)
                                cur[curi + i] = raw[rawi + i];
                            break;
                        case FilterType.Up:
                            for (int i = 0; i < bpp; ++i)
                                cur[curi + i] = ByteCast(raw[rawi + i] + cur[prei + i]);
                            break;
                        case FilterType.Avg:
                            for (int i = 0; i < bpp; ++i)
                                cur[curi + i] = ByteCast(raw[rawi + i] + (cur[prei + i] >> 1));
                            break;
                        case FilterType.Paeth:
                            for (int i = 0; i < bpp; ++i)
                                cur[curi + i] = ByteCast(raw[rawi + i] + Paeth(0, cur[prei + i], 0));
                            break;
                    }

                    rawi += bpp;
                    curi += alphaBpp;
                    prei += alphaBpp;

                    //Handle the rest of the pixels on the scanline
                    switch (filter)
                    {
                        case FilterType.None:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = raw[rawi];
                            break;
                        case FilterType.Sub:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + cur[curi - alphaBpp]);
                            break;
                        case FilterType.Up:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + cur[prei]);
                            break;
                        case FilterType.Avg:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + ((cur[prei] + cur[curi - alphaBpp]) >> 1));
                            break;
                        case FilterType.Paeth:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + Paeth(cur[curi - alphaBpp], cur[prei],
                                                         cur[prei - alphaBpp]));
                            break;
                        case FilterType.AvgFirst:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + (cur[curi - alphaBpp] >> 1));
                            break;
                        case FilterType.PaethFirst:
                            for (int i = 0, n = width - 1; i < n; ++i, curi += bdiff, prei += bdiff)
                            for (int j = 0; j < bpp; ++j, ++curi, ++prei, ++rawi)
                                cur[curi] = ByteCast(raw[rawi] + Paeth(cur[curi - alphaBpp], 0, 0));
                            break;
                    }
                }
            }
        }
    }

    internal static class ImageLoader
    {
        private static PngDecoder decoder;

        public static Bitmap LoadFile(Stream stream)
        {
            if (decoder == null)
            {
                decoder = new PngDecoder();
            }
            
            byte[] bytes = new byte[stream.Length];
            
            int numBytesToRead = (int)stream.Length;
            int numBytesRead = 0;

            while (numBytesToRead > 0)
            {
                int n = stream.Read(bytes, numBytesRead, numBytesToRead);

                if (n == 0) break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            
            decoder.Decode(bytes, out var bitmap);

            return bitmap;
        }
    }
}