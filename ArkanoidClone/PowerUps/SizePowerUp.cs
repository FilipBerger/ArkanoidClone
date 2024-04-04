using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone.PowerUps
{
    public class SizePowerUp : PowerUps
    {
        private Vector2 newSize;

        public SizePowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, Vector2 newSize, float sizePowerUpTimer)
            : base(texture, position, speed, boundingBox, 0) // Pass 0 as newSpeed, as it's not relevant for SizePowerUp
        {
            this.newSize = newSize;
            this.sizePowerUpTimer = sizePowerUpTimer;
        }

        public override void ApplyEffect(PlayerBar playerBar, PowerUpManager powerUpManager)
        {
            powerUpManager.ApplySizePowerUpWithDuration(playerBar, newSize, 10);
            sizePowerUpTimer = 10;
        }

        public void Update(GameTime gameTime, PlayerBar playerBar)
        {
            if (!isActive)
                return;

            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                isActive = false;
                ApplyEffect(playerBar, playerBar.PowerUpManager);
            }

            if (sizePowerUpTimer > 0)
                sizePowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
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
