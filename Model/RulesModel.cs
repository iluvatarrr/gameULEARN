using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class RulesModel
    {
        public List<Button> Buttons;
        public ButtonController ButtonController { get; set; }
        public Color color { get; private set; }
        public string rulesText { get; private set; }
        public string headerText { get; private set; }

        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public RulesModel() 
        {
            Buttons = new List<Button>();
            color = Color.White;
            headerText = "Rules";
            rulesText = "Вы попадаете на рудник, где обитают орки.\nВаща цель набрать большее количество\nочков и активировать портал.\n" +
                "Управление:\nвлево/вправо - A/D, прыжок - Spase\nСильный удар - E, обычный - F, слабый - R";
            headertPosition = new Vector2(20, -10);
            rulesPosition = new Vector2(20, 220);
        }
    }
}
