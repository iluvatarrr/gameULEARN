using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;

namespace rpgame2
{
    public enum State
    {
        SplashScreen,
        Final,
        Game,
        Pause,
    }

    public class GameState
    {
        public State State { get; set; }
        public GameState(State state) 
        {
            State = state;
        }

        public void Update()
        {
            switch (State)
            {
                case State.SplashScreen:
                    if (Keyboard.GetState().IsKeyDown(Keys.D1)) State = State.Game;
                    break;
                case State.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) State = State.SplashScreen;
                    break;
            }
        }
    }
}
