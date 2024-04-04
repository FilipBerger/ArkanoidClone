using ArkanoidClone.PowerUps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public static class SizePowerUpSpawner
    {
        public static void SpawnSizePowerUp(Game1 game, Vector2 position)
        {
            if (game != null)
            {
                game.sizePowerUp = new SizePowerUp(game.Content.Load<Texture2D>("ball"),
                                                    position,
                                                    200f,
                                                    new Rectangle(0, 0, 30, 30),
                                                    new Vector2(200, 40),
                                                    15f);
            }
            else
            {
                throw new ArgumentNullException(nameof(game));
            }
        }
    }
}
