using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class Wall : Entity
    {

        private const int WallThickness = 10;

        public static Wall CreateWall(Texture2D texture, GraphicsDevice graphicsDevice, WallPosition position)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            switch (position)
            {
                case WallPosition.Left:
                    return new Wall(texture, Vector2.Zero, new Rectangle(0, 0, WallThickness, screenHeight));
                case WallPosition.Right:
                    return new Wall(texture, new Vector2(screenWidth - WallThickness, 0), new Rectangle(screenWidth - WallThickness, 0, WallThickness, screenHeight));
                case WallPosition.Top:
                    return new Wall(texture, new Vector2(WallThickness, 0), new Rectangle(WallThickness, 0, screenWidth - (2 * WallThickness), WallThickness));
                default:
                    throw new System.ArgumentException("Invalid wall position specified", nameof(position));
            }
        }
        public Wall(Texture2D texture, Vector2 position, Rectangle boundingBox) 
            : base(texture, position, 0f, boundingBox) //Väggarna ska inte röra sig, 0f
        {
        }
           public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, BoundingBox, Color.Green);
        }

        public enum WallPosition
        {
            Left,
            Right,
            Top
        } 
    }
}