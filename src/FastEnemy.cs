using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWar
{
    public class FastEnemy:Enemy
    {
        Texture2D fastEnemy = Raylib.LoadTexture("images/FastEnemy.png");
        private bool IsAvoid;
        private float Timer;
        private float duration = 2.0f;
        private float RapidTimer;
        public int Speed { get; set; }

        public FastEnemy(Vector2 Position,int width=91, int height=93 ,int health=20, int speed = 15):base
        (Position,width,height,health)
        {
         Speed = speed;
         IsAvoid = true;
         Timer= 0f;
         RapidTimer = 0f; 

        }


        public override void Move(Vector2 direction, Vector2 playerPosition)
        {
            Timer += Raylib.GetFrameTime();
            if (Timer >= duration) 
            {
                IsAvoid = !IsAvoid;
                Timer = 0f;
            }

            if(!IsAvoid){
                direction = new Vector2(playerPosition.X - Position.X, playerPosition.Y - Position.Y);
                if (direction.Length() > 0)
                {
                    direction = Vector2.Normalize(direction);

                }

                Position += direction * Speed;
            }
            else{
                direction = new Vector2(Position.X - playerPosition.X, 0);
                RapidTimer += Raylib.GetFrameTime()*15;
                float rapid=(float)Math.Sin(RapidTimer)*6000;
                Position= new Vector2(Position.X + (direction.Length() > 0 ? Speed : 0) * Raylib.GetFrameTime(),Position.Y+rapid*Raylib.GetFrameTime());
            }
                

            
            UpdateHitbox();

        }
        public override void Draw()
        {
            Raylib.DrawTexture(fastEnemy, (int)Position.X, (int)Position.Y, Color.White);
        }

        public override void Attack(float deltatime,Vector2 playerPosition)
        {
            
        }
    }
}
