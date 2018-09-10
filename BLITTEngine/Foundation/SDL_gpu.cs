using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BLITTEngine.Foundation
{
    internal static class SDLGpu
    {
        private static readonly IntPtr sdl_gpu_lib = LoadSDLGPU();

        private static IntPtr LoadSDLGPU()
        {
            string lib_name;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                lib_name = "libSDL2_gpu.dll";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                lib_name = "libSDL2_gpu.so.0";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                lib_name = "libSDL2_gpu.dylib";
                
            }
            else
            {
                Debug.WriteLine("Unknown SDL platform. Attempting to load \"SDL2\"");
                lib_name = "SDL2.dll";
            }



            var lib = FuncLoader.LoadLibrary(lib_name); 

            GPU_SetDebugLevel_f = FuncLoader.LoadFunction<GPU_SetDebugLevel_d>(lib, nameof(GPU_SetDebugLevel));
            GPU_SetInitWindow_f = FuncLoader.LoadFunction<GPU_SetInitWindow_d>(lib, nameof(GPU_SetInitWindow));
            GPU_Init_f = FuncLoader.LoadFunction<GPU_Init_d>(lib, nameof(GPU_Init));
            GPU_Clear_f = FuncLoader.LoadFunction<GPU_Clear_d>(lib, nameof(GPU_Clear));
            GPU_ClearRGB_f = FuncLoader.LoadFunction<GPU_ClearRGB_d>(lib, nameof(GPU_ClearRGB));
            GPU_LoadTarget_f = FuncLoader.LoadFunction<GPU_LoadTarget_d>(lib, nameof(GPU_LoadTarget));
            GPU_FreeTarget_f = FuncLoader.LoadFunction<GPU_FreeTarget_d>(lib, nameof(GPU_FreeTarget));
            GPU_SetVirtualResolution_f = FuncLoader.LoadFunction<GPU_SetVirtualResolution_d>(lib, nameof(GPU_SetVirtualResolution));
            GPU_SetViewport_f = FuncLoader.LoadFunction<GPU_SetViewport_d>(lib, nameof(GPU_SetViewport));
            GPU_LoadImage_f = FuncLoader.LoadFunction<GPU_LoadImage_d>(lib, nameof(GPU_LoadImage));
            GPU_FreeImage_f = FuncLoader.LoadFunction<GPU_FreeImage_d>(lib, nameof(GPU_FreeImage));
            GPU_CreateImage_f = FuncLoader.LoadFunction<GPU_CreateImage_d>(lib, nameof(GPU_CreateImage));
            GPU_UpdateImage_f = FuncLoader.LoadFunction<GPU_UpdateImage_d>(lib, nameof(GPU_UpdateImage));
            GPU_UpdateImageBytes_f = FuncLoader.LoadFunction<GPU_UpdateImageBytes_d>(lib, nameof(GPU_UpdateImageBytes));
            GPU_UpdateImageBytes_f2 = FuncLoader.LoadFunction<GPU_UpdateImageBytes_d2>(lib, nameof(GPU_UpdateImageBytes));
            GPU_Flip_f = FuncLoader.LoadFunction<GPU_Flip_d>(lib, nameof(GPU_Flip));
            GPU_Quit_f = FuncLoader.LoadFunction<GPU_Quit_d>(lib, nameof(GPU_Quit));
            GPU_Blit_f = FuncLoader.LoadFunction<GPU_Blit_d>(lib, nameof(GPU_Blit));
            GPU_Blit_f2 = FuncLoader.LoadFunction<GPU_Blit_d2>(lib, nameof(GPU_Blit));
            GPU_BlitRect_f = FuncLoader.LoadFunction<GPU_BlitRect_d>(lib, nameof(GPU_Blit));
            GPU_BlitRect_f2 = FuncLoader.LoadFunction<GPU_BlitRect_d2>(lib, nameof(GPU_Blit));
            GPU_RectangleFilled_f = FuncLoader.LoadFunction<GPU_RectangleFilled_d>(lib, nameof(GPU_RectangleFilled));
            GPU_Rectangle_f = FuncLoader.LoadFunction<GPU_Rectangle_d>(lib, nameof(GPU_Rectangle));
            GPU_Line_f = FuncLoader.LoadFunction<GPU_Line_d>(lib, nameof(GPU_Line));
            GPU_Pixel_f = FuncLoader.LoadFunction<GPU_Pixel_d>(lib, nameof(GPU_Pixel));
            GPU_SetTargetRGB_f = FuncLoader.LoadFunction<GPU_SetTargetRGB_d>(lib, nameof(GPU_SetTargetRGB));
            GPU_SetTargetRGBA_f = FuncLoader.LoadFunction<GPU_SetTargetRGBA_d>(lib, nameof(GPU_SetTargetRGBA));
            GPU_Circle_f = FuncLoader.LoadFunction<GPU_Circle_d>(lib, nameof(GPU_Circle));
            GPU_CircleFilled_f = FuncLoader.LoadFunction<GPU_CircleFilled_d>(lib, nameof(GPU_CircleFilled));
            
            
                
            

            return lib;
        }


        /* ENUMS _ CONSTANTS */

        public enum GPU_RendererBackEnd
        {
            UNKNOWN = 0,
            OPENGL1_BASE = 1,
            OPENGL1 = 2,
            OPENGL_2 = 3,
            OPENGL_3 = 4,
            OPENGL_5 = 5,
            GLES_1 = 11,
            GLES_2 = 12,
            GLES_3 = 13,
            D3D9 = 21,
            D3D10 = 22,
            D3D11 = 23
        }
        
        public enum GPU_Comparison
        {
            NEVER = 0x0200,
            LESS = 0x0201,
            EQUAL = 0x0202,
            LEQUAL = 0x0203,
            GREATER = 0x0204,
            NOTEQUAL = 0x0205,
            GEQUAL = 0x0206,
            ALWAYS = 0x0207,
        }
        
        public enum GPU_BlendFunc
        {
            ZERO = 0,
            ONE = 1,
            SRC_COLOR = 0x0300,
            DST_COLOR = 0x0306,
            ONE_MINUS_SRC = 0x0301,
            ONE_MINUS_DST = 0x0307,
            SRC_ALPHA = 0x0302,
            DST_ALPHA = 0x0304,
            ONE_MINUS_SRC_ALPHA = 0x0303,
            ONE_MINUS_DST_ALPHA = 0x0305
        }

        public enum GPU_BlendEq
        {
            EQ_ADD = 0x8006,
            EQ_SUBTRACT = 0x800A,
            EQ_REVERSE_SUBTRACT = 0x800B
        }
        
        public enum GPU_BlendPreset
        {
            BLEND_NORMAL = 0,
            BLEND_PREMULTIPLIED_ALPHA = 1,
            BLEND_MULTIPLY = 2,
            BLEND_ADD = 3,
            BLEND_SUBTRACT = 4,
            BLEND_MOD_ALPHA = 5,
            BLEND_SET_ALPHA = 6,
            BLEND_SET = 7,
            BLEND_NORMAL_KEEP_ALPHA = 8,
            BLEND_NORMAL_ADD_ALPHA = 9,
            BLEND_NORMAL_FACTOR_ALPHA = 10
        }

        public enum GPU_Filter
        {
            FILTER_NEAREST = 0,
            FILTER_LINEAR = 1,
            FILTER_LINEAR_MIPMAP = 2
        }
        
        public enum GPU_Snap
        {
            SNAP_NONE = 0,
            SNAP_POSITION = 1,
            SNAP_DIMENSIONS = 2,
            SNAP_POSITION_AND_DIMENSIONS = 3
        }
        
        public enum GPU_Wrap
        {
            WRAP_NONE = 0,
            WRAP_REPEAT = 1,
            WRAP_MIRRORED = 2
        }

        public enum GPU_Format
        {
            FORMAT_LUMINANCE = 1,
            FORMAT_LUMINANCE_ALPHA = 2,
            FORMAT_RGB = 3,
            FORMAT_RGBA = 4,
            FORMAT_ALPHA = 5,
            FORMAT_RG = 6,
            FORMAT_YCbCr422 = 7,
            FORMAT_YCbCr420P = 8,
            FORMAT_BGR = 9,
            FORMAT_BGRA = 10,
            FORMAT_ABGR = 11
        }
        
        public enum GPU_FileFormat
        {
            FILE_AUTO = 0,
            FILE_PNG,
            FILE_BMP,
            FILE_TGA
        }

        public enum GPU_Feature
        {
            NON_POWER_OF_TWO = 0x1,
            RENDER_TARGETS = 0x2,
            BLEND_EQUATIONS = 0x4,
            BLEND_FUNC_SEPARATE = 0x8,
            BLEND_EQUATIONS_SEPARATE = 0x10,
            GL_BGR = 0x20,
            GL_BGRA = 0x40,
            GL_ABGR = 0x80,
            VERTEX_SHADER = 0x100,
            FRAGMENT_SHADER = 0x200,
            PIXEL_SHADER = 0x200,
            GEOMETRY_SHADER = 0x400,
            WRAP_REPEAT_MIRRORED = 0x800,
            CORE_FRAMEBUFFER_OBJECTS = 0x1000,
            
            ALL_BASE = RENDER_TARGETS,
            ALL_BLEND_PRESETS = BLEND_EQUATIONS | BLEND_FUNC_SEPARATE,
            ALL_GL_FORMATS = GL_BGR | GL_BGRA | GL_ABGR,
            BASIC_SHADERS = FRAGMENT_SHADER | VERTEX_SHADER,
            ALL_SHADERS = FRAGMENT_SHADER | VERTEX_SHADER | GEOMETRY_SHADER
        }
        
        public enum GPU_InitFlag
        {
            ENABLE_VSYNC = 0x1,
            DISABLE_VSYNC = 0x2,
            DISABLE_DOUBLE_BUFFR = 0x4,
            DISABLE_AUTO_VIRTUAL_RESOLUTION = 0x8,
            REQUEST_COMPATIBILITY_PROFILE = 0x10,
            USE_ROW_BY_ROW_TEXTURE_UPLOAD_FALLBACK = 0x20,
            USE_COPY_TEXTURE_UPLOAD_FALLBACK = 0x40
        }

        public enum GPU_Primitive
        {
            POINTS = 0x0,
            LINES = 0x1,
            LINE_LOOP = 0x2,
            LINE_STRIP = 0x3,
            TRIANGLES = 0x4,
            TRIANGLE_STRIP = 0x5,
            TRIANGLE_FAN = 0x6
        }
        
        

        public enum GPU_BatchFlag
        {
            XY = 0x1,
            XYZ = 0x2,
            ST = 0x4,
            RGB = 0x8,
            RGBA = 0x10,
            RGB8 = 0x20,
            RGBA8 = 0x40,
            
            XY_ST = (XY | ST),
            XYZ_ST = (XYZ | ST),
            XY_RGB = (XY | RGB),
            XYZ_RGB = (XYZ | RGB),
            XY_RGBA = (XY | RGBA),
            XYZ_RGBA = (XYZ | RGBA),
            XY_ST_RGBA = (XY | ST | RGBA),
            XYZ_ST_RGBA = (XYZ | ST | RGBA),
            XY_RGB8 = (XY | RGB8),
            XYZ_RGB8 = (XYZ | RGB8),
            XY_RGBA8 = (XY | RGBA8),
            XYZ_RGBA8 = (XYZ | RGBA8),
            XY_ST_RGBA8 = (XY | ST | RGBA8),
            XYZ_ST_RGBA8 = (XYZ | ST | RGBA8)
        }
        
        public enum GPU_FlipMode
        {
            NONE = 0x0,
            HORIZONTAL = 0x1,
            VERTICAL = 0x2
        }

        public enum GPU_Type
        {
            BYTE = 0x1400,
            UNSIGNED_BYTE = 0x1401,
            SHORT = 0x1402,
            UNSIGNED_SHORT = 0x1403,
            INT = 0x1404,
            UNSIGNED_INT = 0x1405,
            FLOAT = 0x1406,
            DOUBLE = 0x140A
        }
        
        public enum GPU_Shader
        {
            VERTEX_SHADER = 0,
            FRAGMENT_SHADER = 1,
            PIXEL_SHADER = 1,
            GEOMETRY_SHADER = 2
        }
        
        public enum GPU_ShaderLanguage
        {
            NONE = 0,
            ARB_ASSEMBLY = 1,
            GLSL = 2,
            GLSLES = 3,
            HLSL = 4,
            CG = 5
        }

        public enum GPU_Error
        {
            NONE = 0,
            BACKEND_ERROR = 1,
            DATA_ERROR = 2,
            USER_ERROR = 3,
            UNSUPPORTED_FUNCTION = 4,
            NULL_ARGUMENT = 5,
            FILE_NOT_FOUND = 6
        }

        public enum GPU_DebugLevel
        {
            LEVEL_0 = 0,
            LEVEL_1 = 1,
            LEVEL_2 = 2,
            LEVEL_3 = 3,
            LEVEL_MAX = 3
        }

        public enum GPU_LogLevel
        {
            INFO = 0,
            WARNING,
            ERROR
        }
        
        public const int GPU_MODELVIEW = 0;
        public const int GPU_PROJECTION = 1;
        public const int GPU_DEFAULT_INIT_FLAGS = 0;
        public const int GPU_NONE = 0x0;
        
        /* STRUCTS */
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Rect
        {
            public float x;
            public float y;
            public float w;
            public float h;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_RendererID
        {
            public string name;
            public GPU_RendererBackEnd renderer;
            public int major_version;
            public int minor_version;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_BlendMode
        {
            GPU_BlendFunc source_color;
            GPU_BlendFunc dest_color;
            GPU_BlendFunc source_alpha;
            GPU_BlendFunc dest_alpha;
            GPU_BlendEq color_equation;
            GPU_BlendEq alpha_equation;
        }
        
        /// <summary>
        /// Image object for containing pixel/texture data.
        /// A GPU_Image can be created with GPU_CreateImage(), GPU_LoadImage(), GPU_CopyImage(), or GPU_CopyImageFromSurface().
        /// Free the memory with GPU_FreeImage() when you're done.
        /// *see GPU_CreateImage()
        /// *see GPU_LoadImage()
        /// *see GPU_CopyImage()
        /// *see GPU_CopyImageFromSurface()
        /// *see GPU_Target
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Image
        {
            public IntPtr renderer; // GPU_Renderer
            public IntPtr context_target; // GPU_Target;
            public IntPtr target; // GPU_Target;
            public ushort w, h;
            public bool using_virtual_resolution;
            public GPU_Format format;
            public int num_layers;
            public int bytes_per_pixel;
            public ushort base_w, base_h;
            public ushort texture_w, texture_h;
            public bool has_mipmaps;
            public float anchor_x;
            public float anchor_y;
            public SDL.SDL_Color color;
            public bool use_blending;
            public GPU_BlendMode blend_mode;
            public GPU_Filter filter_mode;
            public GPU_Snap snap_mode;
            public GPU_Wrap wrap_mode_x;
            public GPU_Wrap wrap_mode_y;
            
            public IntPtr data; // void*
            public int refcount;
            public bool is_alias;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Camera
        {
            public float x, y, z;
            public float angle;
            public float zoom;
            public float z_near, z_far;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_ShaderBlock
        {
            public int position_loc;
            public int texcoord_loc;
            public int color_loc;
            private int modelViewProjection_loc;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_MatrixStack
        {
            public uint storage_size;
            public uint size;
            public float[][] matrix;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Context
        {
            public IntPtr context; // void*
            public bool failed;
            public uint windowID;
            public int window_w;
            public int window_h;
            public int drawable_w;
            public int drawable_h;
            public int stored_window_w;
            public int stored_window_h;
            public uint current_shader_program;
            public uint default_textured_shader_program;
            public uint default_untextured_shader_program;
            public GPU_ShaderBlock current_shader_block;
            public GPU_ShaderBlock default_textured_shader_block;
            public GPU_ShaderBlock default_untextured_shader_block;
            public bool shapes_use_blending;
            public GPU_BlendMode shapes_blend_mode;
            public float line_thickness;
            public bool use_texturing;
            public int matrix_mode;
            public GPU_MatrixStack projection_matrix;
            public GPU_MatrixStack modelview_matrix;
            public int refcount;
            public IntPtr data; // void*
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Target
        {
            public IntPtr renderer; // GPU_Renderer
            public IntPtr context_target; // GPU_Target
            public IntPtr image; // GPU_Image
            public IntPtr data; // void*
            public ushort w, h;
            public bool using_virtual_resolution;
            public ushort base_w, base_h;
            public bool use_clip_rect;
            public GPU_Rect clip_rect;
            public bool use_color;
            public SDL.SDL_Color color;
            public GPU_Rect viewport;
            public GPU_Camera camera;
            public bool use_camera;
            public bool use_depth_test;
            public bool use_depth_write;
            public GPU_Comparison depth_function;
            public IntPtr context; // GPU_Context
            public int refcount;
            public bool is_alias;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_AttributeFormat
        {
            public bool is_per_sprite;
            public int num_elems_per_value;
            public GPU_Type type;
            public bool normalize;
            public int stride_bytes;
            public int offset_bytes;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Attribute
        {
            public int location;
            public IntPtr values; // void*
            public GPU_AttributeFormat format;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_AttributeSource
        {
            public bool enabled;
            public int num_values;
            public IntPtr next_value; // void*
            
            public int per_vertex_storage_stride_bytes;
            public int per_vertex_storage_offset_bytes;
            public int per_vertex_storage_size;

            public IntPtr per_vertex_storage; // void*

            public GPU_Attribute attribute;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_ErrorObject
        {
            public string function;
            public GPU_Error error;
            public string details;
        }


        /// <summary>
        /// Renderer object which specializes the API to a particular backend.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Renderer
        {
            public GPU_RendererID id;
            public GPU_RendererID requested_id;
            public uint SDL_init_flags;
            public GPU_InitFlag GPU_init_flags;

            public GPU_ShaderLanguage shader_language;

            public int min_shader_version;
            public int max_shader_version;

            public GPU_Feature enabled_features;

            public IntPtr current_context_target; // GPU_Target

            public bool coordinate_mode;

            public float default_image_anchor_x;
            public float default_image_anchor_y;

            public IntPtr impl; // GPU_RendererImpl
        }


        /* METHODS */

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetInitWindow_d(uint windowID);
        private static GPU_SetInitWindow_d GPU_SetInitWindow_f;
        public static void GPU_SetInitWindow(uint windowID) => GPU_SetInitWindow_f(windowID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint GPU_GetInitWindow_d();
        private static GPU_GetInitWindow_d GPU_GetInitWindow_f;
        public static uint GPU_GetInitWindow() => GPU_GetInitWindow_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetPreInitFlags_d(GPU_InitFlag GPU_Flags);
        private static GPU_SetPreInitFlags_d GPU_SetPreInitFlags_f;
        public static void GPU_SetPreInitFlags(GPU_InitFlag GPU_Flags) => GPU_SetPreInitFlags_f(GPU_Flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetRequiredFeatures_d();
        private static GPU_SetRequiredFeatures_d GPU_SetRequiredFeatures_f;
        public static void GPU_SetRequiredFeatures() => GPU_SetRequiredFeatures_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Feature GPU_GetRequiredFeatures_d();
        private static GPU_GetRequiredFeatures_d GPU_GetRequiredFeatures_f;
        public static GPU_Feature GPU_GetRequiredFeatures() => GPU_GetRequiredFeatures_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetDefaultRendererOrder_d(out int order_size, GPU_RendererID[] order);
        private static GPU_GetDefaultRendererOrder_d GPU_GetDefaultRendererOrder_f;
        public static void GPU_GetDefaultRendererOrder(out int order_size, GPU_RendererID[] order) => GPU_GetDefaultRendererOrder_f(out order_size, order);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetRendererOrder_d(out int order_size, IntPtr order);
        private static GPU_GetRendererOrder_d GPU_GetRendererOrder_f;
        public static void GPU_GetRendererOrder(out int order_size, IntPtr order) => GPU_GetRendererOrder_f(out order_size, order);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetRendererOrder_d(int order_size, IntPtr order);
        private static GPU_SetRendererOrder_d GPU_SetRendererOrder_f;
        public static void GPU_SetRendererOrder(int order_size, IntPtr order) => GPU_SetRendererOrder_f(order_size, order);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_Init_d(ushort w, ushort h, uint SDL_flags);
        private static GPU_Init_d GPU_Init_f;
        public static /*GPU_Target*/ IntPtr GPU_Init(ushort w, ushort h, uint SDL_flags) => GPU_Init_f(w, h, SDL_flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_InitRenderer_d(GPU_RendererBackEnd renderer_backend, ushort w, ushort h, uint SDL_flags);
        private static GPU_InitRenderer_d GPU_InitRenderer_f;
        public static /*GPU_Target*/ IntPtr GPU_InitRenderer(GPU_RendererBackEnd renderer_backend, ushort w, ushort h, uint SDL_flags) => GPU_InitRenderer_f(renderer_backend, w, h, SDL_flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_InitRendererByID_d(GPU_RendererID renderer_request, ushort w, ushort h, uint SDL_flags);
        private static GPU_InitRendererByID_d GPU_InitRendererByID_f;
        public static /*GPU_Target*/ IntPtr GPU_InitRendererByID(GPU_RendererID renderer_request, ushort w, ushort h, uint SDL_flags) => GPU_InitRendererByID_f(renderer_request, w, h, SDL_flags);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_IsFeatureEnabled_d(GPU_Feature feature);
        private static GPU_IsFeatureEnabled_d GPU_IsFeatureEnabled_f;
        public static bool GPU_IsFeatureEnabled(GPU_Feature feature) => GPU_IsFeatureEnabled_f(feature);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_CloseCurrentRenderer_d();
        private static GPU_CloseCurrentRenderer_d GPU_CloseCurrentRenderer_f;
        public static void GPU_CloseCurrentRenderer() => GPU_CloseCurrentRenderer_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Quit_d();
        private static GPU_Quit_d GPU_Quit_f;
        public static void GPU_Quit() => GPU_Quit_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetDebugLevel_d(GPU_DebugLevel level);
        private static GPU_SetDebugLevel_d GPU_SetDebugLevel_f;
        public static void GPU_SetDebugLevel(GPU_DebugLevel level) => GPU_SetDebugLevel_f(level);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_DebugLevel GPU_GetDebugLevel_d();
        private static GPU_GetDebugLevel_d GPU_GetDebugLevel_f;
        public static GPU_DebugLevel GPU_GetDebugLevel() => GPU_GetDebugLevel_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_LogInfo_d();
        private static GPU_LogInfo_d GPU_LogInfo_f;
        public static void GPU_LogInfo() => GPU_LogInfo_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_LogWarning_d();
        private static GPU_LogWarning_d GPU_LogWarning_f;
        public static void GPU_LogWarning() => GPU_LogWarning_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_LogError_d();
        private static GPU_LogError_d GPU_LogError_f;
        public static void GPU_LogError() => GPU_LogError_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetLogCallback_d();
        private static GPU_SetLogCallback_d GPU_SetLogCallback_f;
        public static void GPU_SetLogCallback() => GPU_SetLogCallback_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_PushErrorCode_d();
        private static GPU_PushErrorCode_d GPU_PushErrorCode_f;
        public static void GPU_PushErrorCode() => GPU_PushErrorCode_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_ErrorObject GPU_PopErrorCode_d();
        private static GPU_PopErrorCode_d GPU_PopErrorCode_f;
        public static GPU_ErrorObject GPU_PopErrorCode() => GPU_PopErrorCode_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string GPU_GetErrorString_d(GPU_Error error);
        private static GPU_GetErrorString_d GPU_GetErrorString_f;
        public static string GPU_GetErrorString(GPU_Error error) => GPU_GetErrorString_f(error);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetErrorQueueMax_d(uint max);
        private static GPU_SetErrorQueueMax_d GPU_SetErrorQueueMax_f;
        public static void GPU_SetErrorQueueMax(uint max) => GPU_SetErrorQueueMax_f(max);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_RendererID GPU_MakeRendererID_d(string name, GPU_RendererBackEnd renderer_backend, int major_version, int minor_version);
        private static GPU_MakeRendererID_d GPU_MakeRendererID_f;
        public static GPU_RendererID GPU_MakeRendererID(string name, GPU_RendererBackEnd renderer_backend, int major_version, int minor_version)
            => GPU_MakeRendererID_f(name, renderer_backend, major_version, minor_version);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_RendererID GPU_GetRendererID_d(GPU_RendererBackEnd renderer_backend);
        private static GPU_GetRendererID_d GPU_GetRendererID_f;
        public static GPU_RendererID GPU_GetRendererID(GPU_RendererBackEnd renderer_backend) => GPU_GetRendererID_f(renderer_backend);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GPU_GetNumRegisteredRenderers_d();
        private static GPU_GetNumRegisteredRenderers_d GPU_GetNumRegisteredRenderers_f;
        public static int GPU_GetNumRegisteredRenderers() => GPU_GetNumRegisteredRenderers_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetRegisteredRendererList_d();
        private static GPU_GetRegisteredRendererList_d GPU_GetRegisteredRendererList_f;
        public static void GPU_GetRegisteredRendererList() => GPU_GetRegisteredRendererList_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_RegisterRenderer_d();
        private static GPU_RegisterRenderer_d GPU_RegisterRenderer_f;
        public static void GPU_RegisterRenderer() => GPU_RegisterRenderer_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_RendererBackEnd GPU_ReserveNextRendererEnum_d();
        private static GPU_ReserveNextRendererEnum_d GPU_ReserveNextRendererEnum_f;
        public static GPU_RendererBackEnd GPU_ReserveNextRendererEnum() => GPU_ReserveNextRendererEnum_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int GPU_GetNumActiveRenderers_d();
        private static GPU_GetNumActiveRenderers_d GPU_GetNumActiveRenderers_f;
        public static int GPU_GetNumActiveRenderers() => GPU_GetNumActiveRenderers_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetActiveRendererList_d();
        private static GPU_GetActiveRendererList_d GPU_GetActiveRendererList_f;
        public static void GPU_GetActiveRendererList() => GPU_GetActiveRendererList_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GPU_GetCurrentRenderer_d();
        private static GPU_GetCurrentRenderer_d GPU_GetCurrentRenderer_f;
        public static IntPtr GPU_GetCurrentRenderer() => GPU_GetCurrentRenderer_f(); // TODO

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetCurrentRenderer_d(GPU_RendererID id);
        private static GPU_SetCurrentRenderer_d GPU_SetCurrentRenderer_f;
        public static void GPU_SetCurrentRenderer(GPU_RendererID id) => GPU_SetCurrentRenderer_f(id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr GPU_GetRenderer_d(GPU_RendererID id);
        private static GPU_GetRenderer_d GPU_GetRenderer_f;
        public static IntPtr GPU_GetRenderer(GPU_RendererID id) => GPU_GetRenderer_f(id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_FreeRenderer_d(IntPtr renderer);
        private static GPU_FreeRenderer_d GPU_FreeRenderer_f;
        public static void GPU_FreeRenderer(IntPtr renderer) => GPU_FreeRenderer_f(renderer);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_ResetRendererState_d();
        private static GPU_ResetRendererState_d GPU_ResetRendererState_f;
        public static void GPU_ResetRendererState() => GPU_ResetRendererState_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetCoordinateMode_d(bool use_math_coords);
        private static GPU_SetCoordinateMode_d GPU_SetCoordinateMode_f;
        public static void GPU_SetCoordinateMode(bool use_math_coords) => GPU_SetCoordinateMode_f(use_math_coords);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_GetCoordinateMode_d();
        private static GPU_GetCoordinateMode_d GPU_GetCoordinateMode_f;
        public static bool GPU_GetCoordinateMode() => GPU_GetCoordinateMode_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetDefaultAnchor_d(float anchor_x, float anchor_y);
        private static GPU_SetDefaultAnchor_d GPU_SetDefaultAnchor_f;
        public static void GPU_SetDefaultAnchor(float anchor_x, float anchor_y) => GPU_SetDefaultAnchor_f(anchor_x, anchor_y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetDefaultAnchor_d(out int anchor_x, out int anchor_y);
        private static GPU_GetDefaultAnchor_d GPU_GetDefaultAnchor_f;
        public static void GPU_GetDefaultAnchor(out int anchor_x, out int anchor_y) => GPU_GetDefaultAnchor_f(out anchor_x, out anchor_y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_GetContextTarget_d();
        private static GPU_GetContextTarget_d GPU_GetContextTarget_f;
        public static /*GPU_Target*/ IntPtr GPU_GetContextTarget() => GPU_GetContextTarget_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_GetWindowTarget_d(uint windowID);
        private static GPU_GetWindowTarget_d GPU_GetWindowTarget_f;
        public static /*GPU_Target*/ IntPtr GPU_GetWindowTarget(uint windowID) => GPU_GetWindowTarget_f(windowID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_MakeCurrent_d(/*GPU_Target*/ IntPtr target, uint windowID);
        private static GPU_MakeCurrent_d GPU_MakeCurrent_f;
        public static void GPU_MakeCurrent(/*GPU_Target*/ IntPtr target, uint windowID) => GPU_MakeCurrent_f(target, windowID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_SetWindowResolution_d(ushort w, ushort h);
        private static GPU_SetWindowResolution_d GPU_SetWindowResolution_f;
        public static bool GPU_SetWindowResolution(ushort w, ushort h) => GPU_SetWindowResolution_f(w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_SetFullscreen_d(bool enable_fullscreen, bool use_desktop_resolution);
        private static GPU_SetFullscreen_d GPU_SetFullscreen_f;
        public static bool GPU_SetFullscreen(bool enable_fullscreen, bool use_desktop_resolution) => GPU_SetFullscreen_f(enable_fullscreen, use_desktop_resolution);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetShapeBlending_d(bool enable);
        private static GPU_SetShapeBlending_d GPU_SetShapeBlending_f;
        public static void GPU_SetShapeBlending(bool enable) => GPU_SetShapeBlending_f(enable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_BlendMode GPU_GetBlendModeFromPreset_d(GPU_BlendPreset preset);
        private static GPU_GetBlendModeFromPreset_d GPU_GetBlendModeFromPreset_f;
        public static GPU_BlendMode GPU_GetBlendModeFromPreset(GPU_BlendPreset preset) => GPU_GetBlendModeFromPreset_f(preset);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetShapeBlendFunction_d(GPU_BlendFunc source_color, GPU_BlendFunc dest_color, GPU_BlendFunc source_alpha, GPU_BlendFunc dest_alpha);
        private static GPU_SetShapeBlendFunction_d GPU_SetShapeBlendFunction_f;
        public static void GPU_SetShapeBlendFunction(GPU_BlendFunc source_color, GPU_BlendFunc dest_color, GPU_BlendFunc source_alpha, GPU_BlendFunc dest_alpha)
            => GPU_SetShapeBlendFunction_f(source_color, dest_color, source_alpha, dest_alpha);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetShapeBlendEquation_d(GPU_BlendEq color_equation, GPU_BlendEq alpha_equation);
        private static GPU_SetShapeBlendEquation_d GPU_SetShapeBlendEquation_f;
        public static void GPU_SetShapeBlendEquation(GPU_BlendEq color_equation, GPU_BlendEq alpha_equation) => GPU_SetShapeBlendEquation_f(color_equation, alpha_equation);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetShapeBlendMode_d(GPU_BlendPreset mode);
        private static GPU_SetShapeBlendMode_d GPU_SetShapeBlendMode_f;
        public static void GPU_SetShapeBlendMode(GPU_BlendPreset mode) => GPU_SetShapeBlendMode_f(mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float GPU_SetLineThickness_d(float thickness);
        private static GPU_SetLineThickness_d GPU_SetLineThickness_f;
        public static float GPU_SetLineThickness(float thickness) => GPU_SetLineThickness_f(thickness);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate float GPU_GetLineThickness_d();
        private static GPU_GetLineThickness_d GPU_GetLineThickness_f;
        public static float GPU_GetLineThickness() => GPU_GetLineThickness_f();


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_CreateAliasTarget_d(/*GPU_Target*/ IntPtr target);
        private static GPU_CreateAliasTarget_d GPU_CreateAliasTarget_f;
        public static /*GPU_Target*/ IntPtr GPU_CreateAliasTarget(/*GPU_Target*/ IntPtr target) => GPU_CreateAliasTarget_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_LoadTarget_d(/*GPU_Image*/ IntPtr image);
        private static GPU_LoadTarget_d GPU_LoadTarget_f;
        public static /*GPU_Target*/ IntPtr GPU_LoadTarget(/*GPU_Image*/ IntPtr image) => GPU_LoadTarget_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Target*/ IntPtr GPU_GetTarget_d(/*GPU_Image*/ IntPtr image);
        private static GPU_GetTarget_d GPU_GetTarget_f;
        public static /*GPU_Target*/ IntPtr GPU_GetTarget(/*GPU_Image*/ IntPtr image) => GPU_GetTarget_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_FreeTarget_d(/*GPU_Target*/ IntPtr target);
        private static GPU_FreeTarget_d GPU_FreeTarget_f;
        public static void GPU_FreeTarget(/*GPU_Target*/ IntPtr target) => GPU_FreeTarget_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetVirtualResolution_d(/*GPU_Target*/ IntPtr target, ushort w, ushort h);
        private static GPU_SetVirtualResolution_d GPU_SetVirtualResolution_f;
        public static void GPU_SetVirtualResolution(/*GPU_Target*/ IntPtr target, ushort w, ushort h) => GPU_SetVirtualResolution_f(target, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetVirtualResolution_d(/*GPU_Target*/ IntPtr target, out ushort w, out ushort h);
        private static GPU_GetVirtualResolution_d GPU_GetVirtualResolution_f;
        public static void GPU_GetVirtualResolution(/*GPU_Target*/ IntPtr target, out ushort w, out ushort h) => GPU_GetVirtualResolution_f(target, out w, out h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_GetVirtualCoords_d(/*GPU_Target*/ IntPtr target, out float x, out float y, out float displayX, out float displayY);
        private static GPU_GetVirtualCoords_d GPU_GetVirtualCoords_f;
        public static void GPU_GetVirtualCoords(/*GPU_Target*/ IntPtr target, out float x, out float y, out float displayX, out float displayY)
            => GPU_GetVirtualCoords_f(target, out x, out y, out displayX, out displayY);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UnsetVirtualResolution_d(/*GPU_Target*/ IntPtr target);
        private static GPU_UnsetVirtualResolution_d GPU_UnsetVirtualResolution_f;
        public static void GPU_UnsetVirtualResolution(/*GPU_Target*/ IntPtr target) => GPU_UnsetVirtualResolution_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Rect GPU_MakeRect_d(float x, float y, float w, float h);
        private static GPU_MakeRect_d GPU_MakeRect_f;
        public static GPU_Rect GPU_MakeRect(float x, float y, float w, float h) => GPU_MakeRect_f(x, y, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL.SDL_Color GPU_MakeColor_d(byte r, byte g, byte b, byte a);
        private static GPU_MakeColor_d GPU_MakeColor_f;
        public static SDL.SDL_Color GPU_MakeColor(byte r, byte g, byte b, byte a) => GPU_MakeColor_f(r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetViewport_d(/*GPU_Target*/ IntPtr target, GPU_Rect viewport);
        private static GPU_SetViewport_d GPU_SetViewport_f;
        public static void GPU_SetViewport(/*GPU_Target*/ IntPtr target, GPU_Rect viewport) => GPU_SetViewport_f(target, viewport);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UnsetViewport_d(/*GPU_Target*/ IntPtr target);
        private static GPU_UnsetViewport_d GPU_UnsetViewport_f;
        public static void GPU_UnsetViewport(/*GPU_Target*/ IntPtr target) => GPU_UnsetViewport_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Camera GPU_GetDefaultCamera_d();
        private static GPU_GetDefaultCamera_d GPU_GetDefaultCamera_f;
        public static GPU_Camera GPU_GetDefaultCamera() => GPU_GetDefaultCamera_f();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Camera GPU_GetCamera_d(/*GPU_Target*/ IntPtr target);
        private static GPU_GetCamera_d GPU_GetCamera_f;
        public static GPU_Camera GPU_GetCamera(/*GPU_Target*/ IntPtr target) => GPU_GetCamera_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Camera GPU_SetCamera_d(/*GPU_Target*/ IntPtr target, /*GPU_Camera*/ IntPtr cam);
        private static GPU_SetCamera_d GPU_SetCamera_f;
        public static GPU_Camera GPU_SetCamera(/*GPU_Target*/ IntPtr target, /*GPU_Camera*/ IntPtr cam) => GPU_SetCamera_f(target, cam);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_EnableCamera_d(/*GPU_Target*/ IntPtr target, bool use_camera);
        private static GPU_EnableCamera_d GPU_EnableCamera_f;
        public static void GPU_EnableCamera(/*GPU_Target*/ IntPtr target, bool use_camera) => GPU_EnableCamera_f(target, use_camera);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_IsCameraEnabled_d(/*GPU_Target*/ IntPtr target);
        private static GPU_IsCameraEnabled_d GPU_IsCameraEnabled_f;
        public static bool GPU_IsCameraEnabled(/*GPU_Target*/ IntPtr target) => GPU_IsCameraEnabled_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_AddDepthBuffer_d(/*GPU_Target*/ IntPtr target);
        private static GPU_AddDepthBuffer_d GPU_AddDepthBuffer_f;
        public static bool GPU_AddDepthBuffer(/*GPU_Target*/ IntPtr target) => GPU_AddDepthBuffer_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetDepthTest_d(/*GPU_Target*/ IntPtr target, bool enable);
        private static GPU_SetDepthTest_d GPU_SetDepthTest_f;
        public static void GPU_SetDepthTest(/*GPU_Target*/ IntPtr target, bool enable) => GPU_SetDepthTest_f(target, enable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetDepthWrite_d(/*GPU_Target*/ IntPtr target, bool enable);
        private static GPU_SetDepthWrite_d GPU_SetDepthWrite_f;
        public static void GPU_SetDepthWrite(/*GPU_Target*/ IntPtr target, bool enable) => GPU_SetDepthWrite_f(target, enable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetDepthFunction_d(/*GPU_Target*/ IntPtr target, GPU_Comparison compare_operation);
        private static GPU_SetDepthFunction_d GPU_SetDepthFunction_f;
        public static void GPU_SetDepthFunction(/*GPU_Target*/ IntPtr target, GPU_Comparison compare_operation) => GPU_SetDepthFunction_f(target, compare_operation);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL.SDL_Color GPU_GetPixel_d(/*GPU_Target*/ IntPtr target, short x, short y);
        private static GPU_GetPixel_d GPU_GetPixel_f;
        public static SDL.SDL_Color GPU_GetPixel(/*GPU_Target*/ IntPtr target, short x, short y) => GPU_GetPixel_f(target, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Rect GPU_SetClipRect_d(/*GPU_Target*/ IntPtr target, GPU_Rect rect);
        private static GPU_SetClipRect_d GPU_SetClipRect_f;
        public static GPU_Rect GPU_SetClipRect(/*GPU_Target*/ IntPtr target, GPU_Rect rect) => GPU_SetClipRect_f(target, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate GPU_Rect GPU_SetClip_d(/*GPU_Target*/ IntPtr target, short x, short y, short w, short h);
        private static GPU_SetClip_d GPU_SetClip_f;
        public static GPU_Rect GPU_SetClip(/*GPU_Target*/ IntPtr target, short x, short y, short w, short h) => GPU_SetClip_f(target, x, y, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UnsetClip_d(/*GPU_Target*/ IntPtr target);
        private static GPU_UnsetClip_d GPU_UnsetClip_f;
        public static void GPU_UnsetClip(/*GPU_Target*/ IntPtr target) => GPU_UnsetClip_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_IntersectRect_d(GPU_Rect a, GPU_Rect b, out GPU_Rect result);
        private static GPU_IntersectRect_d GPU_IntersectRect_f;
        public static bool GPU_IntersectRect(GPU_Rect a, GPU_Rect b, out GPU_Rect result) => GPU_IntersectRect_f(a, b, out result);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_IntersectClipRect_d(/*GPU_Target*/ IntPtr target, GPU_Rect b, out GPU_Rect result);
        private static GPU_IntersectClipRect_d GPU_IntersectClipRect_f;
        public static bool GPU_IntersectClipRect(/*GPU_Target*/ IntPtr target, GPU_Rect b, out GPU_Rect result) => GPU_IntersectClipRect_f(target, b, out result);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetTargetColor_d(/*GPU_Target*/ IntPtr target, SDL.SDL_Color color);
        private static GPU_SetTargetColor_d GPU_SetTargetColor_f;
        public static void GPU_SetTargetColor(/*GPU_Target*/ IntPtr target, SDL.SDL_Color color) => GPU_SetTargetColor_f(target, color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetTargetRGB_d(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b);
        private static GPU_SetTargetRGB_d GPU_SetTargetRGB_f;
        public static void GPU_SetTargetRGB(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b) => GPU_SetTargetRGB_f(target, r, g, b);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetTargetRGBA_d(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b, byte a);
        private static GPU_SetTargetRGBA_d GPU_SetTargetRGBA_f;
        public static void GPU_SetTargetRGBA(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b, byte a) => GPU_SetTargetRGBA_f(target, r, g, b, a);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UnsetTargetColor_d(/*GPU_Target*/ IntPtr target);
        private static GPU_UnsetTargetColor_d GPU_UnsetTargetColor_f;
        public static void GPU_UnsetTargetColor(/*GPU_Target*/ IntPtr target) => GPU_UnsetTargetColor_f(target);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*SDL_Surface*/ IntPtr GPU_LoadSurface_d(string filename);
        private static GPU_LoadSurface_d GPU_LoadSurface_f;
        public static /*SDL_Surface*/ IntPtr GPU_LoadSurface(string filename) => GPU_LoadSurface_f(filename);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool GPU_SaveSurface_d(/*SDL_Surface*/ IntPtr surface, string filename, GPU_FileFormat format);
        private static GPU_SaveSurface_d GPU_SaveSurface_f;
        public static bool GPU_SaveSurface(/*SDL_Surface*/ IntPtr surface, string filename, GPU_FileFormat format) => GPU_SaveSurface_f(surface, filename, format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Image*/ IntPtr GPU_CreateImage_d(ushort w, ushort h, GPU_Format format);
        private static GPU_CreateImage_d GPU_CreateImage_f;
        public static /*GPU_Image*/ IntPtr GPU_CreateImage(ushort w, ushort h, GPU_Format format) => GPU_CreateImage_f(w, h, format);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Image*/ IntPtr GPU_CreateImageUsingTexture_d(IntPtr handle, bool take_ownership);
        private static GPU_CreateImageUsingTexture_d GPU_CreateImageUsingTexture_f;
        public static /*GPU_Image*/ IntPtr GPU_CreateImageUsingTexture(IntPtr handle, bool take_ownership) => GPU_CreateImageUsingTexture_f(handle, take_ownership);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Image*/ IntPtr GPU_LoadImage_d(string filename);
        private static GPU_LoadImage_d GPU_LoadImage_f;
        public static /*GPU_Image*/ IntPtr GPU_LoadImage(string filename) => GPU_LoadImage_f(filename);

        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Image*/ IntPtr GPU_CreateAliasImage_d(/*GPU_Image*/ IntPtr image);
        private static GPU_CreateAliasImage_d GPU_CreateAliasImage_f;
        public static /*GPU_Image*/ IntPtr GPU_CreateAliasImage(/*GPU_Image*/ IntPtr image) => GPU_CreateAliasImage_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate /*GPU_Image*/ IntPtr GPU_CopyImage_d(/*GPU_Image*/ IntPtr image);
        private static GPU_CopyImage_d GPU_CopyImage_f;
        public static /*GPU_Image*/ IntPtr GPU_CopyImage(/*GPU_Image*/ IntPtr image) => GPU_CopyImage_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_FreeImage_d(/*GPU_Image*/ IntPtr image);
        private static GPU_FreeImage_d GPU_FreeImage_f;
        public static void GPU_FreeImage(/*GPU_Image*/ IntPtr image) => GPU_FreeImage_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_SetImageVirtualResolution_d(/*GPU_Image*/ IntPtr image, ushort w, ushort h);
        private static GPU_SetImageVirtualResolution_d GPU_SetImageVirtualResolution_f;
        public static void GPU_SetImageVirtualResolution(/*GPU_Image*/ IntPtr image, ushort w, ushort h) => GPU_SetImageVirtualResolution_f(image, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UnsetImageVirtualResolution_d(/*GPU_Image*/ IntPtr image);
        private static GPU_UnsetImageVirtualResolution_d GPU_UnsetImageVirtualResolution_f;
        public static void GPU_UnsetImageVirtualResolution(/*GPU_Image*/ IntPtr image) => GPU_UnsetImageVirtualResolution_f(image);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UpdateImage_d(/*GPU_Image*/ IntPtr image, ref GPU_Rect image_rect, /*SDL_Surface*/ IntPtr surface, ref GPU_Rect surface_rect);
        private static GPU_UpdateImage_d GPU_UpdateImage_f;
        public static void GPU_UpdateImage(/*GPU_Image*/ IntPtr image, ref GPU_Rect image_rect, /*SDL_Surface*/ IntPtr surface, ref GPU_Rect surface_rect)
            => GPU_UpdateImage_f(image, ref image_rect, surface, ref surface_rect);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UpdateImageBytes_d(/*GPU_Image*/ IntPtr image, /*GPU_Rect*/ ref GPU_Rect image_rect,  [In,Out] byte[] bytes, int bytes_per_row);
        private static GPU_UpdateImageBytes_d GPU_UpdateImageBytes_f;
        public static void GPU_UpdateImageBytes(/*GPU_Image*/ IntPtr image, /*GPU_Rect*/ ref GPU_Rect image_rect,  [In,Out] byte[] bytes, int bytes_per_row)
            => GPU_UpdateImageBytes_f(image, ref image_rect, bytes, bytes_per_row);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_UpdateImageBytes_d2(/*GPU_Image*/ IntPtr image, /*GPU_Rect*/ IntPtr image_rect,  [In,Out] byte[] bytes, int bytes_per_row);
        private static GPU_UpdateImageBytes_d2 GPU_UpdateImageBytes_f2;
        public static void GPU_UpdateImageBytes(/*GPU_Image*/ IntPtr image, /*GPU_Rect*/ IntPtr image_rect,  [In,Out] byte[] bytes, int bytes_per_row)
            => GPU_UpdateImageBytes_f2(image, image_rect, bytes, bytes_per_row);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Clear_d(IntPtr target);
        private static GPU_Clear_d GPU_Clear_f;
        public static void GPU_Clear(IntPtr target) => GPU_Clear_f(target); // GPU_Target
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_ClearRGB_d(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b);
        private static GPU_ClearRGB_d GPU_ClearRGB_f;
        public static void GPU_ClearRGB(/*GPU_Target*/ IntPtr target, byte r, byte g, byte b) => GPU_ClearRGB_f(target, r, g, b);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Flip_d(IntPtr target);
        private static GPU_Flip_d GPU_Flip_f;
        public static void GPU_Flip(/*GPU_Target*/ IntPtr target) => GPU_Flip_f(target); // // GPU_Target
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Blit_d(/*GPU_Image*/ IntPtr image, ref GPU_Rect src_rect, /*GPU_Target*/ IntPtr target, float x, float y);
        private static GPU_Blit_d GPU_Blit_f;
        public static void GPU_Blit(/*GPU_Image*/ IntPtr image, ref GPU_Rect src_rect, /*GPU_Target*/ IntPtr target, float x, float y) 
            => GPU_Blit_f(image, ref src_rect, target, x, y);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Blit_d2(/*GPU_Image*/ IntPtr image, IntPtr src_rect, /*GPU_Target*/ IntPtr target, float x, float y);
        private static GPU_Blit_d2 GPU_Blit_f2;
        public static void GPU_Blit(/*GPU_Image*/ IntPtr image, IntPtr src_rect, /*GPU_Target*/ IntPtr target, float x, float y) 
            => GPU_Blit_f2(image, src_rect, target, x, y);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_BlitRect_d(/*GPU_Image*/ IntPtr image, ref GPU_Rect src_rect, /*GPU_Target*/ IntPtr target, ref GPU_Rect dst_rect);
        private static GPU_BlitRect_d GPU_BlitRect_f;
        public static void GPU_BlitRect(/*GPU_Image*/ IntPtr image, ref GPU_Rect src_rect, /*GPU_Target*/ IntPtr target, ref GPU_Rect dst_rect) 
            => GPU_BlitRect_f(image, ref src_rect, target, ref dst_rect);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_BlitRect_d2(/*GPU_Image*/ IntPtr image, IntPtr src_rect, /*GPU_Target*/ IntPtr target, IntPtr dst_rect);
        private static GPU_BlitRect_d2 GPU_BlitRect_f2;
        public static void GPU_BlitRect(/*GPU_Image*/ IntPtr image, IntPtr src_rect, /*GPU_Target*/ IntPtr target, IntPtr dst_rect) 
            => GPU_BlitRect_f2(image, src_rect, target, dst_rect);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_RectangleFilled_d(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
        private static GPU_RectangleFilled_d GPU_RectangleFilled_f;
        public static void GPU_RectangleFilled(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color) 
            => GPU_RectangleFilled_f(target, x1, y1, x2, y2, color);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Rectangle_d(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
        private static GPU_Rectangle_d GPU_Rectangle_f;
        public static void GPU_Rectangle(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color) 
            =>GPU_Rectangle_f(target, x1, y1, x2, y2, color);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Line_d(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color);
        private static GPU_Line_d GPU_Line_f;
        public static void GPU_Line(/*GPU_Target*/ IntPtr target, float x1, float y1, float x2, float y2, SDL.SDL_Color color) 
            => GPU_Line_f(target, x1, y1, x2, y2, color);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Pixel_d(/*GPU_Target*/ IntPtr target, float x, float y, SDL.SDL_Color color);
        private static GPU_Pixel_d GPU_Pixel_f;
        public static void GPU_Pixel(/*GPU_Target*/ IntPtr target, float x, float y, SDL.SDL_Color color) => GPU_Pixel_f(target, x, y, color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_Circle_d(/*GPU_Target*/ IntPtr target, float x, float y, float radius, SDL.SDL_Color color);
        private static GPU_Circle_d GPU_Circle_f;
        public static void GPU_Circle(/*GPU_Target*/ IntPtr target, float x, float y, float radius, SDL.SDL_Color color) 
            => GPU_Circle_f(target, x, y, radius, color);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void GPU_CircleFilled_d(/*GPU_Target*/ IntPtr target, float x, float y, float radius, SDL.SDL_Color color);
        private static GPU_CircleFilled_d GPU_CircleFilled_f;
        public static void GPU_CircleFilled(/*GPU_Target*/ IntPtr target, float x, float y, float radius, SDL.SDL_Color color) 
            => GPU_CircleFilled_f(target, x, y, radius, color);


            
            
    }
}