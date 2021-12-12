using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day12.Solve(Day12.Input, true));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
