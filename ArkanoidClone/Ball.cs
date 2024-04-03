using ArkanoidClone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class Ball : Entity
{
    public Vector2 Velocity { get; set; } 

    public Ball(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle boundingBox) : base(texture, position, 0, boundingBox) // Hastighet sätts till 0 eftersom hastigheten kommer att styras av Velocity-egenskapen
    {
        Velocity = velocity;
    }

    public void Update(GameTime gameTime, List<Entity> entities)
    {

        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;


        BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);


        foreach (Entity entity in entities)
        {
            if (entity != this && BoundingBox.Intersects(entity.BoundingBox))
            {

                HandleCollision(entity, gameTime);
                break;
            }
        }

    }

    private void HandleCollision(Entity entity, GameTime gameTime)
    {
        Position -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        

        if (entity is Wall)
        {
            
            Rectangle entityRect = entity.BoundingBox; // Antag att BoundingBox är en Rectangle

            // Kolla om bollens bounding box interagerar med botten av entitetens bounding box
            if (BoundingBox.Intersects(new Rectangle(entityRect.Left, entityRect.Bottom, entityRect.Width, 1)))
            {
                // Här kan du hantera logiken för när bollen träffar botten av entiteten
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
            else
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }

        }
        else if (entity is Brick)
        {
            //Velocity = new Vector2(Velocity.X, -Velocity.Y);
            Rectangle entityRect = entity.BoundingBox; // Antag att BoundingBox är en Rectangle

            // Kolla om bollens bounding box interagerar med botten av entitetens bounding box
            if (BoundingBox.Intersects(new Rectangle(entityRect.Left, entityRect.Bottom, entityRect.Width, 1)))
            {
                // Här kan du hantera logiken för när bollen träffar botten av entiteten
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
            else
            {
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
        }
        else if (entity is PlayerBar)
        {
            
            //float relativeIntersectX = (BoundingBox.Center.X - entity.BoundingBox.Left) / (float)entity.BoundingBox.Width; // äldre version
            float relativeIntersectX = Math.Abs((BoundingBox.Center.X - entity.BoundingBox.Center.X) / (float)entity.BoundingBox.Width); //ny test version
            float maxBounceAngle = MathHelper.Pi / 3;
            float bounceAngle = relativeIntersectX * maxBounceAngle;

            // Beräkna den nya hastigheten baserat på vinkeln
            float newVelocityX = (float)Math.Sin(bounceAngle) * Velocity.Length();
            float newVelocityY = -(float)Math.Cos(bounceAngle) * Velocity.Length();

            // Tilldela den nya hastigheten till Velocity
            Velocity = new Vector2(newVelocityX, newVelocityY);

        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, BoundingBox, Color.White);
    }
}

