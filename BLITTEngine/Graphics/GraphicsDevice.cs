using System;
using BLITTEngine.Foundation;
using BLITTEngine.Temporal;

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
            Bgfx.SetRenderState(RenderState.BlendNormal | RenderState.CullClockwise);
            
        }

        public static void Shutdown()
        {
            Bgfx.Shutdown();
        }

        public static void DrawDebugInfo()
        {
            Bgfx.DebugTextClear();

            Bgfx.DebugTextWrite(2, 2, DebugColor.White, DebugColor.Black, $"FPS: {GameClock.FPS}");
        }

        public static void Resize(int width, int height)
        {

            Bgfx.Reset(width, height, ResetFlags.Vsync);
            Bgfx.SetViewRect(0, 0, 0, width, height);
        }

        public static void Begin()
        {
            
        }

        public static void Flip()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }
        
        
    }
}