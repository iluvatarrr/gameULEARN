using Microsoft.Xna.Framework.Graphics;

namespace rpgame2.Model
{
    public class Animation
    {
        public bool IsFinished { get; set; }
        public float FrameSpeed { get; set; }
        public Texture2D Texture { get; private set; }
        public int CurrentFrame { get; set; }
        public bool IsLooping { get; set; }
        public int FrameHeight { get { return Texture.Height; } }
        public int FrameWidth { get { return Texture.Width / FrameCount; } }
        public int FrameCount { get; private set; }


        public Animation(Texture2D texture, int frameCount) 
        {
            Texture = texture;
            FrameSpeed = 0.1f;
            FrameCount = frameCount;
            IsLooping = true;
            IsFinished = false;
        }
        public Animation(Animation animation)
        {
            Texture = animation.Texture;
            FrameSpeed = animation.FrameSpeed;
            CurrentFrame = animation.CurrentFrame;
            FrameCount = animation.FrameCount;
            IsLooping = animation.IsLooping;
            IsFinished = animation.IsFinished;
        }
    }
}
