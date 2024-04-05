using ArkanoidClone;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

public class Ball : Entity
{
    public Vector2 Velocity { get; set; }
    

    private int previousLife;

    // Konstruktor för att skapa en ny boll med angiven textur, position, hastighet och begränsningsruta
    public Ball(Texture2D texture, Vector2 position, Vector2 velocity, Rectangle boundingBox) : base(texture, position, 0, boundingBox) // Hastighet sätts till 0 eftersom hastigheten kommer att styras av Velocity-egenskapen
    {
        Velocity = velocity;
    }

    // Metod för att uppdatera bollens tillstånd
    public Life Update(GameTime gameTime, List<Entity> entities, PlayerBar playerBar, Life life, Vector2 originalBallPosition)
    {
        // Uppdatera bollens position baserat på dess hastighet och den förflutna tiden
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Spara det tidigare antalet av liv för senare referens
        previousLife = life.RemainingLives;

        // Uppdatera begränsningsrutan baserat på dess nya position
        BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, BoundingBox.Width, BoundingBox.Height);

        // Kontrollera om bollen har gått utanför spelfältet
        CheckOutOfBounds(playerBar, life, originalBallPosition);

        // Loopa genom alla entiteter i en lista
        foreach (Entity entity in entities)
        {
            // Om denna entiteten inte är bollen och om dess begränsningsruta kolliderar med bollens begränsningsruta
            if (entity != this && BoundingBox.Intersects(entity.BoundingBox))
            {
                // Hantera kollisionen mellan bollen och den andra entiteten
                HandleCollision(entity, gameTime);

                // Avbryt loopen vid kollision
                break;
            }
        }
        // Returnera life objektet efter att det har uppdaterats
        return life;
    }

    // Metod för att hantera kollision med bricks
    public BrickManager UpdateBricks(BrickManager brickManager)
    {
        // Kontroll för att se om brickmanager är null
        if (brickManager != null)
        {
            // Loopa igenom alla bricks
            foreach (Brick brick in brickManager.Bricks)
            {
                // Om bollens begränsningsruta kolliderar med bricks begränsningsruta
                if (BoundingBox.Intersects(brick.BoundingBox))
                {
                    // Hantera kollision mellan boll och bricks genom metod i brickManager klassen
                    brickManager.HandleCollision(brick);

                    // Avbryt loopen efter att kollisionen har hanterats
                    break;
                }
            }
        }
        // Returnera uppdaterad brickManager
        return brickManager;
    }

    // Metod för att hantera kollision mellan bollen och andra entiteter
    private void HandleCollision(Entity entity, GameTime gameTime)
    {
        Position -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Spara den föregående positionen för bollen för senare referens
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
        else if (entity is ShitShooter)
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
    public void ResetPosition(Vector2 originalPosition)
    {
        Position = originalPosition;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, BoundingBox, Color.White);
    }
}

