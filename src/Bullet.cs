using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceWar.src
{
    public class Bullet
    {
        public int Speed { get; set; }
        public int Damage { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }

        public Raylib_cs.Rectangle Hitbox;

        public bool IsActive { get; set; }

        public bool IsHoming { get; set; }

        public float lifetime { get; set; }
        private float maxlifetime;
        public Bullet(Vector2 startPosition, int speed, Vector2 direction, int damage, int width, int height, bool isHoming, float Maxlifetime = 4f)
        {
            Position = startPosition;
            Speed = speed;
            Damage = damage;
            Direction = direction;
            Hitbox = new Raylib_cs.Rectangle((int)Position.X, (int)Position.Y, width, height);
            IsActive = true;
            IsHoming = isHoming;
            lifetime = 0;
            maxlifetime = Maxlifetime;
        }
        public void UpdateDirection(Vector2 Target)
        {
            if (IsHoming)
            {
                Vector2 newDirection = Target - Position;
                if (newDirection.Length() > 0)
                {
                    newDirection = Vector2.Normalize(newDirection);
                }
                float homingFactor = Math.Max(0, 1 - lifetime / maxlifetime);
                Direction = Vector2.Lerp(Direction, newDirection, homingFactor);
            }
        }

        public void Move(float deltaTime)
        {
            Position += Direction * Speed;
            lifetime += deltaTime;

            Hitbox.X = (int)Position.X;
            Hitbox.Y = (int)Position.Y;
            if (Position.X < 0 || Position.X > Raylib.GetScreenWidth() ||
       Position.Y < 0 || Position.Y > Raylib.GetScreenHeight())
            {
                IsActive = false;
            }
        }

        public void OnHit()
        {
            IsActive = false;
        }
    }


}
