﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public bool IsLooping { get; set; }
        public bool IsFinished { get; set; }
        public float FrameSpeed { get; set; }
        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount) 
        {
            Texture = texture;
            FrameCount = frameCount;
            IsLooping = true;
            IsFinished = false;
            FrameSpeed = 0.1f;
        }
    }
}
