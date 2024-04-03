using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public class SizePowerUp : PowerUps
    {
        private Vector2 newSize;

        public SizePowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, Vector2 newSize)
            : base(texture, position, speed, boundingBox, 0)
        {
            this.newSize = newSize;
        }

        public override void ApplyEffect(PlayerBar playerBar)
        {

            Vector2 newSize = playerBar.Size * new Vector2(2f, 2f);

            playerBar.ApplySizePowerUpWithDuration(2f, 10);
            sizePowerUpTimer = 10;

            Console.WriteLine("Size power-up applied to player bar!");
        }

    }
}