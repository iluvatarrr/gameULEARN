using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;

namespace rpgame2.View
{
    public class Orc
    {
        protected Texture2D texture;
        public OrcController OrcController;

        public Orc(OrcController orcController)
        {
            OrcController = orcController;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            OrcController.AnimationView.Draw(spriteBatch);
        }
    }
}
