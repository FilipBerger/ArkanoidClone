﻿using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {
        private Texture2D texture;
        private Vector2 position; 
        private float speed;
        private Rectangle boundingBox;
        private int lives;

        public int Lives { get { return lives; } }


        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int lives) : base (texture, position, speed, boundingBox) 
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.boundingBox = boundingBox;
            this.lives = lives;
        }

        public void Update(GameTime gameTime)
        {
            var keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
                BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);
            }

            if (Position.X > 1024 - BoundingBox.Width) //640 - 480 spelstorlek
            {
                Position = new Vector2(1024 - BoundingBox.Width, Position.Y);
            }
            else if (Position.X < 200) //+Texture.Width / 2)
            {
                Position = new Vector2(200, Position.Y);// + Texture.Width / 2, Position.Y);
            }
        }

        public void DecreaseLife() 
        {
            lives--;
        }

        public void ResetLives(int initialLives)
        {
            lives = initialLives;
        }

    }
}