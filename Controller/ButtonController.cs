using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using System;

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
        public static void MoreMusicButton(object sender, EventArgs e) => MusicValue.PlusMusic();
        public static void LessMusicButton(object sender, EventArgs e) => MusicValue.MinusMusic();

        public static void FirstLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelStates.First);
        }
        public static void SecondLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelStates.Second);
        }
        public static void ThirdLevelButton(object sender, EventArgs e)
        {
            GameState.ChengeState(State.Game);
            LevelState.ChengeState(LevelStates.Third);
        }
        public static void ExitButton(object sender, EventArgs e) => Game.Exit();
    }
}
