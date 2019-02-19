using System;

namespace BLITTEngine.Core.Resources
{
    [Serializable]
    public class ShaderProgramData
    {
        public string Id;
        public byte[] VertexShader;
        public byte[] FragmentShader;
    }
}