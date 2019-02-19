using BLITTEngine;

namespace BLITTDemo
{
    internal static class Program
    {
        private static void Main()
        {
            using (var game = new Game())
            {
                game.Start(new Demo2());
            }
        }
    }
}