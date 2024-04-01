//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace ArkanoidClone
//{
//    public class PowerUpItem : Entity
//    {
//        public bool IsActive { get; private set; }

//        public PowerUpItem(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox)
//            : base(texture, position, speed, boundingBox)
//        {
//            IsActive = true;
//        }

//        public void Update(GameTime gameTime, PlayerBar playerBar)
//        {
//            if (!IsActive)
//                return;

//            Position = new Vector2(Position.X, Position.Y + Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
//            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

//            if (BoundingBox.Intersects(playerBar.BoundingBox))
//            {
//                IsActive = false;
//                float factor = 2f; 
//                playerBar.ApplySizePowerUpWithDuration(factor, 10); 
//            }
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            if (IsActive)
//            {
//                spriteBatch.Draw(Texture, Position, Color.White);
//            }
//        }

//        public void Activate(Vector2 position)
//        {
//            Position = position;
//            IsActive = true;
//        }
//    }
//}
