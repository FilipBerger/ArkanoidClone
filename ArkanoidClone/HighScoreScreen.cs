﻿using Microsoft.Xna.Framework;
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
        private List<HighScore> highScores = new List<HighScore>();
        private SpriteFont font;
        private Vector2 position = new Vector2(100, 100);

        public HighScoreScreen(SpriteFont font, string jsonFilePath)
        {
            this.font = font;
            highScores = HighScoresManager.LoadHighScores(jsonFilePath);
        }

       
    }
}