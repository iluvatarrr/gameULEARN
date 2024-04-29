using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using rpgame2.View;
using System;


namespace rpgame2
{
    public class Firstevel
    {
        private GameState GameState;
        public LevelModel LevelModel { get; private set; }
        public Texture2D BackgroundImage { get; private set; }

        public Texture2D GrassTexture;
        public Texture2D DirthTexture;
        public Texture2D GrassGrondLeft;
        public Texture2D GrassGrondMiddle;
        public Texture2D GrassGrondRight;

        public Firstevel(Texture2D backgroundImage, LevelModel levelModel,
            Texture2D grassTextre, Texture2D dirthTextre, Texture2D grassGrondLeft, Texture2D grassGrondMiddle, Texture2D grassGrondRight)
        {
            BackgroundImage = backgroundImage;
            LevelModel = levelModel;
            GrassTexture = grassTextre;
            DirthTexture = dirthTextre;
            GrassGrondLeft = grassGrondLeft;
            GrassGrondMiddle = grassGrondMiddle;
            GrassGrondRight = grassGrondRight;
        }

        public void Update(GameState gameState)
        {
            GameState = gameState;
            LevelModel.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameState.State.Equals(State.Game))
            {

                spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
                Texture2D currentTexture;
                for (int x = 0; x < LevelModel.mapMatrix.GetLength(0); x++)
                    for (int y = 0; y < LevelModel.mapMatrix.GetLength(1); y++)
                    {
                        if (LevelModel.mapMatrix[x, y] == 0) continue;
                        var posX = y * LevelModel.sizeOfElement;
                        var posY = x * LevelModel.sizeOfElement;
                        if (LevelModel.mapMatrix[x, y] == 1) currentTexture = GrassTexture;
                        else if (LevelModel.mapMatrix[x, y] == 2) currentTexture = GrassGrondLeft;
                        else if (LevelModel.mapMatrix[x, y] == 3) currentTexture = GrassGrondMiddle;
                        else if (LevelModel.mapMatrix[x, y] == 4) currentTexture = GrassGrondRight;
                        else currentTexture = DirthTexture;
                        spriteBatch.Draw(currentTexture, new Vector2(posX, posY), Color.White);
                    }
            }
        }
    }
}
