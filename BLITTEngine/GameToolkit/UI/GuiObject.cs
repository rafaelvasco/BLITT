using BLITTEngine.Core.Common;
using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    public abstract class GuiObject
    {
        protected GuiObject(int id, float x, float y, float w, float h)
        {
            _id = id;
            _rect = Rect.FromBox(x, y, w, h);
        }
        
        public int Id => _id;
        
        public abstract void Update();
        
        public abstract void Draw(Canvas canvas);
        
        public virtual void Enter(){}
        
        public virtual void Leave(){}
        
        public virtual void Reset(){}

        public virtual bool IsDone()
        {
            return true;
        }
        
        public virtual void OnFocus(bool focused) {}
        
        public virtual void OnMouseOver(bool over) {}

        public virtual bool OnMouseMove(float x, float y)
        {
            return false;
        }

        public virtual bool OnMouseButton(MouseButton button, bool down)
        {
            return false;
        }

        public virtual bool OnMouseWheel(int notches)
        {
            return false;
        }

        protected int _id;
        internal bool _static;
        internal bool _visible;
        internal bool _enabled;
        internal Rect _rect;
        internal Gui _gui;
        internal GuiObject _next;
        internal GuiObject _prev;

    }
}