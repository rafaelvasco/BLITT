using System;
using System.Collections.Generic;
using System.Numerics;
using BLITTEngine.Core.Common;
using BLITTEngine.Core.Foundation;
using BLITTEngine.Core.Graphics;
using Vector2 = System.Numerics.Vector2;

namespace BLITTEngine.Core.Resources
{
    public class ShaderParameter
    {
        internal Uniform Uniform;

        internal int ArrayLength;

        internal bool Constant;

        internal bool SubmitedOnce;

        public Vector4 Value => _value;

        private Vector4 _value;

        internal ShaderParameter(string name)
        {
            this.Uniform = new Uniform(name, UniformType.Vector4);
        }

        public void SetValue(float v)
        {
            ArrayLength = 1;
            _value.X = v;
        }

        public void SetValue(Vector2 v)
        {
            ArrayLength = 2;
            
            _value.X = v.X;
            _value.Y = v.Y;
        }

        public void SetValue(Vector3 v)
        {
            ArrayLength = 3;
            
            _value.X = v.X;
            _value.Y = v.Y;
            _value.Z = v.Z;
        }

        public void SetValue(Vector4 v)
        {
            ArrayLength = 4;
            _value = v;
        }

        public void SetValue(Color color)
        {
            ArrayLength = 4;
            
            _value.X = color.Rf;
            _value.Y = color.Gf;
            _value.Z = color.Bf;
            _value.W = color.Af;
        }
    }

    public class ShaderProgram : Resource
    {
        internal Program Program;

        private ShaderParameter[] Parameters;

        private Dictionary<string, int> ParametersMap;

        internal Uniform[] Samplers;


        internal ShaderProgram(Program program, IReadOnlyList<string> samplers, IReadOnlyList<string> _params)
        {
            this.Program = program;
            
            BuildSamplersList(samplers);
            
            BuildParametersList(_params);
        }

        public ShaderParameter GetParameter(string name)
        {
            if (ParametersMap.TryGetValue(name, out var index))
            {
                return Parameters[index];
            }

            return null;
        }

        internal unsafe void SubmitValues()
        {
            for (int i = 0; i < Parameters.Length; ++i)
            {
                var p = Parameters[i];

                if (p.ArrayLength == 0)
                {
                    continue;
                }

                if (p.Constant)
                {
                    if (p.SubmitedOnce)
                    {
                        continue;
                    }
                    else
                    {
                        p.SubmitedOnce = true;
                    }
                    
                }
                
                var val = p.Value;
                
                Bgfx.SetUniform(p.Uniform, &val);
            }
        }

        private void BuildSamplersList(IReadOnlyList<string> samplers)
        {
            Samplers = new Uniform[samplers.Count];

            for (int i = 0; i < samplers.Count; ++i)
            {
                Samplers[i] = new Uniform(samplers[i], UniformType.Sampler);
            }
        }

        private void BuildParametersList(IReadOnlyList<string> _params)
        {
            Parameters = new ShaderParameter[_params.Count];
            ParametersMap = new Dictionary<string, int>();
            
            for (int i = 0; i < _params.Count; ++i)
            {
                Parameters[i] = new ShaderParameter(_params[i]);
                ParametersMap.Add(_params[i], i);
            }
        }

        internal override void Dispose()
        {
            for (int i = 0; i < Samplers.Length; ++i)
            {
                Samplers[i].Dispose();
            }

            for (int i = 0; i < Parameters.Length; ++i)
            {
                Parameters[i].Uniform.Dispose();
            }

            Program.Dispose();
        }
    }
}