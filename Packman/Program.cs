using System;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace Packman
{
    internal class Program
    {
        private const string filePath = "map.txt";


        static void Main(string[] args)
        {
            var map = ReadFile();
            int Score = 0;
            int x = 1;
            int y = 1;

            Task.Run(() =>
            {
                while (true)
                {
                    SetPoint(ref map);
                    Thread.Sleep(1000);
                }
            });
            ConsoleKeyInfo pressedKey = new  ConsoleKeyInfo('w',ConsoleKey.W,false,false,false);

            while (true)
            {
                RenderGameField(map, x, y, Score);
                HandleInput(Console.ReadKey(), ref x,ref y, map,ref Score);
                Thread.Sleep(10); 
            }
           
        }

        private static void RenderGameField(char[][] map, int x,int y,int Score)
        {
            Console.Clear();
            PrintMap(map);
            PrintPackman(x, y);
            PrintScore(map, Score);
        }
        
        private static void PrintPackman(int x,int y) {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(x, y);
            Console.Write("@");
        }    

        private static void PrintScore(char[][] map, int Score)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(map[0].Length + 1, 0);
            Console.Write("Score:" + Score);
        }

        private static void SetPoint(ref char[][] map)
        {
           var Point = GeneratePoint(map);

            if (map[Point[1]][Point[0]] != '@' && map[Point[1]][Point[0]] != '.' && map[Point[1]][Point[0]] != '#')
                map[Point[1]][Point[0]] = '.';
        }



        private static int[] GeneratePoint(char[][] map)
        {
            Random rnd = new Random();

            var RandomY = (int)rnd.Next(1,map.Length);
            var RandomX = (int)rnd.Next(1, map[RandomY].Length);

            int[] Point = {RandomX, RandomY};

            return Point;
        }


        private static void HandleInput(ConsoleKeyInfo pressedKey,ref int x,ref int y, char[][] map,ref int Score)
        {
            var direction = GetDirection(pressedKey);

            int nextPositionX = x + direction[0];
            int nextPositionY = y + direction[1];

            if (map[nextPositionY][nextPositionX]==' ')
            {
                x = x + direction[0];
                y = y + direction[1];    
            }
            else if(map[nextPositionY][nextPositionX] == '.')
            {
                x = x + direction[0];
                y = y + direction[1];
                map[nextPositionY][nextPositionX] = ' ';
                Score++;
            }
        }



        public static int[]  GetDirection(ConsoleKeyInfo pressedKey) {

            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.RightArrow)
               direction[0]++;
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
                direction[0]--;
            else if (pressedKey.Key == ConsoleKey.DownArrow)
                direction[1]++;
            else if (pressedKey.Key == ConsoleKey.UpArrow)
                direction[1]--;

            return direction;
        }



        private static void PrintMap(char[][] map)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i= 0;i<map.Length;i++)
            {
                for(int j = 0; j < map[i].Length;j++)
                    Console.Write(map[i][j]);
                Console.WriteLine();
            }
            Console.ForegroundColor= ConsoleColor.White;
        }



        private static char[][] ReadFile()
        {
            var str = File.ReadAllLines(filePath);
            return ArrStrToCharMatr(str); 

        }

        private static char[][] ArrStrToCharMatr(string[] str)
        {
            return str.Select(item => item.ToArray()).ToArray();

        }

        private static int SerachMaxLenStr(string[] str) => str.Max(x => x.Length);
        private static int SerachMinLenStr(string[] str) => str.Min(x => x.Length);

    }
}