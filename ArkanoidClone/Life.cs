using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArkanoidClone
{
    public class Life
    {
        private int remainingLives;

        public int RemainingLives { get { return remainingLives; } }

        public Life(int initialLives)
        {
            remainingLives = initialLives;
        }

        public void DecreaseLife()
        {
            remainingLives--;
        }

        public void ResetLives(int initialLives)
        {
            remainingLives = initialLives;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            Vector2 position = new Vector2(20, 100); // Adjust position as needed
            spriteBatch.DrawString(font, $"Lives: {remainingLives}", position, Color.White);
        }
    }
}
