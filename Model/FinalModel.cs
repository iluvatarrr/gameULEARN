using rpgame2.Controller;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class FinalModel
    {
        public List<Button> Buttons;
        public ButtonController ButtonController { get; set; }
        public Color color { get; private set; }
        public string headerText { get; private set; }
        public Vector2 headertPosition { get; private set; }
        public Vector2 rulesPosition { get; private set; }
        public string rulesText { get; private set; }

        public FinalModel()
        {
            Buttons = new List<Button>();
            color = Color.White;
            headerText = "Final";
            rulesText = "Поздравляю, вы победили!\nСыграем еще уровень?)";
            headertPosition = new Vector2(50, -10);
            rulesPosition = new Vector2(50, 220);
        }
    }
}
