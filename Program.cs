using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day13.Part2(Day13.Dots, Day13.Folding));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }
    }
}
