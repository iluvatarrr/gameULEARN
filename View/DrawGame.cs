using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame;
using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.View
{
    public class DrawGame
    {
        public Game1 Game;
        public DrawGame(Game1 game1)
        {
            Game = game1;
        }
        public void DrawAllGame(SpriteBatch spriteBatch)
        {
            if (GameState.CurrentState.Equals(State.SplashScreen))
                Game.Menu.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Rules))
                Game.Rules.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Pause))
                Game.Pause.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.ChoiceLevel))
                Game.Choise.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Final))
                Game.Final.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Settings))
                Game.Setting.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Game))
                Game.Level.Draw(spriteBatch);
        }
    }
}
