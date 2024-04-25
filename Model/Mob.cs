using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;

namespace rpgame2
{
    public class Mob
    {
        private MainPersonSteps MainPersonSteps = new MainPersonSteps();
        public Vector2 Position;
        public float Speed = 2f;
        public State State;

        public void Update()
        {
            MainPersonSteps.Update();
            if (MainPersonSteps.KeyboardState.Equals(Keys.W)) Position.Y -= Speed;
            if (MainPersonSteps.KeyboardState.Equals(Keys.S)) Position.Y += Speed;
            if (MainPersonSteps.KeyboardState.Equals(Keys.A)) Position.X -= Speed;
            if (MainPersonSteps.KeyboardState.Equals(Keys.D)) Position.X += Speed;
        }
    }
}
