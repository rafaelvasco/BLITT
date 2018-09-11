using BLITTEngine;

namespace BLITTDemo
{
    static class Program
    {
        private static void Main()
        {
            var props = new GameProps()
            {
                Title = "BLITT DEMO",
                ScreenWidth = 800,
                ScreenHeight = 600,
                Fullscreen = false,
                StartingScene = new Demo()
            };

            Game.Run(props);
        }
    }
}