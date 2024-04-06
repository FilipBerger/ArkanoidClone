using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace ArkanoidClone
{
    public class LifeUp : PowerUp
    {
        private bool effectApplied = false;

        public LifeUp(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base(texture, position, speed, boundingBox)
        {
            Texture = texture;
            Position = position;
            Speed = speed;
            BoundingBox = boundingBox;
        }

        public Life ApplyEffect(Life life)
        {
            life.IncreaseLife();

            return life;
        }

        public Life Update(GameTime gameTime, PlayerBar playerBar, Life life)
        {
            Position += new Vector2(0, Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

            if (BoundingBox.Intersects(playerBar.BoundingBox) && !effectApplied)
            {
                ApplyEffect(life);
                effectApplied = true;
            }
            return life;
        }

        public bool EffectApplied
        {
            get { return effectApplied; }
        }
    }
}
