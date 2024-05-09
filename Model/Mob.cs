using Microsoft.Xna.Framework;

namespace rpgame2
{
    public class Mob
    {
        public Vector2 Position;
        public float Speed = 3f;
        public float Jump = 90f;
        public float Gravity = 3f;
        public bool onGravity = true;
        public Vector2 Velocity;
        public bool hasJump = true;
        public bool IsDead = false;
        public int Strange = 10;
        public bool isHit = false;
        public Rectangle Rectangle;

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
