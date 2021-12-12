using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    static class Day12
    {
        /// <param name="allowTwoVisits">false for part 1, true for part 2</param>
        public static int Solve(string[] links, bool allowTwoVisits)
        {
            var adjacency = new Dictionary<string, List<string>>();
            foreach (var link in links)
            {
                var ends = link.Split('-');
                adjacency.AddOrCreate(ends[0], ends[1]);
                adjacency.AddOrCreate(ends[1], ends[0]);
            }

            return ExploreFrom("start", adjacency, new HashSet<string>(), allowTwoVisits);
        }

        private static int ExploreFrom(string node, Dictionary<string, List<string>> adjacency, HashSet<string> seen, bool joker)
        {
            if (node == "end")
                return 1;
            if (Char.IsLower(node[0]))
                seen.Add(node);
            int paths = 0;
            foreach (var dest in adjacency[node])
            {
                if (!seen.Contains(dest))
                {
                    paths += ExploreFrom(dest, adjacency, new HashSet<string>(seen), joker);
                }
                else if (joker && dest != "start")
                {
                    paths += ExploreFrom(dest, adjacency, new HashSet<string>(seen), false);
                }
            }
            return paths;
        }

        private static void AddOrCreate(this Dictionary<string, List<string>> d, string key, string value)
        {
            List<string> list;
            if (d.TryGetValue(key, out list))
                list.Add(value);
            else
                d[key] = new List<string> { value };
        }

        public static readonly string[] Input = @"GC-zi
end-zv
lk-ca
lk-zi
GC-ky
zi-ca
end-FU
iv-FU
lk-iv
lk-FU
GC-end
ca-zv
lk-GC
GC-zv
start-iv
zv-QQ
ca-GC
ca-FU
iv-ca
start-lk
zv-FU
start-zi".Split(Environment.NewLine);

        //10 paths
        //36 on part 2
        public static readonly string[] Sample = @"start-A
start-b
A-c
A-b
b-d
A-end
b-end".Split(Environment.NewLine);
    }
}
