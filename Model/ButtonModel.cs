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
        public bool CanUse;
        public int timeCounter { get; private set; }
        public Color PenColor { get; private set; }
        public bool Clicked { get; private set; }
        public event EventHandler Click;
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public string Text { get; set; }
        public ButtonModel()
        {
            PenColor = Color.White;
            timeCounter = 0;

        }
        public void Update()
        {
            if (timeCounter <= 255)
            {
                PenColor = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
                timeCounter += 4;
            }
            else
            {
                PenColor = (IsHovering) ? Color.Gold : Color.White;
                PreviousMouse = CurrentMouse;
                CurrentMouse = Mouse.GetState();
                var mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);
                IsHovering = false;
                if (mouseRectangle.Intersects(Rectangle))
                {
                    IsHovering = true;
                    if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                        Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
