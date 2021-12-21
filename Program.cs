using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day21.Part1(Day21.Input));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
