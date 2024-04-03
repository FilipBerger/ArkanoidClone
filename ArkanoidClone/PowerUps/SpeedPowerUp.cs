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
    }
}
