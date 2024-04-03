using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(20, 20), Color.White);
        }
    }
}
