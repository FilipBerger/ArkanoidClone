using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class ShitShooter : Enemy
    {
        public Texture2D bulletTexture;
        private float bulletSpeed;
        public List<ShitBullet> bullets;
        private float direction = 1;
        private double shootingTimer = 1;
        private double shootingInterval = 2;


        public ShitShooter(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints, Texture2D bulletTexture, float bulletSpeed)
            : base(texture, position, speed, boundingBox, hitpoints)
        {
            this.bulletTexture = bulletTexture;
            this.bulletSpeed = bulletSpeed;
            this.bullets = new List<ShitBullet>();
        }

        //Creates a bullet
        public void Shoot()
        {
            var bulletPosition = new Vector2(Position.X, Position.Y + Texture.Height - 500);
            var bulletBoundingBox = new Rectangle((int)Position.X, (int)Position.Y + Texture.Height, 20, 25);
            var bullet = new ShitBullet(bulletTexture, bulletPosition, bulletSpeed, bulletBoundingBox);
            bullets.Add(bullet);
        }

        public void Update(GameTime gameTime, PlayerBar playerBar, Life life)
        {
            Position += new Vector2(direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);
            if (Position.X > 1024 - BoundingBox.Width)
            {
                Position = new Vector2(1024 - BoundingBox.Width, Position.Y);
                direction = -1; // Changes the direction of the enemy left
            }
            else if (Position.X < 200)
            {
                Position = new Vector2(200, Position.Y);
                direction = 1; // Changes the direction of the enemy right
            }

            //It is shooting bullets every second
            shootingTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (shootingTimer >= shootingInterval)
            {
                Shoot();
                shootingTimer = 0; // Reset the timer
            }


            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var bullet = bullets[i];
                life = bullet.Update(gameTime, playerBar, life);
                if (bullet.IsMarkedForRemoval)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Texture, BoundingBox, Color.White);

            foreach (var bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }


        }

    }
}
