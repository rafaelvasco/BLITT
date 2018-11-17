using BLITTEngine.Core.Foundation;
using System;
using System.Numerics;

namespace BLITTEngine.Core.Graphics
{
    public class ShaderParameter
    {
        internal Uniform Uniform;

        private Vector4 value;

        internal ShaderParameter(string name)
        {
            this.Uniform = new Uniform(name, UniformType.Vector4);
        }

        public void SetValue(float v)
        {
            value.X = v;
            value.Y = 0;
            value.Z = 0;
            value.W = 0;
        }

        public void SetValue(Vector2 v)
        {
            value.X = v.X;
            value.Y = v.Y;
            value.Z = 0;
            value.W = 0;
        }

        public void SetValue(Vector3 v)
        {
            value.X = v.X;
            value.Y = v.Y;
            value.Z = v.Z;
            value.W = 0;
        }

        public void SetValue(Vector4 v)
        {
            value = v;
        }

        public void SetValue(Color color)
        {
            value.X = color.R / 255f;
            value.Y = color.G / 255f;
            value.Z = color.B / 255f;
            value.W = color.A / 255f;
        }
    }

    public class ShaderProgram : IDisposable
    {
        internal static GraphicsContext GraphicsContext;

        internal Program Program;

        internal ShaderParameter[] Parameters;

        internal Uniform[] Samplers;

        private int param_idx;
        private int sample_idx;

        internal ShaderProgram(Program program)
        {
            this.Program = program;
            Parameters = new ShaderParameter[16];
            Samplers = new Uniform[8];
        }

        public ShaderParameter CreateParameter(string name)
        {
            var parameter = new ShaderParameter(name);

            Parameters[param_idx++] = parameter;

            return parameter;
        }

        public void AddTextureUniform(string name)
        {
            Samplers[sample_idx++] = new Uniform(name, UniformType.Int1);
        }

        public void Dispose()
        {
            while (sample_idx > 0)
            {
                Samplers[--sample_idx].Dispose();
            }

            while (param_idx > 0)
            {
                Parameters[--param_idx].Uniform.Dispose();
               
            }

            Program.Dispose();
        }
    }
}
