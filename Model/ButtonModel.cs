using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;


namespace rpgame2.Model
{
    public class ButtonModel
    {
        public MouseState CurrentMouse;

        public bool IsHovering;

        public MouseState PreviousMouse { get; private set; }
        //public bool CanUse;
        public int timeCounter { get; set; }
        public Color PenColor { get; set; }
        public bool Clicked { get; private set; }

        public event EventHandler Click;
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public string Text { get; set; }
        public ButtonModel(Vector2 position, string text)
        {
            PenColor = Color.White;
            timeCounter = 0;
            Position = position;
            Text = text;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Text.Length * 40, 48);
        }
    }
}
