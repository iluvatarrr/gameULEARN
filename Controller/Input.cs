using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.Controller
{
    public class Input
    {
        public Keys CurrentKey { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Fight { get; set; }
        public Keys Fight2 { get; set; }
        public Keys Fight3 { get; set; }
        public Keys Jump { get; set; }
        public Keys None { get; set; }
    }
}

//using Microsoft.Xna.Framework.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace rpgame2.Controller
//{
//    public class Input
//    {
//        private Keys Down = Keys.S;
//        private Keys Left = Keys.A;
//        private Keys Right = Keys.D;
//        private Keys Up = Keys.W;
//        private Keys Fight = Keys.E;
//        private Keys Fight2 = Keys.R;
//        private Keys Fight3 = Keys.F;
//        private Keys Jump = Keys.Space;
//        private Keys None = Keys.None;

//        public string GetKeys()
//        {
//            KeyboardState keyboardState = Keyboard.GetState();
//            if (keyboardState.IsKeyDown(Keys.S)) return "Down";
//            if (keyboardState.IsKeyDown(Keys.A)) return "Left";
//            if (keyboardState.IsKeyDown(Keys.D)) return "Right";
//            if (keyboardState.IsKeyDown(Keys.W)) return "Up";
//            if (keyboardState.IsKeyDown(Keys.E)) return "Fight";
//            if (keyboardState.IsKeyDown(Keys.R)) return "Fight2";
//            if (keyboardState.IsKeyDown(Keys.F)) return "Fight3";
//            if (keyboardState.IsKeyDown(Keys.Space)) return "Jump";
//            if (keyboardState.IsKeyDown(Keys.Escape)) return "Escape";
//            if (keyboardState.IsKeyDown(Keys.Enter)) return "Jump";

//            else return "None";
//        }
//    }
//}