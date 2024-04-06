using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArkanoidClone
{
    public class BrickManager
    {
        List<Brick> bricks = new List<Brick>();
        List<SizeUp> sizeUps = new List<SizeUp>();
        List<LifeUp> lifeUps = new List<LifeUp>();
        List<ShitShooter> shitShooters = new List<ShitShooter>();
        private Texture2D sizeUpTexture;
        private Texture2D lifeUpTexture;
        private Texture2D shitShooterTexture;
        private Texture2D shitBulletTexture;

        public BrickManager(Texture2D brickTexture, Texture2D sizeUpTexture, Texture2D lifeUpTexture, Texture2D shitShooterTexture, Texture2D shitBulletTexture)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    bricks.Add(new Brick(brickTexture,
                    new Vector2(230 + j * 45, 50 + i * 15),
                    0f,
                    new Rectangle(230 + j * 45, 50 + i * 15, 45, 15),
                    1));
                }
            }

            this.sizeUpTexture = sizeUpTexture;
            this.lifeUpTexture = lifeUpTexture;
            this.shitShooterTexture = shitShooterTexture;
            this.shitBulletTexture = shitBulletTexture;
        }

        public List<Brick> Update()
        {
            return bricks;
        }

        public PlayerBar UpdateSizeUps(PlayerBar playerBar, GameTime gameTime)
        {
            List<SizeUp> sizeUpsToRemove = new List<SizeUp>();

            foreach (SizeUp sizeUp in sizeUps)
            {
                PlayerBar updatedPlayerBar = sizeUp.Update(gameTime, playerBar);

                // If effectApplied is true, the playerBar instance will change, indicating the effect has been applied.
                if (sizeUp.EffectApplied)
                {
                    sizeUpsToRemove.Add(sizeUp);
                }

                if (sizeUp.Position.Y > 720)
                {
                    sizeUpsToRemove.Add(sizeUp);
                }

                // Ensure the playerBar reference is updated only if the effect was applied.
                if (updatedPlayerBar != playerBar)
                {
                    playerBar = updatedPlayerBar;
                }
            }

            foreach (SizeUp sizeUpToRemove in sizeUpsToRemove)
            {
                sizeUps.Remove(sizeUpToRemove);
            }

            return playerBar;

        }

        public Life UpdateLifeUps(PlayerBar playerBar, GameTime gameTime, Life life)
        {
            List<LifeUp> lifeUpsToRemove = new List<LifeUp>();

            foreach (LifeUp lifeUp in lifeUps)
            {
                Life updatedLife = lifeUp.Update(gameTime, playerBar, life);

                // If effectApplied is true, the powerup can be flagged for removal
                if (lifeUp.EffectApplied)
                {
                    lifeUpsToRemove.Add(lifeUp);
                }

                if (lifeUp.Position.Y > 720)
                {
                    lifeUpsToRemove.Add(lifeUp);
                }

                // Ensure the playerBar reference is updated only if the effect was applied.
                if (updatedLife != life)
                {
                    life = updatedLife;
                }
            }

            foreach (LifeUp lifeUpToRemove in lifeUpsToRemove)
            {
                lifeUps.Remove(lifeUpToRemove);
            }

            return life;

        }

        public void HandleBallCollisionWithShitShooter(ShitShooter shitShooter)
        {
            shitShooter.HitPoints--;

            if (shitShooter.HitPoints <= 0)
            {
                shitShooters.Remove(shitShooter);
            }
        }

        public void HandleBallCollisionWithBrick(Brick brick)
        {
            brick.HitPoints--;
            if (brick.HitPoints <= 0)
            {
                // Generate a random number between 0 and 1
                Random rand = new Random();
                double chance = rand.NextDouble();

                if (chance < 0.05) // 5% chance
                {
                    sizeUps.Add(new SizeUp(sizeUpTexture, brick.Position, 100f, new Rectangle(
                        (int)brick.Position.X,
                        (int)brick.Position.Y,
                        25,
                        25)));
                }

                if (chance > 0.05 && chance < 0.1) // 5% chance
                {
                    lifeUps.Add(new LifeUp(lifeUpTexture, 
                        brick.Position, 
                        100f, 
                        new Rectangle((int)brick.Position.X, (int)brick.Position.Y, 25, 25)));
                }

                if (chance > 0.1 && chance < 1) // 5% chance
                {
                    shitShooters.Add(new ShitShooter(shitShooterTexture, 
                        brick.Position, 
                        100f, 
                        new Rectangle((int)brick.Position.X, (int)brick.Position.Y, 30, 20),
                        1,
                        shitBulletTexture,
                        150f));
                }

                bricks.Remove(brick);

            }
        }

        public List<Brick> Bricks
        {
            get { return bricks; }
            set { bricks = value; }
        }

        public List<SizeUp> SizeUps
        {
            get { return sizeUps; }
            set { sizeUps = value; }
        }

        public List<LifeUp> LifeUps
        {
            get { return lifeUps; }
            set { lifeUps = value; }
        }
        public List<ShitShooter> ShitShooters
        {
            get { return shitShooters; }
            set { shitShooters = value; }
        }
    }
}
