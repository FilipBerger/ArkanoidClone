using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone.GameObjects
{
    public class MainMenuScreen
    {
        private int selectedOption = 0;
        private List<string> options = new List<string> { "Start Game", "High Scores", "Exit" };
        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100); // Placeholder position, was a bit too much work to make everything centered correctly.

        public MainMenuScreen(SpriteFont font)
        {
            this.font = font;

        }

        public GameState Update(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
            {
                selectedOption = (selectedOption + 1) % options.Count;
            }
            else if (keyboardState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
            {
                selectedOption = (selectedOption - 1 + options.Count) % options.Count;
            }
            else if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
            {
                switch (selectedOption)
                {
                    case 0:
                        return GameState.SetUpStage1;
                    case 1:
                        return GameState.ViewingHighScores;
                    case 2:
                        return GameState.Exiting;
                }
            }


            return GameState.MainMenu;
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