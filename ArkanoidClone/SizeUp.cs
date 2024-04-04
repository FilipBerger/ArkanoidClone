using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class SizeUp : PowerUps
    {
        public SizeUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            BoundingBox = boundingBox;
        }

        public PlayerBar ApplyEffect(PlayerBar playerBar)
        {
            var newPlayerBar = new PlayerBar(playerBar.Texture, playerBar.Position, playerBar.Speed, new Rectangle(
                playerBar.BoundingBox.X,
                playerBar.BoundingBox.Y,
                playerBar.BoundingBox.Width + 50,
                playerBar.BoundingBox.Height));

            return newPlayerBar;
        }

        public PlayerBar Update(GameTime gameTime, PlayerBar playerBar)
        {
            Position += new Vector2(0, Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                return ApplyEffect(playerBar);
            }
            else return playerBar;
        }
    }
}