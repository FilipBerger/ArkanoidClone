using ArkanoidClone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class Ball : Entity
{
    public Vector2 Velocity { get; set; }


    private int previousLife;

    public Ball(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle boundingBox) : base(texture, position, 0, boundingBox) // Hastighet sätts till 0 eftersom hastigheten kommer att styras av Velocity-egenskapen
    {
        Velocity = velocity;
    }

    public Life Update(GameTime gameTime, List<Entity> entities, PlayerBar playerBar, Life life, Vector2 originalBallPosition)
    {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        previousLife = life.RemainingLives;

        BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);
        CheckOutOfBounds(playerBar, life, originalBallPosition);

        foreach (Entity entity in entities)
        {
            if (entity != this && BoundingBox.Intersects(entity.BoundingBox))
            {
                HandleCollision(entity, gameTime);
                break;
            }
        }
        return life;
    }

    public BrickManager DetectCollisionWithBrickOrShitShooter(BrickManager brickManager)
    {

        if (brickManager != null)
        {
            foreach (Brick brick in brickManager.Bricks)
            {
                if (BoundingBox.Intersects(brick.BoundingBox))
                {
                    brickManager.HandleBallCollisionWithBrick(brick);
                    break;
                }
            }

            foreach (ShitShooter shitShooter in brickManager.ShitShooters)
            {
                if (BoundingBox.Intersects(shitShooter.BoundingBox))
                {
                    brickManager.HandleBallCollisionWithShitShooter(shitShooter);
                    break;
                }
            }
        }

        return brickManager;
    }

    private void HandleCollision(Entity entity, GameTime gameTime)
    {
        Position -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        Vector2 oldPosition = Position;

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
            Rectangle intersection = Rectangle.Intersect(BoundingBox, entity.BoundingBox);

            // Beräkna om bollen träffar brickan från sidan eller från toppen/botten
            bool hitFromSide = intersection.Width < intersection.Height;

            if (hitFromSide)
            {
                // Ändra riktningen horisontellt
                Velocity = new Vector2(-Velocity.X, Velocity.Y);
            }
            else
            {
                // Ändra riktningen vertikalt
                Velocity = new Vector2(Velocity.X, -Velocity.Y);
            }
        }
        else if (entity is PlayerBar)
        {
            float intersectX = BoundingBox.Center.X - entity.BoundingBox.Center.X;

            float normalizedIntersectX = intersectX / entity.BoundingBox.Width;

            float angle = normalizedIntersectX * MathHelper.ToRadians(60); // 60 grader är en vanlig vinkeländring i Arkanoid


            Vector2 newVelocity = Vector2.Transform(Velocity, Matrix.CreateRotationZ(angle));


            float newVelocityX = (float)Math.Sin(angle) * Velocity.Length();

            float newVelocityY = -(float)Math.Cos(angle) * Velocity.Length();

            Velocity = new Vector2(newVelocityX, newVelocityY);
        }
    }


    public Life CheckOutOfBounds(PlayerBar playerBar, Life life, Vector2 originalBallPosition)
    {
        if (Position.Y > playerBar.Position.Y + 20)
        {
            life.DecreaseLife();
            // Återställ bollens position om livet har minskats
            if (life.RemainingLives < previousLife)
            {
                ResetPosition(originalBallPosition);
            }
        }
        return life;
    }
    //public bool CheckOutOfBounds(PlayerBar playerBar)
    //{
    //    if (Position.Y > playerBar.Position.Y + 20)
    //    {
    //        return true;
    //    }
    //    return false;
    //}
    public void ResetPosition(Vector2 originalPosition)
    {
        Position = originalPosition;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, BoundingBox, Color.White);
    }


}

