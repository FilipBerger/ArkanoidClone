﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArkanoidClone.PowerUps; 

namespace ArkanoidClone.PowerUps
{
    public class SizePowerUp : PowerUps
    {
        private Vector2 newSize;

        public SizePowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed, float sizePowerUpTimer)
            : base(texture, position, speed, boundingBox, newSpeed)
        {
            this.sizePowerUpTimer = sizePowerUpTimer;
        }

        public override void ApplyEffect(PlayerBar playerBar, PowerUpManager powerUpManager)
        {
            powerUpManager.ApplySizePowerUpWithDuration(playerBar, 2f, 10); 
            sizePowerUpTimer = 10;
        }
    }
}
