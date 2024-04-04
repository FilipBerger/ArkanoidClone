using ArkanoidClone.PowerUps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public static class SpeedPowerUpSpawner
    {
        public static void SpawnSpeedPowerUp(Game1 game, Vector2 position)
        {
            if (game != null)
            {
                game.speedPowerUp = new SpeedPowerUp(game.Content.Load<Texture2D>("ball"),
                    position,
                    200f,
                    new Rectangle(0, 0, 30, 30),
                    1000f,
                    15f
                );
            }
            else
            {
                throw new ArgumentNullException(nameof(game));
            }
        }
    }
}
