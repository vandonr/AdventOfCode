using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    public class Day18
    {
        public static int Part2(string[] snailfishNb)
        {
            var max = 0;
            for (int i = 0; i < snailfishNb.Length; i++)
            {
                for (int j = 0; j < snailfishNb.Length; j++)
                {
                    if (i == j)
                        continue;

                    var leaves = new List<Pair>();
                    var a = Parse(snailfishNb[i], leaves);
                    var b = Parse(snailfishNb[j], leaves);
                    CreateLinks(leaves);

                    var current = new Pair(a, b);
                    while (Explode(current) || Split(current)) ;

                    var mag = current.Magnitude();
                    if (mag > max)
                        max = mag;
                }
            }
            return max;
        }

        public static int Part1(string[] snailfishNb)
        {
            var leaves = new List<Pair>();
            var current = Parse(snailfishNb[0], leaves);
            CreateLinks(leaves);
            for (int i = 1; i < snailfishNb.Length; i++)
            {
                var rightmost = current.RightC;
                while (rightmost.Value < 0)
                    rightmost = rightmost.RightC;
                leaves = new List<Pair> { rightmost };
                current = new Pair(current, Parse(snailfishNb[i], leaves));

                CreateLinks(leaves);

                //reduce
                while (Explode(current) || Split(current)) ;
            }

            return current.Magnitude();
        }

        private static void CreateLinks(List<Pair> leaves)
        {
            for (int l = 1; l < leaves.Count; l++)
            {
                leaves[l].LeftNeigh = leaves[l - 1];
                leaves[l - 1].RightNeigh = leaves[l];
            }
        }

        private static bool Explode(Pair p, int depth = 0)
        {
            if (p.Value >= 0)
                return false;
            if(depth >= 4 && p.RightC.Value >= 0 && p.LeftC.Value >= 0)
            {
                // do explode
                p.LeftNeigh = p.LeftC.LeftNeigh;
                p.RightNeigh = p.RightC.RightNeigh;
                if (p.LeftC.LeftNeigh != null)
                {
                    p.LeftC.LeftNeigh.Value += p.LeftC.Value;
                    p.LeftC.LeftNeigh.RightNeigh = p;
                }
                if (p.RightC.RightNeigh != null)
                {
                    p.RightC.RightNeigh.Value += p.RightC.Value;
                    p.RightC.RightNeigh.LeftNeigh = p;
                }
                p.Value = 0;
                p.LeftC = null;
                p.RightC = null;

                return true;
            }
            return Explode(p.LeftC, depth + 1) || Explode(p.RightC, depth + 1);
        }

        private static bool Split(Pair p)
        {
            if(p.Value > 9)
            {
                //do split
                p.LeftC = new Pair(p.Value / 2);
                p.RightC = new Pair((p.Value / 2) + (p.Value % 2));
                p.LeftC.LeftNeigh = p.LeftNeigh;
                p.LeftC.RightNeigh = p.RightC;
                if(p.LeftNeigh != null)
                    p.LeftNeigh.RightNeigh = p.LeftC;
                p.RightC.RightNeigh = p.RightNeigh;
                p.RightC.LeftNeigh = p.LeftC;
                if (p.RightNeigh != null)
                    p.RightNeigh.LeftNeigh = p.RightC;

                p.Value = -1;
                p.LeftNeigh = null;
                p.RightNeigh = null;

                return true;
            }
            if (p.Value >= 0)
                return false;
            return Split(p.LeftC) || Split(p.RightC);
        }

        private class Pair
        {
            public Pair LeftC;
            public Pair RightC;
            
            public int Value = -1;
            public Pair LeftNeigh;
            public Pair RightNeigh;

            public Pair(Pair left, Pair right)
            {
                this.LeftC = left;
                this.RightC = right;
            }

            public Pair(int val) { Value = val; }

            public override string ToString()
            {
                if (Value >= 0)
                    return Value.ToString();
                return $"[{LeftC},{RightC}]";
            }

            public int Magnitude()
            {
                if (Value >= 0) return Value;
                return 3*LeftC.Magnitude() + 2*RightC.Magnitude();
            }
        }

        private static Pair Parse(string s, List<Pair> leaves)
        {
            if (s.Length == 1)
            {
                var l = new Pair(int.Parse(s));
                leaves.Add(l);
                return l;
            }

            int il = 0, ir = 0;
            int depth = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var c = s[i];
                switch (c)
                {
                    case '[':
                        depth++;
                        break;
                    case ']':
                        if (depth == 1)
                        {
                            ir = i;
                            goto endl;
                        }
                        else
                            depth--;
                        break;
                    case ',':
                        if (depth == 1)
                            il = i;
                        break;
                }
            }
        endl:
            var left = Parse(s.Substring(1, il - 1), leaves);            
            var right = Parse(s.Substring(il + 1, ir - il - 1), leaves);
            return new Pair(left, right);
        }

        public static readonly string[] Input = @"[2,[0,[9,[5,9]]]]
