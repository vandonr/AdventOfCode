using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day18.Part1(Day18.Input));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
