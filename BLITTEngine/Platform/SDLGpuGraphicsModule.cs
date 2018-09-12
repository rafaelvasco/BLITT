using System;
using System.Collections.Generic;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;
using Color = BLITTEngine.Graphics.Color;
using static BLITTEngine.Foundation.SDLGpu;
using static BLITTEngine.Foundation.SDL;

namespace BLITTEngine.Platform
{
    internal class SDLGpuGraphicsModule : GraphicsModule
    {
        private readonly IntPtr main_target;
        private IntPtr current_target;
        
        private SDL_Color draw_color;
        private GPU_Rect blit_rect;
        private GPU_Rect blit_dst_rect;

        private readonly List<Texture> textures;

        public SDLGpuGraphicsModule(uint windowId, int screen_w, int screen_h)
        {
            
            Console.WriteLine($"GRAPHICS MODULE INIT: {screen_w}, {screen_h}");
            
#if DEBUG
            GPU_SetDebugLevel(GPU_DebugLevel.LEVEL_MAX);
#else        
            GPU_SetDebugLevel(GPU_DebugLevel.LEVEL_0);
#endif
            
            GPU_SetInitWindow(windowId);
            main_target = GPU_InitRenderer(GPU_RendererBackEnd.OPENGL_3, (ushort) screen_w, (ushort) screen_h, 0);

            if (main_target == IntPtr.Zero)
            {
                throw new Exception("Failed to Initialize SDL_Gpu");
            }

            current_target = main_target;
            
            textures = new List<Texture>();
        }

        public Texture CreateTexture(Pixmap pixmap, bool is_render_target)
        {
            IntPtr gpu_image = GPU_CreateImage((ushort) pixmap.Width, (ushort) pixmap.Height, GPU_Format.FORMAT_RGBA);
            
            GPU_UpdateImageBytes(gpu_image, IntPtr.Zero, pixmap.PixelData, pixmap.Width*4);

            IntPtr render_target_handle = IntPtr.Zero;
            
            if (is_render_target)
            {
                render_target_handle = GPU_LoadTarget(gpu_image);
            }
            
            var tex = new Texture(gpu_image, render_target_handle, pixmap.Width, pixmap.Height);
 
            textures.Add(tex);

            Console.WriteLine($"Graphics: Created Texture from Pixmap");
            
            return tex;
        }

        public Texture CreateTexture(int width, int height, bool is_render_target)
        {
            IntPtr gpu_image = GPU_CreateImage((ushort) width, (ushort) height, GPU_Format.FORMAT_RGBA);
            
            IntPtr render_target_handle = IntPtr.Zero;
            
            if (is_render_target)
            {
                render_target_handle = GPU_LoadTarget(gpu_image);
            }

            var tex = new Texture(gpu_image, render_target_handle, width, height);

            textures.Add(tex);
            
            Console.WriteLine($"Created Empty Texture W: {width}, H{height}");
            
            return tex;
        }

        public void UpdateTexture(Texture texture, Pixmap pixmap)
        {
            Console.WriteLine($"Graphics Update Texture");
            
            if (texture.Width != pixmap.Width || texture.Height != pixmap.Height)
            {
                return;
            }
            
            GPU_UpdateImageBytes(texture.TextureHandle, IntPtr.Zero, pixmap.PixelData, pixmap.Width*4);
        }

        public void Terminate()
        {
            Console.WriteLine($"Grahics Terminate: Disposing {textures.Count} textures.");
            
            foreach (var texture in textures)
            {
                if (texture.IsRenderTarget)
                {
                    GPU_FreeTarget(texture.RenderTargetHandle);
                }
            
                GPU_FreeImage(texture.TextureHandle);
            }
            
            GPU_Quit();
        }

        public void SetColor(ref Color color)
        {
            draw_color.r = color.R;
            draw_color.g = color.G;
            draw_color.b = color.B;
            draw_color.a = color.A;
            GPU_SetTargetRGBA(current_target, color.R, color.G, color.B, color.A);
        }

        public void BeginDraw(Texture target = null)
        {
            current_target = target?.RenderTargetHandle ?? main_target;
        }

        public void EndDraw()
        {
            if(current_target != main_target)
            {
                current_target = main_target;
                GPU_Flip(current_target);
            }
            
            
        }

        public void Flip()
        {
            GPU_Flip(main_target);
        }

        public void Clear(ref Color color)
        {
            GPU_ClearRGB(current_target, color.R, color.G, color.B);
        }

        public void Clear()
        {
            GPU_ClearRGB(current_target, 0, 0, 0);
        }


        public void SetViewport(float x, float y, float w, float h)
        {
            GPU_Rect viewport = new GPU_Rect {x = x, y = y, w = w, h = h};
            GPU_UnsetClip(current_target);
            GPU_SetViewport(current_target, viewport);
           
        }

        public void Resize(float w, float h)
        {
            GPU_SetWindowResolution((ushort) w, (ushort) h);
        }

        public void DrawRect(float x, float y, float w, float h)
        {
            GPU_Rectangle(current_target, x, y, x+w, y+h, draw_color);
        }

        public void FillRect(float x, float y, float w, float h)
        {
            GPU_RectangleFilled(current_target, x, y, x+w, y+h, draw_color);
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            GPU_Line(current_target, x1, y1, x2, y2, draw_color);
        }

        public void DrawCircle(float x, float y, float radius)
        {
            GPU_Circle(current_target, x, y, radius, draw_color);
        }

        public void FillCircle(float x, float y, float radius)
        {
            GPU_CircleFilled(current_target, x, y, radius, draw_color);
        }
        
        public void DrawPixel(float x, float y)
        {
            GPU_Pixel(current_target, x, y, draw_color);
        }

        public void DrawTexture(Texture texture, float x, float y)
        {
            GPU_Blit(texture.TextureHandle, IntPtr.Zero, current_target, x, y);
        }

        public void DrawTexture(Texture texture, float x, float y, ref RectangleI srcRect)
        {
            blit_rect.x = srcRect.X;
            blit_rect.y = srcRect.Y;
            blit_rect.w = srcRect.W;
            blit_rect.h = srcRect.H;
            
            GPU_Blit(texture.TextureHandle, ref blit_rect, current_target, x, y);
        }

        public void DrawTexture(Texture texture, ref Rectangle dstRect)
        {
            blit_dst_rect.x = dstRect.X;
            blit_dst_rect.y = dstRect.Y;
            blit_dst_rect.w = dstRect.W;
            blit_dst_rect.h = dstRect.H;
            
            GPU_BlitRect(texture.TextureHandle, IntPtr.Zero, current_target, ref blit_dst_rect);
        }

        public void DrawTexture(Texture texture, ref RectangleI srcRect, ref Rectangle dstRect)
        {
            blit_rect.x = srcRect.X;
            blit_rect.y = srcRect.Y;
            blit_rect.w = srcRect.W;
            blit_rect.h = srcRect.H;
            
            blit_dst_rect.x = dstRect.X;
            blit_dst_rect.y = dstRect.Y;
            blit_dst_rect.w = dstRect.W;
            blit_dst_rect.h = dstRect.H;
            
            GPU_BlitRect(texture.TextureHandle, ref blit_rect, current_target, ref blit_dst_rect);
        }

        public void Translate(float x, float y)
        {
        }

        public void Rotate(float rotation)
        {
        }

        public void Scale(float scale)
        {
        }

        public void PushTransform()
        {
        }

        public void PopTransform()
        {
        }

       
    }
}