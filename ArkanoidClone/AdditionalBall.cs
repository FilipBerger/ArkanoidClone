using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public class AdditionalBall : Entity
    {
        private bool isActive;
        private float sizePowerUpTimer;
        private float speedPowerUpTimer;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public AdditionalBall(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
            : base(texture, position, speed, boundingBox)
        {
            isActive = true;
            sizePowerUpTimer = 0f;
            speedPowerUpTimer = 0f;
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
                GivePowerUp(playerBar);
                isActive = false;
            }

            if (speedPowerUpTimer > 0)
                speedPowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (sizePowerUpTimer > 0)
                sizePowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (speedPowerUpTimer <= 0)
            {
                playerBar.Speed = playerBar.InitialSpeed;
            }

            if (sizePowerUpTimer <= 0)
            {
                playerBar.ApplySizePowerUpWithDuration(1f, 0);
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
            playerBar.ApplySizePowerUpWithDuration(2f, 10);
            playerBar.ApplySpeedPowerUpForDuration(15, 1000);

            sizePowerUpTimer = 10;
            speedPowerUpTimer = 15;
        }

        public void Spawn(Vector2 position)
        {
            Position = position; 
            isActive = true; 
        }

    }
}
