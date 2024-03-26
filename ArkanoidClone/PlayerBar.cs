using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {

        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox : base(texture, ) 
        { 

        }

        public override void Update()
        {
            var keystate = Keyboard.GetState();


            if (keystate.IsKeyDown(Keys.Left))
            {
                playerBarPosition.X -= playerBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                playerBarPosition.X += playerBarSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (playerBarPosition.X > _graphics.PreferredBackBufferWidth - playerBarTexture.Width / 2)
            {
                playerBarPosition.X = _graphics.PreferredBackBufferWidth - playerBarTexture.Width / 2;
            }
            else if (playerBarPosition.X < playerBarTexture.Width / 2)
            {
                playerBarPosition.X = playerBarTexture.Width / 2;
            }
        }
    }
}