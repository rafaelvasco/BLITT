using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BLITTEngine;
using BLITTEngine.Core.Resources;
using Utf8Json;

namespace BLITTBuilder
{
    public static class Builder
    {
        private static readonly ResourceLoader Loader = new ResourceLoader();
        
        private static ResourceType GetResourceTypeByExt(string ext)
        {
            switch (ext)
            {
                case ".png": return ResourceType.Image;
                case ".vs":
                case ".fs": return ResourceType.Shader;
                case ".wav": return ResourceType.Sfx;
                case ".ogg": return ResourceType.Song;
                case ".fnt": return ResourceType.Font;
                case ".txt": return ResourceType.TextFile;
            }

            throw new Exception("Unrecognized Extension: " + ext);
        }

        public static void BuildGame(string root_path, GameProject project)
        {
            List<ResourcePak> resource_paks = 
                Builder.BuildProjectResources(root_path, project);

            foreach (var pak in resource_paks)
            {
                var bytes = BinarySerializer.Serialize(pak);

                File.WriteAllBytes(Path.Combine(root_path, "Content", pak.Name + ".pak"), bytes);
            }
            
            //BuildGameConfigFile(root_path, project);
            
            Console.WriteLine("Project Built Successfully");
        }

        private static void BuildGameConfigFile(string root_path, GameProject project)
        {
            GameProperties props = new GameProperties()
            {
                Title = project.Title,
                FrameRate = project.FrameRate,
                CanvasWidth = project.CanvasWidth,
                CanvasHeight = project.CanvasHeight,
                Fullscreen = project.StartFullscreen,
                PreloadResourcePaks = project.PreloadPaks
            };
            
            File.WriteAllBytes(Path.Combine(root_path, "Config.json"), 
                JsonSerializer.PrettyPrintByteArray(JsonSerializer.Serialize(props)));
            
        }
        
        private static List<ResourcePak> BuildProjectResources(string root_path, GameProject project)
        {
            var resource_groups = project.Resources;

            string shader_vs_aux = null;
            string shader_fs_aux = null;
            
            var results = new List<ResourcePak>();

            foreach (var resource_group in resource_groups)
            {
                var pak = new ResourcePak(resource_group.Key);

                var files = resource_group
                    .Value
                    .Select(p => new ResourceFileInfo(
                        file_name: Path.GetFileNameWithoutExtension(p),
                        full_path: Path.Combine(root_path, "Content", p),
                        extension: Path.GetExtension(p),
                        type: GetResourceTypeByExt(Path.GetExtension(p))))
                    .OrderBy(f => f.FileName)
                    .GroupBy(f => f.FileName);


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

                                var font_descr_path = res_file_info.FullPath;
                                var font_image_path = Path.Combine(
                                    Path.GetDirectoryName(res_file_info.FullPath),
                                    res_file_info.FileName + ".png"
                                );
                                
                                var font_data = Loader.LoadFontData(font_descr_path, font_image_path);

                                pak.Fonts.Add(res_file_info.FileName, font_data);

                                break;
                            case ResourceType.Shader:

                                switch (res_file_info.Extension)
                                {
                                    case ".vs":
                                        shader_vs_aux = res_file_info.FullPath;
                                        break;
                                    case ".fs":
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
                
                results.Add(pak);
            }

            return results;
        }
    }
}