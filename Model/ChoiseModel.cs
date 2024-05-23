using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame;

namespace rpgame2.Model
{
    public class ChoiseModel
    {

        public List<BtnController> ButtonsController;
        public ButtonFunction ButtonController { get; set; }
        public Color color { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headertPosition { get; private set; }
        public ChoiseModel()
        {
            ButtonsController = new List<BtnController>();
            color = Color.White;
            headerText = "Levels";
            headertPosition = new Vector2(50, -10);

            var FirstButton = new BtnController(new ButtonModel(new Vector2(100, 260), "1. First Level"));
            FirstButton.Click += ButtonFunction.FirstLevelButton;
            ButtonsController.Add(FirstButton);

            var SecondButton = new BtnController(new ButtonModel(new Vector2(100, 330), "2. Second Level"));
            SecondButton.Click += ButtonFunction.SecondLevelButton;
            ButtonsController.Add(SecondButton);

            var ThirdButton = new BtnController(new ButtonModel(new Vector2(100, 400), "3. Third Level"));
            ThirdButton.Click += ButtonFunction.ThirdLevelButton;
            ButtonsController.Add(ThirdButton);

            var MenuButton = new BtnController(new ButtonModel(new Vector2(100, 534), "Menu"));
            MenuButton.Click += ButtonFunction.MenuButton;
            ButtonsController.Add(MenuButton);

            var BackButton = new BtnController(new ButtonModel(new Vector2(100, 634), "Back"));
            BackButton.Click += ButtonFunction.BackButton;
            ButtonsController.Add(BackButton);
        }
        public void Update()
        {
            foreach (var button in ButtonsController)
                button.Update();
        }
    }
}
