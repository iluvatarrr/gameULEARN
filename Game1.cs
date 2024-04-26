using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;
using rpgame2.Controller;
using rpgame2.Model;
using rpgame2.View;
using System.Collections.Generic;

namespace Game1
{

    public class Game1 : Game
    {
        public int ScreenWidth = 1280;
        public int ScreenHeight = 720;
        private Player player;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameState gameState;
        private Menu mainBackground;
        private Firstevel firstevel;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            mainBackground = new Menu(Content.Load<Texture2D>("Battleground3"), Content.Load<SpriteFont>("mainFont"));
            firstevel = new Firstevel(Content.Load<Texture2D>("firstLevelBackground"));
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

            };
            player = new Player(animationDictionary);
        }

        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime, gameState);

            gameState.Update();
            mainBackground.Update(gameState);
            firstevel.Update(gameState);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            mainBackground.Draw(spriteBatch);
            firstevel.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}