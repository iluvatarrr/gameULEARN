using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class FinalModel
    {
        public List<BtnController> ButtonsController;
        public Color color { get; private set; }
        public string headerText { get; private set; }
        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public string rulesText { get; private set; }

        public FinalModel()
        {
            ButtonsController = new List<BtnController>();
            color = Color.White;
            headerText = "Final";
            rulesText = "Поздравляю, вы победили!\nСыграем еще уровень?)";
            headertPosition = new Vector2(50, -10);
            rulesPosition = new Vector2(50, 220);
            var ChoiseButton = new BtnController(new ButtonModel(new Vector2(100, 534), "Choise Level"));
            ChoiseButton.Click += ButtonController.ChoiseButton;
            var MenuButton = new BtnController(new ButtonModel(new Vector2(100, 634), "Menu"));
            MenuButton.Click += ButtonController.MenuButton;
            ButtonsController.Add(ChoiseButton);
            ButtonsController.Add(MenuButton);
        }

        public void Update()
        {
            foreach (var button in ButtonsController)
                button.Update();
        }
    }
}
