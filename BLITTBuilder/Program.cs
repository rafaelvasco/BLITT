using System.IO;
using BLITTEngine.Core.Resources;
using PowerArgs;

namespace BLITTBuilder
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Args.InvokeAction<BuilderExecutor>(args);
        }
    }
}