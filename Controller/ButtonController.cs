using Microsoft.Xna.Framework;
using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static rpgame2.Model.LevelState;

namespace rpgame2.Controller
{
    public class ButtonController
    {
        public static Game Game;
        public ButtonController(Game game) => Game = game;
        public static void NewGameButton(object sender, EventArgs e) => GameState.ChengeState(State.Game);
        public static void SettingsButton(object sender, EventArgs e) => GameState.ChengeState(State.Settings);
        public static void RulesButton(object sender, EventArgs e) => GameState.ChengeState(State.Rules);
        public static void BackButton(object sender, EventArgs e) => GameState.NextState = GameState.PeviousState;
        public static void MenuButton(object sender, EventArgs e) => GameState.ChengeState(State.SplashScreen);
        public static void ChoiseButton(object sender, EventArgs e) => GameState.ChengeState(State.ChoiceLevel);
        public static void FirstLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelNumber.First);
        }
        public static void SecondLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelNumber.Second);
        }
        public static void ThirdLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelNumber.Third);
        }
        public static void ExitButton(object sender, EventArgs e) => Game.Exit();
    }
}
