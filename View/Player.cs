using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public PlayerModel PlayerModel = new PlayerModel();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.Game))
            {
                spriteBatch.Draw(HealthBarTexture, new Vector2(5, 5), Color.White);
                spriteBatch.Draw(TextureHealth, new Rectangle(95, 40, PlayerModel.Health * 2, 28), Color.White);
                if (texture != null) spriteBatch.Draw(texture, PlayerModel.Position, Color.White);
                else if (PlayerModel.controller != null) PlayerModel.controller.Draw(spriteBatch);
                else throw new Exception("Exception warning!!!");
            }
        }

        public Player(Dictionary<string, Animation> currentAnimations, Texture2D textureHealth, Texture2D healthBarTexture)
        {
            PlayerModel.animations = currentAnimations;
            PlayerModel.controller = new AnimationController(PlayerModel.animations.First().Value);
            TextureHealth = textureHealth;
            HealthBarTexture = healthBarTexture;
        }

        public virtual void Update(GameTime gameTime) 
        {
            PlayerModel.ChangeHealth();
            PlayerModel.Update(gameTime);
        }
    }
}
