using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public abstract class Destroyable : Entity
    {
        private int hitpoints;

        protected Destroyable(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(texture, position, speed, boundingBox)
        {

        }



        public int HitPoints
        {
            get { return hitpoints; }
            set { hitpoints = value; }
        }
    }
}