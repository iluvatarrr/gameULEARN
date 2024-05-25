using Microsoft.Xna.Framework;

namespace rpgame2.Model
{
    public class ButtonModel
    {

        public bool IsHovering { get; set; }
        public int timeCounter { get; set; }
        public Color PenColor { get; set; }
        public Color PenColour { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle Rectangle { get; set; }
        public string Text { get; set; }
        public ButtonModel(Vector2 position, string text)
        {
            PenColor = Color.White;
            timeCounter = 0;
            Position = position;
            Text = text;
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Text.Length * 40, 48);
        }
    }
}
