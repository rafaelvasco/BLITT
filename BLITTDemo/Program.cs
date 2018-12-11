using BLITTEngine;

namespace BLITTDemo
{
    internal static class Program
    {
        private static void Main()
        {
            var props = new GameProps()
            {
                Title = "BLITT DEMOS",
                CanvasWidth = 640,
                CanvasHeight = 480,
                Fullscreen = false,
            };

            using (var game = new Game(props))
            {
                game.Start(new Demo1());
            }
        }
    }
}