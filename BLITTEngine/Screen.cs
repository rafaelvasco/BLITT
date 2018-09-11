using System.Numerics;
using BLITTEngine.Temporal;

namespace BLITTEngine
{
    public static class Screen
    {
        private static int w;
        private static int h;
        private static bool full_screen;

        internal static bool ScreenResized;
        internal static bool ScreenToggledUpdate;

        internal static void Init(int width, int height, bool is_full_screen)
        {
            w = width;
            h = height;
            full_screen = is_full_screen;
            
            Game.Platform.OnWinResized += OnPlatformWinResized;
            Game.Platform.OnWinMinimized += OnPlatformWinMinimized;
            Game.Platform.OnWinRestored += OnPlatformWinRestored;
        }

        private static void OnPlatformWinRestored()
        {
            Game.Focused = true;
            GameClock.Paused = false;
        }

        private static void OnPlatformWinMinimized()
        {
            Game.Focused = false;
            GameClock.Paused = true;
        }

        private static void OnPlatformWinResized(int width, int height)
        {
            w = width;
            h = height;
        }

        public static int Width => w;

        public static int Height => h;
        
        public static float CenterX => Width * 0.5f;
        public static float CenterY => Height * 0.5f;
        public static Vector2 Center => new Vector2(CenterX, CenterY);
        

        public static bool Fullscreen
        {
            get => full_screen;
            set
            {
                if (full_screen != value)
                {
                    full_screen = value;
                    ScreenToggledUpdate = true;
                }
            }
        }
        
     

        public static void ToggleFullscreen()
        {
            Fullscreen = !Fullscreen;
        }

        public static void Resize(int new_w, int new_h)
        {
            if (Fullscreen)
            {
                return;
            }
            
            w = new_w;
            h = new_h;
            ScreenResized = true;
        }


        public static void ShowCursor(bool show)
        {
            Game.Platform.ShowWindowCursor(show);
        }

    }
}