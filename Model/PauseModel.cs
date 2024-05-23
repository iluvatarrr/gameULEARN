using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class PauseModel
    {
        public List<BtnController> ButtonsController;
        public Color color { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public PauseModel()
        {
            ButtonsController = new List<BtnController>();
            color = Color.White;
            headerText = "Pause";
            headertPosition = new Vector2(50, -10);
            rulesPosition = new Vector2(50, 220);
            var ChoiseButton = new BtnController(new ButtonModel(new Vector2(100, 434), "Choise Level"));
            ChoiseButton.Click += ButtonFunction.ChoiseButton;
            var MenuButton = new BtnController(new ButtonModel(new Vector2(100, 534), "Menu"));
            MenuButton.Click += ButtonFunction.MenuButton;
            var BackButton = new BtnController(new ButtonModel(new Vector2(100, 634), "Back"));
            BackButton.Click += ButtonFunction.BackButton;
            ButtonsController.Add(MenuButton);
            ButtonsController.Add(ChoiseButton);
            ButtonsController.Add(BackButton);
        }

        public void Update()
        {
            foreach (var button in ButtonsController)
                button.Update();
        }
    }
}
