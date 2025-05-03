using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace SpaceWar.src
{
    internal class CollisionDetector
    {
        public static bool CheckCollision(Spaceship player, Enemy enemy)
        {
            return Raylib.CheckCollisionRecs(player.Hitbox, enemy.Hitbox);
        }

        public static void CheckBulletCollision(List<Bullet> bullets, List<Enemy> enemy, Spaceship player, int amount, int score)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = bullets[i];


                for (int j = enemy.Count - 1; j >= 0; j--)
                {
                    Enemy enemies = enemy[j];
                    if (Raylib.CheckCollisionRecs(bullet.Hitbox, enemies.Hitbox))
                    {
                        enemies.TakeDamage(bullet.Damage, player, amount, score);
                        bullets.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
