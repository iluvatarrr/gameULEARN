using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2.Controller;
using System.Linq;
using rpgame2.Model;

namespace rpgame2.View
{
    public class Orc
    {
        protected Texture2D texture;
        public OrcModel OrcModel = new OrcModel();

        public Orc(Dictionary<string, Animation> currentAnimations)
        {
            OrcModel.animations = currentAnimations;
            OrcModel.controller = new AnimationController(OrcModel.animations.First().Value);
        }

        public void Update(GameTime gameTime)
        {
            OrcModel.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (texture != null) spriteBatch.Draw(texture, OrcModel.Position, Color.White);
            else if (OrcModel.controller != null) OrcModel.controller.Draw(spriteBatch);
            else throw new Exception("Exception warning!!!");
        }
    }
}
