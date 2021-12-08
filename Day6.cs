using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day6
    {
        public static long AllParts(int[] fish, int days)
        {
            //precompute how many fishes one fish becomes in specified nb of days depending on its age at d0
            var numberAfterManyDays = new long[6];

            var simulation = new long[9];
            simulation[5] = 1; //seed it with 1 fish at counter 5
            var simulationtmp = new long[9];
            for (int t = 1; t < days + 5; t++)
            {
                for (int i = 1; i < 9; i++)
                {
                    simulationtmp[i - 1] = simulation[i]; //aging
                }
                simulationtmp[8] = simulation[0]; //spawning
                simulationtmp[6] += simulation[0]; //reset age
                //swap pointers
                var tmp = simulation;
                simulation = simulationtmp;
                simulationtmp = tmp;

                if (t >= days)
                    numberAfterManyDays[days + 5 - t] = simulation.Sum();
            }

            long tot = 0;
            foreach (var f in fish)
            {
                tot += numberAfterManyDays[f];
            }
            return tot;
        }

        public static readonly int[] Input = { 4, 1, 3, 2, 4, 3, 1, 4, 4, 1, 1, 1, 5, 2, 4, 4, 2, 1, 2, 3, 4, 1, 2, 4, 3, 4, 5, 1, 1, 3, 1, 2, 1, 4, 1, 1, 3, 4, 1, 2, 5, 1, 4, 2, 2, 1, 1, 1, 3, 1, 5, 3, 1, 2, 1, 1, 1, 1, 4, 1, 1, 1, 2, 2, 1, 3, 1, 3, 1, 3, 4, 5, 1, 2, 2, 1, 1, 1, 4, 1, 5, 1, 3, 1, 3, 4, 1, 3, 2, 3, 4, 4, 4, 3, 4, 5, 1, 3, 1, 3, 5, 1, 1, 1, 1, 1, 2, 4, 1, 2, 1, 1, 1, 5, 1, 1, 2, 1, 3, 1, 4, 2, 3, 4, 4, 3, 1, 1, 3, 5, 3, 1, 1, 5, 2, 4, 1, 1, 3, 5, 1, 4, 3, 1, 1, 4, 2, 1, 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 4, 5, 1, 2, 5, 3, 1, 1, 3, 1, 1, 1, 1, 5, 1, 2, 5, 1, 1, 1, 1, 1, 1, 3, 5, 1, 3, 2, 1, 1, 1, 1, 1, 1, 1, 4, 5, 1, 1, 3, 1, 5, 1, 1, 1, 1, 3, 3, 1, 1, 1, 4, 4, 1, 1, 4, 1, 2, 1, 4, 4, 1, 1, 3, 4, 3, 5, 4, 1, 1, 4, 1, 3, 1, 1, 5, 5, 1, 2, 1, 2, 1, 2, 3, 1, 1, 3, 1, 1, 2, 1, 1, 3, 4, 3, 1, 1, 3, 3, 5, 1, 2, 1, 4, 1, 1, 2, 1, 3, 1, 1, 1, 1, 1, 1, 1, 4, 5, 5, 1, 1, 1, 4, 1, 1, 1, 2, 1, 2, 1, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 5 };
    }
}
