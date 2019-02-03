using System.IO;

namespace BLITTEngine.Core.Resources.Loaders
{
    internal class ShaderLoader : BaseLoader
    {
        public override Resource Load(Stream vs_stream, Stream fs_stream)
        {
            var vs_file_buffer = new byte[vs_stream.Length];
            vs_stream.Read(vs_file_buffer, 0, vs_file_buffer.Length);

            var fs_file_buffer = new byte[fs_stream.Length];
            fs_stream.Read(fs_file_buffer, 0, fs_file_buffer.Length);

            ShaderProgram shader_program = Game.Instance.GraphicsContext.CreateShader(vs_file_buffer, fs_file_buffer);

            return shader_program;
        }
    }
}