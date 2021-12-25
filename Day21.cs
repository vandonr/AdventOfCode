using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day21
    {
        private const int maxStep = 12;
        private const int winningScore = 21;
        private const int maxScore = winningScore+10;

        // key = score given by 3D3
        // value = number of combinations that give that score
        private static readonly Dictionary<int, ulong> proba = new()
        {
            {3, 1},
            {4, 3},
            {5, 6},
            {6, 7},
            {7, 6},
            {8, 3},
            {9, 1},
        };

        public static ulong Part2(int[] position)
        {
            var p1 = FlattenUniverses(ComputeUniverses(position[0]));
            var p2 = FlattenUniverses(ComputeUniverses(position[1]));

            ulong wins1 = 0, wins2 = 0;

            for (int step = 1; step < maxStep; step++)
            {
                wins1 += p1[step].Item1 * p2[step-1].Item2; //because when p1 plays, p2 still has score from prev round
                wins2 += p1[step].Item2 * p2[step].Item1;
            }
            return Math.Max(wins1, wins2);
        }

        // we only need to know how many universes are above or below 21 for each step,
        // position and exact score don't matter
        private static Tuple<ulong, ulong>[] FlattenUniverses(ulong[,,] u)
        {
            var res = new Tuple<ulong, ulong>[maxStep];
            for (int step = 0; step < maxStep; step++)
            {
                ulong wins = 0, losses = 0;
                for (int pos = 1; pos <= 10; pos++)
                    for (int score = 0; score < maxScore; score++)
                    {
                        if (score < winningScore)
                            losses += u[pos, step, score];
                        else
                            wins += u[pos, step, score];
                    }
                res[step] = Tuple.Create(wins, losses);
            }
            return res;
        }

        private static ulong[,,] ComputeUniverses(int start)
        {
            //nb universes indexed by position, step, score
            var u = new ulong[11, maxStep, maxScore];

            u[start, 0, 0] = 1; //seed

            for (int step = 0; step < maxStep; step++)
                for (int pos = 1; pos <= 10; pos++)
                    for (int score = 0; score < winningScore; score++) //no need to continue after 21
                    {
                        var current = u[pos, step, score];
                        if (current == 0)
                            continue;
                        foreach (var kvp in proba)
                        {
                            var nextPos = 1 + (pos + kvp.Key - 1) % 10;
                            var nextScore = score + nextPos;
                            u[nextPos, step + 1, nextScore] += kvp.Value * current;
                        }
                    }
            return u;
        }

        public static int Part1(int[] position)
        {
            int dicerolls = 0;
            int dice = 0;
            var score = new int[2];
            while (score[0] < 1000 && score[1] < 1000)
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

        public static readonly int[] Input = new int[] { 1, 10 };
        //p1 wins 444356092776315 times
        //p2 wins 341960390180808 times
        public static readonly int[] Sample = new int[] { 4, 8 };
    }
}
