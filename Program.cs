using System;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day3Part2(Input.Day3));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }

        static int Day3Part2(string[] numbers)
        {
            numbers = numbers.OrderByDescending(s => s).ToArray();
            
            var oxy = Convert.ToInt32(DigFor(numbers, Vitals.Oxygen), 2);
            var co2 = Convert.ToInt32(DigFor(numbers, Vitals.CO2), 2);

            return oxy * co2;
        }

        enum Vitals { Oxygen, CO2 }
        private static string DigFor(string[] numbers, Vitals check)
        {
            var lower = 0;
            var upper = numbers.Length - 1;

            for (var step = 0; step < 12; step++)
            {
                // most common digit is the one in the middle
                var middleIndex = lower + (upper - lower) / 2;
                var mostCommonDigit = numbers[middleIndex][step];
                if (mostCommonDigit == '1')
                {
                    //go down
                    for (int i = middleIndex + 1; i <= upper; i++)
                    {
                        if (numbers[i][step] == '0')
                        {
                            if (check == Vitals.Oxygen)
                                upper = i - 1;
                            else
                                lower = i;
                            break;
                        }
                    }
                }
                else
                {
                    //go up
                    for (int i = middleIndex - 1; i > lower; i--)
                    {
                        if (numbers[i][step] == '1')
                        {
                            if (check == Vitals.Oxygen)
                                lower = i + 1;
                            else
                                upper = i;
                            break;
                        }

                    }
                }
                if (lower == upper)
                {
                    return numbers[lower];
                }
            }
            throw new Exception("does not converge to a single value");
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
