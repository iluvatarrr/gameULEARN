using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Model;

namespace rpgame2.View
{
    public class AnimationController
    {
        private Animation animation;
        private float timer;
        public Vector2 Position { get; set; }

        public AnimationController(Animation currentAnimation)
        {
            animation = currentAnimation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation.Texture, Position,
                    new Rectangle(animation.CurrentFrame * animation.FrameWidth, 0,
                    animation.FrameWidth, animation.FrameHeight), Color.White);
        }

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
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer > animation.FrameSpeed)
            {
                timer = 0f;
                animation.CurrentFrame++;
                if (animation.CurrentFrame >= animation.FrameCount)
                    animation.CurrentFrame = 0;
            }
        }
    }
}
