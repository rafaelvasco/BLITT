using System;
using System.IO;
using System.Linq;
using BLITTEngine.Core.Resources;

namespace BLITTBuilder
{
    
    public static class Builder
    {
        private static readonly string[] ACCEPTED_EXT = {".png,", ".vs", ".fs", ".wav", ".ogg", ".fnt"};

        private static ResourceType GetResourceTypeByExt(string ext)
        {
            switch (ext)
            {
                case ".png" : return ResourceType.Image;
                case ".vs" :
                case ".fs" : return ResourceType.Shader;
                case ".wav" : return ResourceType.Sfx;
                case ".ogg" : return ResourceType.Song;
                case ".fnt" : return ResourceType.Font;
            }
            
            throw new Exception("Unrecognized Extension: " + ext);
        }
        
        public static ResourcePak BuildPak(string directory)
        {
            
            var pak = new ResourcePak();

            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).Where(f =>
                    ACCEPTED_EXT.Contains(Path.GetExtension(f), StringComparer.OrdinalIgnoreCase))
                .Select(f => new ResourceFileInfo(Path.GetFileNameWithoutExtension(f), f, Path.GetExtension(f), GetResourceTypeByExt(Path.GetExtension(f))))
                .OrderBy(f => f.FileName)
                .GroupBy(f => f.FileName);


            string font_descr_aux = null;
            string font_sheet_aux = null;

            string shader_vs_aux = null;
            string shader_fs_aux = null;

            foreach (var file_group in files)
            {
                foreach (var res_file_info in file_group)
                {
                    switch (res_file_info.Type)
                    {
                        case ResourceType.Image:

                            var pixmap_data = Loader.LoadPixmapData(res_file_info.FullPath);
                            
                            pak.Images.Add(res_file_info.FileName, pixmap_data);
                            
                            break;
                        
                        case ResourceType.Font:

                            switch (res_file_info.Extension)
                            {
                                case ".fnt":
                                    font_descr_aux = res_file_info.FullPath;
                                    break;
                                case ".png":
                                    font_sheet_aux = res_file_info.FullPath;
                                    break;
                            }

                            if (font_descr_aux != null && font_sheet_aux != null)
                            {
                                var font_data = Loader.LoadFontData(font_descr_aux, font_sheet_aux);

                                font_descr_aux = null;
                                font_sheet_aux = null;
                                
                                pak.Fonts.Add(res_file_info.FileName, font_data);
                            }
                            
                            break;
                        case ResourceType.Shader:

                            switch (res_file_info.Extension)
                            {
                                case ".vs" :
                                    shader_vs_aux = res_file_info.FullPath;
                                    break;
                                case ".fs" :
                                    shader_fs_aux = res_file_info.FullPath;
                                    break;
                            }

                            if (shader_vs_aux != null && shader_fs_aux != null)
                            {
                                var shader_data = Loader.LoadShaderProgramData(shader_vs_aux, shader_fs_aux);

                                pak.Shaders.Add(res_file_info.FileName, shader_data);

                                shader_vs_aux = null;
                                shader_fs_aux = null;
                            }
                            
                            break;
                        case ResourceType.Sfx:

                            var sfx_data = Loader.LoadSfxData(res_file_info.FullPath);
                            
                            pak.Sfx.Add(res_file_info.FileName, sfx_data);
                            
                            break;
                        case ResourceType.Song:

                            var song_data = Loader.LoadSongData(res_file_info.FullPath);
                            
                            pak.Songs.Add(res_file_info.FileName, song_data);
                            
                            break;
                        
                        case ResourceType.TextFile:

                            var text_file_data = Loader.LoadTextFileData(res_file_info.FullPath);
                            
                            pak.TextFiles.Add(res_file_info.FileName, text_file_data);
                            
                            break;
                    }
                }
                
            }

            return pak;
        }
        
    }
}