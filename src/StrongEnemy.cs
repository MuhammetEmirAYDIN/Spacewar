using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar.src
{
    public class StrongEnemy : Enemy
    {
        Texture2D strongEnemy = Raylib.LoadTexture("images/StrongEnemy.png");
        public int Speed { get; set; }
        private float AttackTimer = 0f;
        public List<Bullet> Bullets { get; set; }

        public StrongEnemy(Vector2 position, int width = 213, int height = 194, int health = 80, int speed = 1) : base
        (position, width, height, health)
        {
            Speed = speed;
            Bullets = new List<Bullet>();
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

        public override void Draw()
        {
            Raylib.DrawTexture(strongEnemy, (int)Position.X, (int)Position.Y, Color.White);
        }

        public override void Attack(float DeltaTime, Vector2 playerPosition)
        {

            AttackTimer += DeltaTime;
            if (AttackTimer > 3f)
            {


                Bullet newBullet = new Bullet(new Vector2(Position.X + Hitbox.Width / 2 - 120, Position.Y + Hitbox.Height / 2), 10, new Vector2(-1, 0), 15, 50, 30, false);
                Bullets.Add(newBullet);
                AttackTimer = 0f;
            }

            foreach (var bullet in Bullets)
            {
                bullet.Move(DeltaTime);
            }

            Bullets.RemoveAll(b => !b.IsActive);
        }


    }
}
