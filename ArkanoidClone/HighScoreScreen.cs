using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ArkanoidClone
{
    public class HighScoreScreen
    {
        private List<HighScore> highScores;
        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100);

        public HighScoreScreen(SpriteFont font)
        {
            this.font = font;
            highScores = HighScoreManager.LoadHighScores();
        }
        public GameState Update(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            highScores = HighScoreManager.LoadHighScores();
            if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                return GameState.MainMenu;
            else return GameState.ViewingHighScores;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            highScores = HighScoreManager.LoadHighScores();
            for (int i = 0; i < highScores.Count; i++)
            {
                string text = $"{highScores[i].PlayerName}: {highScores[i].Score}";
                spriteBatch.DrawString(font, text, new Vector2(position.X, position.Y + i * 30), Color.Yellow);
            }
        }
    }
}