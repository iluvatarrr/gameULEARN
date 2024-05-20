using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using rpgame2.Model;
using System;
using rpgame2.View;

namespace rpgame2
{
    public class Level
    {
        public GameState GameState { get; private set; }
        public LevelModel LevelModel { get; set; }
        public Texture2D BackgroundImage { get; private set; }
        public Texture2D Crystal { get; private set; }
        public ScoresCounter ScoresConteter { get; set; }

        private Dictionary<string, Texture2D> LevelTexture;
        public Player Player;
        public List<Orc> OrcList;
        public Level(Dictionary<string, Texture2D> levelTexture)
        {
            LevelTexture = levelTexture;
            BackgroundImage = LevelTexture["levelBG"];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundImage, Vector2.Zero, Color.White);
            Texture2D currentTexture;
            for (int x = 0; x < LevelModel.MapInform.CurrentMap.GetLength(0); x++)
                for (int y = 0; y < LevelModel.MapInform.CurrentMap.GetLength(1); y++)
                {
                    if (LevelModel.MapInform.CurrentMap[x, y] == 0 || LevelModel.MapInform.CurrentMap[x, y] == 8 || LevelModel.MapInform.CurrentMap[x, y] == 11) continue;
                    var posX = y * LevelModel.sizeOfElement;
                    var posY = x * LevelModel.sizeOfElement;
                    if (LevelModel.MapInform.CurrentMap[x, y] == 1) currentTexture = LevelTexture["grass1"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 2) currentTexture = LevelTexture["grass2"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 3) currentTexture = LevelTexture["grass3"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 4) currentTexture = LevelTexture["grass4"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 5) currentTexture = LevelTexture["crystal"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 6) currentTexture = LevelTexture["finalStone"];
                    else if (LevelModel.MapInform.CurrentMap[x, y] == 7) currentTexture = LevelTexture["finalStoneOn"];
                    else currentTexture = LevelTexture["rassTexture"];
                    spriteBatch.Draw(currentTexture, new Vector2(posX, posY), Color.White);
                }
            spriteBatch.DrawString(ScoresConteter.Font, "Score  :" + LevelModel.PlayerModel.Gems.ToString(), ScoresConteter.Position, Color.Gold);
            foreach (var orc in OrcList)
                orc.Draw(spriteBatch);
            Player.Draw(spriteBatch);
        }
    }
}
