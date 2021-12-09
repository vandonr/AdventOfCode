using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day9.Part2(Day9.Input));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
