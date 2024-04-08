using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArkanoidClone
{
    public abstract class Enemy : Destroyable
    {
        private float speed;
        protected Enemy(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitpoints) : base(texture, position, speed, boundingBox, hitpoints)
        {

        }

        public void Update(GameTime gameTime)
        {
        
        } 
        
    }
}