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
        private List<HighScore> highScores = new List<HighScore> { };
        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100); // Placeholder position, var jobbigt att få den centrerad.

        public HighScoreScreen(SpriteFont font)
        {
            this.font = font;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Color color = i == selectedOption ? Color.Yellow : Color.White;
                spriteBatch.DrawString(font, options[i], new Vector2(position.X, position.Y + i * 30), color);
            }
        }
    }
}