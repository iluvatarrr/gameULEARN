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

        private KeyboardState keyboardState;

        public GameState(State state) => CurrentState = state;

        public static void ChengeState(State state) => NextState = state;

        private Input Input = new Input()
        {
            Escape = Keys.Escape,
        };

        public void Update()
        {
            keyboardState = Input.GetState();
            switch (CurrentState)
            {
                case State.Game:
                    if (keyboardState.IsKeyDown(Input.Escape)) NextState = State.Pause;
                    break;
                case State.Settings:
                    if (keyboardState.IsKeyDown(Input.Escape)) NextState = State.SplashScreen;
                    break;
                case State.Rules:
                    if (keyboardState.IsKeyDown(Input.Escape)) NextState = State.SplashScreen;
                    break;
            }
            if (NextState != State.ZeroState)
            {
                PeviousState = CurrentState;
                CurrentState = NextState;
                NextState = State.ZeroState;
            }
        }
    }
}
