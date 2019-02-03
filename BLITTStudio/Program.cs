using BLITTEngine;

namespace BLITTStudio
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var props = new GameProps
            {
                Title = "BLITT Studio",
                CanvasWidth = 1024,
                CanvasHeight = 768,
                Fullscreen = false
            };

            using (var game = new Game(props))
            {
                game.Start(new Studio());
            }
        }
    }
}