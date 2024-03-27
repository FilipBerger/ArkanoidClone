
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArkanoidClone
{

    public class Brick : GameObject
    {
        private Texture2D _texture;
        private Vector2 _position;
        private float _speed;
        private Rectangle _boundingBox;
        private int _hitPoints;

        public Brick(Texture2D texture, Vector2 position, float speed, Rectangle boundingBox, int hitPoints)
        {
            _texture = texture;
            _position = position;
            _speed = speed;
            _boundingBox = boundingBox;
            _hitPoints = hitPoints;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }

    BrickSpawner spawner = new BrickSpawner(brickTexture, spawnPosition, brickSpeed, brickBoundingBox, maxHitPoints);
    List<Brick> bricks = spawner.SpawnBricks(numberOfBricks, numberOfRows);
    gameObjects.AddRange(bricks); // Add the bricks to your game objects
   
        foreach (var gameObject in gameObjects)
    {
        gameObject.Draw(spriteBatch);
    }
}
}
}
