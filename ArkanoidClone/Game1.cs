﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Mime;



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
        private Wall[] walls;
        private SpriteFont menuFont;
        private MainMenuScreen mainMenuScreen;
        private ShitShooter shitShooter;
        private Texture2D bulletTexture;
        private HighScoreScreen highScoreScreen;
        private CreateHighScoreScreen createHighScoreScreen;
        private ScoreManager scoreManager;
        private int initialLives = 3;
        private Life life;
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

            playerBar = new PlayerBar(Content.Load<Texture2D>("49-Breakout-Tiles"),
                new Vector2(GraphicsDevice.Viewport.Width / 2, 600),
                500, 
                new Rectangle(GraphicsDevice.Viewport.Width / 2,
                600,
                100,
                20));

            bulletTexture = Content.Load<Texture2D>("poop");

            shitShooter = new ShitShooter(
        Content.Load<Texture2D>("ufo"), // should be the enemy
        new Vector2(GraphicsDevice.Viewport.Width / 2, 200), // the position
        200, // the speed
        new Rectangle(GraphicsDevice.Viewport.Width / 2, 200, 30, 20), // should be the bounding box
        1, // The hitpoints
        bulletTexture, // The bullet texture
        100 // The bullet speed
        );

            ball = new Ball(
            Content.Load<Texture2D>("ball"),
            new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
            new Vector2(0, 300), // Bollens hastighet: X = 0 (ingen horisontell rörelse), Y = 300 (vertikal rörelse nedåt)
            new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 20, 20)); // Bollens storlek och startposition

           
           // Spawn bricklayout

            for (int i = 0; i < 15; i++)

                for (int j = 0; j < 17; j++)
                {
                    bricks.Add(new Brick(Content.Load<Texture2D>("05-Breakout-Tiles"),
                    new Vector2(230 + j * 45, 50 + i * 15),
                    0f,
                    new Rectangle(230 + j * 45, 50 + i * 15, 45, 15),
                    1));
                }

            //variables to make sure the width of top bar is the same as the side walls.
            int horizontalSpacing = 140;
            int topWallWidth = GraphicsDevice.Viewport.Width - 2 * horizontalSpacing;
            
            // Initialize walls
            //Inside every wall you can change the position for format and Rectangle for bounding box
            walls = new Wall[]
            {
                
                
                //Left wall
                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(140, 0), // Position
                    new Rectangle(140, 0, 50, GraphicsDevice.Viewport.Height)), // Bounding box

                //Right wall
                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(GraphicsDevice.Viewport.Width - 190, 0), // Position
                    new Rectangle(GraphicsDevice.Viewport.Width - 190, 0, 50, GraphicsDevice.Viewport.Height)), // Bounding box

                //Top wall
                new Wall(Content.Load<Texture2D>("Wall-texture"),
                    new Vector2(horizontalSpacing, 0), // Position
                    new Rectangle(horizontalSpacing, 0, topWallWidth, 50)) // Bounding box
            };

            //vad man får för poäng vid träff
            scoreManager = new ScoreManager(brickHitPoints: 50, enemyHitPoints: 100);

            life = new Life(initialLives);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D brickTexture = Content.Load<Texture2D>("05-Breakout-Tiles");
            playerBar.Texture = (Content.Load<Texture2D>("49-Breakout-Tiles"));
            bulletTexture = Content.Load<Texture2D>("poop");
            shitShooter.Texture = Content.Load<Texture2D>("ufo");
            menuFont = Content.Load<SpriteFont>("MenuFont");
            mainMenuScreen = new MainMenuScreen(menuFont);
            highScoreScreen = new HighScoreScreen(menuFont);
            createHighScoreScreen = new CreateHighScoreScreen(menuFont);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                
            KeyboardState currentKeyboardState = Keyboard.GetState();

            switch (currentGameState)
            {
                case GameState.MainMenu:
                    GameState newState = mainMenuScreen.Update(currentKeyboardState, previousKeyboardState);
                    if (newState != GameState.MainMenu)
                    {
                        currentGameState = newState;
                    }
                    break;
                case GameState.Playing:
                    // Här lägger vi all spellogik
                    List<Entity> allEntities = new List<Entity>();
                    allEntities.Add(playerBar);
                    allEntities.Add(walls[2]);
                    allEntities.Add(walls[0]);
                    allEntities.Add(walls[1]);
                    foreach(Brick brick in bricks)
                    {
                        allEntities.Add(brick);
                    }
                    playerBar.Update(gameTime);
                    shitShooter.Update(gameTime, playerBar);
                    ball.Update(gameTime, allEntities);

                    break;
                case GameState.ViewingHighScores:
                    currentGameState = highScoreScreen.Update(currentKeyboardState, previousKeyboardState);
                    break;
                case GameState.CreatingHighScore:
                    currentGameState = createHighScoreScreen.Update(currentKeyboardState, previousKeyboardState);
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

                    foreach (Brick brick in bricks)
                     {
                        brick.Draw(_spriteBatch);
                      }

                    ball.Draw(_spriteBatch);
                    _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);

                    shitShooter.Draw(_spriteBatch);//Detta är enemy

                    //draw score
                    scoreManager.Draw(_spriteBatch, menuFont);

                    // Draw remaining lives
                    Vector2 lifeTextPosition = new Vector2(20, 50);
                    _spriteBatch.DrawString(menuFont, $"Lives: {life.RemainingLives}", lifeTextPosition, Color.White);

                    break;
                case GameState.ViewingHighScores:
                    highScoreScreen.Draw(_spriteBatch);
                    break;
                case GameState.CreatingHighScore:
                    createHighScoreScreen.Draw(_spriteBatch);
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
