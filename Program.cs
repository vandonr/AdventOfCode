using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(Day5(Input.Day5));
            Console.WriteLine($" -- {sw.ElapsedMilliseconds}ms");
        }

        static int Day5(List<Tuple<Point, Point>> segments)
        {
            var occupation = new byte[1000, 1000];
            foreach (var s in segments)
            {
                if(s.Item1.X == s.Item2.X)
                {
                    for (int y = Math.Min(s.Item1.Y, s.Item2.Y); y <= Math.Max(s.Item1.Y, s.Item2.Y); y++)
                        occupation[s.Item1.X, y]++;
                }
                else if (s.Item1.Y == s.Item2.Y)
                {
                    for (int x = Math.Min(s.Item1.X, s.Item2.X); x <= Math.Max(s.Item1.X, s.Item2.X); x++)
                        occupation[x, s.Item1.Y]++;
                }
            }
            return (from byte c in occupation select c >= 2).Count(c => c);
        }

        class GridCompletion
        {
            public int[] rows = new int[5];
            public int[] columns = new int[5];
        }

        static (int part1, int part2) Day4(int[] numbers, List<int[][]> grids)
        {
            // create a map of when a number is drawn
            var drawOrder = new Dictionary<int, int>();
            for (int i = 0; i < numbers.Length; i++)
                drawOrder[numbers[i]] = i;

            var earliestComp = Int32.MaxValue;
            var earliestIndex = -1;
            var latestComp = 0;
            var latesIndex = -1;
            for (int i = 0; i < grids.Count; i++)
            {
                var comp = new GridCompletion();
                for (int x = 0; x < 5; x++)
                {
                    for (int y = 0; y < 5; y++)
                    {
                        var number = grids[i][x][y];
                        var drawn = drawOrder[number];
                        comp.rows[x] = Math.Max(comp.rows[x], drawn);
                        comp.columns[y] = Math.Max(comp.columns[y], drawn);
                    }
                }
                var completionRound = comp.rows.Concat(comp.columns).Min();
                //part 1
                if (completionRound < earliestComp)
                {
                    earliestComp = completionRound;
                    earliestIndex = i;
                }
                //part 2
                if (completionRound > latestComp)
                {
                    latestComp = completionRound;
                    latesIndex = i;
                }
            }

            return (part1: ComputeGridScore(grids[earliestIndex], numbers, earliestComp),
                part2: ComputeGridScore(grids[latesIndex], numbers, latestComp));
        }

        private static int ComputeGridScore(int[][] grid, int[] numbers, int until)
        {
            var flattenedGrid = grid.SelectMany(n => n).ToHashSet();
            for (int i = 0; i <= until; i++)
            {
                var drawn = numbers[i];
                if (flattenedGrid.Contains(drawn))
                    flattenedGrid.Remove(drawn);
            }
            return flattenedGrid.Sum() * numbers[until];
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
                var mostCommonDigit = numbers[Middle(lower, upper)][step];
                var last1 = BinarySearchForBoundary(numbers, lower, upper, step);
                if (mostCommonDigit == '1')
                {
                    if (check == Vitals.Oxygen)
                        upper = last1;
                    else
                        lower = last1 + 1;
                }
                else
                {
                    if (check == Vitals.Oxygen)
                        lower = last1 + 1;
                    else
                        upper = last1;
                }
                if (lower == upper)
                    return numbers[lower];
            }
            throw new Exception("does not converge to a single value");
        }

        /// <summary> Returns the index of the last '1' in the specified range </summary>
        private static int BinarySearchForBoundary(string[] a, int low, int high, int step)
        {
            while (high - low > 1)
            {
                // look in the middle
                var mid = Middle(low, high);
                if (a[mid][step] == '1') // if 1 then border is south
                    low = mid;
                else // else it's north
                    high = mid;
            }
            return low;
        }

        private static int Middle(int low, int high) { Debug.Assert(low < high); return low + (high - low) / 2; }

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
                gamma <<= 1;
                epsilon <<= 1;
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
