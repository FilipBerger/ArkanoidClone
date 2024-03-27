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
                Position = new Vector2(Position.X - speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

            if (keystate.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + speed * (float)gameTime.ElapsedGameTime.TotalSeconds, Position.Y);
            }

            if (Position.X > 640 - Texture.Width / 2) //640 - 480 spelstorlek
            {
                Position = new Vector2(640 - Texture.Width / 2, Position.Y);
            }
            else if (Position.X < 0)//Texture.Width / 2)
            {
                Position = new Vector2(0, Position.Y);//Texture.Width / 2, Position.Y);
            }
        }

    }
}