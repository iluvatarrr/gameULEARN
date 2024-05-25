﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Model;

namespace rpgame2.View
{
    public class AnimationController
    {
        public Animation animation { get; private set; }
        public float timer { get; private set; }
        public Vector2 Position { get; set; }

        public AnimationController(Animation currentAnimation) => animation = currentAnimation;

        public void Play(Animation currentAnumation)
        {
            if (animation == currentAnumation) return;
            animation = currentAnumation;
            animation.CurrentFrame = 0;
            timer = 0;
        }

        public void Stop()
        {
            timer = 0f;
            animation.CurrentFrame = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (!animation.IsLooping && animation.IsFinished) return;

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;
                if (animation.CurrentFrame == animation.FrameCount - 1) animation.IsFinished = true;
                if (animation.CurrentFrame >= animation.FrameCount) animation.CurrentFrame = 0;
            }
        }
    }
}