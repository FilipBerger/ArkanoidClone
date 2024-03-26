using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace ArkanoidClone
{
    public abstract class Entity
    {
        private Vector2 direction;
        private Texture2D texture;
        private Vector2 position;
        private int speed;
        private Rectangle boundingBox;

        public void Draw(Texture2D texture, Vector2 position)
        {
            throw new System.NotImplementedException();
        }

        public abstract void Update();
    }
}