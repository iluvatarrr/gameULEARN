using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using System;

namespace rpgame2
{
    public class Firstevel
    {
        public GameState GameState { get; private set; }
        public LevelModel LevelModel { get; private set; }
        public Texture2D BackgroundImage { get; private set; }
        public Texture2D GrassTexture { get; private set; }
        public Texture2D DirthTexture { get; private set; }
        public Texture2D GrassGrondLeft { get; private set; }
        public Texture2D GrassGrondMiddle { get; private set; }
        public Texture2D GrassGrondRight { get; private set; }
        public Texture2D Crystal { get; private set; }
        public ScoresCounter ScoresConteter { get; private set; }
        public Texture2D FinalStone { get; private set; }
        public Texture2D FinalStoneOn { get; private set; }

        public Firstevel(Texture2D backgroundImage, LevelModel levelModel,
            Texture2D grassTextre, Texture2D dirthTextre, Texture2D grassGrondLeft,
            Texture2D grassGrondMiddle, Texture2D grassGrondRight, Texture2D crystal,
            Texture2D finalStone, Texture2D finalStoneOn, ScoresCounter scoresConteter)
        {
            BackgroundImage = backgroundImage;
            LevelModel = levelModel;
            GrassTexture = grassTextre;
            DirthTexture = dirthTextre;
            GrassGrondLeft = grassGrondLeft;
            GrassGrondMiddle = grassGrondMiddle;
            GrassGrondRight = grassGrondRight;
            Crystal = crystal;
            FinalStone = finalStone;
            FinalStoneOn = finalStoneOn;
            ScoresConteter = scoresConteter;
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
                for (int x = 0; x < LevelModel.MapInform.mapMatrixFirstLevel.GetLength(0); x++)
                    for (int y = 0; y < LevelModel.MapInform.mapMatrixFirstLevel.GetLength(1); y++)
                    {
                        if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 0) continue;
                        var posX = y * LevelModel.sizeOfElement;
                        var posY = x * LevelModel.sizeOfElement;
                        if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 1) currentTexture = GrassTexture;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 2) currentTexture = GrassGrondLeft;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 3) currentTexture = GrassGrondMiddle;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 4) currentTexture = GrassGrondRight;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 5) currentTexture = Crystal;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 6) currentTexture = FinalStone;
                        else if (LevelModel.MapInform.mapMatrixFirstLevel[x, y] == 7) currentTexture = FinalStoneOn;
                        else currentTexture = DirthTexture;
                        spriteBatch.Draw(currentTexture, new Vector2(posX, posY), Color.White);
                    }
                spriteBatch.DrawString(ScoresConteter.Font, "Score  :" + LevelModel.PlayerModel.Gems.ToString(), ScoresConteter.Position, Color.Gold);
            }
        }
    }
}
