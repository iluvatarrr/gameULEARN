using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rpgame2
{
    public class MainPerson
    {
        private GameState GameState;
        private Texture2D mainPersonImage { get; set; }
        private Mob mob = new Mob();

        public MainPerson(Texture2D texture)
        {
            mainPersonImage = texture;
        }

        public void Update()
        {
            mob.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mainPersonImage, mob.Position, Color.White);
        }
    }
}