using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;
using rpgame2.Controller;
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
        private LevelModel LevelModel;
        private GameState gameState;
        private Menu MainBackground;
        private Firstevel Firstevel;
        private ScoresCounter ScoresConteter;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
        }

        protected override void Initialize()
        {
            gameState = new GameState(State.SplashScreen);
            base.Initialize();
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            MainBackground = new Menu(Content.Load<Texture2D>("Battleground3"), Content.Load<SpriteFont>("mainFont"));
            var animationDictionary = new Dictionary<string, Animation>()
            {
                { "WalkUp", new Animation(Content.Load<Texture2D>("Run"), 6) },
                { "WalkDown", new Animation(Content.Load<Texture2D>("Run"),6) },
                { "WalkLeft", new Animation(Content.Load<Texture2D>("RunLeft"), 6) },
                { "WalkRight", new Animation(Content.Load<Texture2D>("Run"), 6) },
                { "None", new Animation(Content.Load<Texture2D>("Idle"), 6) },
                { "Fight", new Animation(Content.Load<Texture2D>("Attack_1"), 4) },
                { "Fight2", new Animation(Content.Load<Texture2D>("Attack_2"), 4) },
                { "Fight3", new Animation(Content.Load<Texture2D>("Attack_3"), 4) },
                { "death", new Animation(Content.Load<Texture2D>("Dead"), 4) },
            };
            var crystal = Content.Load<Texture2D>("LevelTexture\\purpleCrystal");
            var healthTexture = Content.Load<Texture2D>("lava_tile1");
            var healthBarTexture = Content.Load<Texture2D>("HeallthSubBur");
            Player = new Player(animationDictionary, healthTexture, healthBarTexture);
            ScoresConteter = new ScoresCounter(Content.Load<SpriteFont>("ScoresCounterFont\\ScoresConterFont"), Player);
            LevelModel = new LevelModel(Player);
            var grassTexture = Content.Load<Texture2D>("LevelTexture\\grass");
            var rassTexture = Content.Load<Texture2D>("LevelTexture\\dirth");
            var grassGrondLeft = Content.Load<Texture2D>("LevelTexture\\grassGrondLeft");
            var grassGrondMiddle = Content.Load<Texture2D>("LevelTexture\\grassGrondMiddle");
            var grassGrondRight = Content.Load<Texture2D>("LevelTexture\\grassGrondRight");
            var finalStone = Content.Load<Texture2D>("LevelTexture\\finalStone");
            var finalStoneOn = Content.Load<Texture2D>("LevelTexture\\finalStoneOn");
            Firstevel = new Firstevel(Content.Load<Texture2D>("firstLevelBackground"), LevelModel,
                grassTexture, rassTexture, grassGrondLeft, grassGrondMiddle, grassGrondRight, crystal,finalStone, finalStoneOn, ScoresConteter);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            gameState.Update();
            MainBackground.Update(gameState);
            Firstevel.Update(gameState);
            Player.Update(gameTime, gameState);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            MainBackground.Draw(spriteBatch);
            Firstevel.Draw(spriteBatch);
            Player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}