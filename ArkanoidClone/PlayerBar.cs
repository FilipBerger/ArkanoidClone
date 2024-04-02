
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


        public void ApplySpeedPowerUpForDuration(int durationSeconds, float speedValue)
        {
            Speed = speedValue;
            speedPowerUpDuration = durationSeconds;
            speedPowerUpTimer = durationSeconds;
        }

        public void ApplySizePowerUpWithDuration(float factor, float durationSeconds)
        {
            sizePowerUpDuration = durationSeconds;
            sizePowerUpTimer = durationSeconds;

            // Calculate the new size based on the factor and ensure it maintains the center position
            Vector2 newSize = size * factor;
            Vector2 newSizeFromCenter = newSize - size;
            position -= newSizeFromCenter / 2;

            // Update the bounding box with the new size and position
            BoundingBox = new Rectangle(
                (int)position.X,
                (int)position.Y,
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

            if (Position.X > 1024 - BoundingBox.Width) 
            {
                Position = new Vector2(1024 - BoundingBox.Width, Position.Y);
            }
            else if (Position.X < 200) 
            {
                Position = new Vector2(200, Position.Y);
            }
        }
    }
}
