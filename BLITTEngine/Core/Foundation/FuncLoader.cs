using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BLITTEngine.Core.Foundation
{
    internal static class FuncLoader
    {
        private static class Windows
        {
            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

            [DllImport("kernel32", EntryPoint = "LoadLibrary")]
            public static extern IntPtr LoadLibraryW(string lpszLib);
        }

        private static class Linux
        {
            [DllImport("libdl.so.2")]
            public static extern IntPtr dlopen(string path, int flags);

            [DllImport("libdl.so.2")]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        private static class OSX
        {
            [DllImport("/usr/lib/libSystem.dylib")]
            public static extern IntPtr dlopen(string path, int flags);

            [DllImport("/usr/lib/libSystem.dylib")]
            public static extern IntPtr dlsym(IntPtr handle, string symbol);
        }

        private const int RTLD_LAZY = 0x0001;

        public static IntPtr LoadLibrary(string libname)
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            libname = Path.Combine(assemblyLocation, libname);

            switch (CurrentPlatform.OS)
            {
                case OS.Windows:
                    return Windows.LoadLibraryW(libname);

                case OS.MacOSX:
                    return OSX.dlopen(libname, RTLD_LAZY);

                default:
                    return Linux.dlopen(libname, RTLD_LAZY);
            }
        }

        public static T LoadFunction<T>(IntPtr library, string function, bool throwIfNotFound = false)
        {
            var ret = IntPtr.Zero;

            switch (CurrentPlatform.OS)
            {
                case OS.Windows:
                    ret = Windows.GetProcAddress(library, function);
                    break;

                case OS.MacOSX:
                    ret = OSX.dlsym(library, function);
                    break;

                default:
                    ret = Linux.dlsym(library, function);
                    break;
            }

            if (ret == IntPtr.Zero)
            {
                if (throwIfNotFound)
                    throw new EntryPointNotFoundException(function);

                return default(T);
            }

            return Marshal.GetDelegateForFunctionPointer<T>(ret);
        }
    }
}