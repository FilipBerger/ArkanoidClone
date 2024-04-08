using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ArkanoidClone.Enemies
{
    public class Kamikaze
    {
        private float direction = 1;
        public Texture2D kamikazeTexture;
        private float kamikazeSpeed;
        private bool lifeDecreaseApplied = false;
        public bool IsMarkedForRemoval { get; private set; } = false;
        private List<Kamikaze> kamikazes = new List<Kamikaze>();


        private Rectangle boundingBox;
        private Vector2 position;

        public Kamikaze(Texture2D texture, float speed, Rectangle boundingBox, Vector2 position)
        {
            this.position = position;
            kamikazeTexture = texture;
            kamikazeSpeed = speed;
            this.boundingBox = boundingBox;

        }

        public Life LifeDecreaseEffect(Life life)
        {
            life.KamikazeHit();

            return life;
        }

        public Life Update(GameTime gameTime, PlayerBar playerBar, Life life)
        {
            position += new Vector2(0, direction * kamikazeSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            boundingBox = new Rectangle((int)position.X, (int)position.Y, boundingBox.Width, boundingBox.Height);

            if (boundingBox.Intersects(playerBar.BoundingBox) || position.Y > 720)
            {
                IsMarkedForRemoval = true;
            }

            if (boundingBox.Intersects(playerBar.BoundingBox) && !lifeDecreaseApplied)
            {
                LifeDecreaseEffect(life);
                lifeDecreaseApplied = true;
            }

            return life;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kamikazeTexture, boundingBox, Color.White);
        }
    }

}