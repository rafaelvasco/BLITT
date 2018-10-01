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
                CanvasWidth = 800,
                CanvasHeight = 600,
                Fullscreen = false,
            };

            using (var game = new Game(props))
            {
                game.Start();
            }
        }
    }
}