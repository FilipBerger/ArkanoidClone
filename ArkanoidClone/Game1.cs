using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Net.Mime;
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
        public GameState currentGameState;


        

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
           
           
            for (int i = 0; i < 3; i++)

                for (int j = 0; j < 3; j++)
                {
                    bricks.Add(new Brick(Content.Load<Texture2D>("05-Breakout-Tiles"),
                    new Vector2(612 + j * 45, 50 + i * 15),
                    0f,
                    new Rectangle(612 + j * 45, 50 + i * 15, 45, 15),
                    1));
                }




            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerBar.Texture = Content.Load<Texture2D>("49-Breakout-Tiles");

            Texture2D brickTexture = Content.Load<Texture2D>("05-Breakout-Tiles");

            
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            

            playerBar.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkSlateGray);

           
            _spriteBatch.Begin();

            _spriteBatch.Draw(playerBar.Texture, playerBar.BoundingBox, Color.White);

            
            
            foreach (Brick brick in bricks)
            {
                brick.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
