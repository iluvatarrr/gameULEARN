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
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Up { get; set; }
        public Keys Fight { get; set; }
        public Keys Fight2 { get; set; }
        public Keys Fight3 { get; set; }
        public Keys Jump { get; set; }
        public Keys None { get; set; }
        public Keys Game { get; set; }
        public Keys Escape { get; set; }

        public KeyboardState GetState() => Keyboard.GetState();
    }
}