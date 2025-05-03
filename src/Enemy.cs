using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using SpaceWar.src;

namespace SpaceWar
{
    public abstract class Enemy
    {
      public Vector2 Position { get; protected set; }
      public int Health { get; protected set; }
      public Raylib_cs.Rectangle Hitbox;
      public bool IsDead=false;


      public Enemy(Vector2 position, int width, int height, int health)
      {
         Position = position;
         Health = health;
         Hitbox = new Raylib_cs.Rectangle((int)Position.X,(int)Position.Y,width,height);

      }

       
        public abstract void Move(Vector2 direction,Vector2 playerPosition);

        public virtual void TakeDamage(int damage, Spaceship player,int amount,int score)
        {
       
        
           Health -= damage;
           if (Health <= 0) 
           {
               Health = 0;
               IsDead = true;
                    player.TakeExp(amount);
                    player.Score += score;

           }
           
            
          
          
        }

        public abstract void Attack(float deltaTime ,Vector2 playerPosition);
        

        public void UpdateHitbox()
        {
           
            Hitbox.X =(int) Position.X;
            Hitbox.Y =(int) Position.Y;
          
        }

        public abstract void Draw();

    }
}
