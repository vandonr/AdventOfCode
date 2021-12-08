using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day8.Part2(Day8.InputPatterns, Day8.InputDigits));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
