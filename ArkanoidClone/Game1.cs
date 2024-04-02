using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ArkanoidClone
{
    public class Game1 : Game
    {
        private PlayerBar playerBar;
        private Brick brick;
        private List<Brick> bricks = new List<Brick>();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ball ball;
        private AdditionalBall additionalBall; 
        private Wall[] walls;
        private List<Entity> allEntities = new List<Entity>();
        private Wall wallLeft;
        private Wall wallRight;
        private Wall wallTop;
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
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            wallLeft = Wall.CreateWall(Content.Load<Texture2D>("Wall-texture"), GraphicsDevice, Wall.WallPosition.Left);
            wallRight = Wall.CreateWall(Content.Load<Texture2D>("Wall-texture"), GraphicsDevice, Wall.WallPosition.Right);
            wallTop = Wall.CreateWall(Content.Load<Texture2D>("Wall-texture"), GraphicsDevice, Wall.WallPosition.Top);

            playerBar = new PlayerBar(Content.Load<Texture2D>("49-Breakout-Tiles"),
                new Vector2(GraphicsDevice.Viewport.Width / 2, 600),
                500,
                new Rectangle(GraphicsDevice.Viewport.Width / 2, 600, 100, 20),
                wallLeft,
                wallRight);



            // Initialize bricks
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    bricks.Add(new Brick(Content.Load<Texture2D>("05-Breakout-Tiles"),
                        new Vector2(230 + j * 45, 50 + i * 15),
                        0f,
                        new Rectangle(230 + j * 45, 50 + i * 15, 45, 15),
                        1));
                }
            }

            // Initialize ball
            ball = new Ball(Content.Load<Texture2D>("ball"),
                            new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                            new Vector2(0, 300f), // Velocity
                            new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 30, 30));


            // Initialize additional ball
            additionalBall = new AdditionalBall(Content.Load<Texture2D>("ball"),
                new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 100),
                300f,
                new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 100, 30, 30));

            // Initialize walls
            int horizontalSpacing = 140;
            int topWallWidth = GraphicsDevice.Viewport.Width - 2 * horizontalSpacing;
            walls = new Wall[]
   {
    new Wall(Content.Load<Texture2D>("Wall-texture"),
        new Vector2(140, 0),
        new Rectangle(140, 0, 50, GraphicsDevice.Viewport.Height)),
    new Wall(Content.Load<Texture2D>("Wall-texture"),
        new Vector2(GraphicsDevice.Viewport.Width - 190, 0),
        new Rectangle(GraphicsDevice.Viewport.Width - 190, 0, 50, GraphicsDevice.Viewport.Height)),
    new Wall(Content.Load<Texture2D>("Wall-texture"),
        new Vector2(horizontalSpacing, 0),
        new Rectangle(horizontalSpacing, 0, topWallWidth, 50))
   };


            // Add walls to allEntities list
            allEntities.AddRange(walls);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerBar.Texture = (Content.Load<Texture2D>("49-Breakout-Tiles"));
            menuFont = Content.Load<SpriteFont>("MenuFont");
            mainMenuScreen = new MainMenuScreen(menuFont);
        }

        protected override void Update(GameTime gameTime)
        {
            // Exit the game if the escape key is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Get the current keyboard state
            KeyboardState currentKeyboardState = Keyboard.GetState();

            // Switch between different game states
            switch (currentGameState)
            {
                case GameState.MainMenu:
                    // Update the main menu screen
                    GameState newState = mainMenuScreen.Update(gameTime, currentKeyboardState, previousKeyboardState);
                    if (newState != GameState.MainMenu)
                    {
                        currentGameState = newState;
                    }
                    break;
                case GameState.Playing:
                    // Create a list of all entities that the ball should collide with
                    List<Entity> allEntities = new List<Entity>();
                    allEntities.Add(playerBar);
                    allEntities.AddRange(bricks);
                    allEntities.AddRange(walls);

                    // Update the player bar
                    playerBar.Update(gameTime);

                    // Update the ball
                    ball.Update(gameTime, allEntities);

                    // Update additional ball if active
                    additionalBall.Update(gameTime, playerBar);
                    break;
                case GameState.ViewingHighScores:
                    // Handle logic for viewing high scores
                    break;
                case GameState.Exiting:
                    // Exit the game
                    Environment.Exit(0);
                    break;
                case GameState.GameOver:
                    // Handle game over logic
                    break;
            }

            // Update the previous keyboard state
            previousKeyboardState = currentKeyboardState;

            base.Update(gameTime);
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
                    // Draw walls
                    if (wallLeft != null && wallRight != null && wallTop != null)
                    {
                        foreach (var wall in walls)
                        {
                            wall.Draw(_spriteBatch);
                        }
                    }


                    // Draw bricks
                    foreach (Brick brick in bricks)
                    {
                        brick.Draw(_spriteBatch);
                    }

                    // Draw balls and player bar
                    ball.Draw(_spriteBatch);
                    additionalBall.Draw(_spriteBatch); // Draw additional ball
                    _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);
                    break;
                case GameState.ViewingHighScores:
                    // Draw high scores screen
                    break;
                case GameState.Exiting:
                    // Handle game exit
                    break;
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        // Method to apply speed power-up to the player bar
        private void ApplySpeedPowerUp()
        {
            playerBar.ApplySpeedPowerUpForDuration(15, 15); // Example duration and speed values
        }
    }
}
