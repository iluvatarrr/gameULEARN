using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using rpgame2.Model;

namespace rpgame2.View
{
    public class AnimationView
    {
        public Animation Animation { get; private set; }
        public AnimationController AnimationController { get; private set; }
        public AnimationView(AnimationController animationController)
        {
            AnimationController = animationController;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AnimationController.animation.Texture, AnimationController.Position,
            new Rectangle(AnimationController.animation.CurrentFrame * AnimationController.animation.FrameWidth, 0,
            AnimationController.animation.FrameWidth, AnimationController.animation.FrameHeight), Color.White);
        }
    }
}
