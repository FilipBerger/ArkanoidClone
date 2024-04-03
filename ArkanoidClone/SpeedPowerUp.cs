using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public class SpeedPowerUp : PowerUps
    {
        public SpeedPowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed)
            : base(texture, position, speed, boundingBox, newSpeed)
        {
        }

        public override void ApplyEffect(PlayerBar playerBar)
        {
            playerBar.Speed = newSpeed;
            speedPowerUpTimer = 15;
            Console.WriteLine("Speed power-up applied to player bar!");
        }
    }
}