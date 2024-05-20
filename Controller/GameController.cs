using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyGame;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;
using System.Numerics;

namespace rpgame2.Controller
{
    public class GameController
    {
        public Game1 Game;

        public GameController(Game1 game1)
        {
            Game = game1;
        }
        public void UpdateGame(GameTime gameTime)
        {
            Game.GameStateController.Update();
            Game.LevelStateController.Update();
            Game.MusicValue.Update();
            if (LevelState.IsChange) MakeNewLevel(Game.LevelFont, Game.LevelTexture);
            if (GameState.CurrentState.Equals(State.SplashScreen))
                Game.Menu.MenuModel.Update();
            if (GameState.CurrentState.Equals(State.Pause))
                Game.Pause.PauseModel.Update();
            if (GameState.CurrentState.Equals(State.Rules))
                Game.Rules.RulesModel.Update();
            if (GameState.CurrentState.Equals(State.ChoiceLevel))
                Game.Choise.ChoiseModel.Update();
            if (GameState.CurrentState.Equals(State.Settings))
                Game.Setting.SettingModel.Update();
            if (GameState.CurrentState.Equals(State.Final))
                Game.Final.FinalModel.Update();
            if (GameState.CurrentState.Equals(State.Game))
            {
                Game.MusicValue.ChangeSong(Game.GameMusic);
                Game.PlayerController.Update(gameTime);
                Game.Level.LevelModel.Update();
                foreach (var orcController in Game.OrcController)
                    if (!orcController.OrcModel.mayDelite)
                        orcController.Update(gameTime);
            } else Game.MusicValue.ChangeSong(Game.MenuMusic);
        }

        public void MakeNewLevel(Dictionary<string, SpriteFont> LevelFont, Dictionary<string, Texture2D> LevelTexture)
        {
            Game.Level = new Level(LevelTexture);
            Game.Level.ScoresConteter = new ScoresCounter(LevelFont["scoreFont"]);
            Game.Level.LevelModel = new LevelModel();
            Game.Level.LevelModel.PlayerModel = new PlayerModel(Game.animationDictionary);
            Game.PlayerController = new PlayerController(Game.Level.LevelModel.PlayerModel, Game.animationDictionary);
            Game.OrcController = new List<OrcController>();
            Game.Level.Player = new Player(LevelTexture["healthTexture"], LevelTexture["healthBarTexture"], Game.PlayerController);
            Game.Level.OrcList = new List<Orc>();
            Game.Level.LevelModel.OrcModelList = new List<OrcModel>();
            for (var i = 0; i < LevelModel.PositionOrcList.Count; i++)
            {
                var orcModel = new OrcModel();
                var orcController = new OrcController(orcModel, Game.animationOrcDictionary);
                var orc = new Orc(orcController);
                orcModel.Position = LevelModel.PositionOrcList[i];
                Game.Level.LevelModel.OrcModelList.Add(orcModel);
                Game.Level.OrcList.Add(orc);
                Game.OrcController.Add(orcController);
            }
            Game.Level.LevelModel.PlayerModel.Position = PlayerModel.PlayerStartPosition;
        }
    }
}
