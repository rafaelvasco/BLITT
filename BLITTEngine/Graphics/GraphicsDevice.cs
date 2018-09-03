using System;
using System.ComponentModel;
using BLITTEngine.Foundation;

namespace BLITTEngine.Graphics
{
    internal static class GraphicsDevice
    {
        public static void Init(int width, int height, IntPtr targetHandle)
        {
            PlatformData platformData = new PlatformData
            {
                WindowHandle = targetHandle
            };


            Bgfx.SetPlatformData(platformData);
            
            Bgfx.Init();
            
            Bgfx.Reset(width, height, ResetFlags.Vsync);
            Bgfx.SetViewRect(0, 0, 0, width, height);
            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);
            Bgfx.SetViewClear(0, ClearTargets.Color, Color.CornflowerBlue.RGBAI);
            
        }

        public static void Shutdown()
        {
            Bgfx.Shutdown();
        }

        public static void Resize(int width, int height)
        {
            Bgfx.Reset(width, height, ResetFlags.Vsync);
            Bgfx.SetViewRect(0, 0, 0, width, height);
        }

        public static void Flip()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }
        
        
    }
}