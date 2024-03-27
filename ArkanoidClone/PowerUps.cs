using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ArkanoidClone
{
    public abstract class PowerUps : Entity
    {
        protected float newSpeed;
        protected bool isActive = true;

        protected PowerUps(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed)
            : base(texture, position, speed, boundingBox)
        {
            this.newSpeed = newSpeed;
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
        }
        public static void TestPowerUpFunctionality(PlayerBar playerBar)
        {
            SpeedPowerUp speedPowerUp = new SpeedPowerUp(null, Vector2.Zero, 0f, Rectangle.Empty, 1000);
            speedPowerUp.ApplyEffect(playerBar);
        }

        public class SpeedPowerUp : PowerUps
        {
            public SpeedPowerUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, float newSpeed)
                : base(texture, position, speed, boundingBox, newSpeed)
            {
            }

            public override void ApplyEffect(PlayerBar playerBar)
            {
                playerBar.Speed = newSpeed;
                Console.WriteLine("Speed power-up applied to player bar!");
            }
        }

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
                playerBar.Size = newSize;
                playerBar.ApplySizePowerUpWithDuration(newSize, 15);
                Console.WriteLine("Size power-up applied to player bar!");
            }
        }
    }
}
