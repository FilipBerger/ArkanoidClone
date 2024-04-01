using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class BrickShitter : Enemy
    {
        public BrickShitter(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(texture, position, speed, boundingBox, hitpoints)
        {
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}