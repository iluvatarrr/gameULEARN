using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Controller
{
    public class LevelStateController
    {
        public void Update()
        {
            LevelState.IsChange = false;
            if (LevelState.NextState != LevelStates.ZeroState)
            {
                LevelState.PeviousState = LevelState.CurrentState;
                LevelState.CurrentState = LevelState.NextState;
                LevelState.NextState = LevelStates.ZeroState;
                LevelState.IsChange = true;
            }
        }
    }
}
