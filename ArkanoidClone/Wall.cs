namespace ArkanoidClone
{
    public class Wall : Entity
    {
        public Wall(Texture2D texture, Vector2 position, Rectangle boundingBox) 
            : base(texture, position, 0f, boundingBox) //Väggarna ska inte röra sig, 0f
        {
        }


        public override void Draw(SpriteBatch spriteBatch)

        {
            spriteBatch.Draw(Texture, BoundingBox, Color.White);
        }

        public enum WallPosition
        {
            Left,
            Right,
            Top
        } 
    }
}
