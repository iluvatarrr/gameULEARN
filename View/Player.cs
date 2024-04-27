using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using rpgame2.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace rpgame2.View
{
    public class Player
    {
        private GameState GameState;
        protected Texture2D texture;
        public PlayerModel PlayerModel = new PlayerModel();
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.Game))
            {
                if (texture != null)
                    spriteBatch.Draw(texture, PlayerModel.Position, Color.White);
                else if (PlayerModel.controller != null)
                    PlayerModel.controller.Draw(spriteBatch);
                else throw new Exception("Exception warning!!!");
            }
        }

        public Player(Dictionary<string, Animation> currentAnimations)
        {
            PlayerModel.animations = currentAnimations;
            PlayerModel.controller = new AnimationController(PlayerModel.animations.First().Value);
        }

        public virtual void Update(GameTime gameTime, GameState gameState) 
        {
            GameState = gameState;
            PlayerModel.Update(gameTime);
        }
    }
}
