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
        private Texture2D texture; //{ get; set; }
        private Vector2 position; // { get; set; }
        private float speed { get; set; }
        private Rectangle boundingBox { get; set; }


        public Entity(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.boundingBox = boundingBox;

        }

        #region

        public Texture2D Texture 
        {
            get {  return texture; }
            set { texture = value; }
        } 

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public float Speed 
        {
            get { return speed; }
            set { speed = value; }
            
        }

        public Rectangle BoundingBox
        {
            get { return boundingBox; }
            set { boundingBox = value; }
        }

        #endregion
        //public void Draw(Texture2D Texture, Vector2 position)
        //{

        //}

        public abstract void Update(GameTime gameTime);
    }
}