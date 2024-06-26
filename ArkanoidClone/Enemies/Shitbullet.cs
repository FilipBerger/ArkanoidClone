﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Numerics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
namespace ArkanoidClone
{

    public class ShitBullet : Entity
    {
        private Texture2D texture;
        private Vector2 position;
        private float speed;
        private Rectangle boundingBox;
        private bool lifeDecreaseApplied = false;
        public bool IsMarkedForRemoval { get; private set; } = false;

        public ShitBullet(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
            : base(texture, position, speed, boundingBox)
        {
        }

        public Life LifeDecreaseEffect(Life life)
        {
            life.DecreaseLife();

            return life;
        }
        public Life Update(GameTime gameTime, PlayerBar playerBar, Life life)
        {
            // The bullet goes down
            Position += new Vector2(0, Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox) && !lifeDecreaseApplied)
            {
                LifeDecreaseEffect(life);
                lifeDecreaseApplied = true;
            }
            
            //Collision with the playerBar
            if (BoundingBox.Intersects(playerBar.BoundingBox) || Position.Y > 720)
            {
                IsMarkedForRemoval = true;
                
            }

            return life;
        }
        

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(Texture, BoundingBox, Color.White);
            }
        
    }
}
