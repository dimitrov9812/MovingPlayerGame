using System;
using System.Collections.Generic;

namespace MovingGame
{


    class Program
    {
        //Atributes
        static int playerPosX = 1, playerPosY = 1;
        static int enemyPosX, enemyPosY;
        static int itemPosX, itemPosY;
        static int length = 50;
        static int height = 20;
        static char player = '*';
        static char space = '=';
        static char enemy = 'o';
        static char item = '$';
        static int score = 0;
        static int highscore = 0;
        static int level = 1;
        static int maximumLevel = 0;
        static int lives = 5;
        static string choice;
        static int foregroundColor = 15;
        static int backgroundColor = 0;
        static Random random = new Random();
        //Main game
        static void Main(string[] args)
        {
            
            Console.Title = "RUN!";
            Menu();
        }
        //Play the game
        static void Play()
        {
            playerPosX = 1;
            playerPosY = 1;
            score = 0;
            enemyPosX = length;
            enemyPosY = height;
            bool runGame = true;
            CreateItem();
            while (runGame)
            {
                
                Draw();

                if (PlayerCollideWithEnemy())
                {
                    lives -= 1;
                    if (lives == 0)
                    {
                        runGame = false;
                    }
                    runGame = false;
                }
                if (PlayerCollideWithItem())
                {
                    score += 1;
                    CreateItem();
                    Draw();

                }

                ConsoleKeyInfo keyPressed = Console.ReadKey();
                if ((keyPressed.Key == ConsoleKey.W && playerPosY != 1) || (keyPressed.Key == ConsoleKey.S && playerPosY != height))
                {
                    playerPosY += (keyPressed.Key == ConsoleKey.S) ? 1 : -1;
                }
                if ((keyPressed.Key == ConsoleKey.A && playerPosX != 1) || (keyPressed.Key == ConsoleKey.D && playerPosX != length))
                {
                    playerPosX += (keyPressed.Key == ConsoleKey.D) ? 1 : -1;
                }
                MoveEnemy();
            }
        }
        //Menu
        static void Menu()
        {
            Console.WriteLine(" ____________________");
            Console.WriteLine("|Welcome to my game! |");
            Console.WriteLine("|------------------  |");
            Console.WriteLine("|What you want to do?|");
            Console.WriteLine("|                    |");
            Console.WriteLine("|Play-1              |");
            Console.WriteLine("|Settings-2          |");
            Console.WriteLine("|Exit-3              |");
            Console.WriteLine("|____________________|");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    bool playing = true;
                    while (playing)
                    {
                        Console.ForegroundColor = (ConsoleColor)foregroundColor;
                        Console.BackgroundColor = (ConsoleColor)backgroundColor;
                        

                        Play();
                        if (score > highscore)
                        {                            
                                highscore = score;                        
                        }
                        Console.WriteLine("______________________");
                        Console.WriteLine("You have been eaten! ;");
                        Console.WriteLine("FINAL SCORE: " + score);
                        Console.WriteLine("HIGHEST SCORE: " + highscore);
                        Console.WriteLine("______________________");
                        

                        if (lives != 0)
                        { 
                            Console.WriteLine("Do you want to play again? (Y/N): ");
                            Console.WriteLine("Press 'M' if you want to go to the menu");
                            Console.WriteLine("Press 'S' if you want to go to the settings");
                            ConsoleKeyInfo keyPressed = Console.ReadKey();
                            if (keyPressed.Key == ConsoleKey.N) playing = false;
                            else if (keyPressed.Key == ConsoleKey.M)
                            {
                                Console.Clear();
                                Menu();
                            }
                            else if (keyPressed.Key == ConsoleKey.S)
                            {
                                Settings();
                            }
                            else if (keyPressed.Key == ConsoleKey.Y)
                            {
                                length = random.Next(10, 40);
                                height = random.Next(5, 15);
                                level++;

                            }
                        }
                        else
                        {
                            Console.Clear();
                            Menu();
                        }
                        
                        
                        
                    }
                    break;
                case "2":
                    Settings();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;

                default:
                    break;
            }
        }
        //Settings
        static void Settings()
        {
            Console.Clear();
            Console.WriteLine("___________________________________");
            Console.WriteLine("|Choose color for the background -1|");
            Console.WriteLine("|Choose color for the text - 2     |");
            Console.WriteLine("|Back to menu-3                    |");
            Console.WriteLine("|__________________________________|");
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine(" __________________________________________");
                    Console.WriteLine("|Black-0                                   |");
                    Console.WriteLine("|Gray-7                                    |");
                    Console.WriteLine("|Red-12                                    |");
                    Console.WriteLine("|White-15                                  |");
                    Console.WriteLine("|What color do you want for the background?|");
                    Console.WriteLine("|__________________________________________|");
                    Console.Write("Enter the code: ");
                    
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "0":
                            backgroundColor = 0;
                            Console.Clear();
                            Menu();
                            break;
                        case "7":
                            backgroundColor = 7;
                            Console.Clear();
                            Menu();
                            break;
                        case "12":
                            backgroundColor = 12;
                            Console.Clear();
                            Menu();
                            break;
                        case "15":
                            backgroundColor = 15;
                            Console.Clear();
                            Menu();
                            break;

                    }
                        
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine(" __________________________________________");
                    Console.WriteLine("|Black-0                                   |");
                    Console.WriteLine("|Gray-7                                    |");
                    Console.WriteLine("|Red-12                                    |");
                    Console.WriteLine("|White-15                                  |");
                    Console.WriteLine("|What color do you want for the background?|");
                    Console.WriteLine("|__________________________________________|");
                    Console.Write("Enter the code: ");
                    choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "0":
                            foregroundColor = 0;
                            Console.Clear();
                            Menu();
                            break;
                        case "7":
                            foregroundColor = 7;
                            Console.Clear();
                            Menu();
                            break;
                        case "12":
                            foregroundColor = 12;
                            Console.Clear();
                            Menu();
                            break;
                        case "15":
                            foregroundColor = 15;
                            Console.Clear();
                            Menu();
                            break;
                    }
                    break;
                case "3":
                    Console.Clear();
                    Menu();
                    break;


            }
            
        }
        //PlayerItemCollision
        static bool PlayerCollideWithItem()
        {
            if (playerPosX == itemPosX && playerPosY == itemPosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Items
        static void CreateItem()
        {
            int x = random.Next(1, length);
            int y = playerPosY;
            while (y > playerPosX - 2 && y < playerPosY + 2)
            {
                y = random.Next(1, height);
            }
            itemPosX = x;
            itemPosY = y;

        }
        //Move the emeny
        static void MoveEnemy()
        {
            if (random.Next(0, 11) < 3) return;
            if ((random.Next(0, 11) >= 5 && playerPosX != enemyPosX) || playerPosY == enemyPosY)//x
            {
                if (playerPosX < enemyPosX) --enemyPosX;
                else if (playerPosX > enemyPosX) ++enemyPosX;
            }
            else//y
            {
                if (playerPosY < enemyPosY) --enemyPosY;
                else if (playerPosY > enemyPosY) ++enemyPosY;
            }


        }
        //Player-Enemy-Collision
        static bool PlayerCollideWithEnemy()
        {
            if (playerPosX == enemyPosX && playerPosY == enemyPosY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Draw
        static void Draw()
        {

            Console.Clear();

                Console.WriteLine("LEVEL: "+level);
                Console.WriteLine("Lives: " + lives);
                Console.WriteLine(playerPosX + ":" + playerPosY);
                Console.WriteLine(enemyPosX + ":" + enemyPosY);
                Console.WriteLine("Score: " + score);
                for (int y = 1; y <= height; ++y)
                {
                    for (int x = 1; x <= length; ++x)
                    {
                        if (x == playerPosX && y == playerPosY)
                        {
                            Console.Write(player);
                        }
                        else if (x == enemyPosX && y == enemyPosY)
                        {
                            Console.Write(enemy);
                        }
                        else if (x == itemPosX && y == itemPosY)
                        {
                            Console.Write(item);
                        }
                        else
                        {
                            Console.Write(space);
                        }



                    }
                    Console.WriteLine();
                }
            
        }
    }
}
