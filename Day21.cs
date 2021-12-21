using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day21
    {
        public static int Part2()
        {
            return 0;
        }

        public static int Part1(int[] position)
        {
            int dicerolls = 0;
            int dice = 0;
            var score = new int[2];
            while(score[0] < 1000 && score[1] < 1000)
            {
                var player = (dicerolls % 6) / 3;
                position[player] = 1 + (position[player] + ThreeRollsD100(ref dice) - 1) % 10;
                score[player] += position[player];
                dicerolls += 3;
            }
            return dicerolls * score.Min();
        }

        private static int ThreeRollsD100(ref int dice)
        {
            var ret = 0;
            for (int i = 0; i < 3; i++)
            {
                ret += (dice % 100) + 1;
                dice = (dice + 1) % 100;
            }
            return ret;
        }

        public static readonly int[] Input = new int[] {1, 10};
    }
}
