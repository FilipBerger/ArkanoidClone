using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
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
        private Texture2D brickTexture1;
        private Texture2D brickTexture2;

        public BrickManager(Texture2D brickTexture1, Texture2D brickTexture2, Texture2D sizeUpTexture, Texture2D lifeUpTexture, Texture2D shitShooterTexture, Texture2D shitBulletTexture)
        {
            this.brickTexture1 = brickTexture1;
            this.brickTexture2 = brickTexture2;
            this.sizeUpTexture = sizeUpTexture;
            this.lifeUpTexture = lifeUpTexture;
            this.shitShooterTexture = shitShooterTexture;
            this.shitBulletTexture = shitBulletTexture;
        }

        private List<Brick> CreateBrickLayout(Texture2D brickTexture, int numberOfRows, int numberOfColumns, int startingPositionX, int startingPositionY, int brickHP)
        {
            List<Brick> newBrickLayout = new List<Brick>();

            for (int i = 0; i < numberOfRows; i++)
            {
                for (int j = 0; j < numberOfColumns; j++)
                {
                    newBrickLayout.Add(new Brick(brickTexture,
                    new Vector2(startingPositionX + j * 45, startingPositionY + i * 15),
                    0f,
                    new Rectangle(startingPositionX + j * 45, startingPositionY + i * 15, 45, 15),
                    brickHP));
                }
            }

            return newBrickLayout;
        }

        public List<Brick> Update()
        {
            return bricks;
        }

        public GameState UpdateStageProgress(GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameState.PlayingStage1:
                    return bricks.Count > 0 ? GameState.PlayingStage1 : GameState.SetUpStage2;

                case GameState.PlayingStage2:
                    return bricks.Count > 0 ? GameState.PlayingStage2 : GameState.CreatingHighScore;

            }
            return GameState.MainMenu;
        }

        public void SetupStage1()
        {
            //Reset all entities handled by brick manager
            sizeUps = new List<SizeUp>();
            lifeUps = new List<LifeUp>();
            shitShooters = new List<ShitShooter>();

            // Create brick layout for stage 1        6, 15
            bricks = CreateBrickLayout(brickTexture1, 6, 15, 275, 110, 1);
        }

        public void SetupStage2()
        {
            //Reset all entities handled by brick manager
            sizeUps = new List<SizeUp>();
            lifeUps = new List<LifeUp>();
            shitShooters = new List<ShitShooter>();

            // Create brick layout for stage 2
            bricks = CreateBrickLayout(brickTexture2, 6, 15, 275, 110, 2);
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

                if (chance > 0.1 && chance < 0.2) // 10% chance
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
