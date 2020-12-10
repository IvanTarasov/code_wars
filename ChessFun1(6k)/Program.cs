using System;

namespace ChessFun1_6k_
{
    class Program
    {
        static void Main(string[] args)
        {
            string c1 = Console.ReadLine();
            string c2 = Console.ReadLine();

            char[] nums = new char[9] { '0','A','B','C','D','E','F','G','H'};

            int[] cell_1 = new int[2];
            int[] cell_2 = new int[2];
            cell_1[0] = int.Parse(c1[1].ToString());
            cell_2[0] = int.Parse(c2[1].ToString());

            for (int i = 1; i < 9; i++)
            {
                if (c1[0] == nums[i])
                {
                    cell_1[1] = i;
                }
                if (c2[0] == nums[i])
                {
                    cell_2[1] = i;
                }
            }

            bool answer = true;
            if ((cell_2[0] - cell_1[0]) % 2 != 0) 
            {
                changeColor();
            }
            if ((cell_2[1] - cell_1[1]) % 2 != 0)
            {
                changeColor();
            }

            void changeColor()
            {
                if (answer)
                {
                    answer = false;
                }
                else
                {
                    answer = true;
                }
            }

            Console.WriteLine(answer);
        }
    }
}
