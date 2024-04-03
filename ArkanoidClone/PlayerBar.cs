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
        private float speedPowerUpDuration;
        private float sizePowerUpDuration;
        private float speedPowerUpTimer;
        private float sizePowerUpTimer;
        private Wall wallLeft;
        private Wall wallRight;

        public float InitialSpeed
        {
            get { return initialSpeed; }
        }
        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, Wall wallLeft, Wall wallRight) : base(texture, position, speed, boundingBox)
        {
            this.texture = texture;
            this.position = position;
            this.initialSpeed = speed;
            this.boundingBox = boundingBox;
            this.size = new Vector2(boundingBox.Width, boundingBox.Height);
            this.wallLeft = wallLeft;
            this.wallRight = wallRight;
        }

        public void ApplySpeedPowerUpForDuration(float speedValue, float durationSeconds)
        {
            Speed = speedValue;
            speedPowerUpDuration = durationSeconds;
            speedPowerUpTimer = durationSeconds;
        }

        public void ApplySpeedPowerUpForDuration(int durationSeconds, float speedValue)
        {
            Speed = speedValue;
        }

        public void ApplySizePowerUpWithDuration(float factor, float durationSeconds)
        {
            sizePowerUpDuration = durationSeconds;
            sizePowerUpTimer = 0f;

            Vector2 newSize = size * factor;

            BoundingBox = new Rectangle(
                (int)(position.X - newSize.X / 2),
                (int)(position.Y - newSize.Y / 2),
                (int)newSize.X,
                (int)newSize.Y
            );

            size = newSize;
        }
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

            if (Position.X + size.X / 2 > wallRight.Position.X)
            {
                Position = new Vector2(wallRight.Position.X - size.X / 2, Position.Y);
            }
            if (Position.X - size.X / 2 < wallLeft.Position.X + wallLeft.BoundingBox.Width)
            {
                Position = new Vector2(wallLeft.Position.X + wallLeft.BoundingBox.Width + size.X / 2, Position.Y);
            }

            BoundingBox = new Rectangle((int)(Position.X - size.X / 2), (int)(Position.Y - size.Y / 2), (int)size.X, (int)size.Y);

            if (sizePowerUpTimer < sizePowerUpDuration)
            {
                sizePowerUpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                size = new Vector2(boundingBox.Width, boundingBox.Height);
                sizePowerUpTimer = 0f;
            }

            if (speedPowerUpTimer > 0)
            {
                speedPowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (speedPowerUpTimer <= 0)
                {
                    Speed = InitialSpeed;
                    speedPowerUpTimer = 0;
                }
            }
        }


    }
}
