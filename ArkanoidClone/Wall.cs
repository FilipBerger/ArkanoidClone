﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class Wall : Entity
    {
        public Wall(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox)
        {
        }

        public void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }
    }
}