using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ArkanoidClone
{
    public class PlayerBar : Entity
    {
        public Texture2D Texture { get; set; }
        public Vector2 position; /* { get; set; }*/  //Getter och setter ska fungera, just nu fungerar det utan
        public float speed { get; set; }
        public Rectangle boundingBox { get; set; }


        public PlayerBar(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox) : base (texture, position, speed, boundingBox) 
        {
            this.Texture = texture;
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

            if (position.X > 640 - Texture.Width / 2) //640 - 480 spelstorlek
            {
                position.X = 640 - Texture.Width / 2;
            }
            else if (position.X < Texture.Width / 2)
            {
                position.X = Texture.Width / 2;
            }
        }
    }
}