using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day2Part2(Input.Day2));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }

        static int Day2Part2(string[] instructions)
        {
            int x = 0, y = 0, aim = 0;
            foreach (var i in instructions)
            {
                var ins = i.Split(" ");
                var move = Int32.Parse(ins[1]);
                switch (ins[0])
                {
                    case "forward":
                        x += move;
                        y += aim * move;
                        break;
                    case "up":
                        aim -= move;
                        break;
                    case "down":
                        aim += move;
                        break;
                }
            }
            return x * y;
        }

        static int Day2(string[] instructions)
        {
            int x = 0, y = 0;
            foreach (var i in instructions)
            {
                var ins = i.Split(" ");
                var move = Int32.Parse(ins[1]);
                switch (ins[0])
                {
                    case "forward":
                        x += move;
                        break;
                    case "up":
                        y -= move;
                        break;
                    case "down":
                        y += move;
                        break;
                }
            }
            return x * y;
        }

        static int Day1Part2(int[] depth)
        {
            int prev = depth[0];
            int increases = 0;
            for (int i = 3; i < depth.Length; i++)
            {
                if (depth[i] > prev)
                    increases++;
                prev = depth[i-2];
            }
            return increases;
        }

        static int Day1(int[] depth)
        {
            int prev = depth[0];
            int increases = 0;
            for (int i = 1; i < depth.Length; i++)
            {
                if (depth[i] > prev)
                    increases++;
                prev = depth[i];
            }
            return increases;
        }
    }
}
