using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day3(Input.Day3));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }

        static int Day3(string[] numbers)
        {
            var majority = numbers.Length/2;
            var countOfOnes = new int[12]; //numbers are 12 bits long
            foreach(var n in numbers)
            {
                for (int i = 0; i < n.Length; i++)
                {
                    if (n[i] == '1') countOfOnes[i]++;
                }
            }
            // gamma rate is most common bit
            // esplion is the opposite
            var gamma = 0;
            var epsilon = 0;
            foreach(var c in countOfOnes)
            {
                gamma = gamma << 1;
                epsilon = epsilon << 1;
                if (c > majority)
                    gamma++;
                else
                    epsilon++;
            }
            return gamma * epsilon;
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
