using System;
using static BLITTEngine.Foundation.SDLGpu;

namespace BLITTEngine.Graphics
{
    public class Canvas : IDisposable
    {
        private readonly IntPtr gfx_ctx;
        
        internal Canvas(uint windowId, int viewport_width, int viewport_height)
        {
            GPU_SetDebugLevel(GPU_DebugLevel.LEVEL_MAX);
            
            GPU_SetInitWindow(windowId);

            gfx_ctx = GPU_Init((ushort) viewport_width, (ushort) viewport_height, GPU_DEFAULT_INIT_FLAGS);

            if (gfx_ctx == IntPtr.Zero)
            {
                throw new Exception("Failed to Initialize SDL_Gpu");
            }
            
        }

        internal void Begin()
        {
            GPU_Clear(gfx_ctx);
        }

        internal void End()
        {
            GPU_Flip(gfx_ctx);
        }

        public void Dispose()
        {
            GPU_Quit();
        }
    }
}