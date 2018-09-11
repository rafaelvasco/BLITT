using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mime;
using BLITTEngine.Foundation;
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
        private IntPtr target;
        
        private byte clear_r;
        private byte clear_g;
        private byte clear_b;

        private SDL_Color draw_color;
        private GPU_Rect blit_rect;
        private GPU_Rect blit_dst_rect;

        public SDLGpuGraphicsModule(uint windowId, int screen_w, int screen_h)
        {
            
            Console.WriteLine($"GRAPHICS MODULE INIT: {screen_w}, {screen_h}");
            
#if DEBUG
            GPU_SetDebugLevel(GPU_DebugLevel.LEVEL_MAX);
#else        
            GPU_SetDebugLevel(GPU_DebugLevel.LEVEL_0);
#endif
            
            GPU_SetInitWindow(windowId);
            
            main_target = GPU_Init((ushort) screen_w, (ushort) screen_h, GPU_DEFAULT_INIT_FLAGS);

            if (main_target == IntPtr.Zero)
            {
                throw new Exception("Failed to Initialize SDL_Gpu");
            }

            target = main_target;
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
            
            var texture = new Texture(gpu_image, render_target_handle, pixmap.Width, pixmap.Height);
            
            return texture;
        }

        public Texture CreateTexture(int width, int height, bool is_render_target)
        {
            IntPtr gpu_image = GPU_CreateImage((ushort) width, (ushort) height, GPU_Format.FORMAT_RGBA);
            
            IntPtr render_target_handle = IntPtr.Zero;
            
            if (is_render_target)
            {
                render_target_handle = GPU_LoadTarget(gpu_image);
            }
            
            var texture = new Texture(gpu_image, render_target_handle, width, height);
            
            return texture;
        }

        public void DestroyTexture(Texture texture)
        {
            GPU_FreeImage(texture.TextureHandle);

            if (texture.IsRenderTarget)
            {
                GPU_FreeTarget(texture.RenderTargetHandle);
            }
        }

        public void UpdateTexture(Texture texture, Pixmap pixmap)
        {
            if (texture.Width != pixmap.Width || texture.Height != pixmap.Height)
            {
                return;
            }
            
            GPU_UpdateImageBytes(texture.TextureHandle, IntPtr.Zero, pixmap.PixelData, pixmap.Width*4);
        }

        public void Terminate()
        {
            GPU_Quit();
        }

        public void SetColor(ref Color color)
        {
            draw_color.r = color.R;
            draw_color.g = color.G;
            draw_color.b = color.B;
            draw_color.a = color.A;
            GPU_SetTargetRGBA(target, color.R, color.G, color.B, color.A);
        }

        public void SetClearColor(ref Color color)
        {
            clear_r = color.R;
            clear_g = color.G;
            clear_b = color.B;
        }

        public Color GetClearColor()
        {
            return new Color(clear_r, clear_g, clear_b);
        }

        public void SetRenderTarget(Texture texture)
        {
            GPU_Flip(target);
            
            target = texture.RenderTargetHandle;
            
            GPU_ClearRGB(target, clear_r, clear_g, clear_b);
        }

        public void ResetRenderTarget()
        {
            target = main_target;
        }

        public void BeginDraw()
        {
            GPU_ClearRGB(target, clear_r, clear_g, clear_b);
        }

        public void EndDraw()
        {
            GPU_Flip(main_target);
        }

        public void SetViewport(float x, float y, float w, float h)
        {
            GPU_Rect viewport = new GPU_Rect {x = x, y = y, w = w, h = h};
            GPU_UnsetClip(target);
            GPU_SetViewport(target, viewport);
           
        }

        public void Resize(float w, float h)
        {
            GPU_SetWindowResolution((ushort) w, (ushort) h);
        }

        public void DrawRect(float x, float y, float w, float h)
        {
            GPU_Rectangle(target, x, y, x+w, y+h, draw_color);
        }

        public void FillRect(float x, float y, float w, float h)
        {
            GPU_RectangleFilled(target, x, y, x+w, y+h, draw_color);
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            GPU_Line(target, x1, y1, x2, y2, draw_color);
        }

        public void DrawCircle(float x, float y, float radius)
        {
            GPU_Circle(target, x, y, radius, draw_color);
        }

        public void FillCircle(float x, float y, float radius)
        {
            GPU_CircleFilled(target, x, y, radius, draw_color);
        }
        
        public void DrawPixel(float x, float y)
        {
            GPU_Pixel(target, x, y, draw_color);
        }

        public void DrawTexture(Texture texture, float x, float y)
        {
            GPU_Blit(texture.TextureHandle, IntPtr.Zero, target, x, y);
        }

        public void DrawTexture(Texture texture, float x, float y, ref RectangleI srcRect)
        {
            blit_rect.x = srcRect.X;
            blit_rect.y = srcRect.Y;
            blit_rect.w = srcRect.W;
            blit_rect.h = srcRect.H;
            
            GPU_Blit(texture.TextureHandle, ref blit_rect, target, x, y);
        }

        public void DrawTexture(Texture texture, ref RectangleI srcRect, ref RectangleF dstRect)
        {
            blit_rect.x = srcRect.X;
            blit_rect.y = srcRect.Y;
            blit_rect.w = srcRect.W;
            blit_rect.h = srcRect.H;
            
            blit_dst_rect.x = dstRect.X;
            blit_dst_rect.y = dstRect.Y;
            blit_dst_rect.w = dstRect.Width;
            blit_dst_rect.h = dstRect.Height;
            
            GPU_BlitRect(texture.TextureHandle, ref blit_rect, target, ref blit_dst_rect);
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