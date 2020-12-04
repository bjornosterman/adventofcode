using AdventOfCode;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day17_ASCII
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(228, 60);

            var machine = new IntMachine("1,330,331,332,109,4364,1102,1182,1,15,1102,1,1449,24,1001,0,0,570,1006,570,36,1001,571,0,0,1001,570,-1,570,1001,24,1,24,1105,1,18,1008,571,0,571,1001,15,1,15,1008,15,1449,570,1006,570,14,21102,58,1,0,1106,0,786,1006,332,62,99,21102,1,333,1,21101,0,73,0,1106,0,579,1102,1,0,572,1102,0,1,573,3,574,101,1,573,573,1007,574,65,570,1005,570,151,107,67,574,570,1005,570,151,1001,574,-64,574,1002,574,-1,574,1001,572,1,572,1007,572,11,570,1006,570,165,101,1182,572,127,102,1,574,0,3,574,101,1,573,573,1008,574,10,570,1005,570,189,1008,574,44,570,1006,570,158,1105,1,81,21101,340,0,1,1105,1,177,21102,1,477,1,1106,0,177,21102,1,514,1,21102,176,1,0,1105,1,579,99,21102,1,184,0,1106,0,579,4,574,104,10,99,1007,573,22,570,1006,570,165,1001,572,0,1182,21102,1,375,1,21102,1,211,0,1105,1,579,21101,1182,11,1,21101,222,0,0,1105,1,979,21102,388,1,1,21102,233,1,0,1106,0,579,21101,1182,22,1,21102,244,1,0,1106,0,979,21101,0,401,1,21101,255,0,0,1106,0,579,21101,1182,33,1,21102,1,266,0,1106,0,979,21101,414,0,1,21102,277,1,0,1106,0,579,3,575,1008,575,89,570,1008,575,121,575,1,575,570,575,3,574,1008,574,10,570,1006,570,291,104,10,21102,1182,1,1,21102,1,313,0,1106,0,622,1005,575,327,1102,1,1,575,21101,327,0,0,1106,0,786,4,438,99,0,1,1,6,77,97,105,110,58,10,33,10,69,120,112,101,99,116,101,100,32,102,117,110,99,116,105,111,110,32,110,97,109,101,32,98,117,116,32,103,111,116,58,32,0,12,70,117,110,99,116,105,111,110,32,65,58,10,12,70,117,110,99,116,105,111,110,32,66,58,10,12,70,117,110,99,116,105,111,110,32,67,58,10,23,67,111,110,116,105,110,117,111,117,115,32,118,105,100,101,111,32,102,101,101,100,63,10,0,37,10,69,120,112,101,99,116,101,100,32,82,44,32,76,44,32,111,114,32,100,105,115,116,97,110,99,101,32,98,117,116,32,103,111,116,58,32,36,10,69,120,112,101,99,116,101,100,32,99,111,109,109,97,32,111,114,32,110,101,119,108,105,110,101,32,98,117,116,32,103,111,116,58,32,43,10,68,101,102,105,110,105,116,105,111,110,115,32,109,97,121,32,98,101,32,97,116,32,109,111,115,116,32,50,48,32,99,104,97,114,97,99,116,101,114,115,33,10,94,62,118,60,0,1,0,-1,-1,0,1,0,0,0,0,0,0,1,24,0,0,109,4,1202,-3,1,586,21001,0,0,-1,22101,1,-3,-3,21102,1,0,-2,2208,-2,-1,570,1005,570,617,2201,-3,-2,609,4,0,21201,-2,1,-2,1105,1,597,109,-4,2105,1,0,109,5,1201,-4,0,630,20102,1,0,-2,22101,1,-4,-4,21102,1,0,-3,2208,-3,-2,570,1005,570,781,2201,-4,-3,652,21002,0,1,-1,1208,-1,-4,570,1005,570,709,1208,-1,-5,570,1005,570,734,1207,-1,0,570,1005,570,759,1206,-1,774,1001,578,562,684,1,0,576,576,1001,578,566,692,1,0,577,577,21102,702,1,0,1105,1,786,21201,-1,-1,-1,1105,1,676,1001,578,1,578,1008,578,4,570,1006,570,724,1001,578,-4,578,21102,1,731,0,1105,1,786,1106,0,774,1001,578,-1,578,1008,578,-1,570,1006,570,749,1001,578,4,578,21102,1,756,0,1105,1,786,1106,0,774,21202,-1,-11,1,22101,1182,1,1,21101,0,774,0,1105,1,622,21201,-3,1,-3,1106,0,640,109,-5,2106,0,0,109,7,1005,575,802,21001,576,0,-6,21001,577,0,-5,1105,1,814,21102,0,1,-1,21101,0,0,-5,21102,0,1,-6,20208,-6,576,-2,208,-5,577,570,22002,570,-2,-2,21202,-5,55,-3,22201,-6,-3,-3,22101,1449,-3,-3,2102,1,-3,843,1005,0,863,21202,-2,42,-4,22101,46,-4,-4,1206,-2,924,21102,1,1,-1,1105,1,924,1205,-2,873,21101,0,35,-4,1105,1,924,1201,-3,0,878,1008,0,1,570,1006,570,916,1001,374,1,374,1202,-3,1,895,1101,0,2,0,1201,-3,0,902,1001,438,0,438,2202,-6,-5,570,1,570,374,570,1,570,438,438,1001,578,558,922,20101,0,0,-4,1006,575,959,204,-4,22101,1,-6,-6,1208,-6,55,570,1006,570,814,104,10,22101,1,-5,-5,1208,-5,53,570,1006,570,810,104,10,1206,-1,974,99,1206,-1,974,1101,1,0,575,21102,973,1,0,1105,1,786,99,109,-7,2105,1,0,109,6,21102,0,1,-4,21101,0,0,-3,203,-2,22101,1,-3,-3,21208,-2,82,-1,1205,-1,1030,21208,-2,76,-1,1205,-1,1037,21207,-2,48,-1,1205,-1,1124,22107,57,-2,-1,1205,-1,1124,21201,-2,-48,-2,1106,0,1041,21101,-4,0,-2,1105,1,1041,21101,0,-5,-2,21201,-4,1,-4,21207,-4,11,-1,1206,-1,1138,2201,-5,-4,1059,2101,0,-2,0,203,-2,22101,1,-3,-3,21207,-2,48,-1,1205,-1,1107,22107,57,-2,-1,1205,-1,1107,21201,-2,-48,-2,2201,-5,-4,1090,20102,10,0,-1,22201,-2,-1,-2,2201,-5,-4,1103,1201,-2,0,0,1105,1,1060,21208,-2,10,-1,1205,-1,1162,21208,-2,44,-1,1206,-1,1131,1105,1,989,21102,439,1,1,1105,1,1150,21102,477,1,1,1105,1,1150,21101,514,0,1,21101,1149,0,0,1105,1,579,99,21101,1157,0,0,1106,0,579,204,-2,104,10,99,21207,-3,22,-1,1206,-1,1138,2102,1,-5,1176,1201,-4,0,0,109,-6,2105,1,0,14,11,44,1,54,1,54,1,54,1,54,1,54,1,54,1,46,9,46,1,54,1,54,1,54,1,54,1,54,1,54,1,48,7,48,1,54,1,54,1,54,1,47,8,47,1,6,1,47,1,6,1,47,1,6,1,47,1,6,1,47,1,6,9,39,1,14,1,39,1,14,1,13,9,9,9,14,1,13,1,7,1,9,1,22,1,13,1,7,1,9,1,22,1,13,1,7,1,9,1,22,1,1,9,3,1,7,1,9,1,22,1,1,1,7,1,3,1,7,1,9,1,22,9,1,1,3,11,7,1,24,1,5,1,1,1,11,1,1,1,7,1,24,1,5,1,1,1,11,11,24,1,5,1,1,1,13,1,32,1,1,7,13,1,32,1,1,1,3,1,15,1,32,7,11,7,32,1,15,1,3,1,1,1,32,1,13,7,1,1,32,1,13,1,1,1,5,1,32,1,1,7,5,1,1,1,5,1,32,1,1,1,5,1,5,1,1,1,5,1,32,11,3,1,1,1,5,1,34,1,5,1,1,1,3,1,1,1,5,1,34,1,5,1,1,1,3,9,34,1,5,1,1,1,5,1,40,1,5,9,40,1,7,1,46,9,32");
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            int start = 0;
            char direction = ' ';
            int pos = 0;
            var rails = new List<int>();
            var answer = "A,A,C,B,C,B,C,B,C,A\nL,10,L,8,R,8,L,8,R,6\nR,6,R,6,L,8,L,10\nR,6,R,8,R,8\nn\n";
            var answer_i = 0;

            machine = new IntMachine("2,330,331,332,109,4364,1102,1182,1,15,1102,1,1449,24,1001,0,0,570,1006,570,36,1001,571,0,0,1001,570,-1,570,1001,24,1,24,1105,1,18,1008,571,0,571,1001,15,1,15,1008,15,1449,570,1006,570,14,21102,58,1,0,1106,0,786,1006,332,62,99,21102,1,333,1,21101,0,73,0,1106,0,579,1102,1,0,572,1102,0,1,573,3,574,101,1,573,573,1007,574,65,570,1005,570,151,107,67,574,570,1005,570,151,1001,574,-64,574,1002,574,-1,574,1001,572,1,572,1007,572,11,570,1006,570,165,101,1182,572,127,102,1,574,0,3,574,101,1,573,573,1008,574,10,570,1005,570,189,1008,574,44,570,1006,570,158,1105,1,81,21101,340,0,1,1105,1,177,21102,1,477,1,1106,0,177,21102,1,514,1,21102,176,1,0,1105,1,579,99,21102,1,184,0,1106,0,579,4,574,104,10,99,1007,573,22,570,1006,570,165,1001,572,0,1182,21102,1,375,1,21102,1,211,0,1105,1,579,21101,1182,11,1,21101,222,0,0,1105,1,979,21102,388,1,1,21102,233,1,0,1106,0,579,21101,1182,22,1,21102,244,1,0,1106,0,979,21101,0,401,1,21101,255,0,0,1106,0,579,21101,1182,33,1,21102,1,266,0,1106,0,979,21101,414,0,1,21102,277,1,0,1106,0,579,3,575,1008,575,89,570,1008,575,121,575,1,575,570,575,3,574,1008,574,10,570,1006,570,291,104,10,21102,1182,1,1,21102,1,313,0,1106,0,622,1005,575,327,1102,1,1,575,21101,327,0,0,1106,0,786,4,438,99,0,1,1,6,77,97,105,110,58,10,33,10,69,120,112,101,99,116,101,100,32,102,117,110,99,116,105,111,110,32,110,97,109,101,32,98,117,116,32,103,111,116,58,32,0,12,70,117,110,99,116,105,111,110,32,65,58,10,12,70,117,110,99,116,105,111,110,32,66,58,10,12,70,117,110,99,116,105,111,110,32,67,58,10,23,67,111,110,116,105,110,117,111,117,115,32,118,105,100,101,111,32,102,101,101,100,63,10,0,37,10,69,120,112,101,99,116,101,100,32,82,44,32,76,44,32,111,114,32,100,105,115,116,97,110,99,101,32,98,117,116,32,103,111,116,58,32,36,10,69,120,112,101,99,116,101,100,32,99,111,109,109,97,32,111,114,32,110,101,119,108,105,110,101,32,98,117,116,32,103,111,116,58,32,43,10,68,101,102,105,110,105,116,105,111,110,115,32,109,97,121,32,98,101,32,97,116,32,109,111,115,116,32,50,48,32,99,104,97,114,97,99,116,101,114,115,33,10,94,62,118,60,0,1,0,-1,-1,0,1,0,0,0,0,0,0,1,24,0,0,109,4,1202,-3,1,586,21001,0,0,-1,22101,1,-3,-3,21102,1,0,-2,2208,-2,-1,570,1005,570,617,2201,-3,-2,609,4,0,21201,-2,1,-2,1105,1,597,109,-4,2105,1,0,109,5,1201,-4,0,630,20102,1,0,-2,22101,1,-4,-4,21102,1,0,-3,2208,-3,-2,570,1005,570,781,2201,-4,-3,652,21002,0,1,-1,1208,-1,-4,570,1005,570,709,1208,-1,-5,570,1005,570,734,1207,-1,0,570,1005,570,759,1206,-1,774,1001,578,562,684,1,0,576,576,1001,578,566,692,1,0,577,577,21102,702,1,0,1105,1,786,21201,-1,-1,-1,1105,1,676,1001,578,1,578,1008,578,4,570,1006,570,724,1001,578,-4,578,21102,1,731,0,1105,1,786,1106,0,774,1001,578,-1,578,1008,578,-1,570,1006,570,749,1001,578,4,578,21102,1,756,0,1105,1,786,1106,0,774,21202,-1,-11,1,22101,1182,1,1,21101,0,774,0,1105,1,622,21201,-3,1,-3,1106,0,640,109,-5,2106,0,0,109,7,1005,575,802,21001,576,0,-6,21001,577,0,-5,1105,1,814,21102,0,1,-1,21101,0,0,-5,21102,0,1,-6,20208,-6,576,-2,208,-5,577,570,22002,570,-2,-2,21202,-5,55,-3,22201,-6,-3,-3,22101,1449,-3,-3,2102,1,-3,843,1005,0,863,21202,-2,42,-4,22101,46,-4,-4,1206,-2,924,21102,1,1,-1,1105,1,924,1205,-2,873,21101,0,35,-4,1105,1,924,1201,-3,0,878,1008,0,1,570,1006,570,916,1001,374,1,374,1202,-3,1,895,1101,0,2,0,1201,-3,0,902,1001,438,0,438,2202,-6,-5,570,1,570,374,570,1,570,438,438,1001,578,558,922,20101,0,0,-4,1006,575,959,204,-4,22101,1,-6,-6,1208,-6,55,570,1006,570,814,104,10,22101,1,-5,-5,1208,-5,53,570,1006,570,810,104,10,1206,-1,974,99,1206,-1,974,1101,1,0,575,21102,973,1,0,1105,1,786,99,109,-7,2105,1,0,109,6,21102,0,1,-4,21101,0,0,-3,203,-2,22101,1,-3,-3,21208,-2,82,-1,1205,-1,1030,21208,-2,76,-1,1205,-1,1037,21207,-2,48,-1,1205,-1,1124,22107,57,-2,-1,1205,-1,1124,21201,-2,-48,-2,1106,0,1041,21101,-4,0,-2,1105,1,1041,21101,0,-5,-2,21201,-4,1,-4,21207,-4,11,-1,1206,-1,1138,2201,-5,-4,1059,2101,0,-2,0,203,-2,22101,1,-3,-3,21207,-2,48,-1,1205,-1,1107,22107,57,-2,-1,1205,-1,1107,21201,-2,-48,-2,2201,-5,-4,1090,20102,10,0,-1,22201,-2,-1,-2,2201,-5,-4,1103,1201,-2,0,0,1105,1,1060,21208,-2,10,-1,1205,-1,1162,21208,-2,44,-1,1206,-1,1131,1105,1,989,21102,439,1,1,1105,1,1150,21102,477,1,1,1105,1,1150,21101,514,0,1,21101,1149,0,0,1105,1,579,99,21101,1157,0,0,1106,0,579,204,-2,104,10,99,21207,-3,22,-1,1206,-1,1138,2102,1,-5,1176,1201,-4,0,0,109,-6,2105,1,0,14,11,44,1,54,1,54,1,54,1,54,1,54,1,54,1,46,9,46,1,54,1,54,1,54,1,54,1,54,1,54,1,48,7,48,1,54,1,54,1,54,1,47,8,47,1,6,1,47,1,6,1,47,1,6,1,47,1,6,1,47,1,6,9,39,1,14,1,39,1,14,1,13,9,9,9,14,1,13,1,7,1,9,1,22,1,13,1,7,1,9,1,22,1,13,1,7,1,9,1,22,1,1,9,3,1,7,1,9,1,22,1,1,1,7,1,3,1,7,1,9,1,22,9,1,1,3,11,7,1,24,1,5,1,1,1,11,1,1,1,7,1,24,1,5,1,1,1,11,11,24,1,5,1,1,1,13,1,32,1,1,7,13,1,32,1,1,1,3,1,15,1,32,7,11,7,32,1,15,1,3,1,1,1,32,1,13,7,1,1,32,1,13,1,1,1,5,1,32,1,1,7,5,1,1,1,5,1,32,1,1,1,5,1,5,1,1,1,5,1,32,11,3,1,1,1,5,1,34,1,5,1,1,1,3,1,1,1,5,1,34,1,5,1,1,1,3,9,34,1,5,1,1,1,5,1,40,1,5,9,40,1,7,1,46,9,32");
            machine.Input = () => answer[answer_i++];
            machine.Output = o =>
            {
                Console.WriteLine("Dust: " + o);
            };
            machine.Run();
            return;
            machine.Output = o =>
            {
                var c = (char)o;
                Console.Write(c == '.' ? ' ' : c);
                switch (c)
                {
                    case '<':
                    case '>':
                    case 'v':
                    case '^':
                        pos = y * 1000 + x;
                        start = pos;
                        direction = c;
                        goto case '#';
                    case '#':
                        rails.Add(y * 1000 + x);
                        x++;
                        break;
                    case '.':
                        x++;
                        break;
                    case '\n':
                        if (x != 0) width = x;
                        x = 0;
                        y++;
                        height++;
                        break;
                }
            };
            machine.Run();

            bool isCross(int tile)
            {
                return rails.Contains(tile + 1) &&
                    rails.Contains(tile - 1) &&
                    rails.Contains(tile + 1000) &&
                    rails.Contains(tile - 1000);
            }

            var crosses = rails.Where(isCross).ToList();
            foreach (var cross in crosses)
            {
                Console.SetCursorPosition(cross % 1000, cross / 1000);
                Console.Write('O');
            }
            //Console.SetCursorPosition(0, height);
            //var sum = crosses.Sum(x => (x / 1000) * (x % 1000));
            //Console.WriteLine("Sum = " + sum);

            bool isRail(int p) => rails.Contains(p);
            int walk(int p, char d) => d switch { '<' => p - 1, '>' => p + 1, 'v' => p + 1000, '^' => p - 1000, _ => p };
            char turnLeft(char d) => d switch { '>' => '^', '^' => '<', '<' => 'v', 'v' => '>', _ => ' ' };
            char turnRight(char d) => d switch { '>' => 'v', '^' => '>', '<' => '^', 'v' => '<', _ => ' ' };
            bool canWalk(int p, char d) => isRail(walk(p, d));
            bool tryLeft(int p, char d) => canWalk(pos, turnLeft(d));
            bool tryRight(int p, char d) => canWalk(pos, turnRight(d));

            int walkLength = 0;
            var walks = new List<char>();
            while (true)
            {
                if (canWalk(pos, direction))
                {
                    walkLength++;
                    pos = walk(pos, direction);
                    Console.SetCursorPosition(pos % 1000, pos / 1000);
                    Console.Write('X');
                }
                else
                {
                    walks.Add((char)('0' + walkLength));
                    //Console.WriteLine(walkLength);
                    walkLength = 0;
                    if (tryLeft(pos, direction))
                    {
                        direction = turnLeft(direction);
                        walks.Add('L');
                        //Console.Write("L");
                    }
                    else if (tryRight(pos, direction))
                    {
                        direction = turnRight(direction);
                        walks.Add('R');
                        //Console.Write("R");
                    }
                    else break;
                }
            }
            //Console.SetCursorPosition(0, height);
            //Console.WriteLine(string.Join(',', walks));

            var path = string.Concat(walks);
            //Console.WriteLine(path);
            path = path
                .Replace(":", "22222")
                .Replace("8", "2222")
                .Replace("6", "222")
                .Replace("0", "")
                .Replace("2", "11");

            var perm = new List<Section>();
            for (int l = 1; l < path.Length; l++)
            {
                for (int i = 0; i + l < path.Length; i++)
                {
                    perm.Add(new Section { Pattern = path.Substring(i, l), Index = i });
                }
            }

            string compress(string input)
            {
                return input
                    .Replace("1111111111", ":")
                    .Replace("111111111", "9")
                    .Replace("11111111", "8")
                    .Replace("1111111", "7")
                    .Replace("111111", "6")
                    .Replace("11111", "5")
                    .Replace("1111", "4")
                    .Replace("111", "3")
                    .Replace("11", "2");

            }

            string toLine(string input)
            {
                return string.Join(",", input.Select(x => x == ':' ? "10" : x.ToString()));
            }

            string decompress(string input)
            {
                return input
                    .Replace(":", "1111111111")
                    .Replace("10", "1111111111")
                    .Replace("9", "111111111")
                    .Replace("8", "11111111")
                    .Replace("7", "1111111")
                    .Replace("6", "111111")
                    .Replace("5", "11111")
                    .Replace("4", "1111")
                    .Replace("3", "111")
                    .Replace("2", "11")
                    .Replace(",", "");
            }

            var grouping =
                new[] { new Part { Pattern = "ABC" }, new Part { Pattern = "L1111111111L11111111R11111111L11111111R111111" } }.Concat(
                perm
                .GroupBy(x => x.Pattern)
                .Where(x => toLine(compress(x.Key)).Length <= 20)
                .Where(x => x.Key.Length > 1 && x.Count() > 2)
                //.Where(x => x.Key.Length < 20 && x.Count() < 20)
                .OrderByDescending(x => x.Key.Length)
                .Where(x => path.Contains("L1111111111L11111111R11111111L11111111R111111" + x.Key))
                .Take(50)
                .Select(x => new Part { Pattern = x.Key, FoundSections = x.ToList() })
                )
                .ToList();

            int selectedA = -1;
            int selectedB = -1;
            int selectedC = -1;

            void drawMenyItem(int index, char prefix, bool selected)
            {
                Console.ForegroundColor = selected ? ConsoleColor.White : ConsoleColor.Cyan;
                Console.SetCursorPosition(60, index);
                Console.Write(new string(' ', 20));
                Console.SetCursorPosition(60, index);
                Console.Write(prefix + " " + toLine(compress(grouping[index].Pattern))); // + ": " + grouping[index]?.FoundSections?.Count());
            };

            int currentSuggestion = 0;

            void drawMeny()
            {
                for (int i = 0; i < grouping.Count; i++)
                {
                    var prefix =
                        i == 30 ? '*'
                        : i == selectedA ? 'A'
                        : i == selectedB ? 'B'
                        : i == selectedC ? 'C'
                        : ' ';
                    drawMenyItem(i, prefix, i == currentSuggestion);
                }
            };



            void drawSuggestion(Part part, string path, string paint = null)
            {
                path = decompress(path);
                if (paint == null) paint = new string('.', path.Length);
                var sections = part.FoundSections ?? new List<Section>();
                pos = start;
                direction = '^';
                ConsoleColor color;
                try
                {
                    for (int i = 0; i < path.Length; i++)
                    {
                        var isStart = sections.Any(x => x.Index == i);
                        var isEnd = sections.Any(x => i == x.Index + x.Pattern.Length - 1);
                        var isIn = sections.Any(x => i >= x.Index && i < x.Index + x.Pattern.Length);

                        var currentChar =
                            isStart && isEnd ? 'B' :
                            isStart ? 'S' :
                            isEnd ? 'E' :
                            isIn ? 'O' : paint[i];

                        color = currentChar switch { 'S' => ConsoleColor.Yellow, 'E' => ConsoleColor.Yellow, 'O' => ConsoleColor.Yellow, _ => ConsoleColor.Gray };

                        switch (path[i])
                        {
                            case 'L':
                                direction = turnLeft(direction);
                                break;
                            case 'R':
                                direction = turnRight(direction);
                                break;
                            default:
                                pos = walk(pos, direction);
                                //color = (sections.Any(x => x.Index == i && i < x.Index + x.Pattern.Length)) ? ConsoleColor.Green : ConsoleColor.White;
                                Console.ForegroundColor = color;
                                Console.SetCursorPosition(pos % 1000, pos / 1000);
                                Console.Write(currentChar);
                                //Console.Write(currentChar == 'E' ? 'O' : currentChar);
                                //pos = walk(pos, direction);
                                //Console.SetCursorPosition(pos % 1000, pos / 1000);
                                //Console.Write(currentChar == 'S' ? 'O' : currentChar);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(0, height);
                    Console.WriteLine(ex.Message);
                }
            }

            void Clear()
            {
                Console.SetCursorPosition(0, 0);
                var line = new string(' ', width);
                for (int i = 0; i < height; i++) Console.WriteLine(line);
            }

            void drawTry()
            {
                var testPath = string.Concat(grouping[0].Pattern.SelectMany(x => x switch
                {
                    'A' => selectedA == -1 ? "" : grouping[selectedA].Pattern,
                    'B' => selectedB == -1 ? "" : grouping[selectedB].Pattern,
                    'C' => selectedC == -1 ? "" : grouping[selectedC].Pattern,
                    _ => ""
                }));

                var testPaint = string.Concat(grouping[0].Pattern.SelectMany(x => x switch
                {
                    'A' => selectedA == -1 ? "" : new string('A', decompress(grouping[selectedA].Pattern).Length),
                    'B' => selectedB == -1 ? "" : new string('B', decompress(grouping[selectedB].Pattern).Length),
                    'C' => selectedC == -1 ? "" : new string('C', decompress(grouping[selectedC].Pattern).Length),
                    _ => ""
                }));

                drawSuggestion(new Part { }, path, new string('.', path.Length));
                drawSuggestion(new Part { }, testPath, testPaint);
            }

            var stop = false;

            drawMeny();

            while (!stop)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (currentSuggestion < grouping.Count) currentSuggestion++;
                        //drawMeny();
                        //drawSuggestion(grouping[currentSuggestion], path);
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentSuggestion > 0) currentSuggestion--;
                        break;
                    case ConsoleKey.Escape:
                        stop = true;
                        break;
                    case ConsoleKey.A:
                        if (currentSuggestion == 0)
                        {
                            grouping[0].Pattern += "A";
                        }
                        else
                        {
                            selectedA = currentSuggestion;
                        }
                        break;
                    case ConsoleKey.B:
                        if (currentSuggestion == 0)
                        {
                            grouping[0].Pattern += "B";
                        }
                        else
                        {
                            selectedB = currentSuggestion;
                        }
                        break;
                    case ConsoleKey.C:
                        if (currentSuggestion == 0)
                        {
                            grouping[0].Pattern += "C";
                        }
                        else
                        {
                            selectedC = currentSuggestion;
                        }
                        break;
                    case ConsoleKey.Backspace:
                        var g = grouping[currentSuggestion];
                        g.FoundSections = null;
                        if (g.Pattern.Length > 0) g.Pattern = g.Pattern.Substring(0, g.Pattern.Length - 1);
                        break;
                    case ConsoleKey.L:
                        grouping[currentSuggestion].FoundSections = null;
                        grouping[currentSuggestion].Pattern += "L";
                        break;
                    case ConsoleKey.R:
                        grouping[currentSuggestion].FoundSections = null;
                        grouping[currentSuggestion].Pattern += "R";
                        break;
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        grouping[currentSuggestion].FoundSections = null;
                        grouping[currentSuggestion].Pattern += key.KeyChar;
                        break;
                    case ConsoleKey.D0:
                        grouping[currentSuggestion].FoundSections = null;
                        grouping[currentSuggestion].Pattern += ":";
                        break;
                    case ConsoleKey.Delete:
                        grouping[currentSuggestion].FoundSections = null;
                        grouping[currentSuggestion].Pattern = "";
                        break;
                    case ConsoleKey.T:
                        currentSuggestion = 0;
                        break;
                }
                Clear();
                drawMeny();
                if (currentSuggestion == 0)
                {
                    drawTry();
                }
                else
                {
                    drawSuggestion(grouping[currentSuggestion], path);
                }

            }
        }


        public class Section
        {
            public string Pattern;
            public int Index;
        }

        public class Part
        {
            public string Pattern;
            public List<Section> FoundSections = new List<Section>();
        }
    }
}