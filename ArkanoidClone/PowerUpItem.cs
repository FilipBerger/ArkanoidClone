using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public class AdditionalBall : Entity
    {
        public bool isActive { get; private set; } 

        public AdditionalBall(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
            : base(texture, position, speed, boundingBox)
        {
            isActive = true; 
        }
        public void Update(GameTime gameTime, PlayerBar playerBar)
        {
            if (!isActive)
                return; 

            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X,
                    (int)Position.Y,
                    BoundingBox.Width,
                    BoundingBox.Height);
            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                Speed *= -1;
            }
            if (BoundingBox.Intersects(playerBar.BoundingBox))
            {
                playerBar.ApplySizePowerUpWithDuration(new Vector2(300, 100), 10);
                playerBar.ApplySpeedPowerUpForDuration(15, 1000);
                isActive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
        public void Activate(Vector2 position)
        {
            Position = position;
            isActive = true;
        }
    }
}
