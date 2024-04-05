using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public class CreateHighScoreScreen
    {
        private const int PLAYER_NAME_MAX_LENGTH = 10; // Felt that a max name length was needed. Set max at 10 to start with.
        private SpriteFont font;
        private string playerName = "";
        private string message1 = "New high score!";
        private string message2 = "Enter your name: ";
        private string message3 = "Maximum name length is ten characters!";
        private Vector2 positionMessage1 = new Vector2(100, 100);
        private Vector2 positionMessage2 = new Vector2(100, 150);
        private Vector2 positionMessage3 = new Vector2(100, 200);
        private bool isEnteringName = true;
        private int score;  

        public CreateHighScoreScreen(SpriteFont font)
        {
            this.font = font;
            this.score = score;
            // Add logic to get score from somewhere.
        }

        public GameState Update(KeyboardState currentKeyboardState, KeyboardState previousKeyboardState)
        {
            foreach (var key in currentKeyboardState.GetPressedKeys())
            {
                if (!previousKeyboardState.IsKeyDown(key))
                {
                    if (key >= Keys.A && key <= Keys.Z)
                    {
                        if (playerName.Length < PLAYER_NAME_MAX_LENGTH)
                            playerName += key.ToString();
                    }
                }
            }

            if (currentKeyboardState.IsKeyDown(Keys.Back) && !previousKeyboardState.IsKeyDown(Keys.Back) && playerName.Length > 0)
            {
                playerName = playerName[..^1];
            }

            if (currentKeyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter) && playerName.Length > 0)
            {
                HighScore newHighScore = new HighScore(playerName, score);
                HighScoreManager.AddHighScore(newHighScore);
                isEnteringName = false;
            }

            return isEnteringName ? GameState.CreatingHighScore : GameState.MainMenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, message1, positionMessage1, Color.Yellow);
            spriteBatch.DrawString(font, message2 + playerName, positionMessage2, Color.Yellow);
            if (playerName.Length == PLAYER_NAME_MAX_LENGTH)
            {
                spriteBatch.DrawString(font, message3, positionMessage3, Color.Red);
            }
        }
    }
}
