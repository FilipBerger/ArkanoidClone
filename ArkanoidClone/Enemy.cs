using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public abstract class Enemy : Destroyable
    {
        protected Enemy(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox)
        {
        }

        public override void Update(GameTime gameTime) { } 
        
    }
}