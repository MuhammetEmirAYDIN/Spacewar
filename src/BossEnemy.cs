using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar.src
{
    public class BossEnemy : Enemy
    {
        Texture2D bossEnemy = Raylib.LoadTexture("images/BossEnemy.png");
        public int Speed { get; set; }
        public float attackTimer;
        public bool isHomingAttack;
        public float AttackTime = 0f;
        public float counter = 0;
        public List<Bullet> Bullets { get; set; }
        public BossEnemy(Vector2 position, int width = 1002, int height = 809, int health = 1400, int speed = 2) : base
       (position, width, height, health)
        {
            Speed = speed;
            Bullets = new List<Bullet>();
            attackTimer = 0f;
            isHomingAttack = false;
        }
        public override void Move(Vector2 direction, Vector2 playerPosition)
        {
            Vector2 P = new Vector2(Position.X - Speed, Position.Y);
            direction = new Vector2(playerPosition.X - Position.X, 0);
            if (direction.Length() > 0)
            {
                direction = Vector2.Normalize(direction);

            }
            if (Position.X < 900)
            {
                P = new Vector2(900, Position.Y);
                return;
            }
            Position = P;
            UpdateHitbox();
        }

        public override void Attack(float deltaTime, Vector2 playerPosition)
        {
            attackTimer += deltaTime;
            AttackTime += deltaTime;



            if (attackTimer >= 3.0f && isHomingAttack)
            {
                FireHomingBullets(playerPosition);
                attackTimer = 0f;
                if (counter >= 2)
                {
                    isHomingAttack = !isHomingAttack;
                    counter = 0;
                }



            }
            else if (!isHomingAttack)
            {
                FireThreeWayBullets();
                attackTimer = 0f;
                if (counter >= 15)
                {
                    isHomingAttack = !isHomingAttack;
                    counter = 0;
                }

            }


            foreach (var bullet in Bullets)
            {
                bullet.Move(deltaTime);
            }
            Bullets.RemoveAll(b => !b.IsActive);
        }

        private void FireThreeWayBullets()
        {
            Bullet newBullet1 = new Bullet(new Vector2(Position.X + Hitbox.Width / 2 - 120, Position.Y + Hitbox.Height / 2), 30, new Vector2(-1, 0), 20, 50, 30, false);
            Bullet newBullet2 = new Bullet(new Vector2(Position.X + Hitbox.Width / 2 - 120, Position.Y + Hitbox.Height / 2), 30, new Vector2(-1, -0.5f), 20, 50, 30, false);
            Bullet newBullet3 = new Bullet(new Vector2(Position.X + Hitbox.Width / 2 - 120, Position.Y + Hitbox.Height / 2), 30, new Vector2(-1, 0.5f), 20, 50, 30, false);
            Bullets.Add(newBullet1);
            Bullets.Add(newBullet2);
            Bullets.Add(newBullet3);
            counter++;
        }
        private void FireHomingBullets(Vector2 playerPosition)
        {
            Vector2 direction = playerPosition - Position;
            if (direction.Length() > 0)
            {
                direction = Vector2.Normalize(direction);

            }

            Bullet newBullet = new Bullet(new Vector2(Position.X + Hitbox.Width / 2, Position.Y + Hitbox.Height / 2), 10, direction, 15, 50, 30, true);
            Bullets.Add(newBullet);
            counter++;

        }

        public override void Draw()
        {
            Raylib.DrawTexture(bossEnemy, (int)Position.X, (int)Position.Y, Color.White);
        }
    }
}
