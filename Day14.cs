﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode
{
    class Day14
    {
        public static int Part2(string polymer, Dictionary<string, string> pairs)
        {
            polymer = "SN";
            for (int step = 0; step < 40; step++)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < polymer.Length - 1; i++)
                {
                    sb.Append(polymer[i]);
                    sb.Append(pairs[polymer.Substring(i, 2)]);
                }
                sb.Append(polymer[polymer.Length - 1]);
                polymer = sb.ToString();
                Console.WriteLine(step + " - " + polymer.Length);
            }
            return polymer.Length;
        }

        public static int Part1(string polymer, Dictionary<string, string> pairs)
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
        public static readonly Dictionary<string, string> Input = @"HH -> P
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
KB -> F".Split(Environment.NewLine).Select(l => l.Split(" -> ")).ToDictionary(a => a[0], a => a[1]);
    }
}
