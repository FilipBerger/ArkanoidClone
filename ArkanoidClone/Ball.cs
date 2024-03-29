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

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void Bounce()
        {
            throw new System.NotImplementedException();
        }
    }
}