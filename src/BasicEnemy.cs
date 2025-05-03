using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceWar.src
{
    public class BasicEnemy : Enemy
    {
        Texture2D basicEnemy = Raylib.LoadTexture("images/BasicEnemy.png");
        public int Speed { get; set; }

        public BasicEnemy(Vector2 position, int width = 92, int height = 97, int health = 40, int speed = 5) : base
        (position, width, height, health)
        {
            Speed = speed;
        }

        public override void Move(Vector2 direction, Vector2 playerPosition)
        {

            Vector2 P = new Vector2(Position.X - Speed, Position.Y);
            direction = new Vector2(playerPosition.X - Position.X, 0);
            if (direction.Length() > 0)
            {
                direction = Vector2.Normalize(direction);

            }

            Position = P;
            UpdateHitbox();


        }
        public override void Attack(float deltatime, Vector2 playerPosition)
        {

        }

        public override void Draw()
        {

            Raylib.DrawTexture(basicEnemy, (int)Position.X, (int)Position.Y, Color.White);

        }
    }
}
