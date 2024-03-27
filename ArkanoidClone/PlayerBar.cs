using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {
        private Texture2D texture;
        private Vector2 position; //Getter och setter ska fungera, just nu fungerar det utan
        private float speed;
        private Rectangle boundingBox;


        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base (texture, position, speed, boundingBox) 
        {
            this.texture = texture;
            this.position = position;
            this.speed = speed;
            this.boundingBox = boundingBox;
           

        }

        public override void Update(GameTime gameTime)
        {
            var keystate = Keyboard.GetState();


            if (keystate.IsKeyDown(Keys.Left))
            {
                position.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                position.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (position.X > 640 - texture.Width / 2) //640 - 480 spelstorlek
            {
                position.X = 640 - texture.Width / 2;
            }
            else if (position.X < texture.Width / 2)
            {
                position.X = texture.Width / 2;
            }
        }
    }
}