using System;
using System.Collections.Generic;

namespace ChessFun7_5k_
{
    class Program
    {
        /*
         Логика задачи:
            0) поставить на каждую ячейку коня (по очереди)
            1) найти координаты ячеек, которые может атаковать конь (get_coor_bishop),
            2) поставить на каждую из этих ячеек слона и методом перебора найти позицию ладьи для образования треугольника,
            3) повторить (2) для ладьи
         */
        static void Main()
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());

            if (!DataCorrect(cols, rows)) throw new Exception("Incorrect data!");
            
            int count = 0; // ответ задачи
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    // определяем подверженые атаке коня ячейки
                    var b_coor = get_coor_bishop(x, y);

                    // ставим на полученые ячейки слона
                    foreach (var bishop_atack in b_coor)
                    {
                        // определяем подверженые атаке слона ячейки
                        var k_coor = get_coor_knight(bishop_atack[0], bishop_atack[1]);
                        foreach (var knight_atack in k_coor)
                        {
                            // определяем подверженые атаке ладьи ячейки
                            var r_coor = get_coor_rook(knight_atack[0], knight_atack[1]);

                            // если ладья атакует слона - треугольник построен
                            bool b_atack = false;
                            foreach (var rook_atack in r_coor)
                            {
                                if (rook_atack[0] == x && rook_atack[1] == y ) b_atack = true;
                            }
                            if (b_atack) count++;
                        }
                    }

                    // аналогично с ладьей
                    foreach (var bishop_atack in b_coor)
                    {
                        var r_coor = get_coor_rook(bishop_atack[0], bishop_atack[1]);
                        foreach (var rook_atack in r_coor)
                        {
                            var k_coor = get_coor_knight(rook_atack[0], rook_atack[1]);
                            bool b_atack = false;
                            foreach (var knight_atack in k_coor)
                            {
                                if (knight_atack[0] == x && knight_atack[1] == y) b_atack = true;
                            }

                            if (b_atack) count++;
                        }
                    }
                }
            }

            Console.WriteLine("ANSWER: " + count.ToString());

            // координаты для атаки коня
            List<int[]> get_coor_bishop(int x, int y)
            {
                List<int[]> coor = new List<int[]>();

                if ((0 <= x - 2) && (0 <= y - 1)) coor.Add(new int[2] {x - 2, y - 1 });
                if ((0 <= x - 2) && (y + 1 < rows)) coor.Add(new int[2] {x - 2, y + 1 });

                if ((x + 2 < cols) && (0 <= y - 1)) coor.Add(new int[2] { x + 2, y - 1 });
                if ((x + 2 < cols) && (y + 1 < rows)) coor.Add(new int[2] { x + 2, y + 1 });

                if ((0 <= x - 1) && (y + 2 < rows)) coor.Add(new int[2] { x - 1, y + 2 });
                if ((0 <= x - 1) && (0 <= y - 2)) coor.Add(new int[2] { x - 1, y - 2 });

                if ((x + 1 < cols) && (y + 2 < rows)) coor.Add(new int[2] { x + 1, y + 2 });
                if ((x + 1 < cols) && (0 <= y - 2)) coor.Add(new int[2] { x + 1, y - 2 });

                return coor;
            }
            // координаты для атаки слона
            List<int[]> get_coor_knight(int x, int y)
            {
                List<int[]> coor = new List<int[]>();

                for (int r = 0;r <= 5; r++)
                    if ((0 <= x - r) && (0 <= y - r)) coor.Add(new int[2] { x - r, y - r });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if ((0 <= x - r) && (y + r < rows)) coor.Add(new int[2] { x - r, y + r });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if ((x + r < cols) && (y + r < rows)) coor.Add(new int[2] { x + r, y + r });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if ((x + r < cols) && (0 <= y - r)) coor.Add(new int[2] { x + r, y - r });
                    else break;

                return coor;
            }
            // координаты для атаки ладьи
            List<int[]> get_coor_rook(int x, int y)
            {
                List<int[]> coor = new List<int[]>();

                for (int r = 0; r <= 5; r++)
                    if (0 <= x - r) coor.Add(new int[2] { x - r, y });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if (x + r < cols) coor.Add(new int[2] { x + r, y });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if (y + r < rows) coor.Add(new int[2] { x, y + r });
                    else break;

                for (int r = 0; r <= 5; r++)
                    if (0 <= y - r) coor.Add(new int[2] { x, y - r });
                    else break;

                return coor;
            }
        }

        static bool DataCorrect(int n, int m)
        {
            if ((1 <= n && n <= 40) && (1 <= m && m <= 40) && (3 <= n * m))
                return true;
            else return false;
        }
    }
}
