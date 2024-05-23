using rpgame2.Controller;
using rpgame2.View;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame;

namespace rpgame2.Model
{
    public class MenuModel
    {

        public List<BtnController> ButtonsController;
        public Color color { get; private set; }
        public string rulesText { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headerPosition { get; private set; }
        public int timeCounter { get; set; }
        public Vector2 textPosition = new Vector2(100, 220);
        public MenuModel()
        {
            ButtonsController = new List<BtnController>();
            timeCounter = 0;
            headerText = "Рудник";
            headerPosition = new Vector2(100, 220);

            var PlayButton = new BtnController(new ButtonModel(new Vector2(100, 460), "Play"));
            PlayButton.Click += ButtonFunction.ChoiseButton;
            var RulesButton = new BtnController(new ButtonModel(new Vector2(100, 518), "Rules"));
            RulesButton.Click += ButtonFunction.RulesButton;
            var SettingsButton = new BtnController(new ButtonModel(new Vector2(100, 576), "Settings"));
            SettingsButton.Click += ButtonFunction.SettingsButton;
            var ExitButton = new BtnController(new ButtonModel(new Vector2(100, 634), "Exit"));
            ExitButton.Click += ButtonFunction.ExitButton;
            ButtonsController.Add(PlayButton);
            ButtonsController.Add(RulesButton);
            ButtonsController.Add(SettingsButton);
            ButtonsController.Add(ExitButton);
        }
        public void Update()
        {
            if (timeCounter <= 255)
            {
                color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
                timeCounter += 4;
            }
            else color = Color.White;
            foreach (var button in ButtonsController)
                button.Update();
        }
    }
}
