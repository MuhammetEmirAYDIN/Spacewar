using Raylib_cs;
using SpaceWar.src;
using System.Drawing;
using System.Numerics;
using static Raylib_cs.Raylib;
class Program
{
    static void Main(string[] args)
    {
        Raylib.InitWindow(1500, 1000, "MAIN MENU");
        Texture2D MainMenuBackground = Raylib.LoadTexture("images/MainMenuBackground.png");
        Texture2D Title = Raylib.LoadTexture("images/Title.png");
        Texture2D Enter = Raylib.LoadTexture("images/Enter.png");
        bool Enterpressed = true;
        
        
        
        Raylib.SetTargetFPS(60);

        Texture2D spaceShipTexture =LoadTexture("images/Spaceship.png");
        Texture2D backgroundimage =LoadTexture("images/background.png");
        

            Game game = new Game();
            

            while (!Raylib.WindowShouldClose())
            {
            game.Levelup();
            BeginDrawing();
            float deltaTime = Raylib.GetFrameTime();
            if (Enterpressed)
            {
                ClearBackground(Raylib_cs.Color.White);
                DrawTexture(MainMenuBackground,0,0,Raylib_cs.Color.White);
                DrawTexture(Title, 450, 50, Raylib_cs.Color.White);
                DrawTexture(Enter, 250, 800, Raylib_cs.Color.White);
               
                if (IsKeyPressed(KeyboardKey.Enter))
                {
                    
                    Enterpressed = false;
                    UnloadTexture(MainMenuBackground);
                    UnloadTexture(Enter);
                    UnloadTexture(Title);
                }
            }
            else{
                 
                if (game.GameOver())
                {
                    game.EndGame();
                    break;
                }
                game.Update(deltaTime);



                ClearBackground(Raylib_cs.Color.Black);

                


                Raylib.DrawTexture(backgroundimage, 0, 0, Raylib_cs.Color.White);

                game.StartGame(spaceShipTexture);


                game.SpawnEnemy();

                game.DrawEnemies();
                game.UpdateEnemy(deltaTime);

               


                

            }
            Raylib.EndDrawing();
            
        }
        Raylib.UnloadTexture(spaceShipTexture);

        
    }
}