using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Foundation
{
    internal enum OS
    {
        Windows,
        Linux,
        MacOSX,
        Unknown
    }

    internal static class CurrentPlatform
    {
        private static bool init;
        private static OS os;

        public static OS OS
        {
            get
            {
                Init();
                return os;
            }
        }

        [DllImport("libc")]
        private static extern int uname(IntPtr buf);

        private static void Init()
        {
            if (!init)
            {
                var pid = Environment.OSVersion.Platform;

                switch (pid)
                {
                    case PlatformID.Win32NT:
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.WinCE:
                        os = OS.Windows;
                        break;

                    case PlatformID.MacOSX:
                        os = OS.MacOSX;
                        break;

                    case PlatformID.Unix:

                        // Mac can return a value of Unix sometimes, We need to double check it.
                        var buf = IntPtr.Zero;
                        try
                        {
                            buf = Marshal.AllocHGlobal(8192);

                            if (uname(buf) == 0)
                            {
                                var sos = Marshal.PtrToStringAnsi(buf);
                                if (sos == "Darwin")
                                {
                                    os = OS.MacOSX;
                                    return;
                                }
                            }
                        }
                        finally
                        {
                            if (buf != IntPtr.Zero)
                                Marshal.FreeHGlobal(buf);
                        }

                        os = OS.Linux;
                        break;

                    default:
                        os = OS.Unknown;
                        break;
                }

                init = true;
            }
        }
    }
}