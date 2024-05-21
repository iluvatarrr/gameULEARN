using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using rpgame2;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;
using rpgame2.Controller;

namespace MyGame
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameController GameController { get; private set; }
        public LevelStateController LevelStateController { get; private set; }
        public static List<BtnController> ButtonController { get; private set; }
        public PlayerController PlayerController { get; set; }
        public List<OrcController> OrcController { get; set; }
        public DrawGame DrawGame { get; private set; }

        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;
        public GameStateController GameStateController;
        public Menu Menu;
        public Rules Rules;
        public Pause Pause;
        public Choise Choise;
        public Level Level;
        public Setting Setting;
        public Final Final;
        public Game Game;
        public MusicValue MusicValue;
        public Dictionary<string, Animation> animationOrcDictionary;
        public Dictionary<string, Animation>  animationDictionary;
        public Dictionary<string, Texture2D> LevelTexture;
        public Dictionary<string, SpriteFont> LevelFont;
        public Song MenuMusic;
        public Song GameMusic;
        public static GameState ControllerState;

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
            GameController = new GameController(this);
            ControllerState = new GameState(State.SplashScreen);
            GameStateController = new GameStateController();
            LevelStateController = new LevelStateController();
            ButtonController = new List<BtnController>();
            DrawGame = new DrawGame(this);
            if (GameState.CurrentState.Equals(State.SplashScreen) || GameState.CurrentState.Equals(State.Pause)) IsMouseVisible = true;
            else IsMouseVisible = false;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var mainBackground = Content.Load<Texture2D>("GameStateBG\\Battleground3");
            var pauseBackground = Content.Load<Texture2D>("GameStateBG\\PauseBackground");
            var headerFont = Content.Load<SpriteFont>("mainFont");
            var buttonFont = Content.Load<SpriteFont>("Button\\ButtonFont");

            MenuMusic = Content.Load<Song>("Music\\menuSound");
            GameMusic = Content.Load<Song>("Music\\gameSound");
            MusicValue = new MusicValue(buttonFont, MenuMusic);

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
                { "Walk", new Animation(Content.Load<Texture2D>("OrcAnimation\\Run"), 6) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("OrcAnimation\\RunLeft"), 6) },
                { "None", new Animation(Content.Load<Texture2D>("OrcAnimation\\Idle"), 5) },
                { "Fight", new Animation(Content.Load<Texture2D>("OrcAnimation\\Attack_1"), 4) },
                { "death", new Animation(Content.Load<Texture2D>("OrcAnimation\\Dead"), 4) },
            };
            LevelTexture = new Dictionary<string, Texture2D>()
            {
                { "levelBG", Content.Load<Texture2D>("GameStateBG\\firstLevelBackground") },
                { "rassTexture", Content.Load<Texture2D>("LevelTexture\\dirth") },
                { "grass1", Content.Load<Texture2D>("LevelTexture\\grass1") },
                { "grass2", Content.Load<Texture2D>("LevelTexture\\grass2") },
                { "grass3", Content.Load<Texture2D>("LevelTexture\\grass3") },
                { "grass4", Content.Load<Texture2D>("LevelTexture\\grass4") },
                { "finalStone", Content.Load<Texture2D>("LevelTexture\\finalStone") },
                { "finalStoneOn", Content.Load<Texture2D>("LevelTexture\\finalStoneOn") },
                { "crystal", Content.Load<Texture2D>("LevelTexture\\purpleCrystal") },
                { "healthTexture", Content.Load<Texture2D>(("lava_tile1")) },
                { "healthBarTexture", Content.Load<Texture2D>("HeallthSubBur") },
            };
            Menu = new Menu(mainBackground, headerFont, buttonFont, new MenuModel(), Game);
            Rules = new Rules(mainBackground, headerFont, buttonFont, new RulesModel());
            Pause = new Pause(pauseBackground, headerFont, buttonFont, new PauseModel());
            Choise = new Choise(pauseBackground, headerFont, buttonFont, new ChoiseModel());
            Setting = new Setting(mainBackground, headerFont, buttonFont, new SettingModel(), MusicValue);
            Final = new Final(pauseBackground, headerFont, buttonFont, new FinalModel());
            LevelFont = new Dictionary<string, SpriteFont> { { "scoreFont", Content.Load<SpriteFont>("ScoresCounterFont\\ScoresConterFont") }, };
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            GameController.UpdateGame(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            DrawGame.DrawAllGame(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}