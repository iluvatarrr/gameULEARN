using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using rpgame2;

namespace Game1
{

    public class Game1 : Game
    {
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 720;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameState gameState;
        private MainPerson mainPerson;
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
            mainPerson = new MainPerson(Content.Load<Texture2D>("mainPerson"));
            mainBackground = new Menu(Content.Load<Texture2D>("Battleground3"), Content.Load<SpriteFont>("mainFont"));
            firstevel = new Firstevel(Content.Load<Texture2D>("firstLevelBackground"));
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            gameState.Update();
            mainBackground.Update(gameState);
            firstevel.Update(gameState);
            mainPerson.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            mainBackground.Draw(spriteBatch);
            firstevel.Draw(spriteBatch);
            mainPerson.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}