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
        private Texture2D texture;
        private Vector2 position;
        private float speed;
        private Rectangle boundingBox;

        public Entity(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
        {

        }

        public void Draw(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public abstract void Update();
    }
}