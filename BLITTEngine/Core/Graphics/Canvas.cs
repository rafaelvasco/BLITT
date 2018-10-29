using BLITTEngine.Core.Foundation;
using BLITTEngine.Numerics;
using BLITTEngine.Resources;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BLITTEngine.Core.Graphics
{
    public struct GraphicsInfo
    {
        public RendererBackend RendererBackend;
        public int MaxTextureSize;

        public GraphicsInfo(RendererBackend backend, int max_tex_size)
        {
            this.RendererBackend = backend;
            this.MaxTextureSize = max_tex_size;
        }
    }

    public static unsafe class Canvas
    {
        public static GraphicsInfo Info { get; private set; }


        public static int Width => render_surface_width;

        public static int Height => render_surface_height;

        private static int vertex_max_count;

        private static Matrix4x4 projection_matrix;

        private static ShaderProgram base_shader;

        private static Texture2D current_texture;

        private static BlendMode current_blend_mode;

        private static Vertex2D[] vertex_array;

        private static ushort[] index_array;

        private static IndexBuffer index_buffer;

        private static int vertex_index;

        private static int quad_count;

        private static int render_surface_width;

        private static int render_surface_height;

        private static IntRect render_area;

        private static bool ready_to_draw = false;

        private static RenderState render_state;

        private static RenderTarget renderer_surface;

        private static RenderTarget current_target;

        private static List<RenderTarget> render_targets;

        internal static void Init(IntPtr surface_handle, int width, int height, int max_vertex_count)
        {

            render_targets = new List<RenderTarget>();

            vertex_max_count = max_vertex_count;

            Bgfx.SetPlatformData(new PlatformData() { WindowHandle = surface_handle });

            Bgfx.Init();

            Capabilities caps = Bgfx.GetCaps();
            Info = new GraphicsInfo(caps.Backend, caps.MaxTextureSize);

            Bgfx.SetDebugFeatures(DebugFeatures.DisplayText);

            Bgfx.SetViewClear(0, ClearTargets.Color, 0);

            OnScreenResized(width, height);

            Content.LoadEmbededShaders(Info.RendererBackend);
            base_shader = Content.GetBuiltinShader("base_2d");
            base_shader.AddTextureUniform("texture_2d");

            _InitRenderBuffers();

            _SetBlendMode(BlendMode.AlphaBlend);

            Bgfx.Touch(0);
            Bgfx.Frame();

            Console.WriteLine($"Graphics Backend : {Info.RendererBackend}");

            render_surface_width = width;

            render_surface_height = height;

            renderer_surface = CreateTarget(render_surface_width, render_surface_height);

        }

        internal static void Terminate()
        {
            Console.WriteLine(" > Disposing Graphics Device");

            index_buffer.Dispose();

            foreach (var target in render_targets)
            {
                target.Handle.Dispose();
            }

            render_targets.Clear();

            Bgfx.Shutdown();
        }

        public static RenderTarget CreateTarget(int width, int height/*, int layers = 1*/)
        {/*
            if (layers < 1)
            {
                layers = 1;
            }

            var textures = new Texture2D[layers];

            for (var i = 0; i < layers; ++i)
            {
                textures[i] = new Texture2D(width, height, render_target: true);
            }*/

            var render_target = new RenderTarget(/*textures*/width, height);

            render_targets.Add(render_target);

            return render_target;
        }

        public static void FreeTarget(RenderTarget target)
        {
            target.Handle.Dispose();

            render_targets.Remove(target);
        }

        public static void Clear(int color)
        {
            Bgfx.SetViewClear(0, ClearTargets.Color, color);
        }

        public static void Begin(RenderTarget target = null)
        {
            ready_to_draw = true;

            current_target = target ?? renderer_surface;

            Bgfx.SetViewFrameBuffer(0, current_target.Handle);


        }

        public static void End()
        {
            RenderBatch();

            Bgfx.SetViewFrameBuffer(0, FrameBuffer.Invalid);

            _DrawScreenSurface();

            ready_to_draw = false;


        }

        public static void RenderQuad(ref Quad quad)
        {
            if (ready_to_draw)
            {
                if (vertex_index >= vertex_max_count ||
                    current_texture != quad.Tex ||
                    current_blend_mode != quad.Blend)
                {
                    RenderBatch();

                    if (current_blend_mode != quad.Blend)
                    {
                        _SetBlendMode(quad.Blend);
                    }

                    if (quad.Tex != current_texture)
                    {
                        current_texture = quad.Tex;
                    }
                }

                var vidx = vertex_index;
                var qv = quad;

                ref var v0 = ref qv.V0;
                ref var v1 = ref qv.V1;
                ref var v2 = ref qv.V2;
                ref var v3 = ref qv.V3;

                fixed (Vertex2D* vertex_ptr = vertex_array)
                {
                    *(vertex_ptr + vidx++) = new Vertex2D(v0.X, v0.Y, v0.Tx, v0.Ty, v0.Col);
                    *(vertex_ptr + vidx++) = new Vertex2D(v1.X, v1.Y, v1.Tx, v1.Ty, v1.Col);
                    *(vertex_ptr + vidx++) = new Vertex2D(v2.X, v2.Y, v2.Tx, v2.Ty, v2.Col);
                    *(vertex_ptr + vidx++) = new Vertex2D(v3.X, v3.Y, v3.Tx, v3.Ty, v3.Col);
                }

                unchecked
                {
                    vertex_index += 4;
                    quad_count++;
                }
            }
        }

        public static void RenderBatch()
        {
            if (vertex_index == 0)
            {
                return;
            }

            var vertex_buffer = new TransientVertexBuffer(vertex_index, Vertex2D.Layout);

            fixed (void* v = vertex_array)
            {
                Unsafe.CopyBlock((void*)vertex_buffer.Data, v, (uint)vertex_index * 20);
            }

            base_shader.SetTexture(current_texture, "texture_2d");

            Bgfx.SetRenderState(render_state);

            Bgfx.SetIndexBuffer(index_buffer, 0, quad_count * 6);

            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, vertex_index);

            Bgfx.Submit(0, base_shader.Program);

            vertex_index = 0;
            quad_count = 0;
        }

        public static void SaveScreenShot(string path)
        {
            Bgfx.RequestScreenShot(path);
        }

        internal static void Flip()
        {
            Bgfx.Touch(0);
            Bgfx.Frame();
        }

        internal static void OnScreenResized(int width, int height)
        {
            render_area = IntRect.FromBox(0, 0, width, height);

            Bgfx.Reset(render_area.Width, render_area.Height, ResetFlags.Vsync);

            Bgfx.SetViewRect(0, render_area.X1, render_area.Y1, render_area.Width, render_area.Height);

            projection_matrix = Matrix4x4.CreateOrthographicOffCenter(
                left: render_area.X1,
                right: render_area.X2,
                bottom: render_area.Y2,
                top: render_area.Y1,
                zNearPlane: 0.0f,
                zFarPlane: 1000.0f
            );

            var proj_m = projection_matrix;

            Bgfx.SetViewTransform(0, null, &proj_m.M11);
        }

        internal static ShaderProgram CreateShaderProgram(string name, byte[] vertex_shader_src, byte[] frag_shader_src)
        {
            if (vertex_shader_src.Length == 0 || frag_shader_src.Length == 0)
            {
                throw new Exception("Cannot load ShaderProgram with empty shader sources");
            }

            var vertex_shader = new Shader(MemoryBlock.FromArray(vertex_shader_src));
            var frag_shader = new Shader(MemoryBlock.FromArray(frag_shader_src));

            return new ShaderProgram(vertex_shader, frag_shader);
        }

        private static void _SetBlendMode(BlendMode mode)
        {
            current_blend_mode = mode;

            switch (mode)
            {
                case BlendMode.AlphaBlend:

                    render_state = RenderState.WriteR | RenderState.WriteG | RenderState.WriteB | RenderState.WriteA | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendInverseSourceAlpha);

                    break;

                case BlendMode.AlphaAdd:

                    render_state = RenderState.WriteR | RenderState.WriteG | RenderState.WriteB | RenderState.WriteA | RenderState.BlendFunction(RenderState.BlendSourceAlpha, RenderState.BlendOne);

                    break;

                case BlendMode.ColorMul:

                    render_state = RenderState.BlendDarken;

                    break;
            }
        }

        private static void _InitRenderBuffers()
        {
            vertex_array = new Vertex2D[vertex_max_count];

            index_array = new ushort[(vertex_max_count / 4) * 6];

            ushort indice_i = 0;

            for (int i = 0; i < index_array.Length; i += 6, indice_i += 4)
            {
                index_array[i + 0] = (ushort)(indice_i + 0);
                index_array[i + 1] = (ushort)(indice_i + 1);
                index_array[i + 2] = (ushort)(indice_i + 2);
                index_array[i + 3] = (ushort)(indice_i + 0);
                index_array[i + 4] = (ushort)(indice_i + 2);
                index_array[i + 5] = (ushort)(indice_i + 3);
            }

            index_buffer = new IndexBuffer(MemoryBlock.FromArray(index_array));
        }

        private static void _DrawScreenSurface()
        {
            var rx = render_area.X1;
            var ry = render_area.Y1;
            var rx2 = render_area.X2;
            var ry2 = render_area.Y2;

            var vertex_buffer = new TransientVertexBuffer(4, Vertex2D.Layout);

            Vertex2D* v = (Vertex2D*)vertex_buffer.Data;

            *(v + 0) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
            *(v + 1) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
            *(v + 2) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);
            *(v + 3) = new Vertex2D(rx, ry, 0, 0, 0xFFFFFFFF);

            base_shader.SetTexture(renderer_surface.Handle.GetTexture(), "texture_2d");

            Bgfx.SetRenderState(render_state);

            Bgfx.SetIndexBuffer(index_buffer, 0, 6);

            Bgfx.SetVertexBuffer(0, vertex_buffer, 0, 4);

            Bgfx.Submit(0, base_shader.Program);
        }
    }
}