using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone
{
    public class ScoreManager
    {
        private int score;
        private int brickHitPoints;
        private int enemyHitPoints;

        public ScoreManager (int brickHitPoints, int enemyHitPoints)
        {
            this.brickHitPoints = brickHitPoints;
            this.enemyHitPoints = enemyHitPoints;
            this.score = 0;
        }

        public void BrickHit()
        {
            score += brickHitPoints;
        }

        public void EnemyHit()
        {
            score += enemyHitPoints;
        }

        public void TimeBonus(int timeBonus)
        {
            score += timeBonus;
        }

        public int GetScore()
        {
            return score;
        }
    }
}
