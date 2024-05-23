using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyGame;

namespace rpgame2.Model
{
    public class RulesModel
    {
        public List<BtnController> ButtonsController;
        public Color color { get; private set; }
        public string rulesText { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public RulesModel()
        {
            ButtonsController = new List<BtnController>();
            color = Color.White;
            headerText = "Rules";
            rulesText = "Вы попадаете на рудник, где обитают орки.\nВаща цель набрать большее количество\nочков и активировать портал." +
                "Управление:\nвлево/вправо - A/D, прыжок - Spase\nСильный удар - E, обычный - F, слабый - R" + 
                "\nОрки любят темные пути. Двигаются по A*";
            headertPosition = new Vector2(20, -10);
            rulesPosition = new Vector2(20, 220);

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
