using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public class AdditionalBall : Entity
    {
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public AdditionalBall(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
            : base(texture, position, speed, boundingBox)
        {
            isActive = true;
        }

        public void Update(GameTime gameTime, PlayerBar playerBar)
        {
            if (!isActive)
                return;

            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                Speed *= -1;
                GivePowerUp(playerBar); // Give power-up to the player bar
                isActive = false; // Set the ball to be inactive
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.White);
            }
        }

        private void GivePowerUp(PlayerBar playerBar)
        {
            // Apply the power-up to the player bar
            playerBar.ApplySizePowerUpWithDuration(1.5f, 10); // Example: Increase size by 50% for 10 seconds
            playerBar.ApplySpeedPowerUpForDuration(15, 1000); // Example: Increase speed for 15 seconds
        }
    }
}
