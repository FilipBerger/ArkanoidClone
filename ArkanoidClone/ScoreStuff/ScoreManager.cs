using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone.ScoreStuff
{
    public class ScoreManager
    {
        private int score;
        private int brickHitPoints;
        private int enemyHitPoints;

        public ScoreManager(int brickHitPoints, int enemyHitPoints)
        {
            this.brickHitPoints = brickHitPoints;
            this.enemyHitPoints = enemyHitPoints;
            score = 0;
        }

        public void BrickHit() //inuti brickhit och enemyhit kan man lägga till logik från ball.cs och enemy.cs för när dem går sönder
                               //och kan lägga till poäng. Väntar tills all logik på övriga är klara.
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
