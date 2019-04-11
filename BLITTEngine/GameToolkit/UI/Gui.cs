using BLITTEngine.Core.Control;
using BLITTEngine.Core.Control.Mouse;
using BLITTEngine.Core.Graphics;

namespace BLITTEngine.GameToolkit.UI
{
    
    
    public class Gui
    {

        public void AddCtrl(GuiObject ctrl)
        {
            GuiObject last = _root;

            ctrl._gui = this;

            if (_root == null)
            {
                _root = ctrl;
                ctrl._prev = null;
                ctrl._next = null;
            }
            else
            {
                while (last._next != null)
                {
                    last = last._next;
                }

                last._next = ctrl;
                ctrl._prev = last;
                ctrl._next = null;
            }
        }

        public void RemoveCtrl(int id)
        {
            var ctrl = this._root;

            while (ctrl != null)
            {
                if (ctrl.Id == id)
                {
                    if (ctrl._prev != null)
                    {
                        ctrl._prev._next = ctrl._next;
                    }
                    else
                    {
                        _root = ctrl._next;
                    }

                    if (ctrl._next != null)
                    {
                        ctrl._next._prev = ctrl._prev;
                    }

                    return;
                }

                ctrl = ctrl._next;
            }
        }

        public GuiObject GetCtrl(int id)
        {
            var ctrl = _root;

            while (ctrl != null)
            {
                if (ctrl.Id == id)
                {
                    return ctrl;
                }

                ctrl = ctrl._next;
            }

            return null;
        }

        public void MoveCtrl(int id, float x, float y)
        {
            var ctrl = GetCtrl(id);

            if (ctrl == null)
            {
                return;
            }

            ctrl._rect.X2 = x + (ctrl._rect.X2 - ctrl._rect.X1);
            ctrl._rect.Y2 = y + (ctrl._rect.Y2 - ctrl._rect.Y1);
            ctrl._rect.X1 = x;
            ctrl._rect.Y1 = y;

        }

        public void ShowCtrl(int id, bool visible)
        {
            var ctrl = GetCtrl(id);

            if (ctrl == null)
            {
                return;
            }

            ctrl._visible = visible;
        }

        public void EnableCtrl(int id, bool enabled)
        {
            var ctrl = GetCtrl(id);

            if (ctrl == null)
            {
                return;
            }

            ctrl._enabled = enabled;
        }

        public void SetCursor(Sprite spr)
        {
            _spr_cursor = spr;
        }

        public void SetFocus(int id)
        {
            var ctrl_new_focus = GetCtrl(id);

            if (ctrl_new_focus == _ctrl_focused)
            {
                return;
            }

            if (ctrl_new_focus == null)
            {
                _ctrl_focused?.OnFocus(false);

                _ctrl_focused = null;
            }
            else if(!ctrl_new_focus._static && ctrl_new_focus._visible && ctrl_new_focus._enabled)
            {
                _ctrl_focused?.OnFocus(false);

                ctrl_new_focus.OnFocus(true);

                _ctrl_focused = ctrl_new_focus;
            }

        }

        public int GetFocus()
        {
            return _ctrl_focused?.Id ?? 0;
        }

        public void Enter()
        {
            var ctrl = _root;

            while (ctrl != null)
            {
                ctrl.Enter();
                ctrl = ctrl._next;
            }

            _enter_leave = 2;
        }

        public void Leave()
        {
            var ctrl = _root;

            while (ctrl != null)
            {
                ctrl.Leave();
                ctrl = ctrl._next;
            }

            _ctrl_focused = null;
            _ctrl_over = null;
            _ctrl_lock = null;
            _enter_leave = 1;

        }

        public void Reset()
        {
            var ctrl = _root;

            while (ctrl != null)
            {
               ctrl.Reset();
               ctrl = ctrl._next;
            }

            _ctrl_lock = null;
            _ctrl_over = null;
            _ctrl_focused = null;
        }

        public void Move(float dx, float dy)
        {
            var ctrl = _root;

            while (ctrl != null)
            {
                ctrl._rect.X1 += dx;
                ctrl._rect.Y1 += dy;
                ctrl._rect.X2 += dx;
                ctrl._rect.Y2 += dy;

                ctrl = ctrl._next;

            }
        }

