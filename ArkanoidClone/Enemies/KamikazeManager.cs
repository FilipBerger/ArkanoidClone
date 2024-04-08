using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArkanoidClone.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public class KamikazeManager
    {
        private List<Kamikaze> kamikazes = new List<Kamikaze>();
        private Texture2D kamikazeTexture;
        private float kamikazeSpeed;
        private double kamikazeTimer = 0;
        private double kamikazeInterval = 5;
        static Random random = new Random();

        public KamikazeManager(Texture2D texture, float speed)
        {
            kamikazeTexture = texture;
            kamikazeSpeed = speed;
        }

        private void SpawnKamikaze()
        {
            var position = new Vector2(random.Next(300, 900), 200);
            var boundingBox = new Rectangle((int)position.X, (int)position.Y, 60, 60);
            var kamikaze = new Kamikaze(kamikazeTexture, kamikazeSpeed, boundingBox, position);
            kamikazes.Add(kamikaze);
        }
        public void Update(GameTime gameTime, PlayerBar playerBar, Life life)
        {
            kamikazeTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (kamikazeTimer >= kamikazeInterval)
            {
                SpawnKamikaze();
                kamikazeTimer = 0; // Reset the timer
            }



            for (int i = kamikazes.Count - 1; i >= 0; i--)
            {
                var kamikaze = kamikazes[i];
                life = kamikaze.Update(gameTime, playerBar, life);
                if (kamikaze.IsMarkedForRemoval)
                {
                    kamikazes.RemoveAt(i);
                }
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var kamikaze in kamikazes)
            {
                kamikaze.Draw(spriteBatch);
            }
        }
    }


}