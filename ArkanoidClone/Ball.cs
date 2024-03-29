using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class Ball : Entity
    {
        

        public Ball(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox) //Tillfällig
        {
            
        }


        public void Update(GameTime gameTime, Entity entity)
        {
            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);

            if (BoundingBox.Intersects(entity.BoundingBox))
            {
                Speed *= -1;
            }
            

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                //Texture,
                //Position,
                //null,
                //Color.White,
                //0f,
                //new Vector2(Texture.Width / 2, Texture.Height / 2),
                //Vector2.One,
                //SpriteEffects.None,
                //0f
                Texture,
                BoundingBox,
                Color.White
            );
        }

        public void Bounce()
        {
            throw new System.NotImplementedException();
        }
    }
}