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
        public GameState currentGameState;
        private Ball ball;
        private AdditionalBall additionalBall;


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
            20)); //Content.Load kommer funka när det finns en image i Content för paddle.

            ball = new Ball(Content.Load<Texture2D>("ball"),
                 new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2),
                 300f,
                 new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2, 30, 30));

            additionalBall = new AdditionalBall(Content.Load<Texture2D>("ball"),
                     new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 100),
                     300f,
                     new Rectangle(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 100, 30, 30));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerBar.Texture = (Content.Load<Texture2D>("49-Breakout-Tiles"));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            playerBar.Update(gameTime);
            ball.Update(gameTime, playerBar);
            additionalBall.Update(gameTime, playerBar);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // TODO: Add your drawing code here


            _spriteBatch.Begin();
            if (additionalBall.isActive)
            {
                _spriteBatch.Draw(additionalBall.Texture, additionalBall.BoundingBox, Color.Blue);
            }
            _spriteBatch.Draw(ball.Texture, ball.BoundingBox, Color.White);
            _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void ApplySpeedPowerUp()
        {
            playerBar.ApplySpeedPowerUpForDuration(15, 15);
        }
    }
}