using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Foundation
{
    public static class PtrUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PtrToStruct<T>(IntPtr ptr) where T : struct
        {
            return Marshal.PtrToStructure<T>(ptr);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr StructToPtr<T>(T value) where T : struct
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
            Marshal.StructureToPtr(value, ptr, false);
            return ptr;
        }

        public static byte[] IntPtrToByteArray(IntPtr ptr, int length)
        {
            byte[] data = new byte[length];
            Marshal.Copy(ptr, data, 0, length);

            return data;
        }
    }
}