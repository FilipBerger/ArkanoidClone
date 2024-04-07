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
        private string loadingMessage = "Loading...";
        private string readyMessage = "Press 'Enter' to start the next Stage!";

        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100); // Placeholder position, was a bit too much work to make everything centered correctly.

        public NextStageScreen(SpriteFont font)
        {
            this.font = font;

        }

        public bool Update(KeyboardState keyboardState, KeyboardState previousKeyboardState, bool nextStageIsReady)
        {
            if (nextStageIsReady)
            {
                if (keyboardState.IsKeyDown(Keys.Enter) && !previousKeyboardState.IsKeyDown(Keys.Enter))
                    return true;
                else return false;
            }
            else return false;
        }

        public void Draw(SpriteBatch spriteBatch, bool nextStageIsReady)
        {
            spriteBatch.DrawString(font, message, position, Color.White);
            if (nextStageIsReady)
            {
                spriteBatch.DrawString(font, readyMessage, new Vector2(position.X, position.Y + 30), Color.White);
            }
            else
            {
                spriteBatch.DrawString(font, loadingMessage, new Vector2(position.X, position.Y + 30), Color.White);
            }
        }
    }
}
