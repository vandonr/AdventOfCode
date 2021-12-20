using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day20.Part2(Day20.Algo, Day20.Image));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
