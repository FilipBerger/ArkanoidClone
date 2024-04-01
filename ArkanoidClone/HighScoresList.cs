using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone
{
    internal class HighScoresList
    {
        private List<HighScore> highScores = new List<HighScore>();

        public List<HighScore> HighScores
        {
            get { return highScores; }
            set { highScores = value; }
        }
    }
}
