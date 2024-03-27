using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {
       private Texture2D texture;
        private Vector2 position;
        private float initialSpeed;
        private Rectangle boundingBox;
        private Vector2 size;
        private float sizePowerUpDuration;
        private float sizePowerUpTimer;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base (texture, position, speed, boundingBox) 
        {
            this.texture = texture;
            this.position = position;
            this.initialSpeed = speed;
            this.boundingBox = boundingBox;
            this.size = new Vector2(boundingBox.Width, boundingBox.Height);
        }

        public void ApplySpeedPowerUpForDuration(int durationSeconds, float speedValue)
        {
            Speed = speedValue;
        }
        public void ApplySizePowerUpWithDuration(Vector2 newSize, float durationSeconds)
        {
            sizePowerUpDuration = durationSeconds;
            sizePowerUpTimer = 0f; 
            size = newSize;

            float deltaX = (newSize.X - boundingBox.Width) / 2;
            float deltaY = (newSize.Y - boundingBox.Height) / 2;
            float newX = Position.X - deltaX;
            float newY = Position.Y - deltaY;

            Position = new Vector2(newX, newY);
            BoundingBox = new Rectangle((int)newX, (int)newY, (int)newSize.X, (int)newSize.Y);
        }


        public void Update(GameTime gameTime)
        {
            var keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);
            }

            if (Position.X < boundingBox.Width / 2)
            {
                Position = new Vector2(boundingBox.Width / 2, Position.Y);
            }
            else if (Position.X > 1024 - boundingBox.Width / 2)
            {
                Position = new Vector2(1024 - boundingBox.Width / 2, Position.Y);
            }

            if (sizePowerUpTimer < sizePowerUpDuration)
            {
                sizePowerUpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                size = new Vector2(boundingBox.Width, boundingBox.Height);
                sizePowerUpTimer = 0f;
                BoundingBox = new Rectangle((int)(Position.X - boundingBox.Width / 2), (int)(Position.Y - boundingBox.Height / 2), (int)boundingBox.Width, (int)boundingBox.Height);
            }
        }

    }
}