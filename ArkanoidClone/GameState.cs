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
        PlayingStage2,
        ViewingHighScores,
        CreatingHighScore,
        Exiting,
        GameOver
    }
}