using System;
using System.Runtime.InteropServices;

namespace BLITTEngine.Foundation
{
    internal static unsafe class SDLGpu
    {
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
        
        public enum GPU_Flip
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
            public char* name;
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
            public IntPtr context;
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

            public IntPtr data;

        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_Target
        {
            //public GPU_Renderer renderer; // TODO

            //public GPU_Target context_target;

            public GPU_Image image;

            public IntPtr data; // TODO

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

            public GPU_Context* context;
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
            public IntPtr values; /* TODO */
            public GPU_AttributeFormat format;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_AttributeSource
        {
            public bool enabled;
            public int num_values;
            public IntPtr next_value; /* TODO */
            
            public int per_vertex_storage_stride_bytes;
            public int per_vertex_storage_offset_bytes;
            public int per_vertex_storage_size;

            public IntPtr per_vertex_storage; /* TODO */

            public GPU_Attribute attribute;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct GPU_ErrorObject
        {
            public string function;
            public GPU_Error error;
            public string details;
        }
        
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

            public GPU_Target current_context_target;

            public bool coordinate_mode;

            public float default_image_anchor_x;
            public float default_image_anchor_y;

            public GPU_RendererImpl impl;
        }
        
        public struct GPU_RendererImpl
        {
            
        }
        
        /* METHODS */
        
        
    }
}