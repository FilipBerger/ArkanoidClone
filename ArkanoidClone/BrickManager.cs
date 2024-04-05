using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ArkanoidClone
{



    public class BrickManager
    {
        List<Brick> bricks = new List<Brick>();
        List<SizeUp> sizeUps = new List<SizeUp>();
        
        private Texture2D sizeUpTexture;
        private SizeUp sizeUp;
        private LifeUp lifeUp;



        public BrickManager(Texture2D brickTexture, Texture2D sizeUpTexture)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    bricks.Add(new Brick(brickTexture,
                    new Vector2(230 + j * 45, 50 + i * 15),
                    0f,
                    new Rectangle(230 + j * 45, 50 + i * 15, 45, 15),
                    1));
                }
            }

            this.sizeUpTexture = sizeUpTexture;
        }

        public List<Brick> Update()
        {
            return bricks;
        }

        public List<SizeUp> UpdateSizeUps(List<SizeUp> sizeUps)
        {
            this.sizeUps = sizeUps;
            
            return this.sizeUps;

        }

        public void HandleCollision(Brick brick)
        {
            brick.HitPoints--;
            if (brick.HitPoints == 0)
            {
                bricks.Remove(brick);

                // Spawn SizeUp power-up

                sizeUps.Add(new SizeUp(sizeUpTexture, brick.Position, 100f, new Rectangle(
                    (int)brick.Position.X,
                    (int)brick.Position.Y,
                    25,
                    25)));
                
                
                
                // Spawn LifeUp power-up
                //powerUps.Add(new LifeUp());
            }
        }

        public List<Brick> Bricks
        {
            get { return bricks; }
            set { bricks = value; }
        }

        public List<SizeUp> SizeUps
        {
            get { return sizeUps; }
            set { sizeUps = value; }
        }

    }

}
