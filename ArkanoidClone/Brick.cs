using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class Brick : Destroyable
    {
        public Brick(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox)
        {
        }

        public void Break()
        {
            throw new System.NotImplementedException();
        }

        public void Hit()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnPowerUp()
        {
            throw new System.NotImplementedException();
        }

        public void SpawnEnemy()
        {
            throw new System.NotImplementedException();
        }
    }
}