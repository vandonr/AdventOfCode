using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day11
    {
        public static int Part2(char[][] octopuses)
        {
            int flashes = 0;
            int step = 0;
            while(flashes < 100)
            {
                flashes = Step(octopuses);
                step++;
            }
            return step;
        }

        private static (int, int)[] offsets = new (int, int)[]
        {
            (-1, 0), (1, 0), (0, -1), (0, 1), // +
            (-1, -1), (1, 1), (1, -1), (-1, 1), // x
        };

        public static int Part1(char[][] octopuses)
        {
            int flashes = 0;
            for(int step = 0; step < 100; step++)
            {
                flashes += Step(octopuses);
            }
            return flashes;
        }

        private static int Step(char[][] octopuses)
        {
            int flashes = 0;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    octopuses[i][j]++;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (octopuses[i][j] > '9')
                        flashes += Flash(octopuses, i, j);

            return flashes;
        }

        private static int Flash(char[][] octopuses, int i, int j)
        {
            octopuses[i][j] = '0';
            var flashes = 1;
            foreach(var o in offsets)
            {
                var oi = i + o.Item1;
                var oj = j + o.Item2;
                if (0 <= oi && oi < octopuses.Length && 0 <= oj && oj < octopuses[0].Length
                    && octopuses[oi][oj] != '0') //do nothing if already flashed
                {
                    octopuses[oi][oj]++;
                    if (octopuses[oi][oj] > '9')
                        flashes += Flash(octopuses, oi, oj);
                }
            }
            return flashes;
        }

        public static readonly char[][] Input = @"2238518614
4552388553
2562121143
2666685337
7575518784
3572534871
8411718283
7742668385
1235133231
2546165345".Split(Environment.NewLine).Select(l => l.ToCharArray()).ToArray();

        //should return 1656
        public static readonly char[][] Sample = @"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526".Split(Environment.NewLine).Select(l => l.ToCharArray()).ToArray();
    }
}
