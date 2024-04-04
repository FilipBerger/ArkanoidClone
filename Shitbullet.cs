using System;

public class ShitBullet : Entity
{
    public ShitBullet(Texture2D texture, Vector2 position, float speed)
        : base(texture, position, speed, new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height))
    {
    }

    public void Update(GameTime gameTime)
    {
        // Move the bullet downwards
        Position += new Vector2(0, Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);
    }
}
