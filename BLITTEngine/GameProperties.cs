using System;
using System.Runtime.Serialization;

namespace BLITTEngine
{
    [Serializable]
    public struct GameProperties
    {
        [DataMember(Name = "title", IsRequired = false)]
        public string Title;

        [DataMember(Name = "frame_rate", IsRequired = false)]
        public int FrameRate;
        
        [DataMember(Name = "canvas_width", IsRequired = false)]
        public int CanvasWidth;
        
        [DataMember(Name = "canvas_height", IsRequired = false)]
        public int CanvasHeight;
        
        [DataMember(Name = "fullscreen", IsRequired = false)]
        public bool Fullscreen;

        [DataMember(Name = "preload_paks", IsRequired = false)]
        public string[] PreloadResourcePaks;
        
        public static GameProperties Default()
        {
            return new GameProperties
            {
                CanvasWidth = 800,
                CanvasHeight = 600,
                FrameRate = 60,
                Fullscreen = false,
                Title = "Game"
            };
        }
    }
}