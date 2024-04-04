using ArkanoidClone.PowerUps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ArkanoidClone
{
    public class PowerUpManager
    {
        private Vector2 originalSize;
        private Vector2 currentSize;
        private float speedPowerUpDuration;
        private float sizePowerUpDuration;
        private float speedPowerUpTimer;
        private float sizePowerUpTimer;
        private ContentManager content;

        public PowerUpManager(Vector2 initialSize, ContentManager content)
        {
            originalSize = initialSize;
            currentSize = initialSize;
            speedPowerUpDuration = 0;
            sizePowerUpDuration = 0;
            speedPowerUpTimer = 0;
            sizePowerUpTimer = 0;
            this.content = content;
        }

        public Vector2 CurrentSize => currentSize;

        public void ApplySpeedPowerUpForDuration(PlayerBar playerBar, float speedValue, float durationSeconds)
        {
            playerBar.Speed = speedValue;
            speedPowerUpDuration = durationSeconds;
            speedPowerUpTimer = durationSeconds;
        }

        public void ApplySizePowerUpWithDuration(PlayerBar playerBar, Vector2 newSize, float durationSeconds)
        {
            originalSize = playerBar.BoundingBox.Size.ToVector2();

            sizePowerUpDuration = durationSeconds;
            sizePowerUpTimer = 0f;

            playerBar.BoundingBox = new Rectangle(
                (int)(playerBar.Position.X - newSize.X / 2),
                (int)(playerBar.Position.Y - newSize.Y / 2),
                (int)newSize.X,
                (int)newSize.Y
            );

            currentSize = newSize;
        }

        public void Update(GameTime gameTime, PlayerBar playerBar)
        {
            if (sizePowerUpTimer < sizePowerUpDuration)
            {
                sizePowerUpTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                playerBar.BoundingBox = new Rectangle(
                    (int)(playerBar.Position.X - originalSize.X / 2),
                    (int)(playerBar.Position.Y - originalSize.Y / 2),
                    (int)originalSize.X,
                    (int)originalSize.Y
                );

                currentSize = originalSize;
                sizePowerUpTimer = 0f;
            }

            if (speedPowerUpTimer > 0)
            {
                speedPowerUpTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (speedPowerUpTimer <= 0)
                {
                    playerBar.Speed = playerBar.InitialSpeed;
                    speedPowerUpTimer = 0;
                }
            }
        }
    }
}
