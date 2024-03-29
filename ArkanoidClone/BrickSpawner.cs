using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ArkanoidClone
{
    public class BrickSpawner
    {
        private Texture2D _brickTexture;
        private Vector2 _spawnPosition;
        private float _brickSpeed;
        private Rectangle _brickBoundingBox;
        private int _maxHitPoints;

        public BrickSpawner(Texture2D brickTexture, Vector2 spawnPosition, float brickSpeed, Rectangle brickBoundingBox, int maxHitPoints)
        {
            _brickTexture = brickTexture;
            _spawnPosition = spawnPosition;
            _brickSpeed = brickSpeed;
            _brickBoundingBox = brickBoundingBox;
            _maxHitPoints = maxHitPoints;
        }

        public List<Brick> SpawnBricks(int numberOfBricks, int numberOfRows)
        {
            List<Brick> bricks = new List<Brick>();

            for (int row = 0; row < numberOfRows; row++)
            {
                for (int i = 0; i < numberOfBricks; i++)
                {
                    Vector2 position = new Vector2(_spawnPosition.X + (i * _brickBoundingBox.Width), _spawnPosition.Y + (row * _brickBoundingBox.Height));
                    Brick brick = new Brick(_brickTexture, position, _brickSpeed, _brickBoundingBox, _maxHitPoints);
                    bricks.Add(brick);
                }
            }

            return bricks;
        }

       

        internal List<Brick> SpawnBricks(object numberOfBricks, object numberOfRows)
        {
            List<Brick> bricks = new List<Brick>();

            int bricksCount = Convert.ToInt32(numberOfBricks);
            int rowsCount = Convert.ToInt32(numberOfRows);

            for (int row = 0; row < rowsCount; row++)
            {
                for (int i = 0; i < bricksCount; i++)
                {
                    Vector2 position = new Vector2(_spawnPosition.X + (i * _brickBoundingBox.Width), _spawnPosition.Y + (row * _brickBoundingBox.Height));
                    Brick brick = new Brick(_brickTexture, position, _brickSpeed, _brickBoundingBox, _maxHitPoints);
                    bricks.Add(brick);
                }
            }

            return bricks;
        }
    }
    public class Game
    {
        private List<Brick> gameObjects = new List<Brick>();
        private SpriteBatch spriteBatch; 
        private Texture2D brickTexture;
        private Vector2 spawnPosition;
        private float brickSpeed;
        private Rectangle brickBoundingBox;
        private int maxHitPoints;
        private int numberOfRows;
        private int numberOfBricks;

        public void LoadContent()
        {
            BrickSpawner spawner = new BrickSpawner(brickTexture, spawnPosition, brickSpeed, brickBoundingBox, maxHitPoints);
            List<Brick> bricks = spawner.SpawnBricks(numberOfBricks, numberOfRows);
            gameObjects.AddRange(bricks); 
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);
            }
        }

        
    }
}
   