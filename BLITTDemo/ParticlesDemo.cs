﻿using BLITTEngine;
using BLITTEngine.Core.Graphics;
using BLITTEngine.GameObjects;
using System;
using BLITTEngine.Resources;

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

        public override void Draw(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public override void Update(float dt)
        {
            throw new NotImplementedException();
        }
    }
}
