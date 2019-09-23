using System;
using System.Collections;

namespace SnakeGame
{
    class Program
    {
        //定义游戏地图的大小
        const int MapLength = 48;
        const int MapHeight = 27;
        private static int[,] GameMap = new int[MapHeight, MapLength];
        //定义方向
        static int direction = 0;
        //定义Snake的各个点
        public static ArrayList SnakePoints = new ArrayList();
        //snake的长度
        public static int SnakeLength = 1;
        //食物的位置
        static Point foodpoint = new Point(1, 1);
        //游戏结束标志
        static int gameoverflag = 0;
        //分数
        static int score = 0;

        class Point
        {
            public int x;
            public int y;
            public Point(int a1, int a2)
            {
                x = a1;
                y = a2;
            }
        }

        //绘制带字符的一行
        static void PaintLine(string s, int begin)
        {
            //输入：s——要打印的字符串
            //     begin——打印开始的位置
            Console.Write('█');
            for (int j = 1; j < MapLength - 1;)
            {
                if (j == begin)
                {
                    Console.Write(s);
                    if (s.Length % 2 == 0)
                    {
                        j += s.Length / 2;
                    }
                    else
                    {
                        Console.Write(" ");
                        j += ((s.Length + 1) / 2);
                    }
                }
                else
                {
                    Console.Write("  ");
                    j++;
                }
            }
            Console.Write('█');
        }

        //绘制空行
        static void PaintLine()
        {
            Console.Write('█');
            for (int j = 1; j < MapLength - 1;)
            {
                Console.Write("  ");
                j++;
            }
            Console.Write('█');
        }

        //绘制边框行
        static void PaintLineUpDown()
        {
            for (int j = 0; j < MapLength; j++)
            {
                Console.Write('█');
            }
        }

        //画前
        static void GetBefore(Point p)
        {
            switch (direction)
            {
                case 1:
                    {
                        p.x -= 1;
                        break;
                    }
                case 2:
                    {
                        p.x += 1;
                        break;
                    }
                case 3:
                    {
                        p.y -= 1;
                        break;
                    }
                case 4:
                    {
                        p.y += 1;
                        break;
                    }
            }
            SnakePoints[0] = p;
            GameMap[p.x, p.y] = 2;
            Console.SetCursorPosition(p.y * 2, p.x);
            Console.Write("□");
        }

        //删后
        static void DelAfter(Point endp)
        {
            GameMap[endp.x, endp.y] = 0;
            Console.SetCursorPosition(endp.y * 2, endp.x);
            Console.Write("  ");
            Console.SetCursorPosition(0, MapHeight);
        }

        //Snake点的替代
        static void PointChange()
        {
            if (SnakeLength != 1)
            {
                for (int i = 1; i < SnakeLength; i++)
                {
                    SnakePoints[SnakeLength - i] = SnakePoints[SnakeLength - i - 1];
                }
            }
        }

        //空白处理
        static void NullProgress(Point p)
        {
            //记录最后点
            Point endp = (Point)SnakePoints[SnakeLength - 1];
            //Snake 对应的各点替代
            PointChange();
            //画前
            GetBefore(p);
            //删后
            DelAfter(endp);
        }

        //食物处理
        static void FoodProgress(Point p)
        {
            SnakeLength++;
            SnakePoints.Add(p);
            //Snake 对应的各点替代
            PointChange();
            //画前
            GetBefore(p);
            //产生新食物
            foodpoint = FoodGene();
            GameMap[foodpoint.x, foodpoint.y] = 3;
            ShowScore();
        }

        //初始化游戏
        static void Init()
        {
            string gamename = "SNAKE GAME";
            string welcomeword = "Press 'Enter' to start game.";
            //构造地图
            for (int i = 0; i < MapHeight; i++)
            {
                for (int j = 0; j < MapLength; j++)
                {
                    if (j == 0 || i == 0 || i == MapHeight - 1 || j == MapLength - 1)
                    {
                        GameMap[i, j] = 1;
                    }
                }
            }
            //绘制初始界面
            for (int i = 0; i < MapHeight; i++)
            {
                if (i == 0 || i == 26)
                {
                    PaintLineUpDown();
                }
                else
                {
                    if (i == 12)
                        PaintLine(gamename, 20);
                    else if (i == 15)
                        PaintLine(welcomeword, 16);
                    else
                        PaintLine();
                }
                Console.WriteLine();
            }
            //等待输入回车键
            while (Console.Read() != 13)
            {
            }
            //将Snake位置载入地图
            GameMap[MapHeight / 2, MapLength / 2] = 2;
            Point p = new Point(MapHeight / 2, MapLength / 2);
            SnakePoints.Add(p);
            //出现初始画面
            Console.Clear();
            for (int i = 0; i < MapHeight; i++)
            {
                if (i == 0 || i == 26)
                {
                    PaintLineUpDown();
                }
                else
                    PaintLine();
                Console.WriteLine();
            }
            Console.SetCursorPosition(p.y * 2, p.x);
            Console.Write("□");
            Console.SetCursorPosition(MapLength * 2 + 4, MapHeight / 2);
            Console.Write("Score");
            Console.SetCursorPosition(MapLength * 2 + 4, MapHeight / 2 + 1);
            Console.Write(score);
            foodpoint = FoodGene();
            GameMap[foodpoint.x, foodpoint.y] = 3;
        }

