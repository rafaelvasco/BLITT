using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Graphics
{
    internal class DrawBuffer : IDisposable
    {
        public IntPtr BufferPtr => buffer_ptr;
        public VertexPCT[] Data => data;

        private IntPtr buffer_ptr;
        private GCHandle gc_handle;
        private VertexPCT[] data;

        public DrawBuffer(int length)
        {
            data = new VertexPCT[length];
            gc_handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            buffer_ptr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);
        }

        public void Dispose()
        {
            gc_handle.Free();
            data = null;
            GC.SuppressFinalize(this);
        }
    }
}