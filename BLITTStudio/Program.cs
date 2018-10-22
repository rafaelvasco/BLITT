using BLITTEngine;
using System;

namespace BLITTStudio
{
    static class Program
    {
        static void Main(string[] args)
        {
            var props = new GameProps()
            {
                Title = "BLITT DEMO",
                CanvasWidth = 1024,
                CanvasHeight = 768,
                Fullscreen = false,
            };

            using (var game = new Game(props))
            {
                game.Start(new Studio());
            }
        }
    }
}
