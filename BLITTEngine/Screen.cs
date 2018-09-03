using System.Numerics;
using BLITTEngine.Temporal;

namespace BLITTEngine
{
    public static class Screen
    {
        private static int w;
        private static int h;
        private static bool fullscreen;

        internal static bool NeedsUpdate;

        internal static void Init()
        {
            Game.Platform.OnWinResized += OnWinResized;
            Game.Platform.OnWinMinimized += OnWinMinimized;
            Game.Platform.OnWinRestored += OnWinRestored;
        }

        private static void OnWinRestored()
        {
            Game.Focused = true;
            GameClock.Paused = false;
        }

        private static void OnWinMinimized()
        {
            Game.Focused = false;
            GameClock.Paused = true;
        }

        private static void OnWinResized()
        {
            Game.Platform.GetWindowSize(out int width, out int height);
            w = width;
            h = height;

        }

        public static int Width
        {
            get => w;
            set
            {
                if (w != value)
                {
                    w = value;
                    NeedsUpdate = true;
                }
            }
        }

        public static int Height
        {
            get => h;
            set
            {
                if (h != value)
                {
                    h = value;
                    NeedsUpdate = true;
                }
            }
        }
        
        public static float CenterX => w * 0.5f;
        public static float CenterY => h * 0.5f;
        public static Vector2 Center => new Vector2(CenterX, CenterY);
        

        public static bool Fullscreen
        {
            get => fullscreen;
            set
            {
                if (fullscreen != value)
                {
                    fullscreen = value;
                    NeedsUpdate = true;
                }
            }
        }
        
     

        public static void ToggleFullscreen()
        {
            Fullscreen = !Fullscreen;

        }

        public static void OnPlatformWindowResize(int width, int height)
        {
            w = width;
            h = height;
        }

        public static void ShowCursor(bool show)
        {
            Game.Platform.ShowWindowCursor(show);
        }

    }
}