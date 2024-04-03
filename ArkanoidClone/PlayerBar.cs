using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {
        private Texture2D texture;
        private Vector2 position;
        private float initialSpeed;
        private Wall wallLeft;
        private Wall wallRight;

        public float InitialSpeed
        {
            get { return initialSpeed; }
        }

        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, Wall wallLeft, Wall wallRight) : base(texture, position, speed, boundingBox)
        {
            this.texture = texture;
            this.position = position;
            this.initialSpeed = speed;
            this.BoundingBox = boundingBox;
            this.wallLeft = wallLeft;
            this.wallRight = wallRight;
            PowerUpManager = new PowerUpManager(new Vector2(boundingBox.Width, boundingBox.Height));
        }

        public PowerUpManager PowerUpManager { get; private set; }

        public void Update(GameTime gameTime)
        {
            var keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

            if (Position.X + PowerUpManager.CurrentSize.X / 2 > wallRight.Position.X)
            {
                Position = new Vector2(wallRight.Position.X - PowerUpManager.CurrentSize.X / 2, Position.Y);
            }
            if (Position.X - PowerUpManager.CurrentSize.X / 2 < wallLeft.Position.X + wallLeft.BoundingBox.Width)
            {
                Position = new Vector2(wallLeft.Position.X + wallLeft.BoundingBox.Width + PowerUpManager.CurrentSize.X / 2, Position.Y);
            }

            BoundingBox = new Rectangle((int)(Position.X - PowerUpManager.CurrentSize.X / 2), (int)(Position.Y - PowerUpManager.CurrentSize.Y / 2), (int)PowerUpManager.CurrentSize.X, (int)PowerUpManager.CurrentSize.Y);

            PowerUpManager.Update(gameTime, this);
        }
    }
}
