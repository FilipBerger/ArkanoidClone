using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Mime;

namespace ArkanoidClone
{
    public class Game1 : Game
    {
        private PlayerBar playerBar;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ball ball;
        private Wall[] walls;
        //private Wall wallLeft;
        //private Wall wallRight;
        //private Wall wallTop;
        private SpriteFont menuFont;
        private MainMenuScreen mainMenuScreen;

        private GameState currentGameState = GameState.MainMenu;
        private KeyboardState previousKeyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            currentGameState = GameState.MainMenu;
            _graphics.PreferredBackBufferWidth = 1224;
            _graphics.PreferredBackBufferHeight = 720;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

                playerBar = new PlayerBar(Content.Load<Texture2D>("49-Breakout-Tiles"), 
                new Vector2(GraphicsDevice.Viewport.Width / 2, 600),
                500, //500 = speed
                new Rectangle(GraphicsDevice.Viewport.Width / 2,
                600,
                100,
                20));

                ball = new Ball(Content.Load<Texture2D>("ball"),
                     new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                     300f,
                     new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 30, 30));

            // Initialize walls

            walls = new Wall[]
            {
                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(0, 0), // Position
                    new Rectangle(0, 0, 50, GraphicsDevice.Viewport.Height)), // Bounding box

                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(GraphicsDevice.Viewport.Width - 50, 0), // Position
                    new Rectangle(GraphicsDevice.Viewport.Width - 50, 0, 50, GraphicsDevice.Viewport.Height)), // Bounding box

                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(0, 0), // Position
                    new Rectangle(0, 0, GraphicsDevice.Viewport.Width, 50)) // Bounding box
            };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerBar.Texture = (Content.Load<Texture2D>("49-Breakout-Tiles"));
            menuFont = Content.Load<SpriteFont>("MenuFont");
            mainMenuScreen = new MainMenuScreen(menuFont);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState currentKeyboardState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    GameState newState = mainMenuScreen.Update(gameTime, currentKeyboardState, previousKeyboardState);
                    if (newState != GameState.MainMenu)
                    {
                        currentGameState = newState;
                    }
                    break;
                case GameState.Playing:
                    // Här lägger vi all spellogik.
                    playerBar.Update(gameTime);
                    ball.Update(gameTime, playerBar);
                    HandleBallWallCollision();
                    break;
                case GameState.ViewingHighScores:
                    // Här lägger vi logik för HighScores när den klassen är klar.
                    break;
                case GameState.Exiting:
                    // Här lägger vi logik för att avsluta spelet.
                    // Fancy exempel: En ruta som frågar om konfirmation på att avsluta spelet, "Yes" "No".
                    // Lazy exempel: Environment.Exit(0);
                    Environment.Exit(0);
                    break;
                case GameState.GameOver:
                    // Här lägger vi logik för GameOverScreen när den klassen är klar.
                    break;
            }

            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime);

        }

        private void HandleBallWallCollision()
        {
            foreach (var wall in walls)
            {
                wall.HandleCollison(ball);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            _spriteBatch.Begin();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    mainMenuScreen.Draw(_spriteBatch);
                    break;
                case GameState.Playing:
                    //Draw the walls surrounding the game
                    foreach (var wall in walls)
                    {
                        wall.Draw(_spriteBatch);
                    }
                    ball.Draw(_spriteBatch);
                    _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);
                    break;
                case GameState.ViewingHighScores:
                    // Här lägger vi logik för HighScores när den klassen är klar.
                    break;
                case GameState.Exiting:
                    // Här lägger vi logik för att avsluta spelet.
                    // Fancy exempel: En ruta som frågar om konfirmation på att avsluta spelet, "Yes" "No".
                    // Lazy exempel: Environment.Exit(0);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
