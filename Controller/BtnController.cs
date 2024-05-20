using rpgame2.Model;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;

namespace rpgame2.Controller
{
    public class BtnController
    {
        public ButtonModel ButtonModel;
        public MouseState CurrentMouse;
        public event EventHandler Click;
        public MouseState PreviousMouse { get; private set; }

        public BtnController(ButtonModel buttonModel)
        {
            ButtonModel = buttonModel;
        }

        public void Update()
        {
            if (ButtonModel.timeCounter <= 255)
            {
                ButtonModel.PenColor = Color.FromNonPremultiplied(255, 255, 255, ButtonModel.timeCounter % 256);
                ButtonModel.timeCounter += 4;
            }
            else
            {
                ButtonModel.PenColor = (ButtonModel.IsHovering) ? Color.Gold : Color.White;
                PreviousMouse = CurrentMouse;
                CurrentMouse = Mouse.GetState();
                var mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);
                ButtonModel.IsHovering = false;
                if (mouseRectangle.Intersects(ButtonModel.Rectangle))
                {
                    ButtonModel.IsHovering = true;
                    if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                        Click?.Invoke(ButtonModel, new EventArgs());
                }
            }
        }
    }
}
