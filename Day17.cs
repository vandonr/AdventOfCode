using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day17
    {
        public static long Part2(In area)
        {
            var xhitsPerStep = new int[area.xmax];
            var xTwoHitsPerStep = new int[area.xmax];
            var yhitsPerStep = new int[-area.ymax*2];
            var yTwoHitsPerStep = new int[-area.ymax*2];

            for (int vx = 1; vx <= area.xmax; vx++)
            {
                bool hit = false;
                var xpos = 0;
                var xspeed = vx;
                for (int s = 0; s < area.xmax; s++)
                {
                    xpos += xspeed;
                    xspeed = Math.Max(0, xspeed-1);

                    if (area.xmin <= xpos && xpos <= area.xmax)
                    {
                        xhitsPerStep[s]++;
                        if (hit)
                            xTwoHitsPerStep[s]++;
                        hit = true;
                    }
                    else if (xpos > area.xmax) //too far
                        break;
                    else if (xpos < area.xmin && xspeed == 0) //too short
                        break;
                }
            }

            for (int vy = area.ymax; vy <= -area.ymax; vy++)
            {
                bool hit = false;
                var ypos = 0;
                var yspeed = vy;
                for (int s = 0; s < -area.ymax * 2; s++)
                {
                    ypos += yspeed;
                    yspeed--;

                    if (area.ymax <= ypos && ypos <= area.ymin) //bounds flipped because negative
                    {
                        yhitsPerStep[s]++;
                        if (hit)
                            yTwoHitsPerStep[s]++;
                        hit = true;
                    }
                    else if (ypos < area.ymax) //too far
                        break;
                }
            }

            long combinations = 0;
            for (int s = 0; s < Math.Min(xhitsPerStep.Length, yhitsPerStep.Length); s++)
                combinations += xhitsPerStep[s] * yhitsPerStep[s];
            //now remove the combinations we counted twice because they already hit the area in a previous step
            for (int s = 0; s < Math.Min(xhitsPerStep.Length, yhitsPerStep.Length); s++)
                combinations -= xTwoHitsPerStep[s] * yTwoHitsPerStep[s];
            return combinations;
        }

        public static int Part1(In area)
        {
            return area.ymax * (area.ymax + 1) / 2;
        }

        public class In
        {
            public int xmin, xmax;
            public int ymin, ymax;
        }
        public static readonly In Input = new In { xmin = 282, xmax = 314, ymin = -45, ymax = -80 };
    }
}
