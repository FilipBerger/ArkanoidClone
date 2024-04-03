using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public abstract class PowerUps : Entity
    {
        protected float newSpeed;
        protected bool isActive = true;
        protected float speedPowerUpTimer;
        protected float sizePowerUpTimer;

        protected PowerUps(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed)
            : base(texture, position, speed, boundingBox)
        {
            this.newSpeed = newSpeed;
            speedPowerUpTimer = 0f;
        }

        public abstract void ApplyEffect(PlayerBar playerBar);

        public void Update(GameTime gameTime, Entity entity, PlayerBar playerBar)
        {
            if (isActive)
            {
                Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
                BoundingBox = new Rectangle((int)Position.X,
                        (int)Position.Y,
                        BoundingBox.Width,
                        BoundingBox.Height);

                if (BoundingBox.Intersects(entity.BoundingBox))
                {
                    isActive = false;
                    ApplyEffect(playerBar);
                }
            }

            if (speedPowerUpTimer > 0)
                speedPowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (sizePowerUpTimer > 0)
                sizePowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (speedPowerUpTimer <= 0)
            {
                playerBar.Speed = playerBar.InitialSpeed;
                speedPowerUpTimer = 0;
            }

            if (sizePowerUpTimer <= 0)
            {
                playerBar.ApplySizePowerUpWithDuration(1f, 0);
                sizePowerUpTimer = 0;
            }
        }
    }
}
