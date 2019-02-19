using BLITTEngine;

namespace BLITTStudio
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Start(new Studio());
            }
        }
    }
}