        public int Update()
        {
            // Get Mouse State

            var mouse_pos = Input.MousePosition;

            _mouse_x = mouse_pos.X;
            _mouse_y = mouse_pos.Y;

            _mouse_left_pressed = Input.MousePressed(MouseButton.Left);
            _mouse_left_released = Input.MouseReleased(MouseButton.Left);
            _mouse_right_pressed = Input.MousePressed(MouseButton.Right);
            _mouse_right_released = Input.MouseReleased(MouseButton.Right);

            _mouse_wheel = Input.MouseWheel;

            // Update All Controls
            
            GuiObject ctrl = _root;

            while (ctrl != null)
            {
                ctrl.Update();
                ctrl = ctrl._next;
            }

            // Handle Enter/Leave
            
            if (_enter_leave > 0)
            {
                ctrl = _root;

                bool done = true;

                while (ctrl != null)
                {
                    if (!ctrl.IsDone())
                    {
                        done = false;
                        break;
                    }

                    ctrl = ctrl._next;
                }

                if (!done)
                {
                    return 0;
                }

                if (_enter_leave == 1)
                {
                    return -1;
                }

                _enter_leave = 0;
            }
            
            // Handle Keys
            
            // Handle Mouse

            bool left_down = Input.MouseDown(MouseButton.Left);
            bool right_down = Input.MouseDown(MouseButton.Right);

            if (_ctrl_lock != null)
            {
                ctrl = _ctrl_lock;

                if (!left_down && !right_down)
                {
                    _ctrl_lock = null;
                }

                if (ProcessCtrl(ctrl))
                {
                    return ctrl.Id;
                }
            }
            else
            {
                // Find topmost control

                ctrl = _root;

                if (ctrl != null)
                {
                    while (ctrl._next != null)
                    {
                        ctrl = ctrl._next;
                    }
                }

                while (ctrl != null)
                {
                    if (ctrl._enabled && ctrl._rect.Contains(_mouse_x, _mouse_y))
                    {
                        if (_ctrl_over != ctrl)
                        {
                            _ctrl_over?.OnMouseOver(false);
                            
                            ctrl.OnMouseOver(true);

                            _ctrl_over = ctrl;
                        }

                        if (ProcessCtrl(ctrl))
                        {
                            return ctrl.Id;
                        }

                        return 0;
                    }

                    ctrl = ctrl._prev;
                }

                if (_ctrl_over != null)
                {
                    _ctrl_over.OnMouseOver(false);
                    _ctrl_over = null;
                }
            }

            return 0;
        }

        public void Draw(Canvas canvas)
        {
            GuiObject ctrl = _root;

            while (ctrl != null)
            {
                if (ctrl._visible)
                {
                    ctrl.Draw(canvas);
                }

                ctrl = ctrl._next;
            }

            if (Input.IsMouseOver)
            {
                _spr_cursor?.Draw(canvas, _mouse_x, _mouse_y);
            }
        }


        private bool ProcessCtrl(GuiObject ctrl)
        {
            bool result = false;

            if (_mouse_left_pressed)
            {
                _ctrl_lock = ctrl;
                SetFocus(ctrl.Id);
                result = ctrl.OnMouseButton(MouseButton.Left, true);
            }

            if (_mouse_right_pressed)
            {
                _ctrl_lock = ctrl;
                SetFocus(ctrl.Id);
                result = result || ctrl.OnMouseButton(MouseButton.Right, true);
            }

            if (_mouse_left_released)
            {
                result = result || ctrl.OnMouseButton(MouseButton.Left, false);
            }

            if (_mouse_right_released)
            {
                result = result || ctrl.OnMouseButton(MouseButton.Right, false);
            }

            if (_mouse_wheel != 0)
            {
                result = result || ctrl.OnMouseWheel(_mouse_wheel);
            }

            result = result || ctrl.OnMouseMove(_mouse_x - ctrl._rect.X1, _mouse_y - ctrl._rect.Y1);

            return result;
        }

        private GuiObject _root;

        private GuiObject _ctrl_lock;
        private GuiObject _ctrl_focused;
        private GuiObject _ctrl_over;

        private int _enter_leave;
        private Sprite _spr_cursor;

        private int _mouse_x;
        private int _mouse_y;

        private int _mouse_wheel;
        private bool _mouse_left_pressed;
        private bool _mouse_left_released;
        private bool _mouse_right_pressed;
        private bool _mouse_right_released;

    }
}