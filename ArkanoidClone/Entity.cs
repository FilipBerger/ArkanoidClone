using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ArkanoidClone
{
    public abstract class Entity
    {
        // private Vector2 direction;
        public Texture2D Texture { get; set; }
        public Vector2 position { get; set; }
        public float speed { get; set; }
        public Rectangle boundingBox { get; set; }


        public Entity(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
        {
            this.Texture = texture;
            this.position = position;
            this.speed = speed;
            this.boundingBox = boundingBox;

        }

        //public void Draw(Texture2D Texture, Vector2 position)
        //{
            
        //}

        public abstract void Update(GameTime gameTime);
    }
}