[[2,[1,8]],3]
[[[[7,2],6],[[7,8],3]],[9,[[6,9],2]]]
[[[[7,2],[9,8]],7],[4,[[2,2],[5,0]]]]
[[8,[2,2]],[5,[9,[4,9]]]]
[[[[6,2],[4,8]],5],0]
[[3,[3,[6,6]]],[6,9]]
[[[9,5],[[8,2],[4,0]]],[[5,5],[[5,0],[1,9]]]]
[[[[7,4],[8,1]],[2,[7,1]]],2]
[[[[9,6],3],8],[[[9,8],7],[5,[0,8]]]]
[[[4,[4,0]],[[7,3],3]],[8,[3,[8,2]]]]
[[[[8,4],1],6],[[1,[8,7]],1]]
[[[8,2],[[1,4],3]],[[4,5],[[9,1],[7,2]]]]
[[[[5,0],[8,8]],[[4,2],4]],[2,[[4,3],[3,7]]]]
[[[8,7],[2,1]],[9,3]]
[[3,[7,4]],[0,3]]
[4,[[[5,0],[5,2]],3]]
[[[[0,1],0],8],[6,3]]
[[7,[[9,8],[2,7]]],[[[8,8],[9,4]],[[0,5],[4,1]]]]
[[[[3,7],[5,4]],[8,[1,8]]],[[1,8],[[6,9],9]]]
[[[[7,4],[7,7]],7],[1,[[8,2],[1,8]]]]
[[[[6,2],8],[[1,2],3]],[[[3,6],[4,9]],[[3,1],[9,8]]]]
[[[3,[1,1]],[[6,5],[2,2]]],9]
[[[[9,1],4],1],[[[1,3],3],[0,[1,4]]]]
[[[5,0],[4,[6,8]]],[[2,4],[[0,3],[2,6]]]]
[9,[[9,[1,5]],1]]
[[1,[[6,0],[9,2]]],[[[4,2],7],[[2,9],6]]]
[[[[8,2],8],9],[[[4,9],[3,8]],2]]
[[[9,1],[6,5]],[[[9,5],5],1]]
[[[[1,3],5],2],[1,1]]
[[[[0,0],[8,1]],8],8]
[[[[3,3],5],[[9,6],9]],[[3,[0,9]],7]]
[[[6,5],1],1]
[[[4,[1,3]],[[2,2],2]],[[8,0],[[8,1],[2,6]]]]
[9,[[4,6],2]]
[[[5,[8,8]],[[1,8],[4,9]]],[9,[3,6]]]
[[[[9,3],3],0],8]
[[[5,0],[[2,8],[1,1]]],[[[5,6],9],8]]
[[[[5,0],[5,2]],[[7,0],[9,8]]],[3,[[5,7],[5,9]]]]
[[3,[5,7]],1]
[[[[2,5],[0,7]],9],[[[3,2],1],[7,1]]]
[6,[7,[6,0]]]
[[[8,5],[[1,7],[7,6]]],[[1,3],[5,[1,9]]]]
[[[[9,4],[8,3]],1],[[1,6],[[2,5],1]]]
[[[[6,5],[6,6]],[5,5]],[1,8]]
[[[[7,7],[2,2]],3],[1,[[8,6],[5,1]]]]
[[6,[2,4]],[[8,8],[[3,5],6]]]
[[1,[[6,1],[9,3]]],[[2,0],5]]
[[[5,9],[6,[1,9]]],[3,[4,[7,7]]]]
[[[[3,6],[8,5]],[[9,4],[4,1]]],[3,3]]
[[[3,9],[1,6]],2]
[[[[0,9],7],6],[7,[9,[9,9]]]]
[[[5,[6,0]],[8,[7,5]]],[[[8,8],0],[8,1]]]
[[[[6,9],[9,0]],2],[[[0,3],[1,6]],[2,4]]]
[[[[8,2],[3,0]],[[3,8],8]],[6,[[9,3],4]]]
[[[6,6],2],[5,[1,4]]]
[[1,[1,4]],[[[4,3],0],1]]
[[[[9,9],3],0],[[[3,3],[2,8]],[1,0]]]
[[[[1,1],[3,5]],[9,7]],4]
[[[9,[3,6]],5],[[4,9],[9,3]]]
[[8,7],[5,[7,[7,7]]]]
[[[[0,5],[7,3]],[[8,6],8]],[[[4,4],[5,0]],[[2,2],2]]]
[[[5,0],[[1,9],[5,8]]],[[1,5],[[9,3],[0,7]]]]
[[[1,[1,5]],[8,[2,2]]],0]
[[[6,[7,8]],[[0,2],5]],[3,[5,[8,0]]]]
[[[[1,7],2],3],[[[8,7],[7,8]],[7,[5,5]]]]
[[1,[7,[3,3]]],[8,[9,[3,0]]]]
[[5,6],[[5,[2,8]],[[5,5],[8,8]]]]
[[8,[[7,7],[4,0]]],[[5,[0,4]],[6,[6,2]]]]
[[4,[[0,0],[0,1]]],[[3,1],[[6,7],4]]]
[[[[3,2],[4,2]],[[4,4],[6,3]]],[9,[0,[1,9]]]]
[[[[4,6],2],[[9,6],4]],[[9,[9,1]],[0,[1,8]]]]
[[[5,8],[[6,5],[0,4]]],[[0,[6,3]],[2,0]]]
[[6,8],[[5,5],[5,8]]]
[[[7,3],[8,[6,7]]],[[[1,5],2],7]]
[[6,[8,[8,9]]],[[[1,1],[3,0]],[[7,2],[3,7]]]]
[[[[8,1],6],[9,[5,1]]],[[[5,9],[1,9]],5]]
[[[[3,6],[5,7]],[[0,3],8]],[3,[[2,1],0]]]
[[7,[5,1]],[[[3,6],9],[[4,0],6]]]
[[[[3,8],8],0],[[1,[1,4]],[[4,5],[8,5]]]]
[[[8,[0,6]],[4,3]],[8,[[1,5],8]]]
[2,[[1,[9,7]],[[2,0],6]]]
[[[[7,4],4],[[4,9],3]],[[[6,5],[0,5]],[[9,8],[2,6]]]]
[[[3,[7,2]],[[7,7],4]],[[[3,4],[6,0]],[6,3]]]
[[[1,9],[[9,8],9]],5]
[[[4,2],2],[[[4,4],7],5]]
[[[9,1],[2,[1,5]]],[[4,3],[4,[9,5]]]]
[2,[[[8,4],1],[[2,4],2]]]
[[[0,6],5],[1,[[2,0],6]]]
[[[[2,4],[1,7]],[1,0]],[9,5]]
[[7,[3,[2,0]]],[[7,8],8]]
[[9,[1,0]],[[0,4],[[0,1],0]]]
[0,9]
[[[[2,9],[2,4]],[[5,6],8]],[[5,[1,4]],[3,[0,6]]]]
[[5,[[5,8],0]],[[[0,6],[4,5]],[[8,9],[8,3]]]]
[[[[5,2],[7,7]],[0,[4,1]]],[[8,7],[[5,3],7]]]
[[[5,3],5],[0,0]]
[3,5]
[[2,6],5]
[[5,[[6,0],3]],[[3,[8,7]],[2,0]]]".Split(Environment.NewLine);

        // expected [[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]], M=4140
        public static readonly string[] Sample = @"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]".Split(Environment.NewLine);

        // expect [[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]
        public static readonly string[] SampleAdd = @"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]".Split(Environment.NewLine);

        // expect [[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]
        public static readonly string[] SampleAdd2 = new[] {
            "[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]",
            "[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]" };

        //expect [[[[5,0],[7,4]],[5,5]],[6,6]]
        public static readonly string[] SampleAdd3 = new[] {
            "[1,1]",
            "[2,2]",
            "[3,3]",
            "[4,4]",
            "[5,5]",
            "[6,6]",
        };
    }
}
