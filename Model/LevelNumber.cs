using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{
    public class LevelState
    {
        public enum LevelNumber
        {
            First,
            Second,
            Third,
            ZeroState,
        }
            public static LevelNumber PeviousState { get; set; }
            public static bool IsChange;
            public static LevelNumber CurrentState { get; set; }
            public static LevelNumber NextState { get; set; }

            public LevelState(LevelNumber state) => NextState = state;

            public static void ChengeState(LevelNumber state)
            {
                NextState = state;
            }

            public void Update()
            {
                IsChange = false;
                if (NextState != LevelNumber.ZeroState)
                {
                    PeviousState = CurrentState;
                    CurrentState = NextState;
                    NextState = LevelNumber.ZeroState;
                    IsChange = true;
                }
            }


    }
}
