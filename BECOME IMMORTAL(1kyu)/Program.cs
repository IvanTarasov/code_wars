using System;
using System.Diagnostics;
using System.Threading;

namespace BECOME_IMMORTAL_1kyu_
{
    class Program
    {
        static void Main()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // последний непройденый тест
            //Console.WriteLine(ElderAge(28827050410L, 35165045587L, 7109602, 13719506));

            Console.WriteLine(ElderAge(24, 24, 0, 100000000)); 

            stopWatch.Stop();
            long t = stopWatch.ElapsedMilliseconds;
            Console.WriteLine("Time: " + t + "ms");
        }

        static long ElderAge(long n, long m, long k, long newp)
        {
            long time = 0;
            for (long r = 0; r < n; r++)
            {
                for (long c = 0; c < m; c++)
                {
                    var a = r ^ c;
                    if (a > k)
                    {
                        time += a - k;
                    }
                }
            }
            return (time % newp);
        }
    }
}