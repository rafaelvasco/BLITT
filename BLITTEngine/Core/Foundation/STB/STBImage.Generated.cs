﻿using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Foundation.STB
{
    unsafe partial class STBImage
    {
        public class stbi__context
        {
            public uint img_x;
            public uint img_y;
            public int img_n;
            public int img_out_n;
            public stbi_io_callbacks io = new stbi_io_callbacks();
            public void* io_user_data;
            public int read_from_callbacks;
            public int buflen;
            public byte* buffer_start = (byte*)stbi__malloc(128);
            public byte* img_buffer;
            public byte* img_buffer_end;
            public byte* img_buffer_original;
            public byte* img_buffer_original_end;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__result_info
        {
            public int bits_per_channel;
            public int num_channels;
            public int channel_order;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__huffman
        {
            public fixed byte fast[1 << 9];
            public fixed ushort code[256];
            public fixed byte values[256];
            public fixed byte size[257];
            public fixed uint maxcode[18];
            public fixed int delta[17];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__zhuffman
        {
            public fixed ushort fast[1 << 9];
            public fixed ushort firstcode[16];
            public fixed int maxcode[17];
            public fixed ushort firstsymbol[16];
            public fixed byte size[288];
            public fixed ushort value[288];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__zbuf
        {
            public byte* zbuffer;
            public byte* zbuffer_end;
            public int num_bits;
            public uint code_buffer;
            public sbyte* zout;
            public sbyte* zout_start;
            public sbyte* zout_end;
            public int z_expandable;
            public stbi__zhuffman z_length;
            public stbi__zhuffman z_distance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__pngchunk
        {
            public uint length;
            public uint type;
        }

        public class stbi__png
        {
            public stbi__context s = new stbi__context();
            public byte* idata;
            public byte* expanded;
            public byte* _out_;
            public int depth;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct stbi__bmp_data
        {
            public int bpp;
            public int offset;
            public int hsz;
            public uint mr;
            public uint mg;
            public uint mb;
            public uint ma;
            public uint all_a;
        }

        public const int STBI_default = 0;
        public const int STBI_grey = 1;
        public const int STBI_grey_alpha = 2;
        public const int STBI_rgb = 3;
        public const int STBI_rgb_alpha = 4;
        public const int STBI_ORDER_RGB = 0;
        public const int STBI_ORDER_BGR = 1;
        public const int STBI__SCAN_load = 0;
        public const int STBI__SCAN_type = 1;
        public const int STBI__SCAN_header = 2;
        public const int STBI__F_none = 0;
        public const int STBI__F_sub = 1;
        public const int STBI__F_up = 2;
        public const int STBI__F_avg = 3;
        public const int STBI__F_paeth = 4;
        public const int STBI__F_avg_first = 5;
        public const int STBI__F_paeth_first = 6;
        public static float stbi__h2l_gamma_i = (float)(1.0f / 2.2f);
        public static float stbi__h2l_scale_i = (float)(1.0f);

        public static uint[] stbi__bmask =
        {
            0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 65535
        };

        public static int[] stbi__jbias =
        {
            0, -1, -3, -7, -15, -31, -63, -127, -255, -511, -1023, -2047, -4095, -8191, -16383,
            -32767
        };

        public static byte[] stbi__jpeg_dezigzag =
        {
            0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12, 19, 26, 33, 40,
            48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35, 42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51, 58, 59, 52,
            45, 38, 31, 39, 46, 53, 60, 61, 54, 47, 55, 62, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63, 63
        };

        public static int[] stbi__zlength_base =
        {
            3, 4, 5, 6, 7, 8, 9, 10, 11, 13, 15, 17, 19, 23, 27, 31, 35, 43, 51, 59, 67,
            83, 99, 115, 131, 163, 195, 227, 258, 0, 0
        };

        public static int[] stbi__zlength_extra =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5,
            5, 5, 5, 0, 0, 0
        };

        public static int[] stbi__zdist_base =
        {
            1, 2, 3, 4, 5, 7, 9, 13, 17, 25, 33, 49, 65, 97, 129, 193, 257, 385, 513, 769,
            1025, 1537, 2049, 3073, 4097, 6145, 8193, 12289, 16385, 24577, 0, 0
        };

        public static int[] stbi__zdist_extra =
        {
            0, 0, 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11,
            11, 12, 12, 13, 13
        };

        public static byte[] length_dezigzag = { 16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15 };

        public static byte[] stbi__zdefault_length =
        {
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
            8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8,
            8, 8, 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
            9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9,
            9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 7, 7,
            7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 8, 8, 8, 8, 8, 8, 8, 8
        };

        public static byte[] stbi__zdefault_distance =
        {
            5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
            5, 5, 5, 5, 5, 5, 5, 5
        };

        public static byte[] png_sig = { 137, 80, 78, 71, 13, 10, 26, 10 };

        public static byte[] first_row_filter =
        {
            STBI__F_none, STBI__F_sub, STBI__F_none, STBI__F_avg_first,
            STBI__F_paeth_first
        };

        public static byte[] stbi__depth_scale_table = { 0, 0xff, 0x55, 0, 0x11, 0, 0, 0, 0x01 };
        public static int stbi__unpremultiply_on_load = (int)(0);
        public static int stbi__de_iphone_flag = (int)(0);

        public static void stbi__start_mem(stbi__context s, byte* buffer, int len)
        {
            s.io.read = (null);
            s.read_from_callbacks = (int)(0);
            s.img_buffer = s.img_buffer_original = buffer;
            s.img_buffer_end = s.img_buffer_original_end = buffer + len;
        }

        public static void stbi__start_callbacks(stbi__context s, stbi_io_callbacks c, void* user)
        {
            s.io = (stbi_io_callbacks)(c);
            s.io_user_data = user;
            s.buflen = 128;
            s.read_from_callbacks = (int)(1);
            s.img_buffer_original = s.buffer_start;
            stbi__refill_buffer(s);
            s.img_buffer_original_end = s.img_buffer_end;
        }

        public static void stbi__rewind(stbi__context s)
        {
            s.img_buffer = s.img_buffer_original;
            s.img_buffer_end = s.img_buffer_original_end;
        }

        public static int stbi__addsizes_valid(int a, int b)
        {
            if ((b) < (0)) return (int)(0);
            return (a <= 2147483647 - b) ? 1 : 0;
        }

        public static int stbi__mul2sizes_valid(int a, int b)
        {
            if (((a) < (0)) || ((b) < (0))) return (int)(0);
            if ((b) == (0)) return (int)(1);
            return (a <= 2147483647 / b) ? 1 : 0;
        }

        public static int stbi__mad2sizes_valid(int a, int b, int add)
        {
            return
                (int)
                    (((stbi__mul2sizes_valid((int)(a), (int)(b))) != 0) && ((stbi__addsizes_valid((int)(a * b), (int)(add))) != 0)
                        ? 1
                        : 0);
        }

        public static int stbi__mad3sizes_valid(int a, int b, int c, int add)
        {
            return
                (int)
                    ((((stbi__mul2sizes_valid((int)(a), (int)(b))) != 0) && ((stbi__mul2sizes_valid((int)(a * b), (int)(c))) != 0)) &&
                     ((stbi__addsizes_valid((int)(a * b * c), (int)(add))) != 0)
                        ? 1
                        : 0);
        }

        public static int stbi__mad4sizes_valid(int a, int b, int c, int d, int add)
        {
            return
                (int)
                    (((((stbi__mul2sizes_valid((int)(a), (int)(b))) != 0) && ((stbi__mul2sizes_valid((int)(a * b), (int)(c))) != 0)) &&
                      ((stbi__mul2sizes_valid((int)(a * b * c), (int)(d))) != 0)) &&
                     ((stbi__addsizes_valid((int)(a * b * c * d), (int)(add))) != 0)
                        ? 1
                        : 0);
        }

        public static void* stbi__malloc_mad2(int a, int b, int add)
        {
            if (stbi__mad2sizes_valid((int)(a), (int)(b), (int)(add)) == 0) return (null);
            return stbi__malloc((ulong)(a * b + add));
        }

        public static void* stbi__malloc_mad3(int a, int b, int c, int add)
        {
            if (stbi__mad3sizes_valid((int)(a), (int)(b), (int)(c), (int)(add)) == 0) return (null);
            return stbi__malloc((ulong)(a * b * c + add));
        }

        public static void* stbi__malloc_mad4(int a, int b, int c, int d, int add)
        {
            if (stbi__mad4sizes_valid((int)(a), (int)(b), (int)(c), (int)(d), (int)(add)) == 0) return (null);
            return stbi__malloc((ulong)(a * b * c * d + add));
        }

        public static void stbi_set_flip_vertically_on_load(int flag_true_if_should_flip)
        {
            stbi__vertically_flip_on_load = (int)(flag_true_if_should_flip);
        }

        public static void* stbi__load_main(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri,
            int bpc)
        {
            ri->bits_per_channel = (int)(8);
            ri->channel_order = (int)(STBI_ORDER_RGB);
            ri->num_channels = (int)(0);
            if ((stbi__jpeg_test(s)) != 0) return stbi__jpeg_load(s, x, y, comp, (int)(req_comp), ri);
            if ((stbi__png_test(s)) != 0) return stbi__png_load(s, x, y, comp, (int)(req_comp), ri);
            if ((stbi__bmp_test(s)) != 0) return stbi__bmp_load(s, x, y, comp, (int)(req_comp), ri);
            if ((stbi__gif_test(s)) != 0) return stbi__gif_load(s, x, y, comp, (int)(req_comp), ri);
            if ((stbi__psd_test(s)) != 0) return stbi__psd_load(s, x, y, comp, (int)(req_comp), ri, (int)(bpc));
            if ((stbi__tga_test(s)) != 0) return stbi__tga_load(s, x, y, comp, (int)(req_comp), ri);
            return ((byte*)((ulong)((stbi__err("unknown image type")) != 0 ? ((byte*)null) : (null))));
        }

        public static byte* stbi__convert_16_to_8(ushort* orig, int w, int h, int channels)
        {
            int i;
            int img_len = (int)(w * h * channels);
            byte* reduced;
            reduced = (byte*)(stbi__malloc((ulong)(img_len)));
            if ((reduced) == (null)) return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            for (i = (int)(0); (i) < (img_len); ++i)
            {
                reduced[i] = ((byte)((orig[i] >> 8) & 0xFF));
            }
            CRuntime.free(orig);
            return reduced;
        }

        public static ushort* stbi__convert_8_to_16(byte* orig, int w, int h, int channels)
        {
            int i;
            int img_len = (int)(w * h * channels);
            ushort* enlarged;
            enlarged = (ushort*)(stbi__malloc((ulong)(img_len * 2)));
            if ((enlarged) == (null))
                return (ushort*)((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            for (i = (int)(0); (i) < (img_len); ++i)
            {
                enlarged[i] = ((ushort)((orig[i] << 8) + orig[i]));
            }
            CRuntime.free(orig);
            return enlarged;
        }

        public static void stbi__vertical_flip(void* image, int w, int h, int bytes_per_pixel)
        {
            int row;
            ulong bytes_per_row = (ulong)(w * bytes_per_pixel);
            byte* temp = stackalloc byte[2048];
            byte* bytes = (byte*)(image);
            for (row = (int)(0); (row) < (h >> 1); row++)
            {
                byte* row0 = bytes + (ulong)row * bytes_per_row;
                byte* row1 = bytes + (ulong)(h - row - 1) * bytes_per_row;
                ulong bytes_left = (ulong)(bytes_per_row);
                while ((bytes_left) != 0)
                {
                    ulong bytes_copy = (ulong)(((bytes_left) < (2048)) ? bytes_left : 2048);
                    CRuntime.memcpy(temp, row0, (ulong)(bytes_copy));
                    CRuntime.memcpy(row0, row1, (ulong)(bytes_copy));
                    CRuntime.memcpy(row1, temp, (ulong)(bytes_copy));
                    row0 += bytes_copy;
                    row1 += bytes_copy;
                    bytes_left -= (ulong)(bytes_copy);
                }
            }
        }

        public static byte* stbi__load_and_postprocess_8bit(stbi__context s, int* x, int* y, int* comp, int req_comp)
        {
            stbi__result_info ri = new stbi__result_info();
            void* result = stbi__load_main(s, x, y, comp, (int)(req_comp), &ri, (int)(8));
            if ((result) == (null)) return (null);
            if (ri.bits_per_channel != 8)
            {
                result = stbi__convert_16_to_8((ushort*)(result), (int)(*x), (int)(*y),
                    (int)((req_comp) == (0) ? *comp : req_comp));
                ri.bits_per_channel = (int)(8);
            }

            if ((stbi__vertically_flip_on_load) != 0)
            {
                int channels = (int)((req_comp) != 0 ? req_comp : *comp);
                stbi__vertical_flip(result, (int)(*x), (int)(*y), (int)(channels));
            }

            return (byte*)(result);
        }

        public static ushort* stbi__load_and_postprocess_16bit(stbi__context s, int* x, int* y, int* comp, int req_comp)
        {
            stbi__result_info ri = new stbi__result_info();
            void* result = stbi__load_main(s, x, y, comp, (int)(req_comp), &ri, (int)(16));
            if ((result) == (null)) return (null);
            if (ri.bits_per_channel != 16)
            {
                result = stbi__convert_8_to_16((byte*)(result), (int)(*x), (int)(*y),
                    (int)((req_comp) == (0) ? *comp : req_comp));
                ri.bits_per_channel = (int)(16);
            }

            if ((stbi__vertically_flip_on_load) != 0)
            {
                int channels = (int)((req_comp) != 0 ? req_comp : *comp);
                stbi__vertical_flip(result, (int)(*x), (int)(*y), (int)(channels * 2));
            }

            return (ushort*)(result);
        }

        public static ushort* stbi_load_16_from_memory(byte* buffer, int len, int* x, int* y, int* channels_in_file,
            int desired_channels)
        {
            stbi__context s = new stbi__context();
            stbi__start_mem(s, buffer, (int)(len));
            return stbi__load_and_postprocess_16bit(s, x, y, channels_in_file, (int)(desired_channels));
        }

        public static ushort* stbi_load_16_from_callbacks(stbi_io_callbacks clbk, void* user, int* x, int* y,
            int* channels_in_file, int desired_channels)
        {
            stbi__context s = new stbi__context();
            stbi__start_callbacks(s, clbk, user);
            return stbi__load_and_postprocess_16bit(s, x, y, channels_in_file, (int)(desired_channels));
        }

        public static byte* stbi_load_from_memory(byte* buffer, int len, int* x, int* y, int* comp, int req_comp)
        {
            stbi__context s = new stbi__context();
            stbi__start_mem(s, buffer, (int)(len));
            return stbi__load_and_postprocess_8bit(s, x, y, comp, (int)(req_comp));
        }

        public static byte* stbi_load_from_callbacks(stbi_io_callbacks clbk, void* user, int* x, int* y, int* comp,
            int req_comp)
        {
            stbi__context s = new stbi__context();
            stbi__start_callbacks(s, clbk, user);
            return stbi__load_and_postprocess_8bit(s, x, y, comp, (int)(req_comp));
        }

        public static void stbi_hdr_to_ldr_gamma(float gamma)
        {
            stbi__h2l_gamma_i = (float)(1 / gamma);
        }

        public static void stbi_hdr_to_ldr_scale(float scale)
        {
            stbi__h2l_scale_i = (float)(1 / scale);
        }

        public static void stbi__refill_buffer(stbi__context s)
        {
            int n = (int)(s.io.read(s.io_user_data, (sbyte*)(s.buffer_start), (int)(s.buflen)));
            if ((n) == (0))
            {
                s.read_from_callbacks = (int)(0);
                s.img_buffer = s.buffer_start;
                s.img_buffer_end = s.buffer_start;
                s.img_buffer_end++;
                *s.img_buffer = (byte)(0);
            }
            else
            {
                s.img_buffer = s.buffer_start;
                s.img_buffer_end = s.buffer_start;
                s.img_buffer_end += n;
            }
        }

        public static byte stbi__get8(stbi__context s)
        {
            if ((s.img_buffer) < (s.img_buffer_end)) return (byte)(*s.img_buffer++);
            if ((s.read_from_callbacks) != 0)
            {
                stbi__refill_buffer(s);
                return (byte)(*s.img_buffer++);
            }

            return (byte)(0);
        }

        public static int stbi__at_eof(stbi__context s)
        {
            if ((s.io.read) != null)
            {
                if (s.io.eof(s.io_user_data) == 0) return (int)(0);
                if ((s.read_from_callbacks) == (0)) return (int)(1);
            }

            return (int)((s.img_buffer) >= (s.img_buffer_end) ? 1 : 0);
        }

        public static void stbi__skip(stbi__context s, int n)
        {
            if ((n) < (0))
            {
                s.img_buffer = s.img_buffer_end;
                return;
            }

            if ((s.io.read) != null)
            {
                int blen = (int)(s.img_buffer_end - s.img_buffer);
                if ((blen) < (n))
                {
                    s.img_buffer = s.img_buffer_end;
                    s.io.skip(s.io_user_data, (int)(n - blen));
                    return;
                }
            }

            s.img_buffer += n;
        }

        public static int stbi__getn(stbi__context s, byte* buffer, int n)
        {
            if ((s.io.read) != null)
            {
                int blen = (int)(s.img_buffer_end - s.img_buffer);
                if ((blen) < (n))
                {
                    int res;
                    int count;
                    CRuntime.memcpy(buffer, s.img_buffer, (ulong)(blen));
                    count = (int)(s.io.read(s.io_user_data, (sbyte*)(buffer) + blen, (int)(n - blen)));
                    res = (int)((count) == (n - blen) ? 1 : 0);
                    s.img_buffer = s.img_buffer_end;
                    return (int)(res);
                }
            }

            if (s.img_buffer + n <= s.img_buffer_end)
            {
                CRuntime.memcpy(buffer, s.img_buffer, (ulong)(n));
                s.img_buffer += n;
                return (int)(1);
            }
            else return (int)(0);
        }

        public static int stbi__get16be(stbi__context s)
        {
            int z = (int)(stbi__get8(s));
            return (int)((z << 8) + stbi__get8(s));
        }

        public static uint stbi__get32be(stbi__context s)
        {
            uint z = (uint)(stbi__get16be(s));
            return (uint)((z << 16) + stbi__get16be(s));
        }

        public static int stbi__get16le(stbi__context s)
        {
            int z = (int)(stbi__get8(s));
            return (int)(z + (stbi__get8(s) << 8));
        }

        public static uint stbi__get32le(stbi__context s)
        {
            uint z = (uint)(stbi__get16le(s));
            return (uint)(z + (stbi__get16le(s) << 16));
        }

        public static byte stbi__compute_y(int r, int g, int b)
        {
            return (byte)(((r * 77) + (g * 150) + (29 * b)) >> 8);
        }

        public static byte* stbi__convert_format(byte* data, int img_n, int req_comp, uint x, uint y)
        {
            int i;
            int j;
            byte* good;
            if ((req_comp) == (img_n)) return data;
            good = (byte*)(stbi__malloc_mad3((int)(req_comp), (int)(x), (int)(y), (int)(0)));
            if ((good) == (null))
            {
                CRuntime.free(data);
                return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            }

            for (j = (int)(0); (j) < ((int)(y)); ++j)
            {
                byte* src = data + j * x * img_n;
                byte* dest = good + j * x * req_comp;
                switch (((img_n) * 8 + (req_comp)))
                {
                    case ((1) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 2)
                        {
                            dest[0] = (byte)(src[0]);
                            dest[1] = (byte)(255);
                        }
                        break;

                    case ((1) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 3)
                        {
                            dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));
                        }
                        break;

                    case ((1) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 4)
                        {
                            dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));
                            dest[3] = (byte)(255);
                        }
                        break;

                    case ((2) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 1)
                        {
                            dest[0] = (byte)(src[0]);
                        }
                        break;

                    case ((2) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 3)
                        {
                            dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));
                        }
                        break;

                    case ((2) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 4)
                        {
                            dest[0] = (byte)(dest[1] = (byte)(dest[2] = (byte)(src[0])));
                            dest[3] = (byte)(src[1]);
                        }
                        break;

                    case ((3) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 4)
                        {
                            dest[0] = (byte)(src[0]);
                            dest[1] = (byte)(src[1]);
                            dest[2] = (byte)(src[2]);
                            dest[3] = (byte)(255);
                        }
                        break;

                    case ((3) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 1)
                        {
                            dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));
                        }
                        break;

                    case ((3) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 2)
                        {
                            dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));
                            dest[1] = (byte)(255);
                        }
                        break;

                    case ((4) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 1)
                        {
                            dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));
                        }
                        break;

                    case ((4) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 2)
                        {
                            dest[0] = (byte)(stbi__compute_y((int)(src[0]), (int)(src[1]), (int)(src[2])));
                            dest[1] = (byte)(src[3]);
                        }
                        break;

                    case ((4) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 3)
                        {
                            dest[0] = (byte)(src[0]);
                            dest[1] = (byte)(src[1]);
                            dest[2] = (byte)(src[2]);
                        }
                        break;

                    default:
                        return ((byte*)((ulong)((stbi__err("0")) != 0 ? ((byte*)null) : (null))));
                }
            }
            CRuntime.free(data);
            return good;
        }

        public static ushort stbi__compute_y_16(int r, int g, int b)
        {
            return (ushort)(((r * 77) + (g * 150) + (29 * b)) >> 8);
        }

        public static ushort* stbi__convert_format16(ushort* data, int img_n, int req_comp, uint x, uint y)
        {
            int i;
            int j;
            ushort* good;
            if ((req_comp) == (img_n)) return data;
            good = (ushort*)(stbi__malloc((ulong)(req_comp * x * y * 2)));
            if ((good) == (null))
            {
                CRuntime.free(data);
                return (ushort*)((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            }

            for (j = (int)(0); (j) < ((int)(y)); ++j)
            {
                ushort* src = data + j * x * img_n;
                ushort* dest = good + j * x * req_comp;
                switch (((img_n) * 8 + (req_comp)))
                {
                    case ((1) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 2)
                        {
                            dest[0] = (ushort)(src[0]);
                            dest[1] = (ushort)(0xffff);
                        }
                        break;

                    case ((1) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 3)
                        {
                            dest[0] = (ushort)(dest[1] = (ushort)(dest[2] = (ushort)(src[0])));
                        }
                        break;

                    case ((1) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 1, dest += 4)
                        {
                            dest[0] = (ushort)(dest[1] = (ushort)(dest[2] = (ushort)(src[0])));
                            dest[3] = (ushort)(0xffff);
                        }
                        break;

                    case ((2) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 1)
                        {
                            dest[0] = (ushort)(src[0]);
                        }
                        break;

                    case ((2) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 3)
                        {
                            dest[0] = (ushort)(dest[1] = (ushort)(dest[2] = (ushort)(src[0])));
                        }
                        break;

                    case ((2) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 2, dest += 4)
                        {
                            dest[0] = (ushort)(dest[1] = (ushort)(dest[2] = (ushort)(src[0])));
                            dest[3] = (ushort)(src[1]);
                        }
                        break;

                    case ((3) * 8 + (4)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 4)
                        {
                            dest[0] = (ushort)(src[0]);
                            dest[1] = (ushort)(src[1]);
                            dest[2] = (ushort)(src[2]);
                            dest[3] = (ushort)(0xffff);
                        }
                        break;

                    case ((3) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 1)
                        {
                            dest[0] = (ushort)(stbi__compute_y_16((int)(src[0]), (int)(src[1]), (int)(src[2])));
                        }
                        break;

                    case ((3) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 3, dest += 2)
                        {
                            dest[0] = (ushort)(stbi__compute_y_16((int)(src[0]), (int)(src[1]), (int)(src[2])));
                            dest[1] = (ushort)(0xffff);
                        }
                        break;

                    case ((4) * 8 + (1)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 1)
                        {
                            dest[0] = (ushort)(stbi__compute_y_16((int)(src[0]), (int)(src[1]), (int)(src[2])));
                        }
                        break;

                    case ((4) * 8 + (2)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 2)
                        {
                            dest[0] = (ushort)(stbi__compute_y_16((int)(src[0]), (int)(src[1]), (int)(src[2])));
                            dest[1] = (ushort)(src[3]);
                        }
                        break;

                    case ((4) * 8 + (3)):
                        for (i = (int)(x - 1); (i) >= (0); --i, src += 4, dest += 3)
                        {
                            dest[0] = (ushort)(src[0]);
                            dest[1] = (ushort)(src[1]);
                            dest[2] = (ushort)(src[2]);
                        }
                        break;

                    default:
                        return (ushort*)((byte*)((ulong)((stbi__err("0")) != 0 ? ((byte*)null) : (null))));
                }
            }
            CRuntime.free(data);
            return good;
        }

        public static int stbi__build_huffman(stbi__huffman* h, int* count)
        {
            int i;
            int j;
            int k = (int)(0);
            int code;
            for (i = (int)(0); (i) < (16); ++i)
            {
                for (j = (int)(0); (j) < (count[i]); ++j)
                {
                    h->size[k++] = ((byte)(i + 1));
                }
            }
            h->size[k] = (byte)(0);
            code = (int)(0);
            k = (int)(0);
            for (j = (int)(1); j <= 16; ++j)
            {
                h->delta[j] = (int)(k - code);
                if ((h->size[k]) == (j))
                {
                    while ((h->size[k]) == (j))
                    {
                        h->code[k++] = ((ushort)(code++));
                    }
                    if ((code - 1) >= (1 << j)) return (int)(stbi__err("bad code lengths"));
                }
                h->maxcode[j] = (uint)(code << (16 - j));
                code <<= 1;
            }
            h->maxcode[j] = (uint)(0xffffffff);
            CRuntime.memset(h->fast, (int)(255), (ulong)(1 << 9));
            for (i = (int)(0); (i) < (k); ++i)
            {
                int s = (int)(h->size[i]);
                if (s <= 9)
                {
                    int c = (int)(h->code[i] << (9 - s));
                    int m = (int)(1 << (9 - s));
                    for (j = (int)(0); (j) < (m); ++j)
                    {
                        h->fast[c + j] = ((byte)(i));
                    }
                }
            }
            return (int)(1);
        }

        public static void stbi__build_fast_ac(short* fast_ac, stbi__huffman* h)
        {
            int i;
            for (i = (int)(0); (i) < (1 << 9); ++i)
            {
                byte fast = (byte)(h->fast[i]);
                fast_ac[i] = (short)(0);
                if ((fast) < (255))
                {
                    int rs = (int)(h->values[fast]);
                    int run = (int)((rs >> 4) & 15);
                    int magbits = (int)(rs & 15);
                    int len = (int)(h->size[fast]);
                    if (((magbits) != 0) && (len + magbits <= 9))
                    {
                        int k = (int)(((i << len) & ((1 << 9) - 1)) >> (9 - magbits));
                        int m = (int)(1 << (magbits - 1));
                        if ((k) < (m)) k += (int)((~0U << magbits) + 1);
                        if (((k) >= (-128)) && (k <= 127)) fast_ac[i] = ((short)((k << 8) + (run << 4) + (len + magbits)));
                    }
                }
            }
        }

        public static void stbi__grow_buffer_unsafe(stbi__jpeg j)
        {
            do
            {
                int b = (int)((j.nomore) != 0 ? 0 : stbi__get8(j.s));
                if ((b) == (0xff))
                {
                    int c = (int)(stbi__get8(j.s));
                    while ((c) == (0xff))
                    {
                        c = (int)(stbi__get8(j.s));
                    }
                    if (c != 0)
                    {
                        j.marker = ((byte)(c));
                        j.nomore = (int)(1);
                        return;
                    }
                }
                j.code_buffer |= (uint)(b << (24 - j.code_bits));
                j.code_bits += (int)(8);
            } while (j.code_bits <= 24);
        }

        public static int stbi__jpeg_huff_decode(stbi__jpeg j, stbi__huffman* h)
        {
            uint temp;
            int c;
            int k;
            if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
            c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));
            k = (int)(h->fast[c]);
            if ((k) < (255))
            {
                int s = (int)(h->size[k]);
                if ((s) > (j.code_bits)) return (int)(-1);
                j.code_buffer <<= s;
                j.code_bits -= (int)(s);
                return (int)(h->values[k]);
            }

            temp = (uint)(j.code_buffer >> 16);
            for (k = (int)(9 + 1); ; ++k)
            {
                if ((temp) < (h->maxcode[k])) break;
            }
            if ((k) == (17))
            {
                j.code_bits -= (int)(16);
                return (int)(-1);
            }

            if ((k) > (j.code_bits)) return (int)(-1);
            c = (int)(((j.code_buffer >> (32 - k)) & stbi__bmask[k]) + h->delta[k]);
            j.code_bits -= (int)(k);
            j.code_buffer <<= k;
            return (int)(h->values[c]);
        }

        public static int stbi__extend_receive(stbi__jpeg j, int n)
        {
            uint k;
            int sgn;
            if ((j.code_bits) < (n)) stbi__grow_buffer_unsafe(j);
            sgn = (int)((int)j.code_buffer >> 31);
            k = (uint)(CRuntime._lrotl(j.code_buffer, (int)(n)));
            j.code_buffer = (uint)(k & ~stbi__bmask[n]);
            k &= (uint)(stbi__bmask[n]);
            j.code_bits -= (int)(n);
            return (int)(k + (stbi__jbias[n] & ~sgn));
        }

        public static int stbi__jpeg_get_bits(stbi__jpeg j, int n)
        {
            uint k;
            if ((j.code_bits) < (n)) stbi__grow_buffer_unsafe(j);
            k = (uint)(CRuntime._lrotl(j.code_buffer, (int)(n)));
            j.code_buffer = (uint)(k & ~stbi__bmask[n]);
            k &= (uint)(stbi__bmask[n]);
            j.code_bits -= (int)(n);
            return (int)(k);
        }

        public static int stbi__jpeg_get_bit(stbi__jpeg j)
        {
            uint k;
            if ((j.code_bits) < (1)) stbi__grow_buffer_unsafe(j);
            k = (uint)(j.code_buffer);
            j.code_buffer <<= 1;
            --j.code_bits;
            return (int)(k & 0x80000000);
        }

        public static int stbi__jpeg_decode_block(stbi__jpeg j, short* data, stbi__huffman* hdc, stbi__huffman* hac,
            short* fac, int b, ushort* dequant)
        {
            int diff;
            int dc;
            int k;
            int t;
            if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
            t = (int)(stbi__jpeg_huff_decode(j, hdc));
            if ((t) < (0)) return (int)(stbi__err("bad huffman code"));
            CRuntime.memset(data, (int)(0), (ulong)(64 * sizeof(short)));
            diff = (int)((t) != 0 ? stbi__extend_receive(j, (int)(t)) : 0);
            dc = (int)(j.img_comp[b].dc_pred + diff);
            j.img_comp[b].dc_pred = (int)(dc);
            data[0] = ((short)(dc * dequant[0]));
            k = (int)(1);
            do
            {
                uint zig;
                int c;
                int r;
                int s;
                if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
                c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));
                r = (int)(fac[c]);
                if ((r) != 0)
                {
                    k += (int)((r >> 4) & 15);
                    s = (int)(r & 15);
                    j.code_buffer <<= s;
                    j.code_bits -= (int)(s);
                    zig = (uint)(stbi__jpeg_dezigzag[k++]);
                    data[zig] = ((short)((r >> 8) * dequant[zig]));
                }
                else
                {
                    int rs = (int)(stbi__jpeg_huff_decode(j, hac));
                    if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));
                    s = (int)(rs & 15);
                    r = (int)(rs >> 4);
                    if ((s) == (0))
                    {
                        if (rs != 0xf0) break;
                        k += (int)(16);
                    }
                    else
                    {
                        k += (int)(r);
                        zig = (uint)(stbi__jpeg_dezigzag[k++]);
                        data[zig] = ((short)(stbi__extend_receive(j, (int)(s)) * dequant[zig]));
                    }
                }
            } while ((k) < (64));
            return (int)(1);
        }

        public static int stbi__jpeg_decode_block_prog_dc(stbi__jpeg j, short* data, stbi__huffman* hdc, int b)
        {
            int diff;
            int dc;
            int t;
            if (j.spec_end != 0) return (int)(stbi__err("can't merge dc and ac"));
            if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
            if ((j.succ_high) == (0))
            {
                CRuntime.memset(data, (int)(0), (ulong)(64 * sizeof(short)));
                t = (int)(stbi__jpeg_huff_decode(j, hdc));
                diff = (int)((t) != 0 ? stbi__extend_receive(j, (int)(t)) : 0);
                dc = (int)(j.img_comp[b].dc_pred + diff);
                j.img_comp[b].dc_pred = (int)(dc);
                data[0] = ((short)(dc << j.succ_low));
            }
            else
            {
                if ((stbi__jpeg_get_bit(j)) != 0) data[0] += ((short)(1 << j.succ_low));
            }

            return (int)(1);
        }

        public static int stbi__jpeg_decode_block_prog_ac(stbi__jpeg j, short* data, stbi__huffman* hac, short* fac)
        {
            int k;
            if ((j.spec_start) == (0)) return (int)(stbi__err("can't merge dc and ac"));
            if ((j.succ_high) == (0))
            {
                int shift = (int)(j.succ_low);
                if ((j.eob_run) != 0)
                {
                    --j.eob_run;
                    return (int)(1);
                }
                k = (int)(j.spec_start);
                do
                {
                    uint zig;
                    int c;
                    int r;
                    int s;
                    if ((j.code_bits) < (16)) stbi__grow_buffer_unsafe(j);
                    c = (int)((j.code_buffer >> (32 - 9)) & ((1 << 9) - 1));
                    r = (int)(fac[c]);
                    if ((r) != 0)
                    {
                        k += (int)((r >> 4) & 15);
                        s = (int)(r & 15);
                        j.code_buffer <<= s;
                        j.code_bits -= (int)(s);
                        zig = (uint)(stbi__jpeg_dezigzag[k++]);
                        data[zig] = ((short)((r >> 8) << shift));
                    }
                    else
                    {
                        int rs = (int)(stbi__jpeg_huff_decode(j, hac));
                        if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));
                        s = (int)(rs & 15);
                        r = (int)(rs >> 4);
                        if ((s) == (0))
                        {
                            if ((r) < (15))
                            {
                                j.eob_run = (int)(1 << r);
                                if ((r) != 0) j.eob_run += (int)(stbi__jpeg_get_bits(j, (int)(r)));
                                --j.eob_run;
                                break;
                            }
                            k += (int)(16);
                        }
                        else
                        {
                            k += (int)(r);
                            zig = (uint)(stbi__jpeg_dezigzag[k++]);
                            data[zig] = ((short)(stbi__extend_receive(j, (int)(s)) << shift));
                        }
                    }
                } while (k <= j.spec_end);
            }
            else
            {
                short bit = (short)(1 << j.succ_low);
                if ((j.eob_run) != 0)
                {
                    --j.eob_run;
                    for (k = (int)(j.spec_start); k <= j.spec_end; ++k)
                    {
                        short* p = &data[stbi__jpeg_dezigzag[k]];
                        if (*p != 0)
                            if ((stbi__jpeg_get_bit(j)) != 0)
                                if ((*p & bit) == (0))
                                {
                                    if ((*p) > (0)) *p += (short)(bit);
                                    else *p -= (short)(bit);
                                }
                    }
                }
                else
                {
                    k = (int)(j.spec_start);
                    do
                    {
                        int r;
                        int s;
                        int rs = (int)(stbi__jpeg_huff_decode(j, hac));
                        if ((rs) < (0)) return (int)(stbi__err("bad huffman code"));
                        s = (int)(rs & 15);
                        r = (int)(rs >> 4);
                        if ((s) == (0))
                        {
                            if ((r) < (15))
                            {
                                j.eob_run = (int)((1 << r) - 1);
                                if ((r) != 0) j.eob_run += (int)(stbi__jpeg_get_bits(j, (int)(r)));
                                r = (int)(64);
                            }
                            else
                            {
                            }
                        }
                        else
                        {
                            if (s != 1) return (int)(stbi__err("bad huffman code"));
                            if ((stbi__jpeg_get_bit(j)) != 0) s = (int)(bit);
                            else s = (int)(-bit);
                        }
                        while (k <= j.spec_end)
                        {
                            short* p = &data[stbi__jpeg_dezigzag[k++]];
                            if (*p != 0)
                            {
                                if ((stbi__jpeg_get_bit(j)) != 0)
                                    if ((*p & bit) == (0))
                                    {
                                        if ((*p) > (0)) *p += (short)(bit);
                                        else *p -= (short)(bit);
                                    }
                            }
                            else
                            {
                                if ((r) == (0))
                                {
                                    *p = ((short)(s));
                                    break;
                                }
                                --r;
                            }
                        }
                    } while (k <= j.spec_end);
                }
            }

            return (int)(1);
        }

        public static byte stbi__clamp(int x)
        {
            if (((uint)(x)) > (255))
            {
                if ((x) < (0)) return (byte)(0);
                if ((x) > (255)) return (byte)(255);
            }

            return (byte)(x);
        }

        public static void stbi__idct_block(byte* _out_, int out_stride, short* data)
        {
            int i;
            int* val = stackalloc int[64];
            int* v = val;
            byte* o;
            short* d = ((short*)data);
            for (i = (int)(0); (i) < (8); ++i, ++d, ++v)
            {
                if ((((((((d[8]) == (0)) && ((d[16]) == (0))) && ((d[24]) == (0))) && ((d[32]) == (0))) && ((d[40]) == (0))) &&
                     ((d[48]) == (0))) && ((d[56]) == (0)))
                {
                    int dcterm = (int)(d[0] << 2);
                    v[0] =
                        (int)
                            (v[8] =
                                (int)(v[16] = (int)(v[24] = (int)(v[32] = (int)(v[40] = (int)(v[48] = (int)(v[56] = (int)(dcterm))))))));
                }
                else
                {
                    int t0;
                    int t1;
                    int t2;
                    int t3;
                    int p1;
                    int p2;
                    int p3;
                    int p4;
                    int p5;
                    int x0;
                    int x1;
                    int x2;
                    int x3;
                    p2 = (int)(d[16]);
                    p3 = (int)(d[48]);
                    p1 = (int)((p2 + p3) * ((int)((0.5411961f) * 4096 + 0.5)));
                    t2 = (int)(p1 + p3 * ((int)((-1.847759065f) * 4096 + 0.5)));
                    t3 = (int)(p1 + p2 * ((int)((0.765366865f) * 4096 + 0.5)));
                    p2 = (int)(d[0]);
                    p3 = (int)(d[32]);
                    t0 = (int)((p2 + p3) << 12);
                    t1 = (int)((p2 - p3) << 12);
                    x0 = (int)(t0 + t3);
                    x3 = (int)(t0 - t3);
                    x1 = (int)(t1 + t2);
                    x2 = (int)(t1 - t2);
                    t0 = (int)(d[56]);
                    t1 = (int)(d[40]);
                    t2 = (int)(d[24]);
                    t3 = (int)(d[8]);
                    p3 = (int)(t0 + t2);
                    p4 = (int)(t1 + t3);
                    p1 = (int)(t0 + t3);
                    p2 = (int)(t1 + t2);
                    p5 = (int)((p3 + p4) * ((int)((1.175875602f) * 4096 + 0.5)));
                    t0 = (int)(t0 * ((int)((0.298631336f) * 4096 + 0.5)));
                    t1 = (int)(t1 * ((int)((2.053119869f) * 4096 + 0.5)));
                    t2 = (int)(t2 * ((int)((3.072711026f) * 4096 + 0.5)));
                    t3 = (int)(t3 * ((int)((1.501321110f) * 4096 + 0.5)));
                    p1 = (int)(p5 + p1 * ((int)((-0.899976223f) * 4096 + 0.5)));
                    p2 = (int)(p5 + p2 * ((int)((-2.562915447f) * 4096 + 0.5)));
                    p3 = (int)(p3 * ((int)((-1.961570560f) * 4096 + 0.5)));
                    p4 = (int)(p4 * ((int)((-0.390180644f) * 4096 + 0.5)));
                    t3 += (int)(p1 + p4);
                    t2 += (int)(p2 + p3);
                    t1 += (int)(p2 + p4);
                    t0 += (int)(p1 + p3);
                    x0 += (int)(512);
                    x1 += (int)(512);
                    x2 += (int)(512);
                    x3 += (int)(512);
                    v[0] = (int)((x0 + t3) >> 10);
                    v[56] = (int)((x0 - t3) >> 10);
                    v[8] = (int)((x1 + t2) >> 10);
                    v[48] = (int)((x1 - t2) >> 10);
                    v[16] = (int)((x2 + t1) >> 10);
                    v[40] = (int)((x2 - t1) >> 10);
                    v[24] = (int)((x3 + t0) >> 10);
                    v[32] = (int)((x3 - t0) >> 10);
                }
            }
            for (i = (int)(0), v = val, o = _out_; (i) < (8); ++i, v += 8, o += out_stride)
            {
                int t0;
                int t1;
                int t2;
                int t3;
                int p1;
                int p2;
                int p3;
                int p4;
                int p5;
                int x0;
                int x1;
                int x2;
                int x3;
                p2 = (int)(v[2]);
                p3 = (int)(v[6]);
                p1 = (int)((p2 + p3) * ((int)((0.5411961f) * 4096 + 0.5)));
                t2 = (int)(p1 + p3 * ((int)((-1.847759065f) * 4096 + 0.5)));
                t3 = (int)(p1 + p2 * ((int)((0.765366865f) * 4096 + 0.5)));
                p2 = (int)(v[0]);
                p3 = (int)(v[4]);
                t0 = (int)((p2 + p3) << 12);
                t1 = (int)((p2 - p3) << 12);
                x0 = (int)(t0 + t3);
                x3 = (int)(t0 - t3);
                x1 = (int)(t1 + t2);
                x2 = (int)(t1 - t2);
                t0 = (int)(v[7]);
                t1 = (int)(v[5]);
                t2 = (int)(v[3]);
                t3 = (int)(v[1]);
                p3 = (int)(t0 + t2);
                p4 = (int)(t1 + t3);
                p1 = (int)(t0 + t3);
                p2 = (int)(t1 + t2);
                p5 = (int)((p3 + p4) * ((int)((1.175875602f) * 4096 + 0.5)));
                t0 = (int)(t0 * ((int)((0.298631336f) * 4096 + 0.5)));
                t1 = (int)(t1 * ((int)((2.053119869f) * 4096 + 0.5)));
                t2 = (int)(t2 * ((int)((3.072711026f) * 4096 + 0.5)));
                t3 = (int)(t3 * ((int)((1.501321110f) * 4096 + 0.5)));
                p1 = (int)(p5 + p1 * ((int)((-0.899976223f) * 4096 + 0.5)));
                p2 = (int)(p5 + p2 * ((int)((-2.562915447f) * 4096 + 0.5)));
                p3 = (int)(p3 * ((int)((-1.961570560f) * 4096 + 0.5)));
                p4 = (int)(p4 * ((int)((-0.390180644f) * 4096 + 0.5)));
                t3 += (int)(p1 + p4);
                t2 += (int)(p2 + p3);
                t1 += (int)(p2 + p4);
                t0 += (int)(p1 + p3);
                x0 += (int)(65536 + (128 << 17));
                x1 += (int)(65536 + (128 << 17));
                x2 += (int)(65536 + (128 << 17));
                x3 += (int)(65536 + (128 << 17));
                o[0] = (byte)(stbi__clamp((int)((x0 + t3) >> 17)));
                o[7] = (byte)(stbi__clamp((int)((x0 - t3) >> 17)));
                o[1] = (byte)(stbi__clamp((int)((x1 + t2) >> 17)));
                o[6] = (byte)(stbi__clamp((int)((x1 - t2) >> 17)));
                o[2] = (byte)(stbi__clamp((int)((x2 + t1) >> 17)));
                o[5] = (byte)(stbi__clamp((int)((x2 - t1) >> 17)));
                o[3] = (byte)(stbi__clamp((int)((x3 + t0) >> 17)));
                o[4] = (byte)(stbi__clamp((int)((x3 - t0) >> 17)));
            }
        }

        public static byte stbi__get_marker(stbi__jpeg j)
        {
            byte x;
            if (j.marker != 0xff)
            {
                x = (byte)(j.marker);
                j.marker = (byte)(0xff);
                return (byte)(x);
            }

            x = (byte)(stbi__get8(j.s));
            if (x != 0xff) return (byte)(0xff);
            while ((x) == (0xff))
            {
                x = (byte)(stbi__get8(j.s));
            }
            return (byte)(x);
        }

        public static void stbi__jpeg_reset(stbi__jpeg j)
        {
            j.code_bits = (int)(0);
            j.code_buffer = (uint)(0);
            j.nomore = (int)(0);
            j.img_comp[0].dc_pred =
                (int)(j.img_comp[1].dc_pred = (int)(j.img_comp[2].dc_pred = (int)(j.img_comp[3].dc_pred = (int)(0))));
            j.marker = (byte)(0xff);
            j.todo = (int)((j.restart_interval) != 0 ? j.restart_interval : 0x7fffffff);
            j.eob_run = (int)(0);
        }

        public static int stbi__parse_entropy_coded_data(stbi__jpeg z)
        {
            stbi__jpeg_reset(z);
            if (z.progressive == 0)
            {
                if ((z.scan_n) == (1))
                {
                    int i;
                    int j;
                    short* data = stackalloc short[64];
                    int n = (int)(z.order[0]);
                    int w = (int)((z.img_comp[n].x + 7) >> 3);
                    int h = (int)((z.img_comp[n].y + 7) >> 3);
                    for (j = (int)(0); (j) < (h); ++j)
                    {
                        for (i = (int)(0); (i) < (w); ++i)
                        {
                            int ha = (int)(z.img_comp[n].ha);
                            if (
                                stbi__jpeg_decode_block(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd, (stbi__huffman*)z.huff_ac + ha,
                                    z.fast_ac[ha], (int)(n), (ushort*)z.dequant[z.img_comp[n].tq]) == 0) return (int)(0);
                            z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * j * 8 + i * 8, (int)(z.img_comp[n].w2), data);
                            if (--z.todo <= 0)
                            {
                                if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);
                                if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);
                                stbi__jpeg_reset(z);
                            }
                        }
                    }
                    return (int)(1);
                }
                else
                {
                    int i;
                    int j;
                    int k;
                    int x;
                    int y;
                    short* data = stackalloc short[64];
                    for (j = (int)(0); (j) < (z.img_mcu_y); ++j)
                    {
                        for (i = (int)(0); (i) < (z.img_mcu_x); ++i)
                        {
                            for (k = (int)(0); (k) < (z.scan_n); ++k)
                            {
                                int n = (int)(z.order[k]);
                                for (y = (int)(0); (y) < (z.img_comp[n].v); ++y)
                                {
                                    for (x = (int)(0); (x) < (z.img_comp[n].h); ++x)
                                    {
                                        int x2 = (int)((i * z.img_comp[n].h + x) * 8);
                                        int y2 = (int)((j * z.img_comp[n].v + y) * 8);
                                        int ha = (int)(z.img_comp[n].ha);
                                        if (
                                            stbi__jpeg_decode_block(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd,
                                                (stbi__huffman*)z.huff_ac + ha, z.fast_ac[ha], (int)(n), (ushort*)z.dequant[z.img_comp[n].tq]) == 0)
                                            return (int)(0);
                                        z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * y2 + x2, (int)(z.img_comp[n].w2), data);
                                    }
                                }
                            }
                            if (--z.todo <= 0)
                            {
                                if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);
                                if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);
                                stbi__jpeg_reset(z);
                            }
                        }
                    }
                    return (int)(1);
                }
            }
            else
            {
                if ((z.scan_n) == (1))
                {
                    int i;
                    int j;
                    int n = (int)(z.order[0]);
                    int w = (int)((z.img_comp[n].x + 7) >> 3);
                    int h = (int)((z.img_comp[n].y + 7) >> 3);
                    for (j = (int)(0); (j) < (h); ++j)
                    {
                        for (i = (int)(0); (i) < (w); ++i)
                        {
                            short* data = z.img_comp[n].coeff + 64 * (i + j * z.img_comp[n].coeff_w);
                            if ((z.spec_start) == (0))
                            {
                                if (stbi__jpeg_decode_block_prog_dc(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd, (int)(n)) == 0)
                                    return (int)(0);
                            }
                            else
                            {
                                int ha = (int)(z.img_comp[n].ha);
                                if (stbi__jpeg_decode_block_prog_ac(z, data, (stbi__huffman*)z.huff_ac + ha, z.fast_ac[ha]) == 0)
                                    return (int)(0);
                            }
                            if (--z.todo <= 0)
                            {
                                if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);
                                if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);
                                stbi__jpeg_reset(z);
                            }
                        }
                    }
                    return (int)(1);
                }
                else
                {
                    int i;
                    int j;
                    int k;
                    int x;
                    int y;
                    for (j = (int)(0); (j) < (z.img_mcu_y); ++j)
                    {
                        for (i = (int)(0); (i) < (z.img_mcu_x); ++i)
                        {
                            for (k = (int)(0); (k) < (z.scan_n); ++k)
                            {
                                int n = (int)(z.order[k]);
                                for (y = (int)(0); (y) < (z.img_comp[n].v); ++y)
                                {
                                    for (x = (int)(0); (x) < (z.img_comp[n].h); ++x)
                                    {
                                        int x2 = (int)(i * z.img_comp[n].h + x);
                                        int y2 = (int)(j * z.img_comp[n].v + y);
                                        short* data = z.img_comp[n].coeff + 64 * (x2 + y2 * z.img_comp[n].coeff_w);
                                        if (stbi__jpeg_decode_block_prog_dc(z, data, (stbi__huffman*)z.huff_dc + z.img_comp[n].hd, (int)(n)) == 0)
                                            return (int)(0);
                                    }
                                }
                            }
                            if (--z.todo <= 0)
                            {
                                if ((z.code_bits) < (24)) stbi__grow_buffer_unsafe(z);
                                if (!(((z.marker) >= (0xd0)) && ((z.marker) <= 0xd7))) return (int)(1);
                                stbi__jpeg_reset(z);
                            }
                        }
                    }
                    return (int)(1);
                }
            }
        }

        public static void stbi__jpeg_dequantize(short* data, ushort* dequant)
        {
            int i;
            for (i = (int)(0); (i) < (64); ++i)
            {
                data[i] *= (short)(dequant[i]);
            }
        }

        public static void stbi__jpeg_finish(stbi__jpeg z)
        {
            if ((z.progressive) != 0)
            {
                int i;
                int j;
                int n;
                for (n = (int)(0); (n) < (z.s.img_n); ++n)
                {
                    int w = (int)((z.img_comp[n].x + 7) >> 3);
                    int h = (int)((z.img_comp[n].y + 7) >> 3);
                    for (j = (int)(0); (j) < (h); ++j)
                    {
                        for (i = (int)(0); (i) < (w); ++i)
                        {
                            short* data = z.img_comp[n].coeff + 64 * (i + j * z.img_comp[n].coeff_w);
                            stbi__jpeg_dequantize(data, (ushort*)z.dequant[z.img_comp[n].tq]);
                            z.idct_block_kernel(z.img_comp[n].data + z.img_comp[n].w2 * j * 8 + i * 8, (int)(z.img_comp[n].w2), data);
                        }
                    }
                }
            }
        }

        public static int stbi__process_marker(stbi__jpeg z, int m)
        {
            int L;
            switch (m)
            {
                case 0xff:
                    return (int)(stbi__err("expected marker"));

                case 0xDD:
                    if (stbi__get16be(z.s) != 4) return (int)(stbi__err("bad DRI len"));
                    z.restart_interval = (int)(stbi__get16be(z.s));
                    return (int)(1);

                case 0xDB:
                    L = (int)(stbi__get16be(z.s) - 2);
                    while ((L) > (0))
                    {
                        int q = (int)(stbi__get8(z.s));
                        int p = (int)(q >> 4);
                        int sixteen = (p != 0) ? 1 : 0;
                        int t = (int)(q & 15);
                        int i;
                        if ((p != 0) && (p != 1)) return (int)(stbi__err("bad DQT type"));
                        if ((t) > (3)) return (int)(stbi__err("bad DQT table"));
                        for (i = (int)(0); (i) < (64); ++i)
                        {
                            z.dequant[t][stbi__jpeg_dezigzag[i]] = ((ushort)((sixteen) != 0 ? stbi__get16be(z.s) : stbi__get8(z.s)));
                        }
                        L -= (int)((sixteen) != 0 ? 129 : 65);
                    }
                    return (int)((L) == (0) ? 1 : 0);

                case 0xC4:
                    L = (int)(stbi__get16be(z.s) - 2);
                    while ((L) > (0))
                    {
                        byte* v;
                        int* sizes = stackalloc int[16];
                        int i;
                        int n = (int)(0);
                        int q = (int)(stbi__get8(z.s));
                        int tc = (int)(q >> 4);
                        int th = (int)(q & 15);
                        if (((tc) > (1)) || ((th) > (3))) return (int)(stbi__err("bad DHT header"));
                        for (i = (int)(0); (i) < (16); ++i)
                        {
                            sizes[i] = (int)(stbi__get8(z.s));
                            n += (int)(sizes[i]);
                        }
                        L -= (int)(17);
                        if ((tc) == (0))
                        {
                            if (stbi__build_huffman((stbi__huffman*)z.huff_dc + th, sizes) == 0) return (int)(0);
                            stbi__huffman* h = (stbi__huffman*)z.huff_dc + th;
                            v = h->values;
                        }
                        else
                        {
                            if (stbi__build_huffman((stbi__huffman*)z.huff_ac + th, sizes) == 0) return (int)(0);
                            stbi__huffman* h = (stbi__huffman*)z.huff_ac + th;
                            v = h->values;
                        }
                        for (i = (int)(0); (i) < (n); ++i)
                        {
                            v[i] = (byte)(stbi__get8(z.s));
                        }
                        if (tc != 0) stbi__build_fast_ac(z.fast_ac[th], (stbi__huffman*)z.huff_ac + th);
                        L -= (int)(n);
                    }
                    return (int)((L) == (0) ? 1 : 0);
            }

            if ((((m) >= (0xE0)) && (m <= 0xEF)) || ((m) == (0xFE)))
            {
                L = (int)(stbi__get16be(z.s));
                if ((L) < (2))
                {
                    if ((m) == (0xFE)) return (int)(stbi__err("bad COM len"));
                    else return (int)(stbi__err("bad APP len"));
                }
                L -= (int)(2);
                if (((m) == (0xE0)) && ((L) >= (5)))
                {
                    byte* tag = stackalloc byte[5];
                    tag[0] = (byte)('J');
                    tag[1] = (byte)('F');
                    tag[2] = (byte)('I');
                    tag[3] = (byte)('F');
                    tag[4] = (byte)('\0');
                    int ok = (int)(1);
                    int i;
                    for (i = (int)(0); (i) < (5); ++i)
                    {
                        if (stbi__get8(z.s) != tag[i]) ok = (int)(0);
                    }
                    L -= (int)(5);
                    if ((ok) != 0) z.jfif = (int)(1);
                }
                else if (((m) == (0xEE)) && ((L) >= (12)))
                {
                    byte* tag = stackalloc byte[6];
                    tag[0] = (byte)('A');
                    tag[1] = (byte)('d');
                    tag[2] = (byte)('o');
                    tag[3] = (byte)('b');
                    tag[4] = (byte)('e');
                    tag[5] = (byte)('\0');
                    int ok = (int)(1);
                    int i;
                    for (i = (int)(0); (i) < (6); ++i)
                    {
                        if (stbi__get8(z.s) != tag[i]) ok = (int)(0);
                    }
                    L -= (int)(6);
                    if ((ok) != 0)
                    {
                        stbi__get8(z.s);
                        stbi__get16be(z.s);
                        stbi__get16be(z.s);
                        z.app14_color_transform = (int)(stbi__get8(z.s));
                        L -= (int)(6);
                    }
                }
                stbi__skip(z.s, (int)(L));
                return (int)(1);
            }

            return (int)(stbi__err("unknown marker"));
        }

        public static int stbi__process_scan_header(stbi__jpeg z)
        {
            int i;
            int Ls = (int)(stbi__get16be(z.s));
            z.scan_n = (int)(stbi__get8(z.s));
            if ((((z.scan_n) < (1)) || ((z.scan_n) > (4))) || ((z.scan_n) > (z.s.img_n)))
                return (int)(stbi__err("bad SOS component count"));
            if (Ls != 6 + 2 * z.scan_n) return (int)(stbi__err("bad SOS len"));
            for (i = (int)(0); (i) < (z.scan_n); ++i)
            {
                int id = (int)(stbi__get8(z.s));
                int which;
                int q = (int)(stbi__get8(z.s));
                for (which = (int)(0); (which) < (z.s.img_n); ++which)
                {
                    if ((z.img_comp[which].id) == (id)) break;
                }
                if ((which) == (z.s.img_n)) return (int)(0);
                z.img_comp[which].hd = (int)(q >> 4);
                if ((z.img_comp[which].hd) > (3)) return (int)(stbi__err("bad DC huff"));
                z.img_comp[which].ha = (int)(q & 15);
                if ((z.img_comp[which].ha) > (3)) return (int)(stbi__err("bad AC huff"));
                z.order[i] = (int)(which);
            }
            {
                int aa;
                z.spec_start = (int)(stbi__get8(z.s));
                z.spec_end = (int)(stbi__get8(z.s));
                aa = (int)(stbi__get8(z.s));
                z.succ_high = (int)(aa >> 4);
                z.succ_low = (int)(aa & 15);
                if ((z.progressive) != 0)
                {
                    if ((((((z.spec_start) > (63)) || ((z.spec_end) > (63))) || ((z.spec_start) > (z.spec_end))) ||
                         ((z.succ_high) > (13))) || ((z.succ_low) > (13))) return (int)(stbi__err("bad SOS"));
                }
                else
                {
                    if (z.spec_start != 0) return (int)(stbi__err("bad SOS"));
                    if ((z.succ_high != 0) || (z.succ_low != 0)) return (int)(stbi__err("bad SOS"));
                    z.spec_end = (int)(63);
                }
            }

            return (int)(1);
        }

        public static int stbi__free_jpeg_components(stbi__jpeg z, int ncomp, int why)
        {
            int i;
            for (i = (int)(0); (i) < (ncomp); ++i)
            {
                if ((z.img_comp[i].raw_data) != null)
                {
                    CRuntime.free(z.img_comp[i].raw_data);
                    z.img_comp[i].raw_data = (null);
                    z.img_comp[i].data = (null);
                }
                if ((z.img_comp[i].raw_coeff) != null)
                {
                    CRuntime.free(z.img_comp[i].raw_coeff);
                    z.img_comp[i].raw_coeff = null;
                    z.img_comp[i].coeff = null;
                }
                if ((z.img_comp[i].linebuf) != null)
                {
                    CRuntime.free(z.img_comp[i].linebuf);
                    z.img_comp[i].linebuf = (null);
                }
            }
            return (int)(why);
        }

        public static int stbi__process_frame_header(stbi__jpeg z, int scan)
        {
            stbi__context s = z.s;
            int Lf;
            int p;
            int i;
            int q;
            int h_max = (int)(1);
            int v_max = (int)(1);
            int c;
            Lf = (int)(stbi__get16be(s));
            if ((Lf) < (11)) return (int)(stbi__err("bad SOF len"));
            p = (int)(stbi__get8(s));
            if (p != 8) return (int)(stbi__err("only 8-bit"));
            s.img_y = (uint)(stbi__get16be(s));
            if ((s.img_y) == (0)) return (int)(stbi__err("no header height"));
            s.img_x = (uint)(stbi__get16be(s));
            if ((s.img_x) == (0)) return (int)(stbi__err("0 width"));
            c = (int)(stbi__get8(s));
            if (((c != 3) && (c != 1)) && (c != 4)) return (int)(stbi__err("bad component count"));
            s.img_n = (int)(c);
            for (i = (int)(0); (i) < (c); ++i)
            {
                z.img_comp[i].data = (null);
                z.img_comp[i].linebuf = (null);
            }
            if (Lf != 8 + 3 * s.img_n) return (int)(stbi__err("bad SOF len"));
            z.rgb = (int)(0);
            for (i = (int)(0); (i) < (s.img_n); ++i)
            {
                byte* rgb = stackalloc byte[3];
                rgb[0] = (byte)('R');
                rgb[1] = (byte)('G');
                rgb[2] = (byte)('B');
                z.img_comp[i].id = (int)(stbi__get8(s));
                if (((s.img_n) == (3)) && ((z.img_comp[i].id) == (rgb[i]))) ++z.rgb;
                q = (int)(stbi__get8(s));
                z.img_comp[i].h = (int)(q >> 4);
                if ((z.img_comp[i].h == 0) || ((z.img_comp[i].h) > (4))) return (int)(stbi__err("bad H"));
                z.img_comp[i].v = (int)(q & 15);
                if ((z.img_comp[i].v == 0) || ((z.img_comp[i].v) > (4))) return (int)(stbi__err("bad V"));
                z.img_comp[i].tq = (int)(stbi__get8(s));
                if ((z.img_comp[i].tq) > (3)) return (int)(stbi__err("bad TQ"));
            }
            if (scan != STBI__SCAN_load) return (int)(1);
            if (stbi__mad3sizes_valid((int)(s.img_x), (int)(s.img_y), (int)(s.img_n), (int)(0)) == 0)
                return (int)(stbi__err("too large"));
            for (i = (int)(0); (i) < (s.img_n); ++i)
            {
                if ((z.img_comp[i].h) > (h_max)) h_max = (int)(z.img_comp[i].h);
                if ((z.img_comp[i].v) > (v_max)) v_max = (int)(z.img_comp[i].v);
            }
            z.img_h_max = (int)(h_max);
            z.img_v_max = (int)(v_max);
            z.img_mcu_w = (int)(h_max * 8);
            z.img_mcu_h = (int)(v_max * 8);
            z.img_mcu_x = (int)((s.img_x + z.img_mcu_w - 1) / z.img_mcu_w);
            z.img_mcu_y = (int)((s.img_y + z.img_mcu_h - 1) / z.img_mcu_h);
            for (i = (int)(0); (i) < (s.img_n); ++i)
            {
                z.img_comp[i].x = (int)((s.img_x * z.img_comp[i].h + h_max - 1) / h_max);
                z.img_comp[i].y = (int)((s.img_y * z.img_comp[i].v + v_max - 1) / v_max);
                z.img_comp[i].w2 = (int)(z.img_mcu_x * z.img_comp[i].h * 8);
                z.img_comp[i].h2 = (int)(z.img_mcu_y * z.img_comp[i].v * 8);
                z.img_comp[i].coeff = null;
                z.img_comp[i].raw_coeff = null;
                z.img_comp[i].linebuf = (null);
                z.img_comp[i].raw_data = stbi__malloc_mad2((int)(z.img_comp[i].w2), (int)(z.img_comp[i].h2), (int)(15));
                if ((z.img_comp[i].raw_data) == (null))
                    return (int)(stbi__free_jpeg_components(z, (int)(i + 1), (int)(stbi__err("outofmem"))));
                z.img_comp[i].data = (byte*)((((long)z.img_comp[i].raw_data + 15) & ~15));
                if ((z.progressive) != 0)
                {
                    z.img_comp[i].coeff_w = (int)(z.img_comp[i].w2 / 8);
                    z.img_comp[i].coeff_h = (int)(z.img_comp[i].h2 / 8);
                    z.img_comp[i].raw_coeff = stbi__malloc_mad3((int)(z.img_comp[i].w2), (int)(z.img_comp[i].h2), (int)(2),
                        (int)(15));
                    if ((z.img_comp[i].raw_coeff) == (null))
                        return (int)(stbi__free_jpeg_components(z, (int)(i + 1), (int)(stbi__err("outofmem"))));
                    z.img_comp[i].coeff = (short*)((((long)z.img_comp[i].raw_coeff + 15) & ~15));
                }
            }
            return (int)(1);
        }

        public static int stbi__decode_jpeg_header(stbi__jpeg z, int scan)
        {
            int m;
            z.jfif = (int)(0);
            z.app14_color_transform = (int)(-1);
            z.marker = (byte)(0xff);
            m = (int)(stbi__get_marker(z));
            if (!((m) == (0xd8))) return (int)(stbi__err("no SOI"));
            if ((scan) == (STBI__SCAN_type)) return (int)(1);
            m = (int)(stbi__get_marker(z));
            while (!((((m) == (0xc0)) || ((m) == (0xc1))) || ((m) == (0xc2))))
            {
                if (stbi__process_marker(z, (int)(m)) == 0) return (int)(0);
                m = (int)(stbi__get_marker(z));
                while ((m) == (0xff))
                {
                    if ((stbi__at_eof(z.s)) != 0) return (int)(stbi__err("no SOF"));
                    m = (int)(stbi__get_marker(z));
                }
            }
            z.progressive = (int)((m) == (0xc2) ? 1 : 0);
            if (stbi__process_frame_header(z, (int)(scan)) == 0) return (int)(0);
            return (int)(1);
        }

        public static int stbi__decode_jpeg_image(stbi__jpeg j)
        {
            int m;
            for (m = (int)(0); (m) < (4); m++)
            {
                j.img_comp[m].raw_data = (null);
                j.img_comp[m].raw_coeff = (null);
            }
            j.restart_interval = (int)(0);
            if (stbi__decode_jpeg_header(j, (int)(STBI__SCAN_load)) == 0) return (int)(0);
            m = (int)(stbi__get_marker(j));
            while (!((m) == (0xd9)))
            {
                if (((m) == (0xda)))
                {
                    if (stbi__process_scan_header(j) == 0) return (int)(0);
                    if (stbi__parse_entropy_coded_data(j) == 0) return (int)(0);
                    if ((j.marker) == (0xff))
                    {
                        while (stbi__at_eof(j.s) == 0)
                        {
                            int x = (int)(stbi__get8(j.s));
                            if ((x) == (255))
                            {
                                j.marker = (byte)(stbi__get8(j.s));
                                break;
                            }
                        }
                    }
                }
                else if (((m) == (0xdc)))
                {
                    int Ld = (int)(stbi__get16be(j.s));
                    uint NL = (uint)(stbi__get16be(j.s));
                    if (Ld != 4) stbi__err("bad DNL len");
                    if (NL != j.s.img_y) stbi__err("bad DNL height");
                }
                else
                {
                    if (stbi__process_marker(j, (int)(m)) == 0) return (int)(0);
                }
                m = (int)(stbi__get_marker(j));
            }
            if ((j.progressive) != 0) stbi__jpeg_finish(j);
            return (int)(1);
        }

        public static byte* resample_row_1(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
        {
            return in_near;
        }

        public static byte* stbi__resample_row_v_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
        {
            int i;
            for (i = (int)(0); (i) < (w); ++i)
            {
                _out_[i] = ((byte)((3 * in_near[i] + in_far[i] + 2) >> 2));
            }
            return _out_;
        }

        public static byte* stbi__resample_row_h_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
        {
            int i;
            byte* input = in_near;
            if ((w) == (1))
            {
                _out_[0] = (byte)(_out_[1] = (byte)(input[0]));
                return _out_;
            }

            _out_[0] = (byte)(input[0]);
            _out_[1] = ((byte)((input[0] * 3 + input[1] + 2) >> 2));
            for (i = (int)(1); (i) < (w - 1); ++i)
            {
                int n = (int)(3 * input[i] + 2);
                _out_[i * 2 + 0] = ((byte)((n + input[i - 1]) >> 2));
                _out_[i * 2 + 1] = ((byte)((n + input[i + 1]) >> 2));
            }
            _out_[i * 2 + 0] = ((byte)((input[w - 2] * 3 + input[w - 1] + 2) >> 2));
            _out_[i * 2 + 1] = (byte)(input[w - 1]);
            return _out_;
        }

        public static byte* stbi__resample_row_hv_2(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
        {
            int i;
            int t0;
            int t1;
            if ((w) == (1))
            {
                _out_[0] = (byte)(_out_[1] = ((byte)((3 * in_near[0] + in_far[0] + 2) >> 2)));
                return _out_;
            }

            t1 = (int)(3 * in_near[0] + in_far[0]);
            _out_[0] = ((byte)((t1 + 2) >> 2));
            for (i = (int)(1); (i) < (w); ++i)
            {
                t0 = (int)(t1);
                t1 = (int)(3 * in_near[i] + in_far[i]);
                _out_[i * 2 - 1] = ((byte)((3 * t0 + t1 + 8) >> 4));
                _out_[i * 2] = ((byte)((3 * t1 + t0 + 8) >> 4));
            }
            _out_[w * 2 - 1] = ((byte)((t1 + 2) >> 2));
            return _out_;
        }

        public static byte* stbi__resample_row_generic(byte* _out_, byte* in_near, byte* in_far, int w, int hs)
        {
            int i;
            int j;
            for (i = (int)(0); (i) < (w); ++i)
            {
                for (j = (int)(0); (j) < (hs); ++j)
                {
                    _out_[i * hs + j] = (byte)(in_near[i]);
                }
            }
            return _out_;
        }

        public static void stbi__YCbCr_to_RGB_row(byte* _out_, byte* y, byte* pcb, byte* pcr, int count, int step)
        {
            int i;
            for (i = (int)(0); (i) < (count); ++i)
            {
                int y_fixed = (int)((y[i] << 20) + (1 << 19));
                int r;
                int g;
                int b;
                int cr = (int)(pcr[i] - 128);
                int cb = (int)(pcb[i] - 128);
                r = (int)(y_fixed + cr * (((int)((1.40200f) * 4096.0f + 0.5f)) << 8));
                g =
                    (int)
                        (y_fixed + (cr * -(((int)((0.71414f) * 4096.0f + 0.5f)) << 8)) +
                         ((cb * -(((int)((0.34414f) * 4096.0f + 0.5f)) << 8)) & 0xffff0000));
                b = (int)(y_fixed + cb * (((int)((1.77200f) * 4096.0f + 0.5f)) << 8));
                r >>= 20;
                g >>= 20;
                b >>= 20;
                if (((uint)(r)) > (255))
                {
                    if ((r) < (0)) r = (int)(0);
                    else r = (int)(255);
                }
                if (((uint)(g)) > (255))
                {
                    if ((g) < (0)) g = (int)(0);
                    else g = (int)(255);
                }
                if (((uint)(b)) > (255))
                {
                    if ((b) < (0)) b = (int)(0);
                    else b = (int)(255);
                }
                _out_[0] = ((byte)(r));
                _out_[1] = ((byte)(g));
                _out_[2] = ((byte)(b));
                _out_[3] = (byte)(255);
                _out_ += step;
            }
        }

        public static void stbi__setup_jpeg(stbi__jpeg j)
        {
            j.idct_block_kernel = stbi__idct_block;
            j.YCbCr_to_RGB_kernel = stbi__YCbCr_to_RGB_row;
            j.resample_row_hv_2_kernel = stbi__resample_row_hv_2;
        }

        public static void stbi__cleanup_jpeg(stbi__jpeg j)
        {
            stbi__free_jpeg_components(j, (int)(j.s.img_n), (int)(0));
        }

        public static byte stbi__blinn_8x8(byte x, byte y)
        {
            uint t = (uint)(x * y + 128);
            return (byte)((t + (t >> 8)) >> 8);
        }

        public static byte* load_jpeg_image(stbi__jpeg z, int* out_x, int* out_y, int* comp, int req_comp)
        {
            int n;
            int decode_n;
            int is_rgb;
            z.s.img_n = (int)(0);
            if (((req_comp) < (0)) || ((req_comp) > (4)))
                return ((byte*)((ulong)((stbi__err("bad req_comp")) != 0 ? ((byte*)null) : (null))));
            if (stbi__decode_jpeg_image(z) == 0)
            {
                stbi__cleanup_jpeg(z);
                return (null);
            }

            n = (int)((req_comp) != 0 ? req_comp : (z.s.img_n) >= (3) ? 3 : 1);
            is_rgb =
                (int)(((z.s.img_n) == (3)) && (((z.rgb) == (3)) || (((z.app14_color_transform) == (0)) && (z.jfif == 0))) ? 1 : 0);
            if ((((z.s.img_n) == (3)) && ((n) < (3))) && (is_rgb == 0)) decode_n = (int)(1);
            else decode_n = (int)(z.s.img_n);
            {
                int k;
                uint i;
                uint j;
                byte* output;
                byte** coutput = stackalloc byte*[4];
                var res_comp = new stbi__resample[4];
                for (var kkk = 0; kkk < res_comp.Length; ++kkk) res_comp[kkk] = new stbi__resample();
                for (k = (int)(0); (k) < (decode_n); ++k)
                {
                    stbi__resample r = res_comp[k];
                    z.img_comp[k].linebuf = (byte*)(stbi__malloc((ulong)(z.s.img_x + 3)));
                    if (z.img_comp[k].linebuf == null)
                    {
                        stbi__cleanup_jpeg(z);
                        return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
                    }
                    r.hs = (int)(z.img_h_max / z.img_comp[k].h);
                    r.vs = (int)(z.img_v_max / z.img_comp[k].v);
                    r.ystep = (int)(r.vs >> 1);
                    r.w_lores = (int)((z.s.img_x + r.hs - 1) / r.hs);
                    r.ypos = (int)(0);
                    r.line0 = r.line1 = z.img_comp[k].data;
                    if (((r.hs) == (1)) && ((r.vs) == (1))) r.resample = resample_row_1;
                    else if (((r.hs) == (1)) && ((r.vs) == (2))) r.resample = stbi__resample_row_v_2;
                    else if (((r.hs) == (2)) && ((r.vs) == (1))) r.resample = stbi__resample_row_h_2;
                    else if (((r.hs) == (2)) && ((r.vs) == (2))) r.resample = z.resample_row_hv_2_kernel;
                    else r.resample = stbi__resample_row_generic;
                }
                output = (byte*)(stbi__malloc_mad3((int)(n), (int)(z.s.img_x), (int)(z.s.img_y), (int)(1)));
                if (output == null)
                {
                    stbi__cleanup_jpeg(z);
                    return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
                }
                for (j = (uint)(0); (j) < (z.s.img_y); ++j)
                {
                    byte* _out_ = output + n * z.s.img_x * j;
                    for (k = (int)(0); (k) < (decode_n); ++k)
                    {
                        stbi__resample r = res_comp[k];
                        int y_bot = (int)((r.ystep) >= (r.vs >> 1) ? 1 : 0);
                        coutput[k] = r.resample(z.img_comp[k].linebuf, (y_bot) != 0 ? r.line1 : r.line0, (y_bot) != 0 ? r.line0 : r.line1,
                            (int)(r.w_lores), (int)(r.hs));
                        if ((++r.ystep) >= (r.vs))
                        {
                            r.ystep = (int)(0);
                            r.line0 = r.line1;
                            if ((++r.ypos) < (z.img_comp[k].y)) r.line1 += z.img_comp[k].w2;
                        }
                    }
                    if ((n) >= (3))
                    {
                        byte* y = coutput[0];
                        if ((z.s.img_n) == (3))
                        {
                            if ((is_rgb) != 0)
                            {
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    _out_[0] = (byte)(y[i]);
                                    _out_[1] = (byte)(coutput[1][i]);
                                    _out_[2] = (byte)(coutput[2][i]);
                                    _out_[3] = (byte)(255);
                                    _out_ += n;
                                }
                            }
                            else
                            {
                                z.YCbCr_to_RGB_kernel(_out_, y, coutput[1], coutput[2], (int)(z.s.img_x), (int)(n));
                            }
                        }
                        else if ((z.s.img_n) == (4))
                        {
                            if ((z.app14_color_transform) == (0))
                            {
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    byte m = (byte)(coutput[3][i]);
                                    _out_[0] = (byte)(stbi__blinn_8x8((byte)(coutput[0][i]), (byte)(m)));
                                    _out_[1] = (byte)(stbi__blinn_8x8((byte)(coutput[1][i]), (byte)(m)));
                                    _out_[2] = (byte)(stbi__blinn_8x8((byte)(coutput[2][i]), (byte)(m)));
                                    _out_[3] = (byte)(255);
                                    _out_ += n;
                                }
                            }
                            else if ((z.app14_color_transform) == (2))
                            {
                                z.YCbCr_to_RGB_kernel(_out_, y, coutput[1], coutput[2], (int)(z.s.img_x), (int)(n));
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    byte m = (byte)(coutput[3][i]);
                                    _out_[0] = (byte)(stbi__blinn_8x8((byte)(255 - _out_[0]), (byte)(m)));
                                    _out_[1] = (byte)(stbi__blinn_8x8((byte)(255 - _out_[1]), (byte)(m)));
                                    _out_[2] = (byte)(stbi__blinn_8x8((byte)(255 - _out_[2]), (byte)(m)));
                                    _out_ += n;
                                }
                            }
                            else
                            {
                                z.YCbCr_to_RGB_kernel(_out_, y, coutput[1], coutput[2], (int)(z.s.img_x), (int)(n));
                            }
                        }
                        else
                            for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                            {
                                _out_[0] = (byte)(_out_[1] = (byte)(_out_[2] = (byte)(y[i])));
                                _out_[3] = (byte)(255);
                                _out_ += n;
                            }
                    }
                    else
                    {
                        if ((is_rgb) != 0)
                        {
                            if ((n) == (1))
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    *_out_++ = (byte)(stbi__compute_y((int)(coutput[0][i]), (int)(coutput[1][i]), (int)(coutput[2][i])));
                                }
                            else
                            {
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i, _out_ += 2)
                                {
                                    _out_[0] = (byte)(stbi__compute_y((int)(coutput[0][i]), (int)(coutput[1][i]), (int)(coutput[2][i])));
                                    _out_[1] = (byte)(255);
                                }
                            }
                        }
                        else if (((z.s.img_n) == (4)) && ((z.app14_color_transform) == (0)))
                        {
                            for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                            {
                                byte m = (byte)(coutput[3][i]);
                                byte r = (byte)(stbi__blinn_8x8((byte)(coutput[0][i]), (byte)(m)));
                                byte g = (byte)(stbi__blinn_8x8((byte)(coutput[1][i]), (byte)(m)));
                                byte b = (byte)(stbi__blinn_8x8((byte)(coutput[2][i]), (byte)(m)));
                                _out_[0] = (byte)(stbi__compute_y((int)(r), (int)(g), (int)(b)));
                                _out_[1] = (byte)(255);
                                _out_ += n;
                            }
                        }
                        else if (((z.s.img_n) == (4)) && ((z.app14_color_transform) == (2)))
                        {
                            for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                            {
                                _out_[0] = (byte)(stbi__blinn_8x8((byte)(255 - coutput[0][i]), (byte)(coutput[3][i])));
                                _out_[1] = (byte)(255);
                                _out_ += n;
                            }
                        }
                        else
                        {
                            byte* y = coutput[0];
                            if ((n) == (1))
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    _out_[i] = (byte)(y[i]);
                                }
                            else
                                for (i = (uint)(0); (i) < (z.s.img_x); ++i)
                                {
                                    *_out_++ = (byte)(y[i]);
                                    *_out_++ = (byte)(255);
                                }
                        }
                    }
                }
                stbi__cleanup_jpeg(z);
                *out_x = (int)(z.s.img_x);
                *out_y = (int)(z.s.img_y);
                if ((comp) != null) *comp = (int)((z.s.img_n) >= (3) ? 3 : 1);
                return output;
            }
        }

        public static void* stbi__jpeg_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri)
        {
            byte* result;
            stbi__jpeg j = new stbi__jpeg();
            j.s = s;
            stbi__setup_jpeg(j);
            result = load_jpeg_image(j, x, y, comp, (int)(req_comp));

            return result;
        }

        public static int stbi__jpeg_test(stbi__context s)
        {
            int r;
            stbi__jpeg j = new stbi__jpeg();
            j.s = s;
            stbi__setup_jpeg(j);
            r = (int)(stbi__decode_jpeg_header(j, (int)(STBI__SCAN_type)));
            stbi__rewind(s);

            return (int)(r);
        }

        public static int stbi__jpeg_info_raw(stbi__jpeg j, int* x, int* y, int* comp)
        {
            if (stbi__decode_jpeg_header(j, (int)(STBI__SCAN_header)) == 0)
            {
                stbi__rewind(j.s);
                return (int)(0);
            }

            if ((x) != null) *x = (int)(j.s.img_x);
            if ((y) != null) *y = (int)(j.s.img_y);
            if ((comp) != null) *comp = (int)((j.s.img_n) >= (3) ? 3 : 1);
            return (int)(1);
        }

        public static int stbi__jpeg_info(stbi__context s, int* x, int* y, int* comp)
        {
            int result;
            stbi__jpeg j = new stbi__jpeg();
            j.s = s;
            result = (int)(stbi__jpeg_info_raw(j, x, y, comp));

            return (int)(result);
        }

        public static int stbi__bitreverse16(int n)
        {
            n = (int)(((n & 0xAAAA) >> 1) | ((n & 0x5555) << 1));
            n = (int)(((n & 0xCCCC) >> 2) | ((n & 0x3333) << 2));
            n = (int)(((n & 0xF0F0) >> 4) | ((n & 0x0F0F) << 4));
            n = (int)(((n & 0xFF00) >> 8) | ((n & 0x00FF) << 8));
            return (int)(n);
        }

        public static int stbi__bit_reverse(int v, int bits)
        {
            return (int)(stbi__bitreverse16((int)(v)) >> (16 - bits));
        }

        public static int stbi__zbuild_huffman(stbi__zhuffman* z, byte* sizelist, int num)
        {
            int i;
            int k = (int)(0);
            int code;
            int* next_code = stackalloc int[16];
            int* sizes = stackalloc int[17];
            CRuntime.memset(sizes, (int)(0), (ulong)(sizeof(int)));
            CRuntime.memset(((ushort*)(z->fast)), (int)(0), (ulong)((1 << 9) * sizeof(ushort)));
            for (i = (int)(0); (i) < (num); ++i)
            {
                ++sizes[sizelist[i]];
            }
            sizes[0] = (int)(0);
            for (i = (int)(1); (i) < (16); ++i)
            {
                if ((sizes[i]) > (1 << i)) return (int)(stbi__err("bad sizes"));
            }
            code = (int)(0);
            for (i = (int)(1); (i) < (16); ++i)
            {
                next_code[i] = (int)(code);
                z->firstcode[i] = ((ushort)(code));
                z->firstsymbol[i] = ((ushort)(k));
                code = (int)(code + sizes[i]);
                if ((sizes[i]) != 0) if ((code - 1) >= (1 << i)) return (int)(stbi__err("bad codelengths"));
                z->maxcode[i] = (int)(code << (16 - i));
                code <<= 1;
                k += (int)(sizes[i]);
            }
            z->maxcode[16] = (int)(0x10000);
            for (i = (int)(0); (i) < (num); ++i)
            {
                int s = (int)(sizelist[i]);
                if ((s) != 0)
                {
                    int c = (int)(next_code[s] - z->firstcode[s] + z->firstsymbol[s]);
                    ushort fastv = (ushort)((s << 9) | i);
                    z->size[c] = ((byte)(s));
                    z->value[c] = ((ushort)(i));
                    if (s <= 9)
                    {
                        int j = (int)(stbi__bit_reverse((int)(next_code[s]), (int)(s)));
                        while ((j) < (1 << 9))
                        {
                            z->fast[j] = (ushort)(fastv);
                            j += (int)(1 << s);
                        }
                    }
                    ++next_code[s];
                }
            }
            return (int)(1);
        }

        public static byte stbi__zget8(stbi__zbuf* z)
        {
            if ((z->zbuffer) >= (z->zbuffer_end)) return (byte)(0);
            return (byte)(*z->zbuffer++);
        }

        public static void stbi__fill_bits(stbi__zbuf* z)
        {
            do
            {
                z->code_buffer |= (uint)((uint)(stbi__zget8(z)) << z->num_bits);
                z->num_bits += (int)(8);
            } while (z->num_bits <= 24);
        }

        public static uint stbi__zreceive(stbi__zbuf* z, int n)
        {
            uint k;
            if ((z->num_bits) < (n)) stbi__fill_bits(z);
            k = (uint)(z->code_buffer & ((1 << n) - 1));
            z->code_buffer >>= n;
            z->num_bits -= (int)(n);
            return (uint)(k);
        }

        public static int stbi__zhuffman_decode_slowpath(stbi__zbuf* a, stbi__zhuffman* z)
        {
            int b;
            int s;
            int k;
            k = (int)(stbi__bit_reverse((int)(a->code_buffer), (int)(16)));
            for (s = (int)(9 + 1); ; ++s)
            {
                if ((k) < (z->maxcode[s])) break;
            }
            if ((s) == (16)) return (int)(-1);
            b = (int)((k >> (16 - s)) - z->firstcode[s] + z->firstsymbol[s]);
            a->code_buffer >>= s;
            a->num_bits -= (int)(s);
            return (int)(z->value[b]);
        }

        public static int stbi__zhuffman_decode(stbi__zbuf* a, stbi__zhuffman* z)
        {
            int b;
            int s;
            if ((a->num_bits) < (16)) stbi__fill_bits(a);
            b = (int)(z->fast[a->code_buffer & ((1 << 9) - 1)]);
            if ((b) != 0)
            {
                s = (int)(b >> 9);
                a->code_buffer >>= s;
                a->num_bits -= (int)(s);
                return (int)(b & 511);
            }

            return (int)(stbi__zhuffman_decode_slowpath(a, z));
        }

        public static int stbi__zexpand(stbi__zbuf* z, sbyte* zout, int n)
        {
            sbyte* q;
            int cur;
            int limit;
            int old_limit;
            z->zout = zout;
            if (z->z_expandable == 0) return (int)(stbi__err("output buffer limit"));
            cur = ((int)(z->zout - z->zout_start));
            limit = (int)(old_limit = ((int)(z->zout_end - z->zout_start)));
            while ((cur + n) > (limit))
            {
                limit *= (int)(2);
            }
            q = (sbyte*)(CRuntime.realloc(z->zout_start, (ulong)(limit)));
            if ((q) == (null)) return (int)(stbi__err("outofmem"));
            z->zout_start = q;
            z->zout = q + cur;
            z->zout_end = q + limit;
            return (int)(1);
        }

        public static int stbi__parse_huffman_block(stbi__zbuf* a)
        {
            sbyte* zout = a->zout;
            for (; ; )
            {
                int z = (int)(stbi__zhuffman_decode(a, &a->z_length));
                if ((z) < (256))
                {
                    if ((z) < (0)) return (int)(stbi__err("bad huffman code"));
                    if ((zout) >= (a->zout_end))
                    {
                        if (stbi__zexpand(a, zout, (int)(1)) == 0) return (int)(0);
                        zout = a->zout;
                    }
                    *zout++ = ((sbyte)(z));
                }
                else
                {
                    byte* p;
                    int len;
                    int dist;
                    if ((z) == (256))
                    {
                        a->zout = zout;
                        return (int)(1);
                    }
                    z -= (int)(257);
                    len = (int)(stbi__zlength_base[z]);
                    if ((stbi__zlength_extra[z]) != 0) len += (int)(stbi__zreceive(a, (int)(stbi__zlength_extra[z])));
                    z = (int)(stbi__zhuffman_decode(a, &a->z_distance));
                    if ((z) < (0)) return (int)(stbi__err("bad huffman code"));
                    dist = (int)(stbi__zdist_base[z]);
                    if ((stbi__zdist_extra[z]) != 0) dist += (int)(stbi__zreceive(a, (int)(stbi__zdist_extra[z])));
                    if ((zout - a->zout_start) < (dist)) return (int)(stbi__err("bad dist"));
                    if ((zout + len) > (a->zout_end))
                    {
                        if (stbi__zexpand(a, zout, (int)(len)) == 0) return (int)(0);
                        zout = a->zout;
                    }
                    p = (byte*)(zout - dist);
                    if ((dist) == (1))
                    {
                        byte v = (byte)(*p);
                        if ((len) != 0)
                        {
                            do *zout++ = (sbyte)(v); while ((--len) != 0);
                        }
                    }
                    else
                    {
                        if ((len) != 0)
                        {
                            do *zout++ = (sbyte)(*p++); while ((--len) != 0);
                        }
                    }
                }
            }
        }

        public static int stbi__compute_huffman_codes(stbi__zbuf* a)
        {
            stbi__zhuffman z_codelength = new stbi__zhuffman();
            byte* lencodes = stackalloc byte[286 + 32 + 137];
            byte* codelength_sizes = stackalloc byte[19];
            int i;
            int n;
            int hlit = (int)(stbi__zreceive(a, (int)(5)) + 257);
            int hdist = (int)(stbi__zreceive(a, (int)(5)) + 1);
            int hclen = (int)(stbi__zreceive(a, (int)(4)) + 4);
            int ntot = (int)(hlit + hdist);
            CRuntime.memset(((byte*)(codelength_sizes)), (int)(0), (ulong)(19 * sizeof(byte)));
            for (i = (int)(0); (i) < (hclen); ++i)
            {
                int s = (int)(stbi__zreceive(a, (int)(3)));
                codelength_sizes[length_dezigzag[i]] = ((byte)(s));
            }
            if (stbi__zbuild_huffman(&z_codelength, codelength_sizes, (int)(19)) == 0) return (int)(0);
            n = (int)(0);
            while ((n) < (ntot))
            {
                int c = (int)(stbi__zhuffman_decode(a, &z_codelength));
                if (((c) < (0)) || ((c) >= (19))) return (int)(stbi__err("bad codelengths"));
                if ((c) < (16)) lencodes[n++] = ((byte)(c));
                else
                {
                    byte fill = (byte)(0);
                    if ((c) == (16))
                    {
                        c = (int)(stbi__zreceive(a, (int)(2)) + 3);
                        if ((n) == (0)) return (int)(stbi__err("bad codelengths"));
                        fill = (byte)(lencodes[n - 1]);
                    }
                    else if ((c) == (17)) c = (int)(stbi__zreceive(a, (int)(3)) + 3);
                    else
                    {
                        c = (int)(stbi__zreceive(a, (int)(7)) + 11);
                    }
                    if ((ntot - n) < (c)) return (int)(stbi__err("bad codelengths"));
                    CRuntime.memset(lencodes + n, (int)(fill), (ulong)(c));
                    n += (int)(c);
                }
            }
            if (n != ntot) return (int)(stbi__err("bad codelengths"));
            if (stbi__zbuild_huffman(&a->z_length, lencodes, (int)(hlit)) == 0) return (int)(0);
            if (stbi__zbuild_huffman(&a->z_distance, lencodes + hlit, (int)(hdist)) == 0) return (int)(0);
            return (int)(1);
        }

        public static int stbi__parse_uncompressed_block(stbi__zbuf* a)
        {
            byte* header = stackalloc byte[4];
            int len;
            int nlen;
            int k;
            if ((a->num_bits & 7) != 0) stbi__zreceive(a, (int)(a->num_bits & 7));
            k = (int)(0);
            while ((a->num_bits) > (0))
            {
                header[k++] = ((byte)(a->code_buffer & 255));
                a->code_buffer >>= 8;
                a->num_bits -= (int)(8);
            }
            while ((k) < (4))
            {
                header[k++] = (byte)(stbi__zget8(a));
            }
            len = (int)(header[1] * 256 + header[0]);
            nlen = (int)(header[3] * 256 + header[2]);
            if (nlen != (len ^ 0xffff)) return (int)(stbi__err("zlib corrupt"));
            if ((a->zbuffer + len) > (a->zbuffer_end)) return (int)(stbi__err("read past buffer"));
            if ((a->zout + len) > (a->zout_end)) if (stbi__zexpand(a, a->zout, (int)(len)) == 0) return (int)(0);
            CRuntime.memcpy(a->zout, a->zbuffer, (ulong)(len));
            a->zbuffer += len;
            a->zout += len;
            return (int)(1);
        }

        public static int stbi__parse_zlib_header(stbi__zbuf* a)
        {
            int cmf = (int)(stbi__zget8(a));
            int cm = (int)(cmf & 15);
            int flg = (int)(stbi__zget8(a));
            if ((cmf * 256 + flg) % 31 != 0) return (int)(stbi__err("bad zlib header"));
            if ((flg & 32) != 0) return (int)(stbi__err("no preset dict"));
            if (cm != 8) return (int)(stbi__err("bad compression"));
            return (int)(1);
        }

        public static int stbi__parse_zlib(stbi__zbuf* a, int parse_header)
        {
            int final;
            int type;
            if ((parse_header) != 0) if (stbi__parse_zlib_header(a) == 0) return (int)(0);
            a->num_bits = (int)(0);
            a->code_buffer = (uint)(0);
            do
            {
                final = (int)(stbi__zreceive(a, (int)(1)));
                type = (int)(stbi__zreceive(a, (int)(2)));
                if ((type) == (0))
                {
                    if (stbi__parse_uncompressed_block(a) == 0) return (int)(0);
                }
                else if ((type) == (3))
                {
                    return (int)(0);
                }
                else
                {
                    if ((type) == (1))
                    {
                        fixed (byte* b = stbi__zdefault_length)
                        {
                            if (stbi__zbuild_huffman(&a->z_length, b, (int)(288)) == 0) return (int)(0);
                        }
                        fixed (byte* b = stbi__zdefault_distance)
                        {
                            if (stbi__zbuild_huffman(&a->z_distance, b, (int)(32)) == 0) return (int)(0);
                        }
                    }
                    else
                    {
                        if (stbi__compute_huffman_codes(a) == 0) return (int)(0);
                    }
                    if (stbi__parse_huffman_block(a) == 0) return (int)(0);
                }
            } while (final == 0);
            return (int)(1);
        }

        public static int stbi__do_zlib(stbi__zbuf* a, sbyte* obuf, int olen, int exp, int parse_header)
        {
            a->zout_start = obuf;
            a->zout = obuf;
            a->zout_end = obuf + olen;
            a->z_expandable = (int)(exp);
            return (int)(stbi__parse_zlib(a, (int)(parse_header)));
        }

        public static sbyte* stbi_zlib_decode_malloc_guesssize(sbyte* buffer, int len, int initial_size, int* outlen)
        {
            stbi__zbuf a = new stbi__zbuf();
            sbyte* p = (sbyte*)(stbi__malloc((ulong)(initial_size)));
            if ((p) == (null)) return (null);
            a.zbuffer = (byte*)(buffer);
            a.zbuffer_end = (byte*)(buffer) + len;
            if ((stbi__do_zlib(&a, p, (int)(initial_size), (int)(1), (int)(1))) != 0)
            {
                if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));
                return a.zout_start;
            }
            else
            {
                CRuntime.free(a.zout_start);
                return (null);
            }
        }

        public static sbyte* stbi_zlib_decode_malloc(sbyte* buffer, int len, int* outlen)
        {
            return stbi_zlib_decode_malloc_guesssize(buffer, (int)(len), (int)(16384), outlen);
        }

        public static sbyte* stbi_zlib_decode_malloc_guesssize_headerflag(sbyte* buffer, int len, int initial_size,
            int* outlen, int parse_header)
        {
            stbi__zbuf a = new stbi__zbuf();
            sbyte* p = (sbyte*)(stbi__malloc((ulong)(initial_size)));
            if ((p) == (null)) return (null);
            a.zbuffer = (byte*)(buffer);
            a.zbuffer_end = (byte*)(buffer) + len;
            if ((stbi__do_zlib(&a, p, (int)(initial_size), (int)(1), (int)(parse_header))) != 0)
            {
                if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));
                return a.zout_start;
            }
            else
            {
                CRuntime.free(a.zout_start);
                return (null);
            }
        }

        public static int stbi_zlib_decode_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
        {
            stbi__zbuf a = new stbi__zbuf();
            a.zbuffer = (byte*)(ibuffer);
            a.zbuffer_end = (byte*)(ibuffer) + ilen;
            if ((stbi__do_zlib(&a, obuffer, (int)(olen), (int)(0), (int)(1))) != 0) return (int)(a.zout - a.zout_start);
            else return (int)(-1);
        }

        public static sbyte* stbi_zlib_decode_noheader_malloc(sbyte* buffer, int len, int* outlen)
        {
            stbi__zbuf a = new stbi__zbuf();
            sbyte* p = (sbyte*)(stbi__malloc((ulong)(16384)));
            if ((p) == (null)) return (null);
            a.zbuffer = (byte*)(buffer);
            a.zbuffer_end = (byte*)(buffer) + len;
            if ((stbi__do_zlib(&a, p, (int)(16384), (int)(1), (int)(0))) != 0)
            {
                if ((outlen) != null) *outlen = ((int)(a.zout - a.zout_start));
                return a.zout_start;
            }
            else
            {
                CRuntime.free(a.zout_start);
                return (null);
            }
        }

        public static int stbi_zlib_decode_noheader_buffer(sbyte* obuffer, int olen, sbyte* ibuffer, int ilen)
        {
            stbi__zbuf a = new stbi__zbuf();
            a.zbuffer = (byte*)(ibuffer);
            a.zbuffer_end = (byte*)(ibuffer) + ilen;
            if ((stbi__do_zlib(&a, obuffer, (int)(olen), (int)(0), (int)(0))) != 0) return (int)(a.zout - a.zout_start);
            else return (int)(-1);
        }

        public static stbi__pngchunk stbi__get_chunk_header(stbi__context s)
        {
            stbi__pngchunk c = new stbi__pngchunk();
            c.length = (uint)(stbi__get32be(s));
            c.type = (uint)(stbi__get32be(s));
            return (stbi__pngchunk)(c);
        }

        public static int stbi__check_png_header(stbi__context s)
        {
            int i;
            for (i = (int)(0); (i) < (8); ++i)
            {
                if (stbi__get8(s) != png_sig[i]) return (int)(stbi__err("bad png sig"));
            }
            return (int)(1);
        }

        public static int stbi__paeth(int a, int b, int c)
        {
            int p = (int)(a + b - c);
            int pa = (int)(CRuntime.abs((int)(p - a)));
            int pb = (int)(CRuntime.abs((int)(p - b)));
            int pc = (int)(CRuntime.abs((int)(p - c)));
            if ((pa <= pb) && (pa <= pc)) return (int)(a);
            if (pb <= pc) return (int)(b);
            return (int)(c);
        }

        public static int stbi__create_png_image_raw(stbi__png a, byte* raw, uint raw_len, int out_n, uint x, uint y,
            int depth, int color)
        {
            int bytes = (int)((depth) == (16) ? 2 : 1);
            stbi__context s = a.s;
            uint i;
            uint j;
            uint stride = (uint)(x * out_n * bytes);
            uint img_len;
            uint img_width_bytes;
            int k;
            int img_n = (int)(s.img_n);
            int output_bytes = (int)(out_n * bytes);
            int filter_bytes = (int)(img_n * bytes);
            int width = (int)(x);
            a._out_ = (byte*)(stbi__malloc_mad3((int)(x), (int)(y), (int)(output_bytes), (int)(0)));
            if (a._out_ == null) return (int)(stbi__err("outofmem"));
            img_width_bytes = (uint)(((img_n * x * depth) + 7) >> 3);
            img_len = (uint)((img_width_bytes + 1) * y);
            if ((raw_len) < (img_len)) return (int)(stbi__err("not enough pixels"));
            for (j = (uint)(0); (j) < (y); ++j)
            {
                byte* cur = a._out_ + stride * j;
                byte* prior;
                int filter = (int)(*raw++);
                if ((filter) > (4)) return (int)(stbi__err("invalid filter"));
                if ((depth) < (8))
                {
                    cur += x * out_n - img_width_bytes;
                    filter_bytes = (int)(1);
                    width = (int)(img_width_bytes);
                }
                prior = cur - stride;
                if ((j) == (0)) filter = (int)(first_row_filter[filter]);
                for (k = (int)(0); (k) < (filter_bytes); ++k)
                {
                    switch (filter)
                    {
                        case STBI__F_none:
                            cur[k] = (byte)(raw[k]);
                            break;

                        case STBI__F_sub:
                            cur[k] = (byte)(raw[k]);
                            break;

                        case STBI__F_up:
                            cur[k] = ((byte)((raw[k] + prior[k]) & 255));
                            break;

                        case STBI__F_avg:
                            cur[k] = ((byte)((raw[k] + (prior[k] >> 1)) & 255));
                            break;

                        case STBI__F_paeth:
                            cur[k] = ((byte)((raw[k] + stbi__paeth((int)(0), (int)(prior[k]), (int)(0))) & 255));
                            break;

                        case STBI__F_avg_first:
                            cur[k] = (byte)(raw[k]);
                            break;

                        case STBI__F_paeth_first:
                            cur[k] = (byte)(raw[k]);
                            break;
                    }
                }
                if ((depth) == (8))
                {
                    if (img_n != out_n) cur[img_n] = (byte)(255);
                    raw += img_n;
                    cur += out_n;
                    prior += out_n;
                }
                else if ((depth) == (16))
                {
                    if (img_n != out_n)
                    {
                        cur[filter_bytes] = (byte)(255);
                        cur[filter_bytes + 1] = (byte)(255);
                    }
                    raw += filter_bytes;
                    cur += output_bytes;
                    prior += output_bytes;
                }
                else
                {
                    raw += 1;
                    cur += 1;
                    prior += 1;
                }
                if (((depth) < (8)) || ((img_n) == (out_n)))
                {
                    int nk = (int)((width - 1) * filter_bytes);
                    switch (filter)
                    {
                        case STBI__F_none:
                            CRuntime.memcpy(cur, raw, (ulong)(nk));
                            break;

                        case STBI__F_sub:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] = ((byte)((raw[k] + cur[k - filter_bytes]) & 255));
                            }
                            break;

                        case STBI__F_up:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] = ((byte)((raw[k] + prior[k]) & 255));
                            }
                            break;

                        case STBI__F_avg:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] = ((byte)((raw[k] + ((prior[k] + cur[k - filter_bytes]) >> 1)) & 255));
                            }
                            break;

                        case STBI__F_paeth:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] =
                                    ((byte)
                                        ((raw[k] + stbi__paeth((int)(cur[k - filter_bytes]), (int)(prior[k]), (int)(prior[k - filter_bytes]))) &
                                         255));
                            }
                            break;

                        case STBI__F_avg_first:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] = ((byte)((raw[k] + (cur[k - filter_bytes] >> 1)) & 255));
                            }
                            break;

                        case STBI__F_paeth_first:
                            for (k = (int)(0); (k) < (nk); ++k)
                            {
                                cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - filter_bytes]), (int)(0), (int)(0))) & 255));
                            }
                            break;
                    }
                    raw += nk;
                }
                else
                {
                    switch (filter)
                    {
                        case STBI__F_none:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = (byte)(raw[k]);
                                }
                            }
                            break;

                        case STBI__F_sub:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = ((byte)((raw[k] + cur[k - output_bytes]) & 255));
                                }
                            }
                            break;

                        case STBI__F_up:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = ((byte)((raw[k] + prior[k]) & 255));
                                }
                            }
                            break;

                        case STBI__F_avg:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = ((byte)((raw[k] + ((prior[k] + cur[k - output_bytes]) >> 1)) & 255));
                                }
                            }
                            break;

                        case STBI__F_paeth:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] =
                                        ((byte)
                                            ((raw[k] + stbi__paeth((int)(cur[k - output_bytes]), (int)(prior[k]), (int)(prior[k - output_bytes]))) &
                                             255));
                                }
                            }
                            break;

                        case STBI__F_avg_first:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = ((byte)((raw[k] + (cur[k - output_bytes] >> 1)) & 255));
                                }
                            }
                            break;

                        case STBI__F_paeth_first:
                            for (i = (uint)(x - 1);
                                (i) >= (1);
                                --i, cur[filter_bytes] = (byte)(255), raw += filter_bytes, cur += output_bytes, prior += output_bytes)
                            {
                                for (k = (int)(0); (k) < (filter_bytes); ++k)
                                {
                                    cur[k] = ((byte)((raw[k] + stbi__paeth((int)(cur[k - output_bytes]), (int)(0), (int)(0))) & 255));
                                }
                            }
                            break;
                    }
                    if ((depth) == (16))
                    {
                        cur = a._out_ + stride * j;
                        for (i = (uint)(0); (i) < (x); ++i, cur += output_bytes)
                        {
                            cur[filter_bytes + 1] = (byte)(255);
                        }
                    }
                }
            }
            if ((depth) < (8))
            {
                for (j = (uint)(0); (j) < (y); ++j)
                {
                    byte* cur = a._out_ + stride * j;
                    byte* _in_ = a._out_ + stride * j + x * out_n - img_width_bytes;
                    byte scale = (byte)(((color) == (0)) ? stbi__depth_scale_table[depth] : 1);
                    if ((depth) == (4))
                    {
                        for (k = (int)(x * img_n); (k) >= (2); k -= (int)(2), ++_in_)
                        {
                            *cur++ = (byte)(scale * (*_in_ >> 4));
                            *cur++ = (byte)(scale * ((*_in_) & 0x0f));
                        }
                        if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 4));
                    }
                    else if ((depth) == (2))
                    {
                        for (k = (int)(x * img_n); (k) >= (4); k -= (int)(4), ++_in_)
                        {
                            *cur++ = (byte)(scale * (*_in_ >> 6));
                            *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x03));
                            *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x03));
                            *cur++ = (byte)(scale * ((*_in_) & 0x03));
                        }
                        if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 6));
                        if ((k) > (1)) *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x03));
                        if ((k) > (2)) *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x03));
                    }
                    else if ((depth) == (1))
                    {
                        for (k = (int)(x * img_n); (k) >= (8); k -= (int)(8), ++_in_)
                        {
                            *cur++ = (byte)(scale * (*_in_ >> 7));
                            *cur++ = (byte)(scale * ((*_in_ >> 6) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_ >> 5) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_ >> 3) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_ >> 1) & 0x01));
                            *cur++ = (byte)(scale * ((*_in_) & 0x01));
                        }
                        if ((k) > (0)) *cur++ = (byte)(scale * (*_in_ >> 7));
                        if ((k) > (1)) *cur++ = (byte)(scale * ((*_in_ >> 6) & 0x01));
                        if ((k) > (2)) *cur++ = (byte)(scale * ((*_in_ >> 5) & 0x01));
                        if ((k) > (3)) *cur++ = (byte)(scale * ((*_in_ >> 4) & 0x01));
                        if ((k) > (4)) *cur++ = (byte)(scale * ((*_in_ >> 3) & 0x01));
                        if ((k) > (5)) *cur++ = (byte)(scale * ((*_in_ >> 2) & 0x01));
                        if ((k) > (6)) *cur++ = (byte)(scale * ((*_in_ >> 1) & 0x01));
                    }
                    if (img_n != out_n)
                    {
                        int q;
                        cur = a._out_ + stride * j;
                        if ((img_n) == (1))
                        {
                            for (q = (int)(x - 1); (q) >= (0); --q)
                            {
                                cur[q * 2 + 1] = (byte)(255);
                                cur[q * 2 + 0] = (byte)(cur[q]);
                            }
                        }
                        else
                        {
                            for (q = (int)(x - 1); (q) >= (0); --q)
                            {
                                cur[q * 4 + 3] = (byte)(255);
                                cur[q * 4 + 2] = (byte)(cur[q * 3 + 2]);
                                cur[q * 4 + 1] = (byte)(cur[q * 3 + 1]);
                                cur[q * 4 + 0] = (byte)(cur[q * 3 + 0]);
                            }
                        }
                    }
                }
            }
            else if ((depth) == (16))
            {
                byte* cur = a._out_;
                ushort* cur16 = (ushort*)(cur);
                for (i = (uint)(0); (i) < (x * y * out_n); ++i, cur16++, cur += 2)
                {
                    *cur16 = (ushort)((cur[0] << 8) | cur[1]);
                }
            }

            return (int)(1);
        }

        public static int stbi__create_png_image(stbi__png a, byte* image_data, uint image_data_len, int out_n, int depth,
            int color, int interlaced)
        {
            int bytes = (int)((depth) == (16) ? 2 : 1);
            int out_bytes = (int)(out_n * bytes);
            byte* final;
            int p;
            if (interlaced == 0)
                return
                    (int)
                        (stbi__create_png_image_raw(a, image_data, (uint)(image_data_len), (int)(out_n), (uint)(a.s.img_x),
                            (uint)(a.s.img_y), (int)(depth), (int)(color)));
            final = (byte*)(stbi__malloc_mad3((int)(a.s.img_x), (int)(a.s.img_y), (int)(out_bytes), (int)(0)));
            for (p = (int)(0); (p) < (7); ++p)
            {
                int* xorig = stackalloc int[7];
                xorig[0] = (int)(0);
                xorig[1] = (int)(4);
                xorig[2] = (int)(0);
                xorig[3] = (int)(2);
                xorig[4] = (int)(0);
                xorig[5] = (int)(1);
                xorig[6] = (int)(0);
                int* yorig = stackalloc int[7];
                yorig[0] = (int)(0);
                yorig[1] = (int)(0);
                yorig[2] = (int)(4);
                yorig[3] = (int)(0);
                yorig[4] = (int)(2);
                yorig[5] = (int)(0);
                yorig[6] = (int)(1);
                int* xspc = stackalloc int[7];
                xspc[0] = (int)(8);
                xspc[1] = (int)(8);
                xspc[2] = (int)(4);
                xspc[3] = (int)(4);
                xspc[4] = (int)(2);
                xspc[5] = (int)(2);
                xspc[6] = (int)(1);
                int* yspc = stackalloc int[7];
                yspc[0] = (int)(8);
                yspc[1] = (int)(8);
                yspc[2] = (int)(8);
                yspc[3] = (int)(4);
                yspc[4] = (int)(4);
                yspc[5] = (int)(2);
                yspc[6] = (int)(2);
                int i;
                int j;
                int x;
                int y;
                x = (int)((a.s.img_x - xorig[p] + xspc[p] - 1) / xspc[p]);
                y = (int)((a.s.img_y - yorig[p] + yspc[p] - 1) / yspc[p]);
                if (((x) != 0) && ((y) != 0))
                {
                    uint img_len = (uint)(((((a.s.img_n * x * depth) + 7) >> 3) + 1) * y);
                    if (
                        stbi__create_png_image_raw(a, image_data, (uint)(image_data_len), (int)(out_n), (uint)(x), (uint)(y),
                            (int)(depth), (int)(color)) == 0)
                    {
                        CRuntime.free(final);
                        return (int)(0);
                    }
                    for (j = (int)(0); (j) < (y); ++j)
                    {
                        for (i = (int)(0); (i) < (x); ++i)
                        {
                            int out_y = (int)(j * yspc[p] + yorig[p]);
                            int out_x = (int)(i * xspc[p] + xorig[p]);
                            CRuntime.memcpy(final + out_y * a.s.img_x * out_bytes + out_x * out_bytes, a._out_ + (j * x + i) * out_bytes,
                                (ulong)(out_bytes));
                        }
                    }
                    CRuntime.free(a._out_);
                    image_data += img_len;
                    image_data_len -= (uint)(img_len);
                }
            }
            a._out_ = final;
            return (int)(1);
        }

        public static int stbi__compute_transparency(stbi__png z, byte* tc, int out_n)
        {
            stbi__context s = z.s;
            uint i;
            uint pixel_count = (uint)(s.img_x * s.img_y);
            byte* p = z._out_;
            if ((out_n) == (2))
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    p[1] = (byte)((p[0]) == (tc[0]) ? 0 : 255);
                    p += 2;
                }
            }
            else
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    if ((((p[0]) == (tc[0])) && ((p[1]) == (tc[1]))) && ((p[2]) == (tc[2]))) p[3] = (byte)(0);
                    p += 4;
                }
            }

            return (int)(1);
        }

        public static int stbi__compute_transparency16(stbi__png z, ushort* tc, int out_n)
        {
            stbi__context s = z.s;
            uint i;
            uint pixel_count = (uint)(s.img_x * s.img_y);
            ushort* p = (ushort*)(z._out_);
            if ((out_n) == (2))
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    p[1] = (ushort)((p[0]) == (tc[0]) ? 0 : 65535);
                    p += 2;
                }
            }
            else
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    if ((((p[0]) == (tc[0])) && ((p[1]) == (tc[1]))) && ((p[2]) == (tc[2]))) p[3] = (ushort)(0);
                    p += 4;
                }
            }

            return (int)(1);
        }

        public static int stbi__expand_png_palette(stbi__png a, byte* palette, int len, int pal_img_n)
        {
            uint i;
            uint pixel_count = (uint)(a.s.img_x * a.s.img_y);
            byte* p;
            byte* temp_out;
            byte* orig = a._out_;
            p = (byte*)(stbi__malloc_mad2((int)(pixel_count), (int)(pal_img_n), (int)(0)));
            if ((p) == (null)) return (int)(stbi__err("outofmem"));
            temp_out = p;
            if ((pal_img_n) == (3))
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    int n = (int)(orig[i] * 4);
                    p[0] = (byte)(palette[n]);
                    p[1] = (byte)(palette[n + 1]);
                    p[2] = (byte)(palette[n + 2]);
                    p += 3;
                }
            }
            else
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    int n = (int)(orig[i] * 4);
                    p[0] = (byte)(palette[n]);
                    p[1] = (byte)(palette[n + 1]);
                    p[2] = (byte)(palette[n + 2]);
                    p[3] = (byte)(palette[n + 3]);
                    p += 4;
                }
            }

            CRuntime.free(a._out_);
            a._out_ = temp_out;
            return (int)(1);
        }

        public static void stbi_set_unpremultiply_on_load(int flag_true_if_should_unpremultiply)
        {
            stbi__unpremultiply_on_load = (int)(flag_true_if_should_unpremultiply);
        }

        public static void stbi_convert_iphone_png_to_rgb(int flag_true_if_should_convert)
        {
            stbi__de_iphone_flag = (int)(flag_true_if_should_convert);
        }

        public static void stbi__de_iphone(stbi__png z)
        {
            stbi__context s = z.s;
            uint i;
            uint pixel_count = (uint)(s.img_x * s.img_y);
            byte* p = z._out_;
            if ((s.img_out_n) == (3))
            {
                for (i = (uint)(0); (i) < (pixel_count); ++i)
                {
                    byte t = (byte)(p[0]);
                    p[0] = (byte)(p[2]);
                    p[2] = (byte)(t);
                    p += 3;
                }
            }
            else
            {
                if ((stbi__unpremultiply_on_load) != 0)
                {
                    for (i = (uint)(0); (i) < (pixel_count); ++i)
                    {
                        byte a = (byte)(p[3]);
                        byte t = (byte)(p[0]);
                        if ((a) != 0)
                        {
                            byte half = (byte)(a / 2);
                            p[0] = (byte)((p[2] * 255 + half) / a);
                            p[1] = (byte)((p[1] * 255 + half) / a);
                            p[2] = (byte)((t * 255 + half) / a);
                        }
                        else
                        {
                            p[0] = (byte)(p[2]);
                            p[2] = (byte)(t);
                        }
                        p += 4;
                    }
                }
                else
                {
                    for (i = (uint)(0); (i) < (pixel_count); ++i)
                    {
                        byte t = (byte)(p[0]);
                        p[0] = (byte)(p[2]);
                        p[2] = (byte)(t);
                        p += 4;
                    }
                }
            }
        }

        public static int stbi__parse_png_file(stbi__png z, int scan, int req_comp)
        {
            byte* palette = stackalloc byte[1024];
            byte pal_img_n = (byte)(0);
            byte has_trans = (byte)(0);
            byte* tc = stackalloc byte[3];
            ushort* tc16 = stackalloc ushort[3];
            uint ioff = (uint)(0);
            uint idata_limit = (uint)(0);
            uint i;
            uint pal_len = (uint)(0);
            int first = (int)(1);
            int k;
            int interlace = (int)(0);
            int color = (int)(0);
            int is_iphone = (int)(0);
            stbi__context s = z.s;
            z.expanded = (null);
            z.idata = (null);
            z._out_ = (null);
            if (stbi__check_png_header(s) == 0) return (int)(0);
            if ((scan) == (STBI__SCAN_type)) return (int)(1);
            for (; ; )
            {
                stbi__pngchunk c = (stbi__pngchunk)(stbi__get_chunk_header(s));
                switch (c.type)
                {
                    case ((('C') << 24) + (('g') << 16) + (('B') << 8) + ('I')):
                        is_iphone = (int)(1);
                        stbi__skip(s, (int)(c.length));
                        break;

                    case ((('I') << 24) + (('H') << 16) + (('D') << 8) + ('R')):
                        {
                            int comp;
                            int filter;
                            if (first == 0) return (int)(stbi__err("multiple IHDR"));
                            first = (int)(0);
                            if (c.length != 13) return (int)(stbi__err("bad IHDR len"));
                            s.img_x = (uint)(stbi__get32be(s));
                            if ((s.img_x) > (1 << 24)) return (int)(stbi__err("too large"));
                            s.img_y = (uint)(stbi__get32be(s));
                            if ((s.img_y) > (1 << 24)) return (int)(stbi__err("too large"));
                            z.depth = (int)(stbi__get8(s));
                            if (((((z.depth != 1) && (z.depth != 2)) && (z.depth != 4)) && (z.depth != 8)) && (z.depth != 16))
                                return (int)(stbi__err("1/2/4/8/16-bit only"));
                            color = (int)(stbi__get8(s));
                            if ((color) > (6)) return (int)(stbi__err("bad ctype"));
                            if (((color) == (3)) && ((z.depth) == (16))) return (int)(stbi__err("bad ctype"));
                            if ((color) == (3)) pal_img_n = (byte)(3);
                            else if ((color & 1) != 0) return (int)(stbi__err("bad ctype"));
                            comp = (int)(stbi__get8(s));
                            if ((comp) != 0) return (int)(stbi__err("bad comp method"));
                            filter = (int)(stbi__get8(s));
                            if ((filter) != 0) return (int)(stbi__err("bad filter method"));
                            interlace = (int)(stbi__get8(s));
                            if ((interlace) > (1)) return (int)(stbi__err("bad interlace method"));
                            if ((s.img_x == 0) || (s.img_y == 0)) return (int)(stbi__err("0-pixel image"));
                            if (pal_img_n == 0)
                            {
                                s.img_n = (int)(((color & 2) != 0 ? 3 : 1) + ((color & 4) != 0 ? 1 : 0));
                                if (((1 << 30) / s.img_x / s.img_n) < (s.img_y)) return (int)(stbi__err("too large"));
                                if ((scan) == (STBI__SCAN_header)) return (int)(1);
                            }
                            else
                            {
                                s.img_n = (int)(1);
                                if (((1 << 30) / s.img_x / 4) < (s.img_y)) return (int)(stbi__err("too large"));
                            }
                            break;
                        }
                    case ((('P') << 24) + (('L') << 16) + (('T') << 8) + ('E')):
                        {
                            if ((first) != 0) return (int)(stbi__err("first not IHDR"));
                            if ((c.length) > (256 * 3)) return (int)(stbi__err("invalid PLTE"));
                            pal_len = (uint)(c.length / 3);
                            if (pal_len * 3 != c.length) return (int)(stbi__err("invalid PLTE"));
                            for (i = (uint)(0); (i) < (pal_len); ++i)
                            {
                                palette[i * 4 + 0] = (byte)(stbi__get8(s));
                                palette[i * 4 + 1] = (byte)(stbi__get8(s));
                                palette[i * 4 + 2] = (byte)(stbi__get8(s));
                                palette[i * 4 + 3] = (byte)(255);
                            }
                            break;
                        }
                    case ((('t') << 24) + (('R') << 16) + (('N') << 8) + ('S')):
                        {
                            if ((first) != 0) return (int)(stbi__err("first not IHDR"));
                            if ((z.idata) != null) return (int)(stbi__err("tRNS after IDAT"));
                            if ((pal_img_n) != 0)
                            {
                                if ((scan) == (STBI__SCAN_header))
                                {
                                    s.img_n = (int)(4);
                                    return (int)(1);
                                }
                                if ((pal_len) == (0)) return (int)(stbi__err("tRNS before PLTE"));
                                if ((c.length) > (pal_len)) return (int)(stbi__err("bad tRNS len"));
                                pal_img_n = (byte)(4);
                                for (i = (uint)(0); (i) < (c.length); ++i)
                                {
                                    palette[i * 4 + 3] = (byte)(stbi__get8(s));
                                }
                            }
                            else
                            {
                                if ((s.img_n & 1) == 0) return (int)(stbi__err("tRNS with alpha"));
                                if (c.length != (uint)(s.img_n) * 2) return (int)(stbi__err("bad tRNS len"));
                                has_trans = (byte)(1);
                                if ((z.depth) == (16))
                                {
                                    for (k = (int)(0); (k) < (s.img_n); ++k)
                                    {
                                        tc16[k] = ((ushort)(stbi__get16be(s)));
                                    }
                                }
                                else
                                {
                                    for (k = (int)(0); (k) < (s.img_n); ++k)
                                    {
                                        tc[k] = (byte)((byte)(stbi__get16be(s) & 255) * stbi__depth_scale_table[z.depth]);
                                    }
                                }
                            }
                            break;
                        }
                    case ((('I') << 24) + (('D') << 16) + (('A') << 8) + ('T')):
                        {
                            if ((first) != 0) return (int)(stbi__err("first not IHDR"));
                            if (((pal_img_n) != 0) && (pal_len == 0)) return (int)(stbi__err("no PLTE"));
                            if ((scan) == (STBI__SCAN_header))
                            {
                                s.img_n = (int)(pal_img_n);
                                return (int)(1);
                            }
                            if (((int)(ioff + c.length)) < ((int)(ioff))) return (int)(0);
                            if ((ioff + c.length) > (idata_limit))
                            {
                                uint idata_limit_old = (uint)(idata_limit);
                                byte* p;
                                if ((idata_limit) == (0)) idata_limit = (uint)((c.length) > (4096) ? c.length : 4096);
                                while ((ioff + c.length) > (idata_limit))
                                {
                                    idata_limit *= (uint)(2);
                                }
                                p = (byte*)(CRuntime.realloc(z.idata, (ulong)(idata_limit)));
                                if ((p) == (null)) return (int)(stbi__err("outofmem"));
                                z.idata = p;
                            }
                            if (stbi__getn(s, z.idata + ioff, (int)(c.length)) == 0) return (int)(stbi__err("outofdata"));
                            ioff += (uint)(c.length);
                            break;
                        }
                    case ((('I') << 24) + (('E') << 16) + (('N') << 8) + ('D')):
                        {
                            uint raw_len;
                            uint bpl;
                            if ((first) != 0) return (int)(stbi__err("first not IHDR"));
                            if (scan != STBI__SCAN_load) return (int)(1);
                            if ((z.idata) == (null)) return (int)(stbi__err("no IDAT"));
                            bpl = (uint)((s.img_x * z.depth + 7) / 8);
                            raw_len = (uint)(bpl * s.img_y * s.img_n + s.img_y);
                            z.expanded =
                                (byte*)
                                    (stbi_zlib_decode_malloc_guesssize_headerflag((sbyte*)(z.idata), (int)(ioff), (int)(raw_len),
                                        (int*)(&raw_len), is_iphone != 0 ? 0 : 1));
                            if ((z.expanded) == (null)) return (int)(0);
                            CRuntime.free(z.idata);
                            z.idata = (null);
                            if (((((req_comp) == (s.img_n + 1)) && (req_comp != 3)) && (pal_img_n == 0)) || ((has_trans) != 0))
                                s.img_out_n = (int)(s.img_n + 1);
                            else s.img_out_n = (int)(s.img_n);
                            if (
                                stbi__create_png_image(z, z.expanded, (uint)(raw_len), (int)(s.img_out_n), (int)(z.depth), (int)(color),
                                    (int)(interlace)) == 0) return (int)(0);
                            if ((has_trans) != 0)
                            {
                                if ((z.depth) == (16))
                                {
                                    if (stbi__compute_transparency16(z, tc16, (int)(s.img_out_n)) == 0) return (int)(0);
                                }
                                else
                                {
                                    if (stbi__compute_transparency(z, tc, (int)(s.img_out_n)) == 0) return (int)(0);
                                }
                            }
                            if ((((is_iphone) != 0) && ((stbi__de_iphone_flag) != 0)) && ((s.img_out_n) > (2))) stbi__de_iphone(z);
                            if ((pal_img_n) != 0)
                            {
                                s.img_n = (int)(pal_img_n);
                                s.img_out_n = (int)(pal_img_n);
                                if ((req_comp) >= (3)) s.img_out_n = (int)(req_comp);
                                if (stbi__expand_png_palette(z, palette, (int)(pal_len), (int)(s.img_out_n)) == 0) return (int)(0);
                            }
                            else if ((has_trans) != 0)
                            {
                                ++s.img_n;
                            }
                            CRuntime.free(z.expanded);
                            z.expanded = (null);
                            return (int)(1);
                        }
                    default:
                        if ((first) != 0) return (int)(stbi__err("first not IHDR"));
                        if ((c.type & (1 << 29)) == (0))
                        {
                            string invalid_chunk = "XXXX PNG chunk not known";
                            return (int)(stbi__err(invalid_chunk));
                        }
                        stbi__skip(s, (int)(c.length));
                        break;
                }
                stbi__get32be(s);
            }
        }

        public static void* stbi__do_png(stbi__png p, int* x, int* y, int* n, int req_comp, stbi__result_info* ri)
        {
            void* result = (null);
            if (((req_comp) < (0)) || ((req_comp) > (4)))
                return ((byte*)((ulong)((stbi__err("bad req_comp")) != 0 ? ((byte*)null) : (null))));
            if ((stbi__parse_png_file(p, (int)(STBI__SCAN_load), (int)(req_comp))) != 0)
            {
                if ((p.depth) < (8)) ri->bits_per_channel = (int)(8);
                else ri->bits_per_channel = (int)(p.depth);
                result = p._out_;
                p._out_ = (null);
                if (((req_comp) != 0) && (req_comp != p.s.img_out_n))
                {
                    if ((ri->bits_per_channel) == (8))
                        result = stbi__convert_format((byte*)(result), (int)(p.s.img_out_n), (int)(req_comp), (uint)(p.s.img_x),
                            (uint)(p.s.img_y));
                    else
                        result = stbi__convert_format16((ushort*)(result), (int)(p.s.img_out_n), (int)(req_comp), (uint)(p.s.img_x),
                            (uint)(p.s.img_y));
                    p.s.img_out_n = (int)(req_comp);
                    if ((result) == (null)) return result;
                }
                *x = (int)(p.s.img_x);
                *y = (int)(p.s.img_y);
                if ((n) != null) *n = (int)(p.s.img_n);
            }

            CRuntime.free(p._out_);
            p._out_ = (null);
            CRuntime.free(p.expanded);
            p.expanded = (null);
            CRuntime.free(p.idata);
            p.idata = (null);
            return result;
        }

        public static void* stbi__png_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri)
        {
            stbi__png p = new stbi__png();
            p.s = s;
            return stbi__do_png(p, x, y, comp, (int)(req_comp), ri);
        }

        public static int stbi__png_test(stbi__context s)
        {
            int r;
            r = (int)(stbi__check_png_header(s));
            stbi__rewind(s);
            return (int)(r);
        }

        public static int stbi__png_info_raw(stbi__png p, int* x, int* y, int* comp)
        {
            if (stbi__parse_png_file(p, (int)(STBI__SCAN_header), (int)(0)) == 0)
            {
                stbi__rewind(p.s);
                return (int)(0);
            }

            if ((x) != null) *x = (int)(p.s.img_x);
            if ((y) != null) *y = (int)(p.s.img_y);
            if ((comp) != null) *comp = (int)(p.s.img_n);
            return (int)(1);
        }

        public static int stbi__png_info(stbi__context s, int* x, int* y, int* comp)
        {
            stbi__png p = new stbi__png();
            p.s = s;
            return (int)(stbi__png_info_raw(p, x, y, comp));
        }

        public static int stbi__bmp_test_raw(stbi__context s)
        {
            int r;
            int sz;
            if (stbi__get8(s) != 'B') return (int)(0);
            if (stbi__get8(s) != 'M') return (int)(0);
            stbi__get32le(s);
            stbi__get16le(s);
            stbi__get16le(s);
            stbi__get32le(s);
            sz = (int)(stbi__get32le(s));
            r = (int)((((((sz) == (12)) || ((sz) == (40))) || ((sz) == (56))) || ((sz) == (108))) || ((sz) == (124)) ? 1 : 0);
            return (int)(r);
        }

        public static int stbi__bmp_test(stbi__context s)
        {
            int r = (int)(stbi__bmp_test_raw(s));
            stbi__rewind(s);
            return (int)(r);
        }

        public static int stbi__high_bit(uint z)
        {
            int n = (int)(0);
            if ((z) == (0)) return (int)(-1);
            if ((z) >= (0x10000))
            {
                n += (int)(16);
                z >>= 16;
            }

            if ((z) >= (0x00100))
            {
                n += (int)(8);
                z >>= 8;
            }

            if ((z) >= (0x00010))
            {
                n += (int)(4);
                z >>= 4;
            }

            if ((z) >= (0x00004))
            {
                n += (int)(2);
                z >>= 2;
            }

            if ((z) >= (0x00002))
            {
                n += (int)(1);
                z >>= 1;
            }

            return (int)(n);
        }

        public static int stbi__bitcount(uint a)
        {
            a = (uint)((a & 0x55555555) + ((a >> 1) & 0x55555555));
            a = (uint)((a & 0x33333333) + ((a >> 2) & 0x33333333));
            a = (uint)((a + (a >> 4)) & 0x0f0f0f0f);
            a = (uint)(a + (a >> 8));
            a = (uint)(a + (a >> 16));
            return (int)(a & 0xff);
        }

        public static int stbi__shiftsigned(int v, int shift, int bits)
        {
            int result;
            int z = (int)(0);
            if ((shift) < (0)) v <<= -shift;
            else v >>= shift;
            result = (int)(v);
            z = (int)(bits);
            while ((z) < (8))
            {
                result += (int)(v >> z);
                z += (int)(bits);
            }
            return (int)(result);
        }

        public static void* stbi__bmp_parse_header(stbi__context s, stbi__bmp_data* info)
        {
            int hsz;
            if ((stbi__get8(s) != 'B') || (stbi__get8(s) != 'M'))
                return ((byte*)((ulong)((stbi__err("not BMP")) != 0 ? ((byte*)null) : (null))));
            stbi__get32le(s);
            stbi__get16le(s);
            stbi__get16le(s);
            info->offset = (int)(stbi__get32le(s));
            info->hsz = (int)(hsz = (int)(stbi__get32le(s)));
            info->mr = (uint)(info->mg = (uint)(info->mb = (uint)(info->ma = (uint)(0))));
            if (((((hsz != 12) && (hsz != 40)) && (hsz != 56)) && (hsz != 108)) && (hsz != 124))
                return ((byte*)((ulong)((stbi__err("unknown BMP")) != 0 ? ((byte*)null) : (null))));
            if ((hsz) == (12))
            {
                s.img_x = (uint)(stbi__get16le(s));
                s.img_y = (uint)(stbi__get16le(s));
            }
            else
            {
                s.img_x = (uint)(stbi__get32le(s));
                s.img_y = (uint)(stbi__get32le(s));
            }

            if (stbi__get16le(s) != 1) return ((byte*)((ulong)((stbi__err("bad BMP")) != 0 ? ((byte*)null) : (null))));
            info->bpp = (int)(stbi__get16le(s));
            if ((info->bpp) == (1)) return ((byte*)((ulong)((stbi__err("monochrome")) != 0 ? ((byte*)null) : (null))));
            if (hsz != 12)
            {
                int compress = (int)(stbi__get32le(s));
                if (((compress) == (1)) || ((compress) == (2)))
                    return ((byte*)((ulong)((stbi__err("BMP RLE")) != 0 ? ((byte*)null) : (null))));
                stbi__get32le(s);
                stbi__get32le(s);
                stbi__get32le(s);
                stbi__get32le(s);
                stbi__get32le(s);
                if (((hsz) == (40)) || ((hsz) == (56)))
                {
                    if ((hsz) == (56))
                    {
                        stbi__get32le(s);
                        stbi__get32le(s);
                        stbi__get32le(s);
                        stbi__get32le(s);
                    }
                    if (((info->bpp) == (16)) || ((info->bpp) == (32)))
                    {
                        if ((compress) == (0))
                        {
                            if ((info->bpp) == (32))
                            {
                                info->mr = (uint)(0xffu << 16);
                                info->mg = (uint)(0xffu << 8);
                                info->mb = (uint)(0xffu << 0);
                                info->ma = (uint)(0xffu << 24);
                                info->all_a = (uint)(0);
                            }
                            else
                            {
                                info->mr = (uint)(31u << 10);
                                info->mg = (uint)(31u << 5);
                                info->mb = (uint)(31u << 0);
                            }
                        }
                        else if ((compress) == (3))
                        {
                            info->mr = (uint)(stbi__get32le(s));
                            info->mg = (uint)(stbi__get32le(s));
                            info->mb = (uint)(stbi__get32le(s));
                            if (((info->mr) == (info->mg)) && ((info->mg) == (info->mb)))
                            {
                                return ((byte*)((ulong)((stbi__err("bad BMP")) != 0 ? ((byte*)null) : (null))));
                            }
                        }
                        else return ((byte*)((ulong)((stbi__err("bad BMP")) != 0 ? ((byte*)null) : (null))));
                    }
                }
                else
                {
                    int i;
                    if ((hsz != 108) && (hsz != 124))
                        return ((byte*)((ulong)((stbi__err("bad BMP")) != 0 ? ((byte*)null) : (null))));
                    info->mr = (uint)(stbi__get32le(s));
                    info->mg = (uint)(stbi__get32le(s));
                    info->mb = (uint)(stbi__get32le(s));
                    info->ma = (uint)(stbi__get32le(s));
                    stbi__get32le(s);
                    for (i = (int)(0); (i) < (12); ++i)
                    {
                        stbi__get32le(s);
                    }
                    if ((hsz) == (124))
                    {
                        stbi__get32le(s);
                        stbi__get32le(s);
                        stbi__get32le(s);
                        stbi__get32le(s);
                    }
                }
            }

            return (void*)(1);
        }

        public static void* stbi__bmp_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri)
        {
            byte* _out_;
            uint mr = (uint)(0);
            uint mg = (uint)(0);
            uint mb = (uint)(0);
            uint ma = (uint)(0);
            uint all_a;
            byte* pal = stackalloc byte[256 * 4];
            int psize = (int)(0);
            int i;
            int j;
            int width;
            int flip_vertically;
            int pad;
            int target;
            stbi__bmp_data info = new stbi__bmp_data();
            info.all_a = (uint)(255);
            if ((stbi__bmp_parse_header(s, &info)) == (null)) return (null);
            flip_vertically = (int)(((int)(s.img_y)) > (0) ? 1 : 0);
            s.img_y = (uint)(CRuntime.abs((int)(s.img_y)));
            mr = (uint)(info.mr);
            mg = (uint)(info.mg);
            mb = (uint)(info.mb);
            ma = (uint)(info.ma);
            all_a = (uint)(info.all_a);
            if ((info.hsz) == (12))
            {
                if ((info.bpp) < (24)) psize = (int)((info.offset - 14 - 24) / 3);
            }
            else
            {
                if ((info.bpp) < (16)) psize = (int)((info.offset - 14 - info.hsz) >> 2);
            }

            s.img_n = (int)((ma) != 0 ? 4 : 3);
            if (((req_comp) != 0) && ((req_comp) >= (3))) target = (int)(req_comp);
            else target = (int)(s.img_n);
            if (stbi__mad3sizes_valid((int)(target), (int)(s.img_x), (int)(s.img_y), (int)(0)) == 0)
                return ((byte*)((ulong)((stbi__err("too large")) != 0 ? ((byte*)null) : (null))));
            _out_ = (byte*)(stbi__malloc_mad3((int)(target), (int)(s.img_x), (int)(s.img_y), (int)(0)));
            if (_out_ == null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            if ((info.bpp) < (16))
            {
                int z = (int)(0);
                if (((psize) == (0)) || ((psize) > (256)))
                {
                    CRuntime.free(_out_);
                    return ((byte*)((ulong)((stbi__err("invalid")) != 0 ? ((byte*)null) : (null))));
                }
                for (i = (int)(0); (i) < (psize); ++i)
                {
                    pal[i * 4 + 2] = (byte)(stbi__get8(s));
                    pal[i * 4 + 1] = (byte)(stbi__get8(s));
                    pal[i * 4 + 0] = (byte)(stbi__get8(s));
                    if (info.hsz != 12) stbi__get8(s);
                    pal[i * 4 + 3] = (byte)(255);
                }
                stbi__skip(s, (int)(info.offset - 14 - info.hsz - psize * ((info.hsz) == (12) ? 3 : 4)));
                if ((info.bpp) == (4)) width = (int)((s.img_x + 1) >> 1);
                else if ((info.bpp) == (8)) width = (int)(s.img_x);
                else
                {
                    CRuntime.free(_out_);
                    return ((byte*)((ulong)((stbi__err("bad bpp")) != 0 ? ((byte*)null) : (null))));
                }
                pad = (int)((-width) & 3);
                for (j = (int)(0); (j) < ((int)(s.img_y)); ++j)
                {
                    for (i = (int)(0); (i) < ((int)(s.img_x)); i += (int)(2))
                    {
                        int v = (int)(stbi__get8(s));
                        int v2 = (int)(0);
                        if ((info.bpp) == (4))
                        {
                            v2 = (int)(v & 15);
                            v >>= 4;
                        }
                        _out_[z++] = (byte)(pal[v * 4 + 0]);
                        _out_[z++] = (byte)(pal[v * 4 + 1]);
                        _out_[z++] = (byte)(pal[v * 4 + 2]);
                        if ((target) == (4)) _out_[z++] = (byte)(255);
                        if ((i + 1) == ((int)(s.img_x))) break;
                        v = (int)(((info.bpp) == (8)) ? stbi__get8(s) : v2);
                        _out_[z++] = (byte)(pal[v * 4 + 0]);
                        _out_[z++] = (byte)(pal[v * 4 + 1]);
                        _out_[z++] = (byte)(pal[v * 4 + 2]);
                        if ((target) == (4)) _out_[z++] = (byte)(255);
                    }
                    stbi__skip(s, (int)(pad));
                }
            }
            else
            {
                int rshift = (int)(0);
                int gshift = (int)(0);
                int bshift = (int)(0);
                int ashift = (int)(0);
                int rcount = (int)(0);
                int gcount = (int)(0);
                int bcount = (int)(0);
                int acount = (int)(0);
                int z = (int)(0);
                int easy = (int)(0);
                stbi__skip(s, (int)(info.offset - 14 - info.hsz));
                if ((info.bpp) == (24)) width = (int)(3 * s.img_x);
                else if ((info.bpp) == (16)) width = (int)(2 * s.img_x);
                else width = (int)(0);
                pad = (int)((-width) & 3);
                if ((info.bpp) == (24))
                {
                    easy = (int)(1);
                }
                else if ((info.bpp) == (32))
                {
                    if (((((mb) == (0xff)) && ((mg) == (0xff00))) && ((mr) == (0x00ff0000))) && ((ma) == (0xff000000)))
                        easy = (int)(2);
                }
                if (easy == 0)
                {
                    if (((mr == 0) || (mg == 0)) || (mb == 0))
                    {
                        CRuntime.free(_out_);
                        return ((byte*)((ulong)((stbi__err("bad masks")) != 0 ? ((byte*)null) : (null))));
                    }
                    rshift = (int)(stbi__high_bit((uint)(mr)) - 7);
                    rcount = (int)(stbi__bitcount((uint)(mr)));
                    gshift = (int)(stbi__high_bit((uint)(mg)) - 7);
                    gcount = (int)(stbi__bitcount((uint)(mg)));
                    bshift = (int)(stbi__high_bit((uint)(mb)) - 7);
                    bcount = (int)(stbi__bitcount((uint)(mb)));
                    ashift = (int)(stbi__high_bit((uint)(ma)) - 7);
                    acount = (int)(stbi__bitcount((uint)(ma)));
                }
                for (j = (int)(0); (j) < ((int)(s.img_y)); ++j)
                {
                    if ((easy) != 0)
                    {
                        for (i = (int)(0); (i) < ((int)(s.img_x)); ++i)
                        {
                            byte a;
                            _out_[z + 2] = (byte)(stbi__get8(s));
                            _out_[z + 1] = (byte)(stbi__get8(s));
                            _out_[z + 0] = (byte)(stbi__get8(s));
                            z += (int)(3);
                            a = (byte)((easy) == (2) ? stbi__get8(s) : 255);
                            all_a |= (uint)(a);
                            if ((target) == (4)) _out_[z++] = (byte)(a);
                        }
                    }
                    else
                    {
                        int bpp = (int)(info.bpp);
                        for (i = (int)(0); (i) < ((int)(s.img_x)); ++i)
                        {
                            uint v = (uint)((bpp) == (16) ? (uint)(stbi__get16le(s)) : stbi__get32le(s));
                            int a;
                            _out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mr), (int)(rshift), (int)(rcount))) & 255));
                            _out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mg), (int)(gshift), (int)(gcount))) & 255));
                            _out_[z++] = ((byte)((stbi__shiftsigned((int)(v & mb), (int)(bshift), (int)(bcount))) & 255));
                            a = (int)((ma) != 0 ? stbi__shiftsigned((int)(v & ma), (int)(ashift), (int)(acount)) : 255);
                            all_a |= (uint)(a);
                            if ((target) == (4)) _out_[z++] = ((byte)((a) & 255));
                        }
                    }
                    stbi__skip(s, (int)(pad));
                }
            }

            if (((target) == (4)) && ((all_a) == (0)))
                for (i = (int)(4 * s.img_x * s.img_y - 1); (i) >= (0); i -= (int)(4))
                {
                    _out_[i] = (byte)(255);
                }
            if ((flip_vertically) != 0)
            {
                byte t;
                for (j = (int)(0); (j) < ((int)(s.img_y) >> 1); ++j)
                {
                    byte* p1 = _out_ + j * s.img_x * target;
                    byte* p2 = _out_ + (s.img_y - 1 - j) * s.img_x * target;
                    for (i = (int)(0); (i) < ((int)(s.img_x) * target); ++i)
                    {
                        t = (byte)(p1[i]);
                        p1[i] = (byte)(p2[i]);
                        p2[i] = (byte)(t);
                    }
                }
            }

            if (((req_comp) != 0) && (req_comp != target))
            {
                _out_ = stbi__convert_format(_out_, (int)(target), (int)(req_comp), (uint)(s.img_x), (uint)(s.img_y));
                if ((_out_) == (null)) return _out_;
            }

            *x = (int)(s.img_x);
            *y = (int)(s.img_y);
            if ((comp) != null) *comp = (int)(s.img_n);
            return _out_;
        }

        public static int stbi__tga_get_comp(int bits_per_pixel, int is_grey, int* is_rgb16)
        {
            if ((is_rgb16) != null) *is_rgb16 = (int)(0);
            switch (bits_per_pixel)
            {
                case 8:
                    return (int)(STBI_grey);

                case 15:
                case 16:
                    if (((bits_per_pixel) == (16)) && ((is_grey) != 0)) return (int)(STBI_grey_alpha);
                    if ((is_rgb16) != null) *is_rgb16 = (int)(1);
                    return (int)(STBI_rgb);

                case 24:
                case 32:
                    return (int)(bits_per_pixel / 8);

                default:
                    return (int)(0);
            }
        }

        public static int stbi__tga_info(stbi__context s, int* x, int* y, int* comp)
        {
            int tga_w;
            int tga_h;
            int tga_comp;
            int tga_image_type;
            int tga_bits_per_pixel;
            int tga_colormap_bpp;
            int sz;
            int tga_colormap_type;
            stbi__get8(s);
            tga_colormap_type = (int)(stbi__get8(s));
            if ((tga_colormap_type) > (1))
            {
                stbi__rewind(s);
                return (int)(0);
            }

            tga_image_type = (int)(stbi__get8(s));
            if ((tga_colormap_type) == (1))
            {
                if ((tga_image_type != 1) && (tga_image_type != 9))
                {
                    stbi__rewind(s);
                    return (int)(0);
                }
                stbi__skip(s, (int)(4));
                sz = (int)(stbi__get8(s));
                if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32))
                {
                    stbi__rewind(s);
                    return (int)(0);
                }
                stbi__skip(s, (int)(4));
                tga_colormap_bpp = (int)(sz);
            }
            else
            {
                if ((((tga_image_type != 2) && (tga_image_type != 3)) && (tga_image_type != 10)) && (tga_image_type != 11))
                {
                    stbi__rewind(s);
                    return (int)(0);
                }
                stbi__skip(s, (int)(9));
                tga_colormap_bpp = (int)(0);
            }

            tga_w = (int)(stbi__get16le(s));
            if ((tga_w) < (1))
            {
                stbi__rewind(s);
                return (int)(0);
            }

            tga_h = (int)(stbi__get16le(s));
            if ((tga_h) < (1))
            {
                stbi__rewind(s);
                return (int)(0);
            }

            tga_bits_per_pixel = (int)(stbi__get8(s));
            stbi__get8(s);
            if (tga_colormap_bpp != 0)
            {
                if ((tga_bits_per_pixel != 8) && (tga_bits_per_pixel != 16))
                {
                    stbi__rewind(s);
                    return (int)(0);
                }
                tga_comp = (int)(stbi__tga_get_comp((int)(tga_colormap_bpp), (int)(0), (null)));
            }
            else
            {
                tga_comp =
                    (int)
                        (stbi__tga_get_comp((int)(tga_bits_per_pixel),
                            (((tga_image_type) == (3))) || (((tga_image_type) == (11))) ? 1 : 0, (null)));
            }

            if (tga_comp == 0)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            if ((x) != null) *x = (int)(tga_w);
            if ((y) != null) *y = (int)(tga_h);
            if ((comp) != null) *comp = (int)(tga_comp);
            return (int)(1);
        }

        public static int stbi__tga_test(stbi__context s)
        {
            int res = (int)(0);
            int sz;
            int tga_color_type;
            stbi__get8(s);
            tga_color_type = (int)(stbi__get8(s));
            if ((tga_color_type) > (1)) goto errorEnd;
            sz = (int)(stbi__get8(s));
            if ((tga_color_type) == (1))
            {
                if ((sz != 1) && (sz != 9)) goto errorEnd;
                stbi__skip(s, (int)(4));
                sz = (int)(stbi__get8(s));
                if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32)) goto errorEnd;
                stbi__skip(s, (int)(4));
            }
            else
            {
                if ((((sz != 2) && (sz != 3)) && (sz != 10)) && (sz != 11)) goto errorEnd;
                stbi__skip(s, (int)(9));
            }

            if ((stbi__get16le(s)) < (1)) goto errorEnd;
            if ((stbi__get16le(s)) < (1)) goto errorEnd;
            sz = (int)(stbi__get8(s));
            if ((((tga_color_type) == (1)) && (sz != 8)) && (sz != 16)) goto errorEnd;
            if (((((sz != 8) && (sz != 15)) && (sz != 16)) && (sz != 24)) && (sz != 32)) goto errorEnd;
            res = (int)(1);
            errorEnd:
            ;
            stbi__rewind(s);
            return (int)(res);
        }

        public static void stbi__tga_read_rgb16(stbi__context s, byte* _out_)
        {
            ushort px = (ushort)(stbi__get16le(s));
            ushort fiveBitMask = (ushort)(31);
            int r = (int)((px >> 10) & fiveBitMask);
            int g = (int)((px >> 5) & fiveBitMask);
            int b = (int)(px & fiveBitMask);
            _out_[0] = ((byte)((r * 255) / 31));
            _out_[1] = ((byte)((g * 255) / 31));
            _out_[2] = ((byte)((b * 255) / 31));
        }

        public static void* stbi__tga_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri)
        {
            int tga_offset = (int)(stbi__get8(s));
            int tga_indexed = (int)(stbi__get8(s));
            int tga_image_type = (int)(stbi__get8(s));
            int tga_is_RLE = (int)(0);
            int tga_palette_start = (int)(stbi__get16le(s));
            int tga_palette_len = (int)(stbi__get16le(s));
            int tga_palette_bits = (int)(stbi__get8(s));
            int tga_x_origin = (int)(stbi__get16le(s));
            int tga_y_origin = (int)(stbi__get16le(s));
            int tga_width = (int)(stbi__get16le(s));
            int tga_height = (int)(stbi__get16le(s));
            int tga_bits_per_pixel = (int)(stbi__get8(s));
            int tga_comp;
            int tga_rgb16 = (int)(0);
            int tga_inverted = (int)(stbi__get8(s));
            byte* tga_data;
            byte* tga_palette = (null);
            int i;
            int j;
            byte* raw_data = stackalloc byte[4];
            raw_data[0] = (byte)(0);

            int RLE_count = (int)(0);
            int RLE_repeating = (int)(0);
            int read_next_pixel = (int)(1);
            if ((tga_image_type) >= (8))
            {
                tga_image_type -= (int)(8);
                tga_is_RLE = (int)(1);
            }

            tga_inverted = (int)(1 - ((tga_inverted >> 5) & 1));
            if ((tga_indexed) != 0) tga_comp = (int)(stbi__tga_get_comp((int)(tga_palette_bits), (int)(0), &tga_rgb16));
            else tga_comp = (int)(stbi__tga_get_comp((int)(tga_bits_per_pixel), (tga_image_type) == (3) ? 1 : 0, &tga_rgb16));
            if (tga_comp == 0) return ((byte*)((ulong)((stbi__err("bad format")) != 0 ? ((byte*)null) : (null))));
            *x = (int)(tga_width);
            *y = (int)(tga_height);
            if ((comp) != null) *comp = (int)(tga_comp);
            if (stbi__mad3sizes_valid((int)(tga_width), (int)(tga_height), (int)(tga_comp), (int)(0)) == 0)
                return ((byte*)((ulong)((stbi__err("too large")) != 0 ? ((byte*)null) : (null))));
            tga_data = (byte*)(stbi__malloc_mad3((int)(tga_width), (int)(tga_height), (int)(tga_comp), (int)(0)));
            if (tga_data == null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            stbi__skip(s, (int)(tga_offset));
            if (((tga_indexed == 0) && (tga_is_RLE == 0)) && (tga_rgb16 == 0))
            {
                for (i = (int)(0); (i) < (tga_height); ++i)
                {
                    int row = (int)((tga_inverted) != 0 ? tga_height - i - 1 : i);
                    byte* tga_row = tga_data + row * tga_width * tga_comp;
                    stbi__getn(s, tga_row, (int)(tga_width * tga_comp));
                }
            }
            else
            {
                if ((tga_indexed) != 0)
                {
                    stbi__skip(s, (int)(tga_palette_start));
                    tga_palette = (byte*)(stbi__malloc_mad2((int)(tga_palette_len), (int)(tga_comp), (int)(0)));
                    if (tga_palette == null)
                    {
                        CRuntime.free(tga_data);
                        return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
                    }
                    if ((tga_rgb16) != 0)
                    {
                        byte* pal_entry = tga_palette;
                        for (i = (int)(0); (i) < (tga_palette_len); ++i)
                        {
                            stbi__tga_read_rgb16(s, pal_entry);
                            pal_entry += tga_comp;
                        }
                    }
                    else if (stbi__getn(s, tga_palette, (int)(tga_palette_len * tga_comp)) == 0)
                    {
                        CRuntime.free(tga_data);
                        CRuntime.free(tga_palette);
                        return ((byte*)((ulong)((stbi__err("bad palette")) != 0 ? ((byte*)null) : (null))));
                    }
                }
                for (i = (int)(0); (i) < (tga_width * tga_height); ++i)
                {
                    if ((tga_is_RLE) != 0)
                    {
                        if ((RLE_count) == (0))
                        {
                            int RLE_cmd = (int)(stbi__get8(s));
                            RLE_count = (int)(1 + (RLE_cmd & 127));
                            RLE_repeating = (int)(RLE_cmd >> 7);
                            read_next_pixel = (int)(1);
                        }
                        else if (RLE_repeating == 0)
                        {
                            read_next_pixel = (int)(1);
                        }
                    }
                    else
                    {
                        read_next_pixel = (int)(1);
                    }
                    if ((read_next_pixel) != 0)
                    {
                        if ((tga_indexed) != 0)
                        {
                            int pal_idx = (int)(((tga_bits_per_pixel) == (8)) ? stbi__get8(s) : stbi__get16le(s));
                            if ((pal_idx) >= (tga_palette_len))
                            {
                                pal_idx = (int)(0);
                            }
                            pal_idx *= (int)(tga_comp);
                            for (j = (int)(0); (j) < (tga_comp); ++j)
                            {
                                raw_data[j] = (byte)(tga_palette[pal_idx + j]);
                            }
                        }
                        else if ((tga_rgb16) != 0)
                        {
                            stbi__tga_read_rgb16(s, raw_data);
                        }
                        else
                        {
                            for (j = (int)(0); (j) < (tga_comp); ++j)
                            {
                                raw_data[j] = (byte)(stbi__get8(s));
                            }
                        }
                        read_next_pixel = (int)(0);
                    }
                    for (j = (int)(0); (j) < (tga_comp); ++j)
                    {
                        tga_data[i * tga_comp + j] = (byte)(raw_data[j]);
                    }
                    --RLE_count;
                }
                if ((tga_inverted) != 0)
                {
                    for (j = (int)(0); (j * 2) < (tga_height); ++j)
                    {
                        int index1 = (int)(j * tga_width * tga_comp);
                        int index2 = (int)((tga_height - 1 - j) * tga_width * tga_comp);
                        for (i = (int)(tga_width * tga_comp); (i) > (0); --i)
                        {
                            byte temp = (byte)(tga_data[index1]);
                            tga_data[index1] = (byte)(tga_data[index2]);
                            tga_data[index2] = (byte)(temp);
                            ++index1;
                            ++index2;
                        }
                    }
                }
                if (tga_palette != (null))
                {
                    CRuntime.free(tga_palette);
                }
            }

            if (((tga_comp) >= (3)) && (tga_rgb16 == 0))
            {
                byte* tga_pixel = tga_data;
                for (i = (int)(0); (i) < (tga_width * tga_height); ++i)
                {
                    byte temp = (byte)(tga_pixel[0]);
                    tga_pixel[0] = (byte)(tga_pixel[2]);
                    tga_pixel[2] = (byte)(temp);
                    tga_pixel += tga_comp;
                }
            }

            if (((req_comp) != 0) && (req_comp != tga_comp))
                tga_data = stbi__convert_format(tga_data, (int)(tga_comp), (int)(req_comp), (uint)(tga_width),
                    (uint)(tga_height));
            tga_palette_start =
                (int)(tga_palette_len = (int)(tga_palette_bits = (int)(tga_x_origin = (int)(tga_y_origin = (int)(0)))));
            return tga_data;
        }

        public static int stbi__psd_test(stbi__context s)
        {
            int r = (((stbi__get32be(s)) == (0x38425053))) ? 1 : 0;
            stbi__rewind(s);
            return (int)(r);
        }

        public static int stbi__psd_decode_rle(stbi__context s, byte* p, int pixelCount)
        {
            int count;
            int nleft;
            int len;
            count = (int)(0);
            while ((nleft = (int)(pixelCount - count)) > (0))
            {
                len = (int)(stbi__get8(s));
                if ((len) == (128))
                {
                }
                else if ((len) < (128))
                {
                    len++;
                    if ((len) > (nleft)) return (int)(0);
                    count += (int)(len);
                    while ((len) != 0)
                    {
                        *p = (byte)(stbi__get8(s));
                        p += 4;
                        len--;
                    }
                }
                else if ((len) > (128))
                {
                    byte val;
                    len = (int)(257 - len);
                    if ((len) > (nleft)) return (int)(0);
                    val = (byte)(stbi__get8(s));
                    count += (int)(len);
                    while ((len) != 0)
                    {
                        *p = (byte)(val);
                        p += 4;
                        len--;
                    }
                }
            }
            return (int)(1);
        }

        public static void* stbi__psd_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri,
            int bpc)
        {
            int pixelCount;
            int channelCount;
            int compression;
            int channel;
            int i;
            int bitdepth;
            int w;
            int h;
            byte* _out_;
            if (stbi__get32be(s) != 0x38425053)
                return ((byte*)((ulong)((stbi__err("not PSD")) != 0 ? ((byte*)null) : (null))));
            if (stbi__get16be(s) != 1) return ((byte*)((ulong)((stbi__err("wrong version")) != 0 ? ((byte*)null) : (null))));
            stbi__skip(s, (int)(6));
            channelCount = (int)(stbi__get16be(s));
            if (((channelCount) < (0)) || ((channelCount) > (16)))
                return ((byte*)((ulong)((stbi__err("wrong channel count")) != 0 ? ((byte*)null) : (null))));
            h = (int)(stbi__get32be(s));
            w = (int)(stbi__get32be(s));
            bitdepth = (int)(stbi__get16be(s));
            if ((bitdepth != 8) && (bitdepth != 16))
                return ((byte*)((ulong)((stbi__err("unsupported bit depth")) != 0 ? ((byte*)null) : (null))));
            if (stbi__get16be(s) != 3)
                return ((byte*)((ulong)((stbi__err("wrong color format")) != 0 ? ((byte*)null) : (null))));
            stbi__skip(s, (int)(stbi__get32be(s)));
            stbi__skip(s, (int)(stbi__get32be(s)));
            stbi__skip(s, (int)(stbi__get32be(s)));
            compression = (int)(stbi__get16be(s));
            if ((compression) > (1)) return ((byte*)((ulong)((stbi__err("bad compression")) != 0 ? ((byte*)null) : (null))));
            if (stbi__mad3sizes_valid((int)(4), (int)(w), (int)(h), (int)(0)) == 0)
                return ((byte*)((ulong)((stbi__err("too large")) != 0 ? ((byte*)null) : (null))));
            if (((compression == 0) && ((bitdepth) == (16))) && ((bpc) == (16)))
            {
                _out_ = (byte*)(stbi__malloc_mad3((int)(8), (int)(w), (int)(h), (int)(0)));
                ri->bits_per_channel = (int)(16);
            }
            else _out_ = (byte*)(stbi__malloc((ulong)(4 * w * h)));
            if (_out_ == null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            pixelCount = (int)(w * h);
            if ((compression) != 0)
            {
                stbi__skip(s, (int)(h * channelCount * 2));
                for (channel = (int)(0); (channel) < (4); channel++)
                {
                    byte* p;
                    p = _out_ + channel;
                    if ((channel) >= (channelCount))
                    {
                        for (i = (int)(0); (i) < (pixelCount); i++, p += 4)
                        {
                            *p = (byte)((channel) == (3) ? 255 : 0);
                        }
                    }
                    else
                    {
                        if (stbi__psd_decode_rle(s, p, (int)(pixelCount)) == 0)
                        {
                            CRuntime.free(_out_);
                            return ((byte*)((ulong)((stbi__err("corrupt")) != 0 ? ((byte*)null) : (null))));
                        }
                    }
                }
            }
            else
            {
                for (channel = (int)(0); (channel) < (4); channel++)
                {
                    if ((channel) >= (channelCount))
                    {
                        if (((bitdepth) == (16)) && ((bpc) == (16)))
                        {
                            ushort* q = ((ushort*)(_out_)) + channel;
                            ushort val = (ushort)((channel) == (3) ? 65535 : 0);
                            for (i = (int)(0); (i) < (pixelCount); i++, q += 4)
                            {
                                *q = (ushort)(val);
                            }
                        }
                        else
                        {
                            byte* p = _out_ + channel;
                            byte val = (byte)((channel) == (3) ? 255 : 0);
                            for (i = (int)(0); (i) < (pixelCount); i++, p += 4)
                            {
                                *p = (byte)(val);
                            }
                        }
                    }
                    else
                    {
                        if ((ri->bits_per_channel) == (16))
                        {
                            ushort* q = ((ushort*)(_out_)) + channel;
                            for (i = (int)(0); (i) < (pixelCount); i++, q += 4)
                            {
                                *q = ((ushort)(stbi__get16be(s)));
                            }
                        }
                        else
                        {
                            byte* p = _out_ + channel;
                            if ((bitdepth) == (16))
                            {
                                for (i = (int)(0); (i) < (pixelCount); i++, p += 4)
                                {
                                    *p = ((byte)(stbi__get16be(s) >> 8));
                                }
                            }
                            else
                            {
                                for (i = (int)(0); (i) < (pixelCount); i++, p += 4)
                                {
                                    *p = (byte)(stbi__get8(s));
                                }
                            }
                        }
                    }
                }
            }

            if ((channelCount) >= (4))
            {
                if ((ri->bits_per_channel) == (16))
                {
                    for (i = (int)(0); (i) < (w * h); ++i)
                    {
                        ushort* pixel = (ushort*)(_out_) + 4 * i;
                        if ((pixel[3] != 0) && (pixel[3] != 65535))
                        {
                            float a = (float)(pixel[3] / 65535.0f);
                            float ra = (float)(1.0f / a);
                            float inv_a = (float)(65535.0f * (1 - ra));
                            pixel[0] = ((ushort)(pixel[0] * ra + inv_a));
                            pixel[1] = ((ushort)(pixel[1] * ra + inv_a));
                            pixel[2] = ((ushort)(pixel[2] * ra + inv_a));
                        }
                    }
                }
                else
                {
                    for (i = (int)(0); (i) < (w * h); ++i)
                    {
                        byte* pixel = _out_ + 4 * i;
                        if ((pixel[3] != 0) && (pixel[3] != 255))
                        {
                            float a = (float)(pixel[3] / 255.0f);
                            float ra = (float)(1.0f / a);
                            float inv_a = (float)(255.0f * (1 - ra));
                            pixel[0] = ((byte)(pixel[0] * ra + inv_a));
                            pixel[1] = ((byte)(pixel[1] * ra + inv_a));
                            pixel[2] = ((byte)(pixel[2] * ra + inv_a));
                        }
                    }
                }
            }

            if (((req_comp) != 0) && (req_comp != 4))
            {
                if ((ri->bits_per_channel) == (16))
                    _out_ = (byte*)(stbi__convert_format16((ushort*)(_out_), (int)(4), (int)(req_comp), (uint)(w), (uint)(h)));
                else _out_ = stbi__convert_format(_out_, (int)(4), (int)(req_comp), (uint)(w), (uint)(h));
                if ((_out_) == (null)) return _out_;
            }

            if ((comp) != null) *comp = (int)(4);
            *y = (int)(h);
            *x = (int)(w);
            return _out_;
        }

        public static int stbi__gif_test_raw(stbi__context s)
        {
            int sz;
            if ((((stbi__get8(s) != 'G') || (stbi__get8(s) != 'I')) || (stbi__get8(s) != 'F')) || (stbi__get8(s) != '8'))
                return (int)(0);
            sz = (int)(stbi__get8(s));
            if ((sz != '9') && (sz != '7')) return (int)(0);
            if (stbi__get8(s) != 'a') return (int)(0);
            return (int)(1);
        }

        public static int stbi__gif_test(stbi__context s)
        {
            int r = (int)(stbi__gif_test_raw(s));
            stbi__rewind(s);
            return (int)(r);
        }

        public static int stbi__gif_header(stbi__context s, stbi__gif g, int* comp, int is_info)
        {
            byte version;
            if ((((stbi__get8(s) != 'G') || (stbi__get8(s) != 'I')) || (stbi__get8(s) != 'F')) || (stbi__get8(s) != '8'))
                return (int)(stbi__err("not GIF"));
            version = (byte)(stbi__get8(s));
            if ((version != '7') && (version != '9')) return (int)(stbi__err("not GIF"));
            if (stbi__get8(s) != 'a') return (int)(stbi__err("not GIF"));
            stbi__g_failure_reason = "";
            g.w = (int)(stbi__get16le(s));
            g.h = (int)(stbi__get16le(s));
            g.flags = (int)(stbi__get8(s));
            g.bgindex = (int)(stbi__get8(s));
            g.ratio = (int)(stbi__get8(s));
            g.transparent = (int)(-1);
            if (comp != null) *comp = (int)(4);
            if ((is_info) != 0) return (int)(1);
            if ((g.flags & 0x80) != 0) stbi__gif_parse_colortable(s, g.pal, (int)(2 << (g.flags & 7)), (int)(-1));
            return (int)(1);
        }

        public static int stbi__gif_info_raw(stbi__context s, int* x, int* y, int* comp)
        {
            stbi__gif g = new stbi__gif();
            if (stbi__gif_header(s, g, comp, (int)(1)) == 0)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            if ((x) != null) *x = (int)(g.w);
            if ((y) != null) *y = (int)(g.h);

            return (int)(1);
        }

        public static void stbi__out_gif_code(stbi__gif g, ushort code)
        {
            byte* p;
            byte* c;
            if ((g.codes[code].prefix) >= (0)) stbi__out_gif_code(g, (ushort)(g.codes[code].prefix));
            if ((g.cur_y) >= (g.max_y)) return;
            p = &g._out_[g.cur_x + g.cur_y];
            c = &g.color_table[g.codes[code].suffix * 4];
            if ((c[3]) >= (128))
            {
                p[0] = (byte)(c[2]);
                p[1] = (byte)(c[1]);
                p[2] = (byte)(c[0]);
                p[3] = (byte)(c[3]);
            }

            g.cur_x += (int)(4);
            if ((g.cur_x) >= (g.max_x))
            {
                g.cur_x = (int)(g.start_x);
                g.cur_y += (int)(g.step);
                while (((g.cur_y) >= (g.max_y)) && ((g.parse) > (0)))
                {
                    g.step = (int)((1 << g.parse) * g.line_size);
                    g.cur_y = (int)(g.start_y + (g.step >> 1));
                    --g.parse;
                }
            }
        }

        public static byte* stbi__process_gif_raster(stbi__context s, stbi__gif g)
        {
            byte lzw_cs;
            int len;
            int init_code;
            uint first;
            int codesize;
            int codemask;
            int avail;
            int oldcode;
            int bits;
            int valid_bits;
            int clear;
            stbi__gif_lzw* p;
            lzw_cs = (byte)(stbi__get8(s));
            if ((lzw_cs) > (12)) return (null);
            clear = (int)(1 << lzw_cs);
            first = (uint)(1);
            codesize = (int)(lzw_cs + 1);
            codemask = (int)((1 << codesize) - 1);
            bits = (int)(0);
            valid_bits = (int)(0);
            for (init_code = (int)(0); (init_code) < (clear); init_code++)
            {
                ((stbi__gif_lzw*)(g.codes))[init_code].prefix = (short)(-1);
                ((stbi__gif_lzw*)(g.codes))[init_code].first = ((byte)(init_code));
                ((stbi__gif_lzw*)(g.codes))[init_code].suffix = ((byte)(init_code));
            }
            avail = (int)(clear + 2);
            oldcode = (int)(-1);
            len = (int)(0);
            for (; ; )
            {
                if ((valid_bits) < (codesize))
                {
                    if ((len) == (0))
                    {
                        len = (int)(stbi__get8(s));
                        if ((len) == (0)) return g._out_;
                    }
                    --len;
                    bits |= (int)((int)(stbi__get8(s)) << valid_bits);
                    valid_bits += (int)(8);
                }
                else
                {
                    int code = (int)(bits & codemask);
                    bits >>= codesize;
                    valid_bits -= (int)(codesize);
                    if ((code) == (clear))
                    {
                        codesize = (int)(lzw_cs + 1);
                        codemask = (int)((1 << codesize) - 1);
                        avail = (int)(clear + 2);
                        oldcode = (int)(-1);
                        first = (uint)(0);
                    }
                    else if ((code) == (clear + 1))
                    {
                        stbi__skip(s, (int)(len));
                        while ((len = (int)(stbi__get8(s))) > (0))
                        {
                            stbi__skip(s, (int)(len));
                        }
                        return g._out_;
                    }
                    else if (code <= avail)
                    {
                        if ((first) != 0) return ((byte*)((ulong)((stbi__err("no clear code")) != 0 ? ((byte*)null) : (null))));
                        if ((oldcode) >= (0))
                        {
                            p = (stbi__gif_lzw*)g.codes + avail++;
                            if ((avail) > (4096)) return ((byte*)((ulong)((stbi__err("too many codes")) != 0 ? ((byte*)null) : (null))));
                            p->prefix = ((short)(oldcode));
                            p->first = (byte)(g.codes[oldcode].first);
                            p->suffix = (byte)(((code) == (avail)) ? p->first : g.codes[code].first);
                        }
                        else if ((code) == (avail))
                            return ((byte*)((ulong)((stbi__err("illegal code in raster")) != 0 ? ((byte*)null) : (null))));
                        stbi__out_gif_code(g, (ushort)(code));
                        if (((avail & codemask) == (0)) && (avail <= 0x0FFF))
                        {
                            codesize++;
                            codemask = (int)((1 << codesize) - 1);
                        }
                        oldcode = (int)(code);
                    }
                    else
                    {
                        return ((byte*)((ulong)((stbi__err("illegal code in raster")) != 0 ? ((byte*)null) : (null))));
                    }
                }
            }
        }

        public static void stbi__fill_gif_background(stbi__gif g, int x0, int y0, int x1, int y1)
        {
            int x;
            int y;
            byte* c = (byte*)g.pal + g.bgindex;
            for (y = (int)(y0); (y) < (y1); y += (int)(4 * g.w))
            {
                for (x = (int)(x0); (x) < (x1); x += (int)(4))
                {
                    byte* p = &g._out_[y + x];
                    p[0] = (byte)(c[2]);
                    p[1] = (byte)(c[1]);
                    p[2] = (byte)(c[0]);
                    p[3] = (byte)(0);
                }
            }
        }

        public static byte* stbi__gif_load_next(stbi__context s, stbi__gif g, int* comp, int req_comp)
        {
            int i;
            byte* prev_out = null;
            if (((g._out_) == null) && (stbi__gif_header(s, g, comp, (int)(0)) == 0)) return null;
            if (stbi__mad3sizes_valid((int)(g.w), (int)(g.h), (int)(4), (int)(0)) == 0)
                return ((byte*)((ulong)((stbi__err("too large")) != 0 ? ((byte*)null) : (null))));
            prev_out = g._out_;
            g._out_ = (byte*)(stbi__malloc_mad3((int)(4), (int)(g.w), (int)(g.h), (int)(0)));
            if ((g._out_) == null) return ((byte*)((ulong)((stbi__err("outofmem")) != 0 ? ((byte*)null) : (null))));
            switch ((g.eflags & 0x1C) >> 2)
            {
                case 0:
                    stbi__fill_gif_background(g, (int)(0), (int)(0), (int)(4 * g.w), (int)(4 * g.w * g.h));
                    break;

                case 1:
                    if ((prev_out) != null) CRuntime.memcpy(g._out_, prev_out, (ulong)(4 * g.w * g.h));
                    g.old_out = prev_out;
                    break;

                case 2:
                    if ((prev_out) != null) CRuntime.memcpy(g._out_, prev_out, (ulong)(4 * g.w * g.h));
                    stbi__fill_gif_background(g, (int)(g.start_x), (int)(g.start_y), (int)(g.max_x), (int)(g.max_y));
                    break;

                case 3:
                    if ((g.old_out) != null)
                    {
                        for (i = (int)(g.start_y); (i) < (g.max_y); i += (int)(4 * g.w))
                        {
                            CRuntime.memcpy(&g._out_[i + g.start_x], &g.old_out[i + g.start_x], (ulong)(g.max_x - g.start_x));
                        }
                    }
                    break;
            }

            for (; ; )
            {
                switch (stbi__get8(s))
                {
                    case 0x2C:
                        {
                            int prev_trans = (int)(-1);
                            int x;
                            int y;
                            int w;
                            int h;
                            byte* o;
                            x = (int)(stbi__get16le(s));
                            y = (int)(stbi__get16le(s));
                            w = (int)(stbi__get16le(s));
                            h = (int)(stbi__get16le(s));
                            if (((x + w) > (g.w)) || ((y + h) > (g.h)))
                                return ((byte*)((ulong)((stbi__err("bad Image Descriptor")) != 0 ? ((byte*)null) : (null))));
                            g.line_size = (int)(g.w * 4);
                            g.start_x = (int)(x * 4);
                            g.start_y = (int)(y * g.line_size);
                            g.max_x = (int)(g.start_x + w * 4);
                            g.max_y = (int)(g.start_y + h * g.line_size);
                            g.cur_x = (int)(g.start_x);
                            g.cur_y = (int)(g.start_y);
                            g.lflags = (int)(stbi__get8(s));
                            if ((g.lflags & 0x40) != 0)
                            {
                                g.step = (int)(8 * g.line_size);
                                g.parse = (int)(3);
                            }
                            else
                            {
                                g.step = (int)(g.line_size);
                                g.parse = (int)(0);
                            }
                            if ((g.lflags & 0x80) != 0)
                            {
                                stbi__gif_parse_colortable(s, g.lpal, (int)(2 << (g.lflags & 7)),
                                    (int)((g.eflags & 0x01) != 0 ? g.transparent : -1));
                                g.color_table = (byte*)(g.lpal);
                            }
                            else if ((g.flags & 0x80) != 0)
                            {
                                if (((g.transparent) >= (0)) && (g.eflags & 0x01) != 0)
                                {
                                    prev_trans = (int)(g.pal[g.transparent * 4 + 3]);
                                    g.pal[g.transparent * 4 + 3] = (byte)(0);
                                }
                                g.color_table = (byte*)(g.pal);
                            }
                            else return ((byte*)((ulong)((stbi__err("missing color table")) != 0 ? ((byte*)null) : (null))));
                            o = stbi__process_gif_raster(s, g);
                            if ((o) == (null)) return (null);
                            if (prev_trans != -1) g.pal[g.transparent * 4 + 3] = ((byte)(prev_trans));
                            return o;
                        }
                    case 0x21:
                        {
                            int len;
                            if ((stbi__get8(s)) == (0xF9))
                            {
                                len = (int)(stbi__get8(s));
                                if ((len) == (4))
                                {
                                    g.eflags = (int)(stbi__get8(s));
                                    g.delay = (int)(stbi__get16le(s));
                                    g.transparent = (int)(stbi__get8(s));
                                }
                                else
                                {
                                    stbi__skip(s, (int)(len));
                                    break;
                                }
                            }
                            while ((len = (int)(stbi__get8(s))) != 0)
                            {
                                stbi__skip(s, (int)(len));
                            }
                            break;
                        }
                    case 0x3B:
                        return null;

                    default:
                        return ((byte*)((ulong)((stbi__err("unknown code")) != 0 ? ((byte*)null) : (null))));
                }
            }
        }

        public static void* stbi__gif_load(stbi__context s, int* x, int* y, int* comp, int req_comp, stbi__result_info* ri)
        {
            byte* u = null;
            stbi__gif g = new stbi__gif();

            u = stbi__gif_load_next(s, g, comp, (int)(req_comp));
            if ((u) != null)
            {
                *x = (int)(g.w);
                *y = (int)(g.h);
                if (((req_comp) != 0) && (req_comp != 4))
                    u = stbi__convert_format(u, (int)(4), (int)(req_comp), (uint)(g.w), (uint)(g.h));
            }
            else if ((g._out_) != null) CRuntime.free(g._out_);

            return u;
        }

        public static int stbi__gif_info(stbi__context s, int* x, int* y, int* comp)
        {
            return (int)(stbi__gif_info_raw(s, x, y, comp));
        }

        public static int stbi__bmp_info(stbi__context s, int* x, int* y, int* comp)
        {
            void* p;
            stbi__bmp_data info = new stbi__bmp_data();
            info.all_a = (uint)(255);
            p = stbi__bmp_parse_header(s, &info);
            stbi__rewind(s);
            if ((p) == (null)) return (int)(0);
            if ((x) != null) *x = (int)(s.img_x);
            if ((y) != null) *y = (int)(s.img_y);
            if ((comp) != null) *comp = (int)((info.ma) != 0 ? 4 : 3);
            return (int)(1);
        }

        public static int stbi__psd_info(stbi__context s, int* x, int* y, int* comp)
        {
            int channelCount;
            int dummy;
            if (x == null) x = &dummy;
            if (y == null) y = &dummy;
            if (comp == null) comp = &dummy;
            if (stbi__get32be(s) != 0x38425053)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            if (stbi__get16be(s) != 1)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            stbi__skip(s, (int)(6));
            channelCount = (int)(stbi__get16be(s));
            if (((channelCount) < (0)) || ((channelCount) > (16)))
            {
                stbi__rewind(s);
                return (int)(0);
            }

            *y = (int)(stbi__get32be(s));
            *x = (int)(stbi__get32be(s));
            if (stbi__get16be(s) != 8)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            if (stbi__get16be(s) != 3)
            {
                stbi__rewind(s);
                return (int)(0);
            }

            *comp = (int)(4);
            return (int)(1);
        }

        public static int stbi__info_main(stbi__context s, int* x, int* y, int* comp)
        {
            if ((stbi__jpeg_info(s, x, y, comp)) != 0) return (int)(1);
            if ((stbi__png_info(s, x, y, comp)) != 0) return (int)(1);
            if ((stbi__gif_info(s, x, y, comp)) != 0) return (int)(1);
            if ((stbi__bmp_info(s, x, y, comp)) != 0) return (int)(1);
            if ((stbi__psd_info(s, x, y, comp)) != 0) return (int)(1);
            if ((stbi__tga_info(s, x, y, comp)) != 0) return (int)(1);
            return (int)(stbi__err("unknown image type"));
        }

        public static int stbi_info_from_memory(byte* buffer, int len, int* x, int* y, int* comp)
        {
            stbi__context s = new stbi__context();
            stbi__start_mem(s, buffer, (int)(len));
            return (int)(stbi__info_main(s, x, y, comp));
        }

        public static int stbi_info_from_callbacks(stbi_io_callbacks c, void* user, int* x, int* y, int* comp)
        {
            stbi__context s = new stbi__context();
            stbi__start_callbacks(s, c, user);
            return (int)(stbi__info_main(s, x, y, comp));
        }
    }
}