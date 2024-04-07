using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{
    public enum GameState
    {
        MainMenu,
        PlayingStage1,
        SetUpStage2,
        PlayingStage2,
        ViewingHighScores,
        CreatingHighScore,
        Exiting,
        GameOver
    }
}