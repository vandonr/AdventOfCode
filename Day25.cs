﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Day25
    {
        public static int Part2()
        {
            return 0;
        }

        public static int Part1(char[][] cucumbers)
        {
            Buffer = new char[cucumbers.Length][];
            for (int i = 0; i < cucumbers.Length; i++)
                Buffer[i] = new char[cucumbers[0].Length];

            var move = true;
            var step = 0;
            while (move)
            {
                move = Move(ref cucumbers, '>');
                var m2 = Move(ref cucumbers, 'v');
                move |= m2;
                step++;
            }
            return step;
        }

        private static char[][] Buffer;

        private static bool Move(ref char[][] cucumbers, char dir)
        {
            bool moved = false;

            for (int i = 0; i < cucumbers.Length; i++)
                for (int j = 0; j < cucumbers[0].Length; j++)
                    Buffer[i][j] = cucumbers[i][j];

            for (int i = 0; i < cucumbers.Length; i++)
            {
                for (int j = 0; j < cucumbers[0].Length; j++)
                {
                    var inext = dir == 'v' ? (i + 1) % cucumbers.Length : i;
                    var jnext = dir == '>' ? (j + 1) % cucumbers[0].Length : j;
                    if (cucumbers[i][j] == dir && cucumbers[inext][jnext] == '.')
                    {
                        Buffer[inext][jnext] = cucumbers[i][j];
                        Buffer[i][j] = '.';
                        moved = true;
                    }
                }
            }
            var tmp = cucumbers;
            cucumbers = Buffer;
            Buffer = tmp;

            return moved;
        }

        public static readonly char[][] Input = @">.>..v..v.>vvvvv..>>>.>vv>vv>.......vv.vvv..v..vv.>v...>>>>>>.v>.>..>v.>.>v..v.vv.>.>...v.v>....v..v.v>.....v....>.>..v>v>.........>.v.....
.>>.v..>..>vv>vv..vv>....vv>.>.>.>v>>......>>v..vv>.>>>..>>.>v.v.>v>..........>>....v.v....>>>>>.vv.v.v>v.v>...>>.>vv.v..>v>.v>..v>..>>...>
vv...>.>...>.>.v..>...>....>vv.vv.>..v.>>.>v...>.>>.v>v.v>..>....>.vv..>v..v>.>.>.>.>.v>.v>.....>vv..>...>>...>.vvv>.v......>v....v.>>.>.>.
v>v>...>.>>.v>v.v....vv>..v.v..............v.>v..>..>..v.vvv...>...vv>.>v.>>>v.>>.v>>>>..v..v..>...>v..>vvv...>..>...v>v.v.vv..v.>.>vv.>>..
vv...>.v...v..v>.v..v....>...vv..>v>.>.>>>...>vv.vv...vv>>..>..>.>..........v..v.v.>.>...>.>v.v>.v..>vv....v.....>.v>..>>>>v.....>......v..
v>>..>>>v...>.>>...>>vv>...vvv>.v>..vv.>>..v.>vv.v..>>v>vvv.v.>v..>v>vvv..vv.>>v..v..v.....v.>.>>>vv.>...>>>>>..>vv>>>.v....vv...v.>.>vv...
>v..>>v..>..>....>.>.v.>..>...v..>.>.>.>...v.>v>>>.vv>vvv.>>.v>.>v>.vv......v>.vv..>...v>>.v>...vv.v>.v>.>>..v>.v.v...>>>>>.v.v>.v.v....>..
.>>>.>..v>>>>>>>vv.v>>...>>>v..vvv>.....>.......>.v>.>>>..v.>>>.v.v>.>>....v.>.>>.v.v>v.v.v>>v>v.v.v.v>>>>v.v....v..v>...>.v.>.......vvv>.>
..v......v>v.>vvv.v.......>.....v>.....vvv..>...>v....>..>>..>.v.>..v.>>>...>vvv..vv>.>v>.vvv>.>......>>v.v>>v.v..v>vv.>v.>..>>v>>.v.vv.v.v
>v...>.>..vv..vv.v.v>>..>>..v..v..>.>...>.vv..vv.>..>..v.v.vv.....v.....v>..>>>.v..v>vv>v..>...vv>>>.v.>>.v>....vv..vvvv..v>v.>>...v.>v.>.v
.>..v.>v.>....>>.v>..>>.v..v....v..v..>v..v>>>v.v>...>..v>.>.>>>.>..v>...v.>.>..vvv..v>v>.vv.>v.v...>...vv..v>v.>>v>.>>.>>..vv>.v.v>v.>.v..
.v>>.>..>.>>..>..>.v.v.v>.>v...>.........v>>....>.vv>>v.>v>.............>.>v>v.v..>v...>.>v...v..v..v.v>>....v.>>.>v>..v>>.vv..v.v...v>..>.
>v..vv..>..v.>>.vvv.v.>>>>vv.>v.>v.....>...v...>.v.......v>v.>vv...>v..vv.>.vv.>.v>>v>>v.v>.>....>.>>>>.........vv.>..v..>v.v.vv.v..>>v...>
v.>.>>....>..>.>.v....vv.>>v>>...>..v..>>>>.v.>v>>..v>......v>v>>..v.v>..v.>>v..>.v>v.v.vvv>.>v.....v.>.>..v..>>.>..>.>v>..>.vv>v>>..>>v>>.
v..>>.vv...>.v.>vvv>>>>.>vvv.v......>vv.>.>..v..v.v>>.>>>..vv..v>>..v>vvv>..>>.>v>v..>.v.>vv.v....>.......vv.v...v.v.v>>v.vv.>.>..>.>..v.v>
vv>.v>...>.>>....>v.>..vv>..v.>.>>.v.v>...>>...v..v.vv>.vv....>...v>.vv.vv.v.>v.vv.v>>v.>>vv.v.>...vv.>v.......>>.>.vv>.>v>>.v>.v.>..vv.>.>
.>....>...>v>.>>>..>vv...v>...v>...v>.>v>......>.>v>..v>.>..>v...>...v..>.vv..vvvv>...v.>v.v....vv..>.v..v.vv...>vv..>.v.vv.>>.v.v.>..v....
vvv>>v......v....>.>v....>.>vv....>.>.>>vv>......v>.v..>.v..vv...v>vvv.v>.....vv..>vvvv.v.>...>.>v>>vv>..vv.....>.>v..>v...>v>..v.>..v.....
.v.>>..>>v>>>>>.v>.v>.>.v.>.vv....>.v..>v.v>>.v.v>vv..>....>....vv..v...v>vv>v>....>>.>.vv>.v>vv....v.v>..>>...vvv>v.......>.v>v.v..v...vv.
.>.vvv>.....>.>v.v..vv...v.v.vv.>...v...>vv..v.>..>v..>v..vv.v........v>>..>.v...v.>..vvv>vv>.>v.>>..>.>....>.v>>>v>..>v.>>.v...v..v....v.v
vv..vvv>.>v.v>>...>v.>.>>..vvv>>..>.v>>>>....v......vv>>>v.>...v..v>v..>>v..>>.v..v..v>>v.v..v.vv.vvvvv>.v.>....v..v.>.v.....>....>.....>..
.>.v.vv...v>...>.v.v.v.>.>..>.>v..>v...v.>>..v...>>v>v>vv..>..>.v.>.>.>>.....>>v.>v>>...>..>.>>..>.v>...>v.....>.v.>>..vv.v.v...v>.>.>.....
.v>>vv>.v>.>vvvv....v>v>.v......v...>>>.>>.>..vvv..>....>>v>vv.>.>v>vv.>.v.v..v.v..>v>.v>v>.vv>v.>>v>...vv>.....>>v>v>v..v>..>.vv.>>.>...>.
v.v.>.v....v>.v.v..>v>v.v.v>.vv>.......>vv..>>.vv>>vv.>..v>>.>vv>v>>..v..>v>>>>vvv>>..v..v>v>>v>...>.v..>>...>.>.v.v.vv.>v...v.>>.>.>.>.>>>
>>>v.>.vv>.v.>.>v.>>.>..v>..>v>>..vv..>.>.v...v>.v.>...>>vvv>v>>>v>v>....>v..vv.>.v>...>.>>>.v>.v...v....>>v>v..>.>...>..>>>.v..>v..>v..>v>
.v...v...>...v>.>..vv.>....vv....v...>...>>v..>.vv.>v.>.>.>.v.v>.v.>vv>.v>v.>....>..>..>.v...v..>>..vvv.v.>v>..>vv..>.>...v...vv.vv>...v.v.
..v.....v>>vv.v>v>vv.>>..vvv.>v..v>.......>>>.>...>..v>v.>>..v.vvv..>.>>.>v.>v..>>.vvv..vv....v>...v.>>...>>....>>......>.>>...>.v.>....v.>
>.>......>..v>>vv>.v.v...v....v>..>.v>>>v....v..>>>>>....v......v>...>.>..>..>v.>>>>..>.v>>v>.....>.v.v.>..vv>>>.>v>..>.v>>>vvv..>...v.....
v..>v>vv>.v>>.>>v>.v..>.>.v..v>>>>..>vv.v.v>vvvv>.>..v.vv>v...>v>>.v..>..>v.v.....>..vvv>.v...>.vv..>>>>>.v.v.vv.v>..v..>..>v.vv.>.v..>.v.>
>..v...v>>>.>vvvv>..v.>....>vvvvv.>v.v>v>.>.>v>>>.v..>.vvv.>>..>v...>.v..>v>.v>vvv.>..>.v>v...>v>.v>...>.....>v>...v......v>.v.>>>...>.vv..
>...v.>v.v.>>.>..>.>vv.vv...>>v.>v.vv.>v.>..>.v>..v..v.v..v.........>>vvv.>...>vv.v.>vv>v>v.>>>vv>vv.>..>...>>.>.>>..>....>v.v..v..>...v.>>
>v.>.....v....>.....>..v>v.>.v>>v>...>vv.>v.>v..v...v>v.v.>..>v>....vvvv..>.>.vv.>.>v.>v.v....v.v....>>.>..>.>>....v.>vv..vv..v......v>.>.>
>.vv.v>v....vv>>.>v..>...>..>.>>...>>...v.>v>.....>..v..v.v..v>...v>vv..>.v>.v.v.....vvv..v>vv>.>>vv..>>v.>>.>.v.v..>.>>v.v.v>......>...>v.
.v.>....>v..>>>..>...>v>>..>>..>.>v>.v.....v....v.v.>...>>>....v......vv.v.......v>.>>.v..>.>>.vv>v....>v.>....>...vvvv..>v>.v.v..>v.>v.>..
v>..v>>..>>>.>>>v..>.>.>>.>>....v.v..v.v.v...v>.>..v>..v>>v.>.>.>vv.....>>>..>>.......>.>.v.v..>.>...>v.vv.vvv....v>>vv>>v.>.vvv.>>vv...>vv
>..>>>v>v..vvv>.v.>>..v.vv>..v.vv>>v......vv.v.>v>>v>.....>>.>>.>....v.vv>.>v......>>v.>>.v.>..v....>..v>.>v.>...v...........>..>v..v.>v.>>
.v.>..v...v>...vv.>..>>.>>v...v...>..vv.....v......v..............>.v......>.>>v.>...>v..>v>...v.vv>..>v>>.>.v...>.v>vv..vv.>vv>..>.......>
.vv...v..vvvv.>...>.......vv>.vv.>v.>.v..v>v..>>v..v.v..vv>>..>.vv..>>.>..>..v......>..v.v>vv>v..v.vv>vv.....>.....>v.v>>.........v>..vv>.>
v.>..>vv>..>>v..v>.v.vv..>>...v....v..>>.v..v>v..>>.v...>.>>.>..v.>..>v>.>v.>v.v>..>v>>....v.>>v...v...>vvv....v>...>....v...>v>>.vvv.....>
>>>>>>>.>>.v..v>..>.>.v.vv.>vvv......>v......>v>v.v.vv.v>.v.>........>v>.v...>....vvvv.>v..v...v......v>.v...>..>.>..>..>.v..>>..vv.v..>..>
>..>.vv.v>.v..>.>v...>.>.v>vvv>.v...v...v>vvv.>....vvv>>.vv>......>.>..>.vv.v.v.v.v.v>v.........>v.>v>v..>>..vv.v>v.v.v.>>>v>...v>.>v.>v.>.
.>>.>>..v>..v....>>..v>>v..>..>v..v.v..v..>v..>...>v.v.>>>v..vv.>>.v>v...v.....v.>v>..v>.v>>v.>.>..>>v....>>>.>vv..>vv..vv..>v>.>.v.>>vvv..
..v.v>>..>v..>.>.>....v>>.v.v.v.....>..>....>v>>.>....>.>v>>.....v>..vv.>v.>..>v.....vv.>>>...>>.....>.>v...........>>....>..v>....v..vv>v.
vv>...>>..vv..v.v..>>.v.>vvv.>.v>v>v.>>>>...>v.v>...>v>..v.>..v..v.>....v.>.>v>.>...v..>v..>v.v>..vv..>.>v.v>.v.v>.v.>>vv.>v>.v.v.>..>..v>.
...>>>...vv>v.v...>v.>>..v.>...v..vvv.v..v>vv>v.>v.>...v..>.>>.>v.>...v..v>.v>v>v>..>v.>.>>vvvv>>.v..vv......>.v.>>>v>.>.v>.v.>.vv..>>.....
>>>..>vv.v.vv...>.>v..v.>.>>.v>v>>>v.v........v.v.v...>..v>>v......v>>>.v>v..v>...>>>.vv.>v..>>.vv>>v.v........>..>>>v.>.>..v>v>.vv...v..>v
.v.>......>...>>.>.v..v>>v.>vv..vv.>..>>>....>.v.>....v.>..v......v.>>>.>v..v....vvvv.....v....>.v>>v...v..v>>..v>..>.>v>v..>.>....v>v.v>..
.>.>.vv.vv.....v.>..v>.>v.v.v....v..>>vv.v..vv.>..v...vvvvvv>.vv......>v.v..v.>v>.v..>>vv>..v>v.>>.v...v.....>v>..>>.v.>..>.>..>v.v>.vv>...
........v.>....vv.>..>vv>...vv.>.....>..v.>>v..>vv>>v>.......v.>>v...>vv>...vv.>...vv..v>.v.>...v.>>>>>..v..v>>v.>...>.>>>v>...v>.v..>..>..
>.v.>>.v......>...v.>>..>v...v.>.....>v....>>v>.>.>..v>>..v.v>.>..vvvv.v......>..v.v.>>.v..v....v.vvv.......vv>...v.v...v>v>>.>..v>.>>.....
.v>.>>v.v>>.>>.......>>.>...v>>.>>v..>...>..vv.v...>vv....vvv.vv..>.v.>vv>.v>.>>..>.>....v>..v.v..v>.>...vvv.>v...v...>.v...>>vv>...v..>>..
..>.....v.>v.v..v.>v.>v>>..>v.v.vv..v.v..v..v..v>..>.v...>..>v>.v.v>.v>>vv.v.vv>vv>v>...>...>>v.v>.>>v.v>....v..vv..>>>>>.>.>..>v..>>...>>>
>>>.>.>.v.v>.>v>..v....v.v.v.v.>.vv>...v.>v.>..v..>.>...v.....v>v.>vvvvvvv...>>>..>.>..vv.v.>vv.>.v.>>>.v>>.>.v.>.vvv>v.>>.v.v.v.........vv
.>.v.vv.vv.>v...v.>..v>>>v...v..>..v.>v.v.>>.v...v.v.vv.v...v...v>.>..v...>.>.vv..vv..vvvv>.v.>>..>v>>>>v..vv.>..v.v.v.>>......>.v....vv.vv
>v....>vv>..vv..v..>.>..>v.vv.>....>>>...>>>.vvv>>v.v.v.>>.v>vvv>.>>.>..v.v>>>>....v>>.v>.v...>v....>>vvv.>v.....v.>v...v.>v..>>v..v..vv.v.
vv>.v.v>.>v..>.>>vv.v>..vvvvv.......v>..>.vv.>v.v..v..>v>>>v>vvvvv>v.vv.>>.v..v......v>....v..>...v>v.>vv.v>.>v.v..>>.v..>>>v...>..v.v...>.
v.>v..>....v.>>.>..vv..>v>v.>....>....>v.>.v.>>.v>..vv.>v.vvv.>.....>..>.>...>>>..vv>>v...>>..>vvv.>>..v>.vv...v...v..>>v..v>..>.v..vv....v
>v>.>v.>>..vv......vv..v...>...vv....>>....>.v..>vvv>..>.vv.vv>>v.>v>.>.v..v>v...v>>..>>>.>...vv..v.>>>...>.v.v>..vv.>v>......v..>v>.>..v..
.v>.>...vv>vv.>v...>..>v.>v....>..>v>v.>.vv>..v.>v.>v.v>.vv.>>v.....>.vv..v....>vv.>.v.>>vv.v...>..>>v>.>v.>...v.v..>.>......vvvvv...v.v>.>
v..v>v...v>>v.v.>.>.>v.v>.>v>.vv.v.v.v..>vv..>.>v>.>v...>v>v.vv>>....>...>..vv.vv.vvvv.>....>>>v>.....>>vvvv.>.v...>vv..vv.>.>>.....>v.....
...>v....>.v>..>.v>..v>v..>...>.>..v....>>>..>..vv.>.v>..>>v...v.v...v..v..v>v>....>>...vv.>>..>v.vvv>.....>.>v.v....>v..>.>v......>.v>>v>.
v....vv.....>>v.vv.vv..>>...>v.>v>v...v>>..v>.v>>>v.v.....>>vv>..v..v>.vv>v>v>..vv..>>...v...>v>....v>..v>.>.>>...>>>....>v.>.>.>.vvvvv.>v>
v......>v..v...>>vv>>>.vvv.>>.vvv>..>..v>.>.>>..vv...v>v...>..>>....v........v...>>.v>...v..>.>>......>..>>...>v.v..>..>v.>>..>.>..v.v.>v..
.>>>>vv>...>.v...v.vvvv.>>>>>...v>>.vv.>..v>.v>>vv.v..vv>...>.v...v..>.v..>..v..v..v.v>..>>>v.v.v.......>v..>.v>>>v...>....>>v>>v>>>v>>.v>.
..v..v.....v..v>>vv>>.>>v>>...>>....>>.>.v>........>.>.>>.>....v.>v>..v.>v.v.vv>..>..v.>.v...>.>v.>..vv..>vv.>>..v....>.>v..v>.>>>vv......>
>.v........v>..vv>.v..v.>>.vv>v>..>v.>v>.>v....vv.>..vv>>v..v.....v>..>v...>...>>vv.>>vv>>>v>.>.v...vv...vv...v..>v...v.>v.v>..>.v......>..
>>>...vv.v.>.v>>>.v.v..>>>.vvv>....v>>....v...v.v>>..>>.>....v..v..v.>v..>..>...>.vv.>.v.v..>v>.v..>......>>>..>>.....>.v>>..v..v.>...v.v>.
.vv.v.>.v...>vvv>.....vv.vv>v>..v....v.>>.v..v>..>.v>.v.>..>>....>v.....>vvv.>v.....>.v>>..>>.>...v>>>.vv.v>v..>>.v>.v........>......v..>>>
v>..>>.v...v>.>.v>..vv.>....v..>...>v.>.>..v>.>.>>.v..vv..vvv...v..vv.v>.v>..>....>v.>vvv.>>.v.>vvv...v..v>>.v.v.....>.>.>...>v.>v.>.vvv>>>
.>>.v>.v..>...>.v.v...v>>.vvv..v>.>v.>>>>.v..>v..vv...>..>>>.>..>...>v>>v>..v....>>vvv..>.>....>.>v>.>.vv.v..>.v>>>>v>>v...v.>>>.v.vvv.>vvv
.....v..v..>.>.>..>>.>.v>vvv.v.....>.v.>v...>v>v....>>>>..>>...v.>.>.....>..v.v.v.>v>.>..>v..>v...vv.>.v...v>>vvv...vv.v......v>.>>..>v>...
>v..vv>.v..>v>..v.vv.>>.>>v....vv>..v>>v.>.v>.>>v...>..>..v>>..>vv.....v>>v..>v>>>.>.>>.v>v...>v...v...>..v.v.v>....>.>v.>.v...vv.v>>.vvv.v
...>v>.>.>>.vv..>.>..v.>>v>v..v>>...vv>.v.v...>..v>>..vv....>v.v..>v>.>.>>.......v...>v>.>.v...v...>>.>>v.vv>v..>...vv>..v>.v..>.v>vvv>vvv>
v.vv>..>>.....>>.>>v.vv>v.vv>...vvv...>..v>.v>.v>..>v.vv>.v.>..v.....>v..>v>.vv>.vv>>..>v..v...v>.v.v.v>>v.vv>..>.>v.>.vv>.>.>v>vv.>v.>>.>v
>>..v>.>..v......>v.v>....>.v.....>>.v.>.v...vv>>...v>.v>..v..v>..>v.vv>>v>..>.v..>.....v>....vvv>v>...>..v.v..>vv..vv..v.v.v...>>.>>>....v
v>...v>vvvv..vvvv.v....>v>...>v>>vv..>v.v.>>.>..>.v.>vv.>..v>>>.v>vv>>.>.v.........>.....>.....v.v>.>v.......>..v>..>..v.v.>v.>v.>vv.>v...>
.v..vvv>.vvv>>...>..vvv...>>.....v>v..vv.>v...........v...v>.>.vvvv.....>vv..>v.v.>..v>v.>v.>>.>.v.>.>v>.>v.>>...>vv>v..>>v....v.>.>vv.>vv>
.v>.v.....>vv.v..>.>v..>>..v.vv.>v.v>.vv>..vv.>....v...>..>..v>.>>...>>>....v..>..v..vv.>>>>>v.>..>.....vv>.....>v>.>..>>..vv.>..>..>v>.v..
>.>.>>>>..>.v.>.>.>..v.>.>>>..vv>.>vv..>.......>>v.v>.>.v>v.v.>.>.....v..>v..v.v.v.v>v........>v.>vv.v.v>>.v>..v>.v...vv...v.vv.vvv..v.....
.v>.>....v.v..v..v...>>v>.......>.v>>v.vv.>.>..>.v>.>v..>>vvv.v.......>v...v..v>...v>>.v>v>>vvv....v...>.v>..v.v....v>.>>...>..v....vv.v>>v
...>>v.v.>v.>..>>...v>vvvv.>..>...>vv>.>..>v.v.v.>>....>>.v.>...>>>>.vv.>.vvv>v...vv.v>v>>v....v>.>..v..>>..v.........>v>.>>.v>>>.vv..v....
.v......v.v.v>>..v..>>.>v>>v>>..v>v.v>..v>...>>.>>..>v..vvvv..>......v....v.v..>>.v>v.>.>.v.vvvv>.....>v>.>..v>>..>...>vv..vvv>vv.........v
...>.vv..v.v..v..>>..vvv...>v..v..>>>..v..v.>..>..>v...>......>v.>>v...>v.>>>.>.v..v.v..>>.v.>>..v...vvv....>v>>..v..v>v.>...>.v.vv>v>..v..
>..>.>.vv.>.>.v..v..v.>v...>.>v....>>v.vvvvv>>v...>vv.v....>v...vv....v.v.......>..v.>.v.>..v..v..>>v.>>>v..v>.>vv...>...>v.>>.v.>vv..>>..>
..>.>.v>.v.v>v.>.>>.v...>....>>v>vv.vv>v..>v>>...>vvv.>.v..>>>>..v.>>.vv..>>..>v>..v>v..>>v>.vv.v.vv.....vv.v..>.>.v.v>>....v..>....v>v>>v.
..>.>.v.vvv..v.>>.>.>.>...>.>..>.>>..v>..>v.v.....>.vv...vv..>>>..v>>.v.>..>..v>..>...v>>vv...v..v..v>>.v.v.v>v.>.>>..>.>..>......v........
>....>...v..v..>...>vv>v.vv.>vv.v>.v.>.v....v.>......vv.>vv>.>v....>.v>.vv>>.>..>v...v.v....>>...>v.vv.v.....>v...v......>.>.>....>...>v>v>
..>vv...vv.>.>..v>.>.>>>..v.v>v>>.>vv.>.v.v.>..>>....vv.....>vv.vvv.>>>vv.vv..v>.>.>..v>.v.>>...>>v..>v.>>.v......vv>>.>>v.>.>.>>.>.v...>.>
>..v>>.v>..v.v.>.vv.>...v....>...>..vvvvvv........v.vv>>.....v>.v...>.>>...vv..>...>>....v>.....>>.v....>v.v...v..v..>.vvv.>v...v.v>v.>...>
.>.vv.>..>>..>.v>..vv.vv.v>v.>.v.>....>..vv.>.>..>v..>..v.>>v..>vvv.>.>vvvvv..>.>...>>.v..v>v....>...>..>v.>...v....v>>v.v.v.v>v>.v.>.>...>
>.>>v..>..>....>v>.v.v>.v.>.>..v>v.>v>>>.>>..v...>.v.vv>.>.....>.v.>...v.v.v>>>.v..>>v>...v>.>v....v.>>.vv>....>v>.vv...>v.>..v>v>v.>..v...
v.v..>>v..v>>.vv>.>.v.>v...v....v.vv>..vv...>..v>..vvvv..v.v>v.vv....>.>.>....v>>..>vv.>..>v.>.>..>...v.>.>v>.v.>v>>>>>>v.v>.>v>.v...>..>..
>>..vv.>...v>.>.v.>v.>..vv>>vv>.v>v>...vv...v>.>.>>v...v>>vv.v.vv.....v....v>v........>v>v..>v.>>v...v>>..v..v.>>....v>....>vv>.v.>vv.>.v..
>..>>...>.v.vv>v..v>>>>.>v.vv....v...>v..>>vv>v...>..v.>v>>..v>.>>>.>....vv..v..vvv>v.....v.>.>v..vvv....>..vv....>>v..v.v...v.....>>v.....
...>.>..v>vv>....vv>v.v>>>..vv...>......>.....>..>...vv>..v>.>v>>.v..>>..vv>v.v..>.>.v..v.>v>>.v...v.v>v..v>v>v.>.v>v..v.v...>.>v>..>.....>
v.v>.v.>.....>..>v....vvvv>>v>v>.>v.>vv.>.v....>v..v.v....v>vv..>...v....v..v....v>v>.>..>.v>.vv.v.vv..v>.v>.>v.>.>..>..>v......v..>v.v..vv
v>.v>....>...>v..>v>..vv>....>v>v..>v...v..v>>...v>>...>v>..v.>v.v......vvv>.v.v.....v......v>v.>...v>.>.>.>>.v.>>>.v>...v..vvv>>....>v...>
vv..v>v>..v...v.>>>vv..v...>.v...>..v..v.v>..>.vvv..>v..v.vv>v..>...>vvv>v>...>v>..v>..v>.v>>..>>..v>v.v.>v.v>....>..>v..>.v..v>>>v..>>.>v.
v...>v>.>>vv>.vv....>.>>v.v>>v.v..v>>.vvv....>.>>.v>>........v.>v....v....v>.>.>v..>v....v>>v...>.>.>v.v.v.v.>...>>..v..>v.v>...vv.v>v...>v
.>...>vv..>v..>.vv>.>.>v.>vv>v..>v.>..v>>vvvv.v>...v..>.>v.v.vv....>v...v............>vv.>.v>..>v...>>..v>.>.>...>.v.vv.v.>...>.>.>>v..vv..
..>.>...>vvv>.....>>vv.>>v.>.>...vv>v.>.v...>.>.....>>..v....>..>..vvv.>vvvvv...>...>..v>v>vvv...>v>.....vvv.>>>.>.......>.......>v..v.v>..
v...>>vv..>>..vv..v>.>.......>.vv.>>>>..>.>v..>...>.v.vv..v.v.....v>v...>>.>..>....>>..>.v..v.>vv..v>..vv>.>.v>v.>..v.>.v.vv.>...>..>v.v>vv
.v..vv>...vv.v>.vv.v.v...>v>.vvv..>>.>>v>>.>.vvvv.>...v>>>vv.>v>..>.>...>.v>.vv.v.>...v.......v.>.>v.v.v.>vv>...v.>.>...v..v>vv.>>.......>.
>.>.....v.>>v.>v.>.>v.v>v>..>.>>v..v..v.v..>.....>.v>v.v.v>vv..v........>.>v...v>v..vv>v.vv.>vv.>v..v..>vv...>...vv.>.>.v>.>.vv.v>..v.vv.v.
...>v>.>>.>..vv...v.v.v..v..vv...>.v......v>.>>vv..v.>.>>>....v.v..vv.vv....>v>.>>.vv.v>.v.>....v.>>.vvv>v....v>v>.>v>.v.>vv.vv.>v>>>.>>..v
.v..v.v>.>vv.vv.>>.v>v>v.>....>>.vv..v.>.vv.>...>>......v.>v>.v...v.>>v.v>vvvv>vvv.vv>..v>...v>>.vv.vvv>.>.v.v.>>vv>>>>v...vv>.v>v>v...>vv>
.v>vv>>...>>....vvv>.......vvvv..>>>v>v..v......>..vvv.v>vv.>...>..>.v......>....v>.>v...v>>.vv>..v....>vv.>...>>.>>>.>vvv>>>>.v.>.v..>.v.v
>.v>..v..v>..>....>>>v.vv>...>.>>>v.>v>.vv...v.>v...vv..>>>>.>>..v....v.......>.>.v.>.>v>>>..>....vv..>..>>.>v>.>..>v.>>v....v..>>>v.>vv>..
vv.v..v>>....v.>v>>.>>.>..>.v>v..>v..>vv......v>.v.v..>>.v>.v>vv>.>..>...>>.vvv>...v.v.....v.>vvv>.>.v>.>>.v.v.>v...>...>.....v.>>>>vv..v..
v.>>.>...>..>>.>>vv>>>...>v>v>>vv..v....>...vv.....>vv>.>>.v....vv>.>.>...>v..>.vv.vv.vv....>v>.>..v>....v>.>.vv.>.>>...>.v..v.>>v>>...vv..
v....>..>>..>>>>v.>..>vv..v>>....v>vv.v..>v.>v.v.>...>v....>v.>>>v>.>..>v.>>...v>>..vv.>vv>v>..>>v.>.v.>v........>v.v>.........>>..>>.v>v.>
vv..v.>>>v>....vv..vv.v>>v.v...vvv>...vv>.>...>>>vv>>...>v.v>..>v....v.>v.v..v.>v>>>>.>v.v.vv.>>..v.....v...v>...>vv>.>vvvv>>.v..>.v..>v...
>...v..vv.....v..>.>>...v..>..v.vv>>>>..>.>.>v.v>v...>v>>v.>>..>.vv.v.>>vv..vv.>>>.>v.>v.>>...>>>.>.>>.>...v>v...v>.>>.>>.v.vv.>.>>>.>>vvvv
.vvv>.>v.>.....v>>.>.vvv.v.v..vv.v>.....vvv.vv........vv...>...>.v.v.>.....v.>.....v.>.v>>.v.....>v.v>.>...v>..>>.v.v.v.>..v>>>>.>...v>vv.v
>>>>...>v..>vv>.>v...v..v>>v...v.v..v...>.>>..v>v.v.vv>.>v....v...v.v>.v>>v..v.v>v>...>vv>..>vv>>..vv>..>.>.>..>.v.>>>..>...v>..>.>.v.v>vvv
>v>.>.>.v..vv.>v.vv>..v..v.....v>.>>>>..>v...v.>vvv.>.>>>.>>>.....v>.v..v.>.vv.v>..v..>vvvv..>vv.v>>>v.v>..v.v..v..>....>>>.>...v..>..v..>.
.>..v>v>.v.vvv....>..v>..v...>.vv.>v..>.v.v>>..>>.v.v.v.>...>.>..v>v>.......>>.>>.>v...>>v>.v..>.>>>....v>>..v.>.>v...v>v>>v.>.v>...>.v>...
..>>>v.>....vvv..>.v...v.v>v>.v.vv>..>.>v.v.>vv.v..>....v...>v.v>........>v>vv.v.v.v..>v>....>.>...>....v.v.>>v...>v.>v.v>>>v.v.v>v...>>>..
>.vvv.v.v>vv.v..v>.>.>.vvvvv.>.vv>vv....v>>>v.>v..v.v.>>>.>>>.>.>>..v......>vv>...>v....>>>>......v>..v>vv..v...v>.>.v..>v.>..>>.v>.>......
...v......vv.>.>...>v>vvv>>v>...v...>v...v.>....v.v...>vv..vv>v..>vv..v>.>.....v...v....>>...>v>v.vvv>..>>v.vvvvv...>>.v..>v.>.>v>..v>>.>>v
..v>.>..vv>v.>v.>>.>>>...v>vvv>v.v>>v>v>vv..vv...v......v..>.>.v..>.>.v.v.>>.>>>>...vvv..>>v>...v.>...>vv..vv>v.vv.>.v...>vv.>>>>>.>..v>v.>
>.........>.v>.>...>.v>.....>>..v.......>.v>...>>v>.vvv.......vv.>...v.>.v>...>v>.....>...v.>>vvv....>vv>..v>.vvv>...>.v.v...>v.>.v.>.....v
.v.>>>.vv>...>vv.>..vv..>v.>>.>..v.....v..>>>v..>..v>>.....>..v..vv.....v>......>v>vvv.vvv>>.>...v.>.v>.>v....>>>.......>..>>vv...>...>..>.
..>v>>...vv>.>>v.>.>.>vv.>>v...>..>v>..v>v>vv.v...>v>v.>..v>.v>>>.>...v>......v>..>v>v.v.>vv>v.>v.v..v.>..v..>.>v>>....v.>v>>vv>vv..>.....>
.v.>.v>v>...>vv>.vv...vvv.v.v>>..v>>>>..>>...>.vvv....v...>...>..>.>..v...v....>...>>.vv....v.v.>vv>v.>.>..vv>>vv>v>>>v>.>v.v>>..v>.>v..v..
.>..v..........>.>.vv.vvv.>v...>.vv.v..>v........v..>>vv>..>.v.>..>.>..v..>..vv.v>>v.....>>vv>>.>v.....>...v...>.v.v>.v>..v..>.>.>v>>..>.>.
.vv.>>>v.>.>...vv.v....>vv.>.v>>.v...v.>.>..>>>..>vv.>>..vvv.>....>>>..>v....vvv.>>.>.v>...>>...v.>..v...v.>>>.>>>..>>....vv.vvv.v...v..>.v
v>.v>.>...v>vvv.vv.v..v.v.v>.>.vv.>.>...v>>.v..v>...>>.v.........v>>vvv.v>.v>v...v..v>>>.v.>.>>>>v.......>vv..vv..v.vv>>...>vv.v..v.v......
>>.v>..>.>.>>...>...v..v>.v...v.>.>.v..vv....v..v.v>.>>.>>>v....>.>.v.>>>v....v>.>......v>..>>>v.>..v..vv>.v>>....>v>v....>v.>.v..>....vvvv
.vvv>>.v.>..vv..vvv......>.v>v.vv.>v>v.>>>..>v>...v.>.>vvvv......>vvv.v..>v.v..>v...v>>..>vv....v..v>v>....vv>..>.>....>....>v.vv.v>>.>>.v>
>.vvvvv..>v>v.>..v>.v....v..>..v.vv.>v>>>v>>v....>v....>v.>v>...>>vv.>...>v..>....>.>.v..v.>.v>>...vv.>.v.v>>v....vvv..v>....v>v.>v.>.v..>.
.v>.>v..>.v..>v.v...v.vv>....v.v>vv>.vvv>.>>.v.>v>>..>>>vvvv...>.>vv....v..v>.>.>...v.v>v>>..>v.v>v.v>>.>.v.>v>..v..>v>..>...>..>vvv>.v>..v
>.>.vv>.>>.>v>v>>>.>.>.>v..v..vv...>..........v>vv.>v..>..v.>v..>..v>.v>......>.v...v..v.v.v..v..>>..v..v>...v>vv>v.>>v.>>..vv...v.v>vv.>v.
...>v..vv>v..>>.vv>>.v..v>>...>.vvvv>..v.>v...>..>v.v...v.>>>.v...>>..v.>>.>....>.v.vv..v>....vv.v..>..vv..>....vv.v.v>.......v.>v.vv.>v>..
.v.v...>v......vvv>..>.v..>>..v..>v..>..v.>.>>.>.>.vv..>...>..vvv.>..v....v>v.>>.>>...>......vvv.vv....>>....>v.v>v>>>vv.>..>v...v.v.>....>
v.>.v>.>>>.v...>>.....v>...>v>>v..>..v....>>v.vv..>.v...>.>.>v..v.>.>..>v>>..v.>.>v>v>.>>.v.vvvv.v.>..>vv.....>>v....>.v.>>....>...>.v..vv.
v.vvvvv.v.......>..v>.v>v...v.>>>..>..>>>>..v..v..v...>....vv>v.v>......>.....>>>...v...v>vv>v>>.v.>.v...v.v>.v.>v..>..v.......vvvv.v.>v.>.".Split(Environment.NewLine).Select(l=> l.ToCharArray()).ToArray();

        public static readonly char[][] Sample = @"v...>>.vv>
.vv>>.vv..
>>.>v>...v
>>v>>.>.v.
v>v.vv.v..
>.>>..v...
.vv..>.>v.
v.v..>>v.v
....v..v.>".Split(Environment.NewLine).Select(l => l.ToCharArray()).ToArray();
    }
}
