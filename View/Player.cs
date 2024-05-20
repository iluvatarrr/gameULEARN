using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Controller;
using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace rpgame2.View
{
    public class Player
    {
        protected Texture2D texture;
        public Texture2D TextureHealth;
        public Texture2D HealthBarTexture;
        PlayerController PlayerController;

        public Player(Texture2D textureHealth, Texture2D healthBarTexture, PlayerController playerController)
        {
            TextureHealth = textureHealth;
            PlayerController = playerController;
            HealthBarTexture = healthBarTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(HealthBarTexture, new Vector2(5, 5), Color.White);
                spriteBatch.Draw(TextureHealth, new Rectangle(95, 40, PlayerController.PlayerModel.Health * 2, 28), Color.White);
                PlayerController.AnimationView.Draw(spriteBatch);
        }
    }
}
