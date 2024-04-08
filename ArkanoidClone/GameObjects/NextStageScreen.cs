using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    internal class NextStageScreen
    {
        private string message = "Get ready for the next stage!";
        private string readyMessage = "Press 'Enter' to start the next Stage!";

        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100); // Placeholder position, was a bit too much work to make everything centered correctly.

        public NextStageScreen(SpriteFont font)
        {
            this.font = font;

        }

        public GameState Update(KeyboardState keyboardState, KeyboardState previousKeyboardState)
        {
                if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    return GameState.PlayingStage2;
                else return GameState.SetUpStage2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, message, position, Color.White);
            spriteBatch.DrawString(font, readyMessage, new Vector2(position.X, position.Y + 30), Color.Yellow);
        }
    }
}
