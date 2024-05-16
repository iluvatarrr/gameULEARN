using rpgame2.Controller;
using rpgame2.View;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class MenuModel
    {

        public List<Button> Buttons;
        public ButtonController ButtonController { get; set; }
        public Color color { get; private set; }
        public string rulesText { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headerPosition { get; private set; }
        public int timeCounter { get; set; }
        public Vector2 textPosition = new Vector2(100, 220);
        public MenuModel()
        {
            Buttons = new List<Button>();
            timeCounter = 0;
            headerText = "Рудник";
            headerPosition = new Vector2(100, 220);
        }
        public void Update()
        {
            if (timeCounter <= 255)
            {
                color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
                timeCounter += 4;
            }
            else color = Color.White;
            foreach (var button in Buttons)
                button.Update();
        }
    }
}
