
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
        private Texture2D brickTexture;

        public bool IsDestroyed { get; private set; }

        public Brick(Texture2D brickTexture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(brickTexture, position, speed, boundingBox, hitpoints)
        {
            this.brickTexture = brickTexture;
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
