using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLITTDemo
{
    public class ParticlesDemo : Scene
    {
        private ParticleEmitter emitter;
        private Sprite particle;

        public override void Init()
        {
            particle = new Sprite(Content.GetTexture2D("particles"), 0, 0, 32, 32 );
        }

        public override void Draw(Renderer2D canvas)
        {
            throw new NotImplementedException();
        }

        public override void Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
