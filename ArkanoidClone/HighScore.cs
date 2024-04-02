using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone
{
    public class HighScore
    {
        private string playerName;
        private int score;

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
