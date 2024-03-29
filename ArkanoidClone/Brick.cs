
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
      
        public Brick(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(texture, position, speed, boundingBox, hitpoints)
        
        {
            this.Texture = texture;
            this.Position = position;
            this.BoundingBox = boundingBox;
            this.HitPoints = hitpoints;
            this.Speed = speed;

        }



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
