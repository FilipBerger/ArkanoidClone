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
        
        private const int WallThickness = 50;

        public static Wall CreateWall(Texture2D texture, GraphicsDevice graphicsDevice, WallPosition position)
        {
            int screenWidth = graphicsDevice.Viewport.Width;
            int screenHeight = graphicsDevice.Viewport.Height;

            int wallOffsetX = 140; // Number of pixels to move from the edges
            

            switch (position)
            {
                case WallPosition.Left:
                    return new Wall(texture, new Vector2(wallOffsetX, 0), new Rectangle(wallOffsetX, 0, WallThickness, screenHeight));
                case WallPosition.Right:
                    int rightWallX = screenWidth - WallThickness;
                    return new Wall(texture, new Vector2(rightWallX - wallOffsetX, 0), new Rectangle(rightWallX - wallOffsetX, 0, WallThickness, screenHeight));
                case WallPosition.Top:
                    return new Wall(texture, new Vector2(wallOffsetX, 0), new Rectangle(wallOffsetX, 0, screenWidth - (2 * wallOffsetX), WallThickness));
                default:
                    throw new ArgumentException("Invalid wall position specified", nameof(position));
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
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public enum WallPosition
        {
            Left,
            Right,
            Top
        } 
    }
}