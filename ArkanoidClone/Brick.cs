
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{

    public class Brick : Destroyable
    {
        public bool IsDestroyed { get; private set; }

        public Brick(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(texture, position, speed, boundingBox, hitpoints)
        {
            this.Texture = texture;
            this.Position = position;
            this.BoundingBox = boundingBox;
            this.HitPoints = hitpoints;
            this.Speed = speed;

        }
        

        /*public void HandleCollision(Entity entity, GameTime gameTime)
        {
            if (entity is Ball)
            {
                HitPoints--;
                if(HitPoints == 0)
                {
                    Rectangle entityRect = entity.BoundingBox; 


                    if (BoundingBox.Intersects(new Rectangle(entityRect.Left, entityRect.Bottom, entityRect.Width, 1)))
                    {
                        Brick.Remove(this);
                    }
                    else
                    {
                        Brick.Remove(this);
                    }
                    IsDestroyed = true;
                }


                
            }
        }

        public static void Remove(Brick brick)
        {
           
            brick.Position = new Vector2(-1000, -1000);
        }
*/
        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }
    }
}
