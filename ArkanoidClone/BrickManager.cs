using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone
{
    public class BrickManager
    {
        List<Brick> bricks = new List<Brick>();

        public BrickManager(Texture2D brickTexture, int v)
        {
            for (int i = 0; i < 15; i++)

                for (int j = 0; j < 17; j++)
                {
                    bricks.Add(new Brick(brickTexture,
                    new Vector2(230 + j * 45, 50 + i * 15),
                    0f,
                    new Rectangle(230 + j * 45, 50 + i * 15, 45, 15),
                    1));
                }
        }

        public List<Brick> Update()
        {
            return bricks;
        }

        public void HandleCollision(Brick brickHit)
        {
            foreach (Brick brick in bricks)
            {
                if (brickHit is Brick)
                {
                    brick.HitPoints--;
                    if (brick.HitPoints == 0)
                    {
                        bricks.Remove(brick);

                        // Randomly spawn new enemy

                        /*int randomNumber = RandomNumberGenerator.GenerateRandomNumber(1, 10);
                        Console.WriteLine(randomNumber);
                        if (randomNumber == 1)
                        {
                            // Spawn enemy1
                        }*/


                    }
                }
            }

           
        }
/*
        public class RandomNumberGenerator
        {
            private static Random random = new Random();

            public static int GenerateRandomNumber(int min, int max)
            {
                return random.Next(min, max + 1);
            }
        }

*/

    }
}
