using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone.ScoreStuff
{
    public class HighScore
    {
        private string playerName;
        private int score;

        public HighScore(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
