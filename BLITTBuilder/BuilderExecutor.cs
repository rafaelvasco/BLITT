using System;
using System.Collections.Generic;
using System.IO;
using BLITTEngine;
using BLITTEngine.Core.Resources;
using PowerArgs;
using Utf8Json;

namespace BLITTBuilder
{
    public class BuilderExecutor
    {
        [HelpHook, ArgShortcut("-?"), ArgDescription("Shows Usage")]
        public bool Help { get; set; }

        [ArgActionMethod, ArgDescription("Builds Game Assets")]
        public void Build(BuildActionArgs args)
        {
            var project_path = args.ProjectFolderPath;
            
            var project_name = args.ProjectName;

            var project_file_path = Path.Combine(args.ProjectFolderPath, project_name + ".json");

            try
            {
                using (var stream = File.OpenRead(project_file_path))
                {
                    GameProject game_project = JsonSerializer.Deserialize<GameProject>(stream);

                    Builder.BuildGame(project_path, game_project);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public class BuildActionArgs
    {
        [ArgRequired, ArgDescription("Project Folder Path"), ArgPosition(1)]
        public string ProjectFolderPath { get; set; }
        
        [ArgRequired, ArgDescription("Project Name"), ArgPosition(2)]
        public string ProjectName { get; set; }
    } 
}