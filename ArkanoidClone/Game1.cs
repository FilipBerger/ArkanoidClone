using ArkanoidClone.Powerups;
using ArkanoidClone.ScoreStuff;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;



namespace ArkanoidClone
{
    public class Game1 : Game
    {
        private PlayerBar playerBar;
        private List<Brick> bricks = new List<Brick>();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Ball ball;
        private Wall[] walls;
        private SpriteFont menuFont;
        private MainMenuScreen mainMenuScreen;
        private HighScoreScreen highScoreScreen;
        private BrickManager brickManager;
        private CreateHighScoreScreen createHighScoreScreen;
        private NextStageScreen nextStageScreen;
        private ScoreManager scoreManager;
        private Life life;
        private Vector2 originalBallPosition;
        private GameState currentGameState = GameState.MainMenu;
        private GameState gameStateBeforePaus;
        private KeyboardState previousKeyboardState;
        private bool stageWasJustSetUp = true;
        private KamikazeManager kamikazeManager;

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

            ball = new Ball(
            Content.Load<Texture2D>("ball"),
            new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
            new Vector2(0, 300), // Bollens hastighet: X = 0 (ingen horisontell rörelse), Y = 300 (vertikal rörelse nedåt)
            new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 20, 20)); // Bollens storlek och startposition

            brickManager = new BrickManager(Content.Load<Texture2D>("05-Breakout-Tiles"),
                Content.Load<Texture2D>("01-Breakout-Tiles"),
                Content.Load<Texture2D>("mario_mushroom"),
                Content.Load<Texture2D>("life_up"),
                Content.Load<Texture2D>("ufo"),
                Content.Load<Texture2D>("poop"));

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

            kamikazeManager = new KamikazeManager(Content.Load<Texture2D>("plane"), 100);