        //游戏的方案
        static void RealTimeLogic()
        {
            Console.SetCursorPosition(0, MapHeight);
            ConsoleKeyInfo Key;
            if (Console.KeyAvailable)
            {
                Key = Console.ReadKey();
                //针对输入进行处理
                if (Key.KeyChar == 'W' || Key.KeyChar == 'w')
                    //不能反向走
                    if (SnakeLength == 1 || direction != 2)
                        direction = 1;
                    else { }
                else if (Key.KeyChar == 'S' || Key.KeyChar == 's')
                    if (SnakeLength == 1 || direction != 1)
                        direction = 2;
                    else { }
                else if (Key.KeyChar == 'A' || Key.KeyChar == 'a')
                    if (SnakeLength == 1 || direction != 4)
                        direction = 3;
                    else { }
                else if (Key.KeyChar == 'D' || Key.KeyChar == 'd')
                    if (SnakeLength == 1 || direction != 3)
                        direction = 4;
                    else { }
                else { }
            }
            Point p = new Point(((Point)SnakePoints[0]).x, ((Point)SnakePoints[0]).y);
            switch (direction)
            {
                //向上走
                case 1:
                    //确定下一个位置是什么
                    switch (GameMap[p.x - 1, p.y])
                    {
                        //1或2游戏结束
                        case 1:
                        case 2:
                            GameOver();
                            return;
                        //3说明前面有食物
                        case 3:
                            {
                                FoodProgress(p);
                                break;
                            }
                        //前面是空的
                        case 0:
                            {
                                NullProgress(p);
                                break;
                            }
                    }
                    break;
                //向下走
                case 2:
                    switch (GameMap[p.x + 1, p.y])
                    {
                        //1或2游戏结束
                        case 1:
                        case 2:
                            GameOver();
                            return;
                        //3说明前面有食物
                        case 3:
                            {
                                FoodProgress(p);
                                break;
                            }
                        //前面是空的
                        case 0:
                            {
                                NullProgress(p);
                                break;
                            }
                    }
                    break;
                //向左走
                case 3:
                    switch (GameMap[p.x, p.y - 1])
                    {
                        //1或2游戏结束
                        case 1:
                        case 2:
                            GameOver();
                            return;
                        //3说明前面有食物
                        case 3:
                            {
                                FoodProgress(p);
                                break;
                            }
                        //前面是空的
                        case 0:
                            {
                                NullProgress(p);
                                break;
                            }
                    }
                    break;
                //向右走
                case 4:
                    switch (GameMap[p.x, p.y + 1])
                    {
                        //1或2游戏结束
                        case 1:
                        case 2:
                            GameOver();
                            return;
                        //3说明前面有食物
                        case 3:
                            {
                                FoodProgress(p);
                                break;
                            }
                        //前面是空的
                        case 0:
                            {
                                NullProgress(p);
                                break;
                            }
                    }
                    break;
                default:
                    break;
            }

        }

        //随机产生食物
        static Point FoodGene()
        {
            Random r = new Random();
            int foodx = r.Next(1, MapHeight - 1);
            int foody = r.Next(1, MapLength - 1);
            while (GameMap[foodx, foody] != 0)
            {
                foodx = r.Next(1, MapHeight - 1);
                foody = r.Next(1, MapLength - 1);
            }
            Point p = new Point(foodx, foody);
            Console.SetCursorPosition(foody * 2, foodx);
            Console.Write("○");
            return p;
        }

        //游戏结束函数
        static void GameOver()
        {
            Console.Clear();
            //绘制初始界面
            for (int i = 0; i < MapHeight; i++)
            {
                if (i == 0 || i == 26)
                {
                    PaintLineUpDown();
                }
                else
                {
                    if (i == 13)
                        PaintLine("Game Over!", 20);
                    else
                        PaintLine();
                }
                Console.WriteLine();
            }
            gameoverflag = 1;
        }

        //记录游戏分数
        static void ShowScore()
        {
            score++;
            Console.SetCursorPosition(MapLength * 2 + 4, MapHeight / 2 + 1);
            Console.Write(score);
            Console.SetCursorPosition(0, MapHeight);
        }

        static void Main(string[] args)
        {
            Init();
            //游戏主循环
            while (true)
            {
                RealTimeLogic();
                if (gameoverflag == 1)
                {
                    break;
                }
                System.Threading.Thread.Sleep(400 - score * 5);
            }
            Console.ReadKey();
        }

    }
}
