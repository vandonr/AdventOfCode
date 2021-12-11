using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day11.Part2(Day11.Input));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
