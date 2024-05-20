using rpgame2.Controller;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class SettingModel
    {
        public List<BtnController> ButtonsController;
        public Color color { get; private set; }
        public string rulesText { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public SettingModel()
        {
            ButtonsController = new List<BtnController>();
            color = Color.White;
            headerText = "Setting";
            rulesText = "Изменить громкость музыки:";
            headertPosition = new Vector2(20, -10);
            rulesPosition = new Vector2(20, 250);
            var MoreButton = new BtnController(new ButtonModel(new Vector2(800, 250), "+"));
            MoreButton.Click += ButtonController.MoreMusicButton;
            ButtonsController.Add(MoreButton);
            var LessButton = new BtnController(new ButtonModel(new Vector2(950, 250), "-"));
            LessButton.Click += ButtonController.LessMusicButton;
            ButtonsController.Add(LessButton);
            var BackButton = new BtnController(new ButtonModel(new Vector2(100, 634), "Back"));
            BackButton.Click += ButtonController.BackButton;
            ButtonsController.Add(BackButton);
        }

        public void Update()
        {
            foreach (var button in ButtonsController)
                button.Update();
        }
    }
}
