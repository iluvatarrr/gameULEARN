using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;

namespace rpgame2.Model
{
    public enum State
    {
        SplashScreen,
        Final,
        ChoiceLevel,
        Game,
        Pause,
        Settings,
        Rules,
        ZeroState
    }

    public class GameState
    {
        public static State PeviousState { get; set; }

        public static State CurrentState { get; set; }
        public static State NextState { get; set; }

        public GameState(State state) => CurrentState = state;

        public static void ChengeState(State state) => NextState = state;
    }
}