            life = new Life();
            originalBallPosition = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2); // Spara den ursprungliga positionen för bollen

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D brickTexture = Content.Load<Texture2D>("05-Breakout-Tiles");
            playerBar.Texture = (Content.Load<Texture2D>("49-Breakout-Tiles"));
            menuFont = Content.Load<SpriteFont>("MenuFont");
            mainMenuScreen = new MainMenuScreen(menuFont);
            highScoreScreen = new HighScoreScreen(menuFont);
            nextStageScreen = new NextStageScreen(menuFont);
            createHighScoreScreen = new CreateHighScoreScreen(menuFont);

            //Musik
            Song backgroundMusic = Content.Load<Song>("Metal-Man-Stage");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;
            MediaPlayer.Play(backgroundMusic);

        }


        private void UpdatePlayingLoop(GameTime gameTime)
        {
            List<Entity> allEntities = new List<Entity>
                    {
                        playerBar,
                        walls[2],
                        walls[0],
                        walls[1]
                    };
            foreach (Brick brick in bricks)
            {
                allEntities.Add(brick);
            }
            playerBar.Update(gameTime);
            bricks = brickManager.Update();
            kamikazeManager.Update(gameTime, playerBar, life);
            foreach (ShitShooter shitShooter in brickManager.ShitShooters)
            {
                shitShooter.Update(gameTime, playerBar, life);
            }
            life = ball.Update(gameTime, allEntities, playerBar, life, originalBallPosition, scoreManager);
            brickManager = ball.DetectCollisionWithBrickOrShitShooter(brickManager);
            playerBar = brickManager.UpdateSizeUps(playerBar, gameTime);
            life = brickManager.UpdateLifeUps(playerBar, gameTime, life);
            currentGameState = brickManager.UpdateStageProgress(currentGameState);
            currentGameState = life.Update(currentGameState);
        }

        private void PauseAtReset()
        {
            if (stageWasJustSetUp)
            {
                stageWasJustSetUp = false;
                gameStateBeforePaus = currentGameState;
                currentGameState = GameState.Paused;
            }
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

                case GameState.SetUpStage1:
                    ResetObjects();
                    scoreManager = scoreManager = new ScoreManager(brickHitPoints: 50, enemyHitPoints: 100);
                    brickManager.SetupStage1();
                    playerBar = new PlayerBar(Content.Load<Texture2D>("49-Breakout-Tiles"),
                new Vector2(GraphicsDevice.Viewport.Width / 2, 600),
                500,
                new Rectangle(GraphicsDevice.Viewport.Width / 2,
                600,
                100,
                20));
                    currentGameState = GameState.PlayingStage1;
                    break;

                case GameState.PlayingStage1:
                    UpdatePlayingLoop(gameTime);
                    PauseAtReset();
                    break;

                case GameState.SetUpStage2:
                    ResetObjects();
                    brickManager.SetupStage2();
                    currentGameState = nextStageScreen.Update(currentKeyboardState, previousKeyboardState);
                    break;

                case GameState.PlayingStage2:
                    UpdatePlayingLoop(gameTime);
                    PauseAtReset();
                    break;

                case GameState.Paused:
                    playerBar.Update(gameTime);
                    currentGameState = UpdatePausedLoop(currentKeyboardState, previousKeyboardState, gameStateBeforePaus);
                    break;

                case GameState.ViewingHighScores:
                    currentGameState = highScoreScreen.Update(currentKeyboardState, previousKeyboardState);
                    break;

                case GameState.CreatingHighScore:
                    currentGameState = createHighScoreScreen.Update(currentKeyboardState, previousKeyboardState, scoreManager.GetScore());
                    break;

                case GameState.Exiting:
                    Environment.Exit(0);
                    break;

                case GameState.GameOver:
                    // Här lägger vi logik för GameOverScreen när den klassen är klar.
                    // Ta bort detta case bara om vi inte ska ha gameover screen. Inget måste.
                    break;
            }

            previousKeyboardState = currentKeyboardState;


            base.Update(gameTime);
        }

        private void ResetObjects()
        {
            stageWasJustSetUp = true;
            life = new Life();
            ball.ResetPosition(originalBallPosition);
        }

        public GameState UpdatePausedLoop(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState, GameState gameStateBeforePaus)
        {
            if (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                return gameStateBeforePaus;
            else return GameState.Paused;
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
                case GameState.PlayingStage1:
                case GameState.PlayingStage2:
                case GameState.Paused:
                    if (currentGameState == GameState.Paused)
                        ShowPressEnterToRelease(_spriteBatch, menuFont);

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

                    kamikazeManager.Draw(_spriteBatch);

                    //Draw score
                    scoreManager.Draw(_spriteBatch, menuFont);

                    // Draw remaining lives
                    Vector2 lifeTextPosition = new Vector2(20, 50);
                    _spriteBatch.DrawString(menuFont, $"Lives: {life.RemainingLives}", lifeTextPosition, Color.White);

                    if (brickManager.SizeUps != null)
                    {
                        foreach (SizeUp sizeUp in brickManager.SizeUps)
                        {
                            sizeUp.Draw(_spriteBatch);
                        }
                    }

                    if (brickManager.LifeUps != null)
                    {
                        foreach (LifeUp lifeUp in brickManager.LifeUps)
                        {
                            lifeUp.Draw(_spriteBatch);
                        }
                    }

                    if (brickManager.ShitShooters != null)
                    {
                        foreach (ShitShooter shitShooter in brickManager.ShitShooters)
                        {
                            shitShooter.Draw(_spriteBatch);
                        }
                    }

                    break;
                case GameState.SetUpStage2:
                    nextStageScreen.Draw(_spriteBatch);
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

        private void ShowPressEnterToRelease(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Press 'Enter' to release the ball!", new Vector2((GraphicsDevice.Viewport.Width / 2) - 123, (GraphicsDevice.Viewport.Height / 2) - 100), Color.Yellow);
        }
    }
}
