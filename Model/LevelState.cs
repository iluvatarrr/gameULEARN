using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Model
{ 
    public enum LevelStates
    {
            First,
            Second,
            Third,
            ZeroState,
    }
    public class LevelState
    {
        public static LevelStates PeviousState { get; set; }
        public static bool IsChange { get; set; }
        public static LevelStates CurrentState { get; set; }
        public static LevelStates NextState { get; set; }

        public LevelState(LevelStates state) => NextState = state;

        public static void ChengeState(LevelStates state)
        {
            NextState = state;
        }
    }
}
