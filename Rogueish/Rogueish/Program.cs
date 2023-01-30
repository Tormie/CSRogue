using System;
using System.Threading;
using System.Collections.Generic;

namespace Rogueish
{
    class Program
    {
        static ConsoleKeyInfo consoleKeyInfo;
        public static Utils ut = new Utils();
        public static char[,] levelMap = new char[64, 32];
        public static string[] combatScreen = new string[32];
        public static char[] enemyCharList = { 'A', 'B', 'C', 'D', 'E', 'G', 'O' };
        public static RoguePlayer player;
        public static List<RogueEnemy> enemyList = new List<RogueEnemy>();
        public static string actionLog;
        public static string guiLine;
        public static string optionsLine;
        static ConsoleColor bgDarkColor = ConsoleColor.Black;
        static ConsoleColor fgDarkColor = ConsoleColor.White;
        static ConsoleColor bgLightColor = ConsoleColor.Yellow;
        static ConsoleColor fgLightColor = ConsoleColor.Black;
        static void Main(string[] args)
        {
            actionLog = "";
            GenerateMap();
            SpawnEnemies();
            player = new RoguePlayer(1, 1, "Niels", 5,'@');
            guiLine = "Level: " + player.charLevel + " Gold: " + player.gold + " HP: " + player.hp + "/" + player.maxhp + " XP: " + player.charXP + "/" + player.reqXP;
            optionsLine = "Actions: (" + player.skills[0] + ") Healing potion. (" + player.skills[1] + ") Bash. (" + player.skills[2] + ") Jump. (" + player.skills[3] + ") Rest.";
            levelMap[player.xPos, player.yPos] = '@';
            PrintMap();
            while(true)
            {
                GetPlayerInput();
            }
            //ScreenTransition();
            //Console.ReadLine();
        }

        static void PrintData(string[] input)
        {
            foreach (string s in input)
            {
                Console.WriteLine(s);
            }
        }

        static void GenerateMap()
        {
            for (int i = 0; i < 64; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    if (j == 0 || j == 31)
                    {
                        levelMap[i, j] = '-';
                    }
                    else if (i == 0 || i == 63)
                    {
                        levelMap[i, j] = '|';
                    } else
                    {
                        levelMap[i, j] = '.';
                    }
                }
            }
            
        }

        static void SpawnEnemies()
        {
            enemyList.Add(new RogueEnemy(24, 12, "Goblin", 1, 'G'));
            enemyList.Add(new RogueEnemy(24, 24, "Orc", 1, 'O'));
        }

        public static void PrintMap()
        {
            guiLine = "Level: " + player.charLevel + " Gold: " + player.gold + " HP: " + player.hp + "/" + player.maxhp + " XP: " + player.charXP + "/" + player.reqXP;

            Console.Clear();
            for (int j = 0; j < 32; j++)
            {
                for (int i = 0; i < 64; i++)
                {
                    if (ut.CalculateDistance(i,j) <= player.viewDistance)
                    {
                        Console.BackgroundColor = bgLightColor;
                        Console.ForegroundColor = fgLightColor;
                        Console.Write(levelMap[i, j]);
                    } else
                    {
                        Console.BackgroundColor = bgDarkColor;
                        Console.ForegroundColor = fgDarkColor;
                        Console.Write(".");
                    }
                    
                    if (i == 63)
                    {
                        Console.Write("\n");
                    }
                }
            }
            Console.WriteLine(guiLine);
            Console.WriteLine(optionsLine);
            Console.WriteLine(actionLog);
        }

        static void TakeTurn(int playerDir)
        {
            actionLog = "";
            switch (playerDir)
            {
                case 1:
                    if (player.yPos < 31)
                    {
                        player.MovePlayer(0, 1);
                    }
                    break;
                case 2:
                    if (player.yPos > 0)
                    {
                        player.MovePlayer(0, -1);
                    }
                    break;
                case 3:
                    if (player.xPos > 0)
                    {
                        player.MovePlayer(-1, 0);
                    }
                    break;
                case 4:
                    if (player.xPos < 63)
                    {
                        player.MovePlayer(1, 0);
                    }
                    break;
            }
            Thread.Sleep(100);
        }

        static void MoveEnemies()
        {
            foreach (RogueEnemy e in enemyList)
            {
                e.MoveEnemy();
            }
        }
        
        static void GetPlayerInput()
        {
            consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.DownArrow)
            {
                TakeTurn(1);
                MoveEnemies();
                PrintMap();
            }
            if (consoleKeyInfo.Key == ConsoleKey.UpArrow)
            {
                TakeTurn(2);
                MoveEnemies();
                PrintMap();
            }
            if (consoleKeyInfo.Key == ConsoleKey.LeftArrow)
            {
                TakeTurn(3);
                MoveEnemies();
                PrintMap();
            }
            if (consoleKeyInfo.Key == ConsoleKey.RightArrow)
            {
                TakeTurn(4);
                MoveEnemies();
                PrintMap();
            }
            if (consoleKeyInfo.Key == ConsoleKey.H)
            {
                actionLog = "";
                player.UsePotion();
                MoveEnemies();
                PrintMap();
            }
            
        }

        static void ScreenTransition()
        {
            int i = 0;
            string wavyLine = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~";
            string straightLine = "----------------------------------------------------------------";
            while (i < 50)
            {
                Console.Clear();
                if (i % 2 == 0)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.WriteLine(wavyLine);
                        } else
                        {
                            Console.WriteLine(straightLine);
                        }
                    }
                } else
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.WriteLine(straightLine);
                        }
                        else
                        {
                            Console.WriteLine(wavyLine);
                        }
                    }
                }
                Thread.Sleep(10);
                i++;
            }
        }
    }
}
