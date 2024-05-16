using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        private Player Player;
        private List<Orc> OrcList;
        private LevelModel LevelModel;
        public static GameState gameState;
        private Menu Menu;
        private Rules Rules;
        private Pause Pause;
        private Choise Choise;
        private LevelState LevelState;
        private Game Game;
        private Level Level;
        private Final Final;
        private ScoresCounter ScoresConteter;
        Dictionary<string, Animation> animationOrcDictionary;
        Dictionary<string, Animation>  animationDictionary;
        public Dictionary<string, Texture2D> LevelTexture;
        public Dictionary<string, SpriteFont> LevelFont;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        protected override void Initialize()
        {
            Game = this;
            gameState = new GameState(State.SplashScreen);
            LevelState = new LevelState(LevelState.LevelNumber.First);
            if (GameState.CurrentState.Equals(State.SplashScreen) || GameState.CurrentState.Equals(State.Pause)) IsMouseVisible = true;
            else IsMouseVisible = false;
            base.Initialize();
        }

        public void MakeLevelModel(Dictionary<string, SpriteFont> LevelFont, Dictionary<string, Texture2D> LevelTexture)
        {
            Player = new Player(animationDictionary, LevelTexture["healthTexture"], LevelTexture["healthBarTexture"]);
            LevelModel = new LevelModel();
            OrcList = new List<Orc>();
            for (var i = 0; i < LevelModel.PositionOrcList.Count; i++)
            {
                var orc = new Orc(animationOrcDictionary);
                orc.OrcModel.Position = LevelModel.PositionOrcList[i];
                OrcList.Add(orc);
            }
            LevelModel.PlayerModel = Player.PlayerModel;
            LevelModel.OrcList = OrcList;
            ScoresConteter = new ScoresCounter(LevelFont["scoreFont"], Player);
            Level = new Level(LevelTexture["levelBG"], LevelModel, LevelTexture["grassTexture"], LevelTexture["rassTexture"], 
                LevelTexture["grassGrondLeft"], LevelTexture["grassGrondMiddle"], LevelTexture["grassGrondRight"], LevelTexture["crystal"], LevelTexture["finalStone"], LevelTexture["finalStoneOn"], ScoresConteter);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var mainBackground = Content.Load<Texture2D>("GameStateBG\\Battleground3");
            var pauseBackground = Content.Load<Texture2D>("GameStateBG\\PauseBackground");
            var headerFont = Content.Load<SpriteFont>("mainFont");
            var buttonFont = Content.Load<SpriteFont>("Button\\ButtonFont");
            Menu = new Menu(mainBackground, headerFont, buttonFont, Game);
            Rules = new Rules(mainBackground, headerFont, buttonFont);
            Pause = new Pause(pauseBackground, headerFont, buttonFont);
            Choise = new Choise(pauseBackground, headerFont, buttonFont);
            Final = new Final(pauseBackground, headerFont, buttonFont);
            animationDictionary = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Run1"), 6) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Run1"),6) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("PlayerAnimation\\RunLeft1"), 6) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Run1"), 6) },
                { "None", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Idle1"), 6) },
                { "Fight", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Attack1"), 4) },
                { "Fight2", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Attack2"), 4) },
                { "Fight3", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Attack3"), 4) },
                { "death", new Animation(Content.Load<Texture2D>("PlayerAnimation\\Dead1"), 4) },
            };
            animationOrcDictionary = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("OrcAnimation\\Run"), 6) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("OrcAnimation\\Run"),6) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("OrcAnimation\\Run"), 6) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("OrcAnimation\\Run"), 6) },
                { "None", new Animation(Content.Load<Texture2D>("OrcAnimation\\Idle"), 5) },
                { "Fight", new Animation(Content.Load<Texture2D>("OrcAnimation\\Attack_1"), 4) },
                { "death", new Animation(Content.Load<Texture2D>("OrcAnimation\\Dead"), 4) },
            };
            LevelTexture = new Dictionary<string, Texture2D>()
            {
                { "levelBG", Content.Load<Texture2D>("GameStateBG\\firstLevelBackground") },
                { "rassTexture", Content.Load<Texture2D>("LevelTexture\\dirth") },
                { "grassGrondLeft", Content.Load<Texture2D>("LevelTexture\\grassGrondLeft") },
                { "grassGrondMiddle", Content.Load<Texture2D>("LevelTexture\\grassGrondMiddle") },
                { "grassGrondRight", Content.Load<Texture2D>("LevelTexture\\grassGrondRight") },
                { "finalStone", Content.Load<Texture2D>("LevelTexture\\finalStone") },
                { "finalStoneOn", Content.Load<Texture2D>("LevelTexture\\finalStoneOn") },
                { "grassTexture", Content.Load<Texture2D>("LevelTexture\\grass") },
                { "crystal", Content.Load<Texture2D>("LevelTexture\\purpleCrystal") },
                { "healthTexture", Content.Load<Texture2D>(("lava_tile1")) },
                { "healthBarTexture", Content.Load<Texture2D>("HeallthSubBur") },
            };
            LevelFont = new Dictionary<string, SpriteFont> { { "scoreFont", Content.Load<SpriteFont>("ScoresCounterFont\\ScoresConterFont") }, };
            MakeLevelModel(LevelFont, LevelTexture);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update();
            LevelState.Update();
            if (LevelState.IsChange) MakeLevelModel(LevelFont, LevelTexture);
            if (GameState.CurrentState.Equals(State.SplashScreen))
                Menu.Update();
            if (GameState.CurrentState.Equals(State.Rules))
                 Rules.Update();
            if (GameState.CurrentState.Equals(State.Pause))
                Pause.Update();
            if (GameState.CurrentState.Equals(State.ChoiceLevel))
                Choise.Update();
            if (GameState.CurrentState.Equals(State.Final))
                Final.Update();
            if (GameState.CurrentState.Equals(State.Game))
            {
                Player.Update(gameTime);
                foreach (var orc in OrcList)
                    orc.Update(gameTime);
                Level.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (GameState.CurrentState.Equals(State.SplashScreen))
                Menu.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Rules))
                Rules.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Pause))
                Pause.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.ChoiceLevel))
                Choise.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Final))
                Final.Draw(spriteBatch);
            if (GameState.CurrentState.Equals(State.Game))
            {
                Level.Draw(spriteBatch);
                foreach (var orc in OrcList)
                    orc.Draw(spriteBatch);
                Player.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}