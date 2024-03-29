using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Net.Mime;

namespace ArkanoidClone
{
    public class Game1 : Game
    {
        private PlayerBar playerBar;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameState currentGameState;

        private List<Brick> _bricks;
        private BrickSpawner _spawner;

        public object Content { get; }

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
                20)); //Content.Load will work when there is an image in the Content folder for the paddle.

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerBar.Texture = Content.Load<Texture2D>("49-Breakout-Tiles");

            // Load the brick texture
            //Texture2D brickTexture = Content.Load<Texture2D>("brickTexture");

            // Create the brick spawner
            //_spawner = new BrickSpawner(brickTexture, new Vector2(0, 0), 1f, new Rectangle(0, 0, 64, 32), 3);

            // Spawn the bricks
            //_bricks = _spawner.SpawnBricks(10, 5);

            // Create the brick spawner
            _spawner = new BrickSpawner(null, new Vector2(0, 0), 1f, new Rectangle(0, 0, 64, 32), 3);

            // Spawn the bricks with different colors
            _bricks = _spawner.SpawnBricksWithColors(10, 5, new Color[] { Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange });

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            playerBar.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);

            // Draw the bricks
            foreach (var brick in _bricks)
            {
                _spriteBatch.Draw(brick.Texture, brick.Position, brick.Color);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
