using System;
using System.Numerics;

namespace BLITTEngine.Numerics
{
     public class Transform2D
    {
        public event Action OnChanged;

        private Transform2D parent;
        private Vector2 position;
        private Vector2 scale = Vector2.One;
        private float rotation;
        private System.Numerics.Matrix3x2 matrix;
        private bool dirty = true;

        private void MakeDirty()
        {
            dirty = true;
            OnChanged?.Invoke();
        }

        public Transform2D Parent
        {
            get => parent;
            set
            {
                if (parent != value)
                {
                    if (parent != null)
                        parent.OnChanged -= MakeDirty;

                    parent = value;

                    if (parent != null)
                        parent.OnChanged += MakeDirty;

                    MakeDirty();
                }

            }
        }

        public float X
        {
            get => position.X;
            set
            {
                if (position.X != value)
                {
                    position.X = value;
                    MakeDirty();
                }
            }
        }

        public float Y
        {
            get => position.Y;
            set
            {
                if (position.Y != value)
                {
                    position.Y = value;
                    MakeDirty();
                }
            }
        }

        public Vector2 Position
        {
            get => position;
            set
            {
                if (position != value)
                {
                    position = value;
                    MakeDirty();
                }
            }
        }

        public Vector2 Scale
        {
            get => scale;
            set
            {
                if (scale != value)
                {
                    scale = value;
                    MakeDirty();
                }
            }
        }

        public float ScaleX
        {
            get => scale.X;
            set
            {
                if (scale.X != value)
                {
                    scale.X = value;
                    MakeDirty();
                }
            }
        }

        public float ScaleY
        {
            get => scale.Y;
            set
            {
                if (scale.Y != value)
                {
                    scale.Y = value;
                    MakeDirty();
                }
            }
        }

        public float Rotation
        {
            get => rotation;
            set
            {
                if (rotation != value)
                {
                    rotation = value;
                    MakeDirty();
                }
            }
        }

        public Matrix3x2 Matrix
        {
            get
            {
                if (dirty)
                {
                    
                    Matrix3x2Ext.CreateTransform(scale, rotation, position, out matrix);
                    if (parent != null)
                        matrix = matrix * parent.Matrix;
                    dirty = false;
                }

                return matrix;
            }
        }

        public Vector2 GlobalPosition
        {
            get
            {
                if (parent != null)
                    return parent.Matrix.TransformPoint(position);
                return position;
            }
        }

        public bool IsAncestorOf(Transform2D t)
        {
            if (t.parent != null)
            {
                var p = t.parent;
                while (p != null)
                {
                    if (p == this)
                        return true;
                    p = p.parent;
                }
            }
            return false;
        }

        public bool IsDescendantOf(Transform2D t)
        {
            return t.IsAncestorOf(this);
        }
    }
}