using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day14
    {
        public static long Part2(string polymer, Dictionary<string, char> pairs)
        {
            // init with just the middle letter
            var lookup = new Dictionary<string, long[]>();
            foreach (var pair in pairs)
            {
                var step = new long[26];
                step[pair.Value - 'A'] = 1;
                lookup[pair.Key] = step;
            }

            // then run the steps
            // where step[n] = step[n-1] of left and right, plus the middle letter
            for (int step = 1; step < 40; step++)
            {
                var prev = lookup;
                lookup = new Dictionary<string, long[]>();
                foreach (var l in prev)
                {
                    var left = new String(new[] { l.Key[0], pairs[l.Key] });
                    var right = new String(new[] { pairs[l.Key], l.Key[1] });
                    var newState = Add(prev[left], prev[right]);
                    newState[pairs[l.Key] - 'A']++;
                    lookup.Add(l.Key, newState);
                }
            }

            // now use the lookup table to get the resulting state for each pair
            var result = new long[26];
            for (int i = 0; i < polymer.Length - 1; i++)
            {
                var key = polymer.Substring(i, 2);
                result = Add(result, lookup[key]);
                result[polymer[i] - 'A']++;
            }
            // add last letter
            result[polymer[polymer.Length - 1] - 'A']++;

            var filtered = result.Where(c => c > 0);
            return filtered.Max() - filtered.Min();
        }

        private static long[] Add(long[] a, long[] b)
        {
            var res = new long[a.Length];
            for (int i = 0; i < a.Length; i++)
                res[i] = a[i] + b[i];
            return res;
        }

        public static int Part1(string polymer, Dictionary<string, char> pairs)
        {
            for (int step = 0; step < 10; step++)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < polymer.Length-1; i++)
                {
                    sb.Append(polymer[i]);
                    sb.Append(pairs[polymer.Substring(i, 2)]);
                }
                sb.Append(polymer[polymer.Length-1]);
                polymer = sb.ToString();
            }

            var counts = polymer.GroupBy(c => c).Select(g => g.Count());

            return counts.Max() - counts.Min();
        }

        public static readonly string StartingForm = "SNVVKOBFKOPBFFFCPBSF";
        public static readonly Dictionary<string, char> Input = @"HH -> P
CH -> P
HK -> N
OS -> N
HV -> S
VC -> C
VO -> K
OC -> C
FB -> S
NP -> S
OK -> H
OO -> N
PP -> B
VK -> B
BV -> N
PN -> K
HC -> C
NS -> K
BO -> C
BN -> O
SP -> H
FK -> K
KF -> N
VP -> H
NO -> N
OH -> N
CC -> O
PK -> P
BF -> K
CP -> N
SH -> V
VS -> P
BH -> B
KS -> H
HB -> K
BK -> S
KV -> C
SF -> B
BB -> O
PC -> S
HN -> S
FP -> S
PH -> C
OB -> O
FH -> K
CS -> P
OF -> N
FF -> V
PV -> B
PF -> C
FC -> S
KC -> O
PS -> V
CO -> F
CK -> O
KH -> H
OP -> O
SK -> S
VB -> P
FN -> H
FS -> P
FV -> N
HP -> O
SB -> N
VN -> V
KK -> P
KO -> V
BC -> B
FO -> H
OV -> H
CF -> H
HF -> K
SS -> V
SC -> N
CB -> B
SV -> C
SN -> P
PB -> B
KP -> S
PO -> B
CN -> F
ON -> B
CV -> S
HO -> O
NF -> F
VH -> P
NN -> S
HS -> S
NV -> V
NH -> C
NB -> B
SO -> K
NC -> C
VF -> B
BS -> V
VV -> N
BP -> P
KN -> C
NK -> O
KB -> F".Split(Environment.NewLine).Select(l => l.Split(" -> ")).ToDictionary(a => a[0], a => a[1][0]);
    }
}
