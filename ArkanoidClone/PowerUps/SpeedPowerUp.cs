using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArkanoidClone.PowerUps;

namespace ArkanoidClone.PowerUps
{
    public class SpeedPowerUp : PowerUps
    {
        public SpeedPowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed, float speedPowerUpTimer)
            : base(texture, position, speed, boundingBox, newSpeed)
        {
            this.speedPowerUpTimer = speedPowerUpTimer;
        }

        public override void ApplyEffect(PlayerBar playerBar, PowerUpManager powerUpManager)
        {
            powerUpManager.ApplySpeedPowerUpForDuration(playerBar, newSpeed, 15);
            speedPowerUpTimer = 15;
        }

        public void Spawn(Vector2 position)
        {
            Position = position;
            isActive = true;
        }

        public void Update(GameTime gameTime, PlayerBar playerBar)
        {
            if (!isActive)
                return;

            // Update the position to move the ball downwards
            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                isActive = false;
                ApplyEffect(playerBar, playerBar.PowerUpManager);
            }

            if (speedPowerUpTimer > 0)
                speedPowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.White);
            }
        }
    }
}
