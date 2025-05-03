using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceWar.src
{
    public class Spaceship
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }

        public Rectangle Hitbox;
        public List<Bullet> Bullets { get; set; }
        public Vector2 position;
        public float shootCooldown { get; set; }
        private float shootTimer = 0f;
        private float InvulTime = 2f;
        private float InvulTimer = 0f;
        public int Exp = 0;
        public int MaxExp = 100;

        public int Score { get; set; }

        public Vector2 Position
        {
            get => position;
            set
            {
                position = new Vector2(
                    Math.Clamp(value.X, 0, Raylib.GetScreenWidth() - 100),
                    Math.Clamp(value.Y, 0, Raylib.GetScreenHeight() - 100)
                );
            }
        }

        public Spaceship(int score = 0, float ShootCooldown = 0.5f, int height = 123, int width = 115, int health = 50, int damage = 10, int speed = 30)
        {
            Health = health;
            Damage = damage;
            Speed = speed;
            Bullets = new List<Bullet>();
            position = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2);
            Hitbox = new Rectangle(Position.X, Position.Y, height, width);
            shootCooldown = ShootCooldown;
            Score = score;


        }
        public void Move(Vector2 direction)
        {
            if (direction.Length() > 0)
            {
                direction = Vector2.Normalize(direction);
            }

            Position += direction * Speed;

            Hitbox.X = Position.X;
            Hitbox.Y = Position.Y;


        }

        public void Shoot()
        {
            if (shootTimer >= shootCooldown)
            {
                Bullet newBullet = new Bullet(new Vector2(Position.X + Hitbox.Width / 2, Position.Y + Hitbox.Height / 2), 10, new Vector2(1, 0), Damage, 25, 10, false);
                Bullets.Add(newBullet);
                shootTimer = 0f;


            }


        }

        public void UpdateInvul(float deltaTime)
        {
            if (InvulTimer > 0)
                InvulTimer -= deltaTime;
        }

        public bool IsInvul()
        {
            return InvulTimer > 0f;
        }

        public void TrigInvul()
        {
            InvulTimer = 2f;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;

        }
        public void UpdateShootTimer(float time)
        {
            shootTimer += time;
        }
        public void TakeExp(int amount)
        {
            Exp += amount;


        }
    }
}
