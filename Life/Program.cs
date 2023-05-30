using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace cli_life
{
    public class Program
    {
        static Board board;
        static string boardSave;
        static bool isStopped = false;
        static bool isPaused = false;
        public static int countLive;
        static List<Figure> figures;
        static int speed = 1;
        public static List<string> check;

        public static void Reset()
        {
            Config a = new Config(File.ReadAllText("../../../../Life/config.json"));
            board = new Board(
                width: a.width,
                height: a.height,
                cellSize: a.cellSize,
                liveDensity: a.liveDensity);
        }

        public static void Render()
        {
            boardSave = "";
            countLive = 0;
            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Columns; col++)
                {
                    var cell = board.Cells[row, col];
                    if (cell.IsAlive)
                    {
                        Console.Write('*');
                        countLive++;
                        boardSave += '*';
                    }
                    else
                    {
                        Console.Write(' ');
                        boardSave += ' ';
                    }
                }
                Console.Write('\n');
                boardSave += '\n';
            }
        }

        public static int Main()
        {
            Reset();
            Thread checking = new Thread(CheckAsync);
            checking.Start();

            while (!isStopped)
            {
                Console.Clear();
                Render();
                board.Advance();
                Thread.Sleep(1000 / speed);
                while (isPaused)
                    Thread.Sleep(250);
            }
            return 0;
        }

        static void CheckAsync()
        {
            while (!isStopped)
            {
                while (Console.KeyAvailable == false)
                    Thread.Sleep(250);
                ConsoleKeyInfo name = Console.ReadKey();
                if (name.KeyChar == 'q')
                    isStopped = true;
                else if (name.KeyChar == 's')
                    File.WriteAllText("Last system cond.txt", boardSave);
                else if (name.KeyChar == 'l')
                    Load("Last system cond.txt");
                if (name.KeyChar == 'i')
                    Info();
                if (name.KeyChar == 'p')
                {
                    isPaused = !isPaused;
                    Console.WriteLine("Пауза. Чтобы продолжить повторно нажмите p");
                }
                if (name.KeyChar == 'e')
                    speed = (speed == 1) ? 1000 : 1;

            }
        }

        public static void Load(string path)
        {
            string[] buf = File.ReadAllLines("../../../../Life/" + path);
            isPaused = true;
            for (int row = 0; row < board.Rows; row++)
                for (int col = 0; col < board.Columns; col++)
                    board.Cells[row, col].IsAlive = buf[row][col] == '*';
            isPaused = false;
        }

        public static void Info()
        {
            Console.WriteLine("\n\nКоличество живых: " + countLive.ToString());
            ReadFigures();
            isPaused = true;
            bool notFound = false;
            check = new List<string>();
            for (int x = 0; x < board.Rows; x++)
            {
                for (int y = 0; y < board.Columns; y++)
                    for (int k = 0; k < figures.Count; k++)
                    {
                        for (int i = x, matrixX = 0; i < x + figures[k].Rows; i++)
                        {
                            for (int j = y, matrixY = 0; j < y + figures[k].Columns; j++)
                            {
                                if (board.Cells[i < board.Rows ? i : i - board.Rows, j < board.Columns ? j : j - board.Columns].IsAlive ^ figures[k].Matrix[matrixX][matrixY])
                                    notFound = true;
                                matrixY++;
                            }
                            matrixX++;
                        }
                        if (!notFound)
                        {
                            Console.WriteLine($"В точке {x.ToString() + " " + y.ToString()} обнаружена фигура {figures[k].Name}");
                            check.Add($"{x} {y} - {figures[k].Name}");
                        }
                        notFound = false;
                    }
            }
            isPaused = false;
            File.WriteAllLines("Founded figures.txt", check);
        }

        static void ReadFigures()
        {
            string buff = File.ReadAllText("../../../../Life/figures.txt");
            string[] lines = buff.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            figures = new List<Figure>();

            string nameBuf;
            string[] buf;
            int columns;
            int rows;
            string[] matrixBuf;

            for (int i = 0; lines[i] != "-"; i = i + rows + 2)
            {
                buf = lines[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                columns = int.Parse(buf[0]);
                rows = int.Parse(buf[1]);
                nameBuf = lines[i];
                matrixBuf = new string[rows];
                for (int j = 0; j < rows; j++)
                    matrixBuf[j] = lines[i + j + 2];

                figures.Add(new Figure(nameBuf, rows, columns, matrixBuf));
            }
        }
    }
}
