using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BLITTBuilder
{
    
    [Serializable]
    public class GameProject
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "frame_rate")]
        public int FrameRate { get; set; }
        
        [DataMember(Name = "canvas_width")]
        public int CanvasWidth { get; set; }
        
        [DataMember(Name = "canvas_height")]
        public int CanvasHeight { get; set; }
        
        [DataMember(Name = "start_fullscreen")]
        public bool StartFullscreen { get; set; }

        [DataMember(Name = "resources")]
        public Dictionary<string, string[]> Resources;

        [DataMember(Name = "preload")]
        public string[] PreloadPaks;
    }
}