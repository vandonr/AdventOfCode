using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    static class Day16
    {
        public static long Part2(BitArray input)
        {
            int i = 0;
            return input.ReadPacket(ref i);
        }

        private static long ReadPacket(this BitArray input, ref int i)
        {
            var version = input.Read(ref i, 3);
            var type = input.Read(ref i, 3);
            if (type == 4)
            {
                long number = 0;
                //skip over value
                while (input[i++])
                    number = (number << 4) + input.Read(ref i, 4);
                number = (number << 4) + input.Read(ref i, 4);
                return number;
            }
            else
            {
                var values = new List<long>();
                if (input[i++])
                {
                    for (var nbSub = input.Read(ref i, 11); nbSub > 0; nbSub--)
                        values.Add(input.ReadPacket(ref i));
                }
                else
                {
                    var lenSub = input.Read(ref i, 15);
                    var endSub = i + lenSub;
                    while (i < endSub)
                        values.Add(input.ReadPacket(ref i));
                }

                switch (type)
                {
                    case 0: //0 = sum
                        return values.Sum();
                    case 1: //1 = product
                        long val = 1;
                        foreach (var item in values)
                            val *= item;
                        return val;
                    case 2: //2 = min
                        return values.Min();
                    case 3: //3 = max
                        return values.Max();
                    case 5: //5 = gt
                        return values[0] > values[1] ? 1 : 0;
                    case 6: //6 = lt
                        return values[0] < values[1] ? 1 : 0;
                    case 7: //7 = equal
                        return values[0] == values[1] ? 1 : 0;
                }
            }
            return version;
        }

        public static int Part1(BitArray input)
        {
            int i = 0;
            return input.ReadPacketVersion(ref i);
        }

        private static int ReadPacketVersion(this BitArray input, ref int i)
        {
            var version = input.Read(ref i, 3);
            var type = input.Read(ref i, 3);
            if (type == 4)
            {
                //skip over value
                while (input[i])
                    i += 5;
                i += 5;
            }
            else
            {
                if (input[i++])
                {
                    for (var nbSub = input.Read(ref i, 11); nbSub > 0; nbSub--)
                        version += input.ReadPacketVersion(ref i);
                }
                else
                {
                    var lenSub = input.Read(ref i, 15);
                    var endSub = i + lenSub;
                    while (i < endSub)
                        version += input.ReadPacketVersion(ref i);
                }
            }
            return version;
        }

        private static int Read(this BitArray a, ref int from, int n)
        {
            int res = 0;
            for  (; n > 0; n--)
            {
                res = (res << 1) + (a[from++] ? 1 : 0);
            }
            return res;
        }

        public static readonly BitArray Input = Parse(@"005532447836402684AC7AB3801A800021F0961146B1007A1147C89440294D005C12D2A7BC992D3F4E50C72CDF29EECFD0ACD5CC016962099194002CE31C5D3005F401296CAF4B656A46B2DE5588015C913D8653A3A001B9C3C93D7AC672F4FF78C136532E6E0007FCDFA975A3004B002E69EC4FD2D32CDF3FFDDAF01C91FCA7B41700263818025A00B48DEF3DFB89D26C3281A200F4C5AF57582527BC1890042DE00B4B324DBA4FAFCE473EF7CC0802B59DA28580212B3BD99A78C8004EC300761DC128EE40086C4F8E50F0C01882D0FE29900A01C01C2C96F38FCBB3E18C96F38FCBB3E1BCC57E2AA0154EDEC45096712A64A2520C6401A9E80213D98562653D98562612A06C0143CB03C529B5D9FD87CBA64F88CA439EC5BB299718023800D3CE7A935F9EA884F5EFAE9E10079125AF39E80212330F93EC7DAD7A9D5C4002A24A806A0062019B6600730173640575A0147C60070011FCA005000F7080385800CBEE006800A30C023520077A401840004BAC00D7A001FB31AAD10CC016923DA00686769E019DA780D0022394854167C2A56FB75200D33801F696D5B922F98B68B64E02460054CAE900949401BB80021D0562344E00042A16C6B8253000600B78020200E44386B068401E8391661C4E14B804D3B6B27CFE98E73BCF55B65762C402768803F09620419100661EC2A8CE0008741A83917CC024970D9E718DD341640259D80200008444D8F713C401D88310E2EC9F20F3330E059009118019A8803F12A0FC6E1006E3744183D27312200D4AC01693F5A131C93F5A131C970D6008867379CD3221289B13D402492EE377917CACEDB3695AD61C939C7C10082597E3740E857396499EA31980293F4FD206B40123CEE27CFB64D5E57B9ACC7F993D9495444001C998E66B50896B0B90050D34DF3295289128E73070E00A4E7A389224323005E801049351952694C000");

        public static readonly BitArray Sample16 = Parse("8A004A801A8002F478");
        public static readonly BitArray Sample12 = Parse("620080001611562C8802118E34");
        public static readonly BitArray Sample23 = Parse("C0015000016115A2E0802F182340");
        public static readonly BitArray Sample31 = Parse("A0016C880162017C3686B18A3D4780");

        private static BitArray Parse(string input)
        {
            int i = 0;
            var res = new BitArray(input.Length * 4);
            var bytes = input.Chunk(2).Select(hex => byte.Parse(new string(hex.ToArray()), System.Globalization.NumberStyles.HexNumber));
            foreach(var b in bytes)
            {
                for (var o = 7; o >= 0; o--)
                    res[i++] = ((b >> o) & 1) == 1;
            }
            return res;
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> list, int chunkSize)
        {
            while (list.Any())
            {
                yield return list.Take(chunkSize);
                list = list.Skip(chunkSize);
            }
        }
    }
}
