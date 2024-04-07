using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ArkanoidClone
{
    public class Life
    {
        private const int INITIAL_LIVES = 3;

        private int remainingLives;

        public int RemainingLives { get { return remainingLives; } }

        public Life()
        {
            remainingLives = INITIAL_LIVES;
        }

        public void DecreaseLife()
        {
            remainingLives--;
        }

        public void IncreaseLife()
        {
            remainingLives++;
        }

        public void ResetLives()
        {
            remainingLives = INITIAL_LIVES;
        }

        public GameState Update()
        {
            if (remainingLives <= 0)
            {
                ResetLives();
                return GameState.CreatingHighScore;
            }
            return GameState.PlayingStage1;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            Vector2 position = new Vector2(20, 100); // Adjust position as needed
            spriteBatch.DrawString(font, $"Lives: {remainingLives}", position, Color.White);
        }
    }
}
