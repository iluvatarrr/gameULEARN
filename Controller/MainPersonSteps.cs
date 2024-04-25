using Game1;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2
{
    public class MainPersonSteps
    {
        public Keys KeyboardState = Keys.None;
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W)) KeyboardState = Keys.W;
            else if (keyboardState.IsKeyDown(Keys.S)) KeyboardState = Keys.S;
            else if (keyboardState.IsKeyDown(Keys.A)) KeyboardState = Keys.A;
            else if (keyboardState.IsKeyDown(Keys.D)) KeyboardState = Keys.D;
            else KeyboardState = Keys.None;
        }
    }
}
