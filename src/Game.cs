using Raylib_cs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace SpaceWar.src
{
    internal class Game
    {
        private Spaceship player;
        public List<Enemy> enemies;
        private bool isGameOver;
        public Vector2 direction = new Vector2(-1, 0);
        private float enemyspawnTimer;
        private float spawnTimer;
        private bool Collisionflag = false;
        private float TotalTime = 0f;
        bool LevelUp = false;
        int level = 0;
        private bool BossSpawned = false;
        public Game()
        {
            player = new Spaceship();
            enemies = new List<Enemy>();
            isGameOver = false;
            enemyspawnTimer = 0f;


        }


        public void Update(float time)
        {
            Vector2 direction = new Vector2();
            if (Raylib.IsKeyDown(KeyboardKey.W)) direction.Y -= 1; // Up
            if (Raylib.IsKeyDown(KeyboardKey.S)) direction.Y += 1; // Down
            if (Raylib.IsKeyDown(KeyboardKey.A)) direction.X -= 1; // Left
            if (Raylib.IsKeyDown(KeyboardKey.D)) direction.X += 1; // Right
            player.Move(direction);

            player.UpdateShootTimer(time);
            player.UpdateInvul(time);


            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                player.Shoot();
            }
            foreach (var bullet in player.Bullets)
            {
                bullet.Move(time);
            }

            TotalTime += Raylib.GetFrameTime();
            player.Bullets.RemoveAll(b => !b.IsActive);
        }

        public void SpawnEnemy()
        {
            Random random = new Random();

            int y = random.Next(10, 800);
            if (enemyspawnTimer >= 2.0f && TotalTime < 10f)
            {

                enemies.Add(new BasicEnemy(new Vector2(1500, y)));
                enemyspawnTimer = 0;


            }
            else if (enemyspawnTimer >= 2.0f && TotalTime >= 10.0f && TotalTime < 20.0f)
            {
                enemies.Add(new FastEnemy(new Vector2(1500, y)));
                enemyspawnTimer = 0;

            }
            else if (enemyspawnTimer >= 2.0f && TotalTime >= 20.0f && TotalTime < 40.0f)
            {
                enemies.Add(new StrongEnemy(new Vector2(1500, y)));
                enemyspawnTimer = 0;

            }
            else if (TotalTime >= 40.0f && !BossSpawned)
            {
                enemies.Add(new BossEnemy(new Vector2(1500, Raylib.GetScreenHeight() / 2 - 440)));
                BossSpawned = true;


            }
            else if (TotalTime >= 60.0f && enemyspawnTimer >= 2.0f)
            {

                enemies.Add(new BasicEnemy(new Vector2(1500, y)));
                enemies.Add(new FastEnemy(new Vector2(1500, y)));
                enemies.Add(new StrongEnemy(new Vector2(1500, y)));
                enemies.Add(new BasicEnemy(new Vector2(1500, y)));
                enemyspawnTimer = 0;
            }

            else if (enemyspawnTimer >= 2.0f && TotalTime >= 80.0f)
            {
                enemies.Add(new FastEnemy(new Vector2(1500, y)));
                enemies.Add(new StrongEnemy(new Vector2(1500, y)));
                enemies.Add(new FastEnemy(new Vector2(1500, y)));
                enemies.Add(new StrongEnemy(new Vector2(1500, y)));
                enemyspawnTimer = 0;
                BossSpawned = false;
            }

            else if (enemyspawnTimer >= 2.0f && TotalTime >= 95.0f && TotalTime <= 105.0f)
            {

                enemies.Add(new FastEnemy(new Vector2(1500, y)));
                enemies.Add(new StrongEnemy(new Vector2(1500, y)));
                enemies.Add(new BossEnemy(new Vector2(1500, Raylib.GetScreenHeight() / 2 - 440)));
                BossSpawned = true;
                enemyspawnTimer = 0;

            }

        }
        public void Increasebulletspeed()
        {
            player.shootCooldown -= 0.1f;
        }

        public void IncreaseDamage()
        {
            player.Damage += 5;
        }

        public void IncreaseSpeed()
        {
            player.Speed += 5;
        }
        public void Levelup()
        {
            if (player.Exp >= player.MaxExp)
            {

                player.Exp = 0;
                level++;
                if (level >= 5)
                {
                    player.MaxExp = 500;
                }
                Rectangle button1 = new Rectangle(400, 150, 700, 50);
                Rectangle button2 = new Rectangle(400, 400, 700, 50);
                Rectangle button3 = new Rectangle(400, 700, 700, 50);
                bool choice = false;

                while (!choice)
                {
                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.DarkPurple);
                    Raylib.DrawText("CHOOSE A POWER UP!!", 400, 50, 70, Color.White);

                    Raylib.DrawRectangleRec(button1, Color.DarkGreen);
                    Raylib.DrawText("+5 bullet Speed increase", (int)button1.X + 10, (int)button1.Y + 15, 20, Color.Black);
                    Raylib.DrawRectangleRec(button2, Color.DarkGreen);
                    Raylib.DrawText("+5 bullet Damage increase", (int)button2.X + 10, (int)button2.Y + 15, 20, Color.Black);
                    Raylib.DrawRectangleRec(button3, Color.DarkGreen);
                    Raylib.DrawText("+5 Speed increase", (int)button3.X + 10, (int)button3.Y + 15, 20, Color.Black);


                    if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                    {
                        Vector2 mousepos = Raylib.GetMousePosition();
                        if (Raylib.CheckCollisionPointRec(mousepos, button1))
                        {
                            Increasebulletspeed();
                            choice = true;
                        }
                        else if (Raylib.CheckCollisionPointRec(mousepos, button2))
                        {
                            IncreaseDamage();
                            choice = true;
                        }
                        else if (Raylib.CheckCollisionPointRec(mousepos, button3))
                        {
                            IncreaseSpeed();
                            choice = true;
                        }
                    }
                    Raylib.EndDrawing();
                }
            }
        }


        public void UpdateEnemy(float Time)
        {
            Collisionflag = false;
            enemyspawnTimer += Time;
            if (enemyspawnTimer >= 2.0f)
            {
                SpawnEnemy();
                enemyspawnTimer = 0;

            }

            foreach (var enemy in enemies)
            {
                if (!enemy.IsDead)
                    enemy.Move(Vector2.Zero, player.Position);
                if (CollisionDetector.CheckCollision(player, enemy) && !player.IsInvul())
                {
                    if (enemy is BasicEnemy basicEnemy)
                    {
                        enemy.TakeDamage(player.Damage, player, 10, 100);

                    }
                    else if (enemy is FastEnemy fastEnemy)
                    {
                        enemy.TakeDamage(player.Damage, player, 20, 200);

                    }
                    else if (enemy is StrongEnemy StrongEnemy)
                    {
                        enemy.TakeDamage(player.Damage, player, 30, 400);

                    }
                    else if (enemy is BossEnemy bossenemy)
                    {
                        enemy.TakeDamage(player.Damage, player, 30, 1000);

                    }
                    player.TakeDamage(15);
                    player.TrigInvul();
                    Collisionflag = true;

                }
                if (enemy is StrongEnemy strongEnemy)
                {
                    strongEnemy.Attack(Time, player.Position);
                    foreach (var bullet in strongEnemy.Bullets)
                    {

                        if (Raylib.CheckCollisionRecs(bullet.Hitbox, player.Hitbox))
                        {
                            player.TakeDamage(bullet.Damage);
                            player.TrigInvul();
                            bullet.OnHit();

                        }
                    }
                    foreach (var bullet in strongEnemy.Bullets)
                    {
                        Raylib.DrawRectangle((int)bullet.Position.X, (int)bullet.Position.Y, 50, 30, Color.Red);
                    }
                }
                if (enemy is BossEnemy BossEnemy)
                {
                    BossEnemy.Attack(Time, player.Position);

                    foreach (var bullet in BossEnemy.Bullets)
                    {
                        bullet.UpdateDirection(player.Position);
                        if (Raylib.CheckCollisionRecs(bullet.Hitbox, player.Hitbox))
                        {
                            player.TakeDamage(bullet.Damage);
                            player.TrigInvul();
                            bullet.OnHit();

                        }
                    }
                    foreach (var bullet in BossEnemy.Bullets)
                    {
                        Raylib.DrawRectangle((int)bullet.Position.X, (int)bullet.Position.Y, 50, 30, Color.Red);
                    }
                }
            }
            foreach (var enemy in enemies)
            {
                if (enemy is BasicEnemy basicEnemy)
                {
                    CollisionDetector.CheckBulletCollision(player.Bullets, enemies, player, 10, 100);
                }
                else if (enemy is FastEnemy fastEnemy)
                {
                    CollisionDetector.CheckBulletCollision(player.Bullets, enemies, player, 20, 200);

                }
                else if (enemy is StrongEnemy StrongEnemy)
                {
                    CollisionDetector.CheckBulletCollision(player.Bullets, enemies, player, 30, 400);
                }
                else if (enemy is BossEnemy bossEnemy)
                {
                    CollisionDetector.CheckBulletCollision(player.Bullets, enemies, player, 100, 1000);
                }
            }


            enemies.RemoveAll(e => e.IsDead);
        }

        public void DrawEnemies()
        {
            foreach (var enemy in enemies)
            {

                enemy.Draw();
            }
        }
        public void StartGame(Texture2D spaceShipTexture)
        {
            Vector2 spaceshipPosition = player.Position;
            Raylib.DrawTexture(spaceShipTexture, (int)spaceshipPosition.X, (int)spaceshipPosition.Y, Color.White);

            foreach (var bullet in player.Bullets)
            {

                Raylib.DrawRectangle((int)bullet.Position.X, (int)bullet.Position.Y, 25, 10, Color.White);

            }


        }

        public bool GameOver()
        {
            return player.Health <= 0 || TotalTime >= 105.0f;



        }
        public void EndGame()
        {

            string filePath = "score.txt";

            using (StreamWriter write = new StreamWriter(filePath, true))
            {
                write.WriteLine($"Score:{player.Score}");
            }
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.Black);


                Raylib.DrawText("GAME OVER", Raylib.GetScreenWidth() / 2 - 150, Raylib.GetScreenHeight() / 2 - 100, 50, Color.Red);


                Raylib.DrawText($"Your Score: {player.Score}", Raylib.GetScreenWidth() / 2 - 150, Raylib.GetScreenHeight() / 2, 30, Color.White);


                Raylib.DrawText("Press ESC to Exit", Raylib.GetScreenWidth() / 2 - 150, Raylib.GetScreenHeight() / 2 + 50, 20, Color.Gray);

                Raylib.EndDrawing();


                if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                {
                    Raylib.CloseWindow();
                    break;
                }
            }

        }
    }
}
