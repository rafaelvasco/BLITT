using System;
using System.Collections.Generic;
using System.Numerics;
using BLITTEngine.Foundation;

namespace BLITTEngine.Core.Graphics
{
    public enum ShaderParameterType
    {
        Vector,
        Matrix
    }
    
    public class ShaderParameter
    {
        private Vector4 value_vector;
        private Matrix4x4 value_matrix;

        public ShaderParameterType Type { get; private set; }
        

        public string Name { get; }

        internal Uniform InternalUniform { get; private set; }

        public ShaderParameter(string name, ShaderParameterType type)
        {
            Type = type;
            Name = name;

            switch (type)
            {
                case ShaderParameterType.Vector:
                    value_vector = new Vector4();
                    break;
                case ShaderParameterType.Matrix:
                    value_matrix = Matrix4x4.Identity;
                    break;
                    
            }
                
            ChangeUniform(type);
        }

        private void ChangeUniform(ShaderParameterType type)
        {
            if (this.Type == type)
            {
                return;
            }

            switch (type)
            {
                case ShaderParameterType.Matrix:
                    this.InternalUniform.Dispose();
                    this.InternalUniform = new Uniform(this.Name, UniformType.Matrix4x4);
                    this.Type = type;
                    break;
                case ShaderParameterType.Vector:
                    this.InternalUniform.Dispose();
                    this.InternalUniform = new Uniform(this.Name, UniformType.Vector4);
                    this.Type = type;
                    break;
            }
        }

        public void SetValue(float value)
        {
            
            value_vector.X = value;
        }

        public void SetValue(bool value)
        {
            value_vector.X = value ? 1.0f : 0.0f;
        }

        public void SetValue(ref Vector4 value)
        {

            value_vector.X = value.X;
            value_vector.Y = value.Y;
            value_vector.Z = value.Z;
            value_vector.W = value.W;
        }

        public void SetValue(ref Vector3 value)
        {

            value_vector.X = value.X;
            value_vector.Y = value.Y;
            value_vector.Z = value.Z;
            value_vector.W = 0.0f;
        }

        public void SetValue(ref Vector2 value)
        {
            value_vector.X = value.X;
            value_vector.Y = value.Y;
            value_vector.Z = 0.0f;
            value_vector.W = 0.0f;
        }

        public void SetValue(float value1, float value2, float value3, float value4)
        {
         
            value_vector.X = value1;
            value_vector.Y = value2;
            value_vector.Z = value3;
            value_vector.W = value4;
        }

        public void SetValue(Matrix4x4 value)
        {
            this.value_matrix = value;
        }

        public unsafe void Submit()
        {
            switch (Type)
            {
                case ShaderParameterType.Vector:
                {
                    var value = value_vector;
                    Bgfx.SetUniform(InternalUniform, &value);
                }
                    break;
                case ShaderParameterType.Matrix:
                {
                    var value = value_matrix;
                    Bgfx.SetUniform(InternalUniform, &value);
                }
                    break;
            }
        }
    }
    
    public class ShaderProgram
    {
        internal Program Program { get; private set; }
        
        private readonly Dictionary<string, int> shader_param_map; 
        private readonly List<ShaderParameter> shader_params;
        private readonly Dictionary<string, Uniform> texture_uniforms; 
        
        internal ShaderProgram(Shader vertexShader, Shader fragShader)
        {
            shader_param_map = new Dictionary<string, int>();
            shader_params = new List<ShaderParameter>();

            texture_uniforms = new Dictionary<string, Uniform>();

            Program = new Program(vertexShader, fragShader, true);

        }
        
        public void AddTextureUniform(string name)
        {
            if (texture_uniforms.ContainsKey(name))
            {
                throw new InvalidOperationException("ShaderProgram already contains a Texture Uniform with this name!");
            }

            var textureUniform = new Uniform(name, UniformType.Int1);

            texture_uniforms.Add(name, textureUniform);
        }

        public void SetTexture(Texture2D texture, string uniformName, byte tex_unit=0)
        {
            if (texture_uniforms.TryGetValue(uniformName, out var uniform))
            {
                Bgfx.SetTexture(tex_unit, uniform, texture.InternalTexture);
            }
        }

        public void SetParameter(string name, float value)
        {
            if (shader_param_map.TryGetValue(name, out var index))
            {
                shader_params[index].SetValue(value);
            }
            else
            {
                var param = new ShaderParameter(name, ShaderParameterType.Vector);

                param.SetValue(value);

                shader_param_map.Add(name, shader_params.Count);
                shader_params.Add(param);
            }
            
        }

        public void SetParameter(string name, Vector2 value)
        {
            if (shader_param_map.TryGetValue(name, out var index))
            {
                shader_params[index].SetValue(ref value);
            }
            else
            {
                var param = new ShaderParameter(name, ShaderParameterType.Vector);

                param.SetValue(ref value);

                shader_param_map.Add(name, shader_params.Count);
                shader_params.Add(param);
            }
        }

        public void SetParameter(string name, Vector3 value)
        {
            if (shader_param_map.TryGetValue(name, out var index))
            {
                shader_params[index].SetValue(ref value);
            }
            else
            {
                var param = new ShaderParameter(name, ShaderParameterType.Vector);

                param.SetValue(ref value);

                shader_param_map.Add(name, shader_params.Count);
                shader_params.Add(param);
            }
        }

        public void SetParameter(string name, Vector4 value)
        {
            if (shader_param_map.TryGetValue(name, out var index))
            {
                shader_params[index].SetValue(ref value);
            }
            else
            {
                var param = new ShaderParameter(name, ShaderParameterType.Vector);

                param.SetValue(ref value);

                shader_param_map.Add(name, shader_params.Count);
                shader_params.Add(param);
            }
        }

        public void SetParameter(string name, Matrix4x4 value)
        {
            if (shader_param_map.TryGetValue(name, out var index))
            {
                shader_params[index].SetValue(value);
            }
            else
            {
                var param = new ShaderParameter(name, ShaderParameterType.Matrix);

                param.SetValue(value);

                shader_param_map.Add(name, shader_params.Count);
                shader_params.Add(param);
            }
        }


        internal void SubmitUniforms()
        {
            foreach (var param in shader_params)
            {
                param.Submit();
            }
        }

        public void Dispose()
        {
            foreach (var param in shader_params)
            {
                param.InternalUniform.Dispose();
            }

            foreach (var textureUniform in texture_uniforms)
            {
                textureUniform.Value.Dispose();
            }

        }

    }
}