using System;
using System.Drawing;
using BLITTEngine.Graphics;
using BLITTEngine.Numerics;
using Color = BLITTEngine.Graphics.Color;

namespace BLITTEngine.Platform
{
    internal interface GraphicsModule
    {
        Texture CreateTexture(Pixmap pixmap, bool is_render_target);
        Texture CreateTexture(int width, int height, bool is_render_target);
        void DestroyTexture(Texture texture);
        void UpdateTexture(Texture texture, Pixmap pixmap);
        void Terminate();
        void SetColor(ref Color color);
        void SetClearColor(ref Color color);
        Color GetClearColor();
        void SetRenderTarget(Texture texture);
        void ResetRenderTarget();
        void BeginDraw();
        void EndDraw();
        void SetViewport(float x, float y, float w, float h);
        void DrawRect(float x, float y, float w, float h);
        void FillRect(float x, float y, float w, float h);
        void DrawLine(float x1, float y1, float x2, float y2);
        void DrawCircle(float x, float y, float radius);
        void FillCircle(float x, float y, float radius);
        void DrawPixel(float x, float y);
        void DrawTexture(Texture texture, float x, float y);
        void DrawTexture(Texture texture, float x, float y, ref RectangleI srcRect);
        void DrawTexture(Texture texture, ref RectangleI srcRect, ref RectangleF dstRect);
        
        void Translate(float x, float y);
        void Rotate(float rotation);
        void Scale(float scale);

        void PushTransform();
        void PopTransform();
    }
}