using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Controller
{
    public class GameStateController
    {
        private KeyboardState keyboardState;

        private Input Input = new Input()
        {
            Escape = Keys.Escape,
        };

        public void Update()
        {
            keyboardState = Input.GetState();
            switch (GameState.CurrentState)
            {
                case State.Game:
                    if (keyboardState.IsKeyDown(Input.Escape)) GameState.NextState = State.Pause;
                    break;
                case State.Settings:
                    if (keyboardState.IsKeyDown(Input.Escape)) GameState.NextState = State.SplashScreen;
                    break;
                case State.Rules:
                    if (keyboardState.IsKeyDown(Input.Escape)) GameState.NextState = State.SplashScreen;
                    break;
            }
            if (GameState.NextState != State.ZeroState)
            {
                GameState.PeviousState = GameState.CurrentState;
                GameState.CurrentState = GameState.NextState;
                GameState.NextState = State.ZeroState;
            }
        }
    }
}
