﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18_Keys
{
    class Program
    {
        static void Main(string[] args)
        {
            int getQuadrant(int p) => (p / 40000)*2 + (p % 1000 / 40);
            var q1 = getQuadrant(39039);
            var q2 = getQuadrant(39041);
            var q3 = getQuadrant(41039);
            var q4 = getQuadrant(41041);

            var input1 = @"
#########
#b.A.@.a#
#########";
            var input2 = @"
########################
#f.D.E.e.C.b.A.@.a.B.c.#
######################.#
#d.....................#
########################";

            var maze = Maze.Parse(Input.Quiz2);
            var steps = maze.Walk();
            Console.WriteLine("Result: " + steps);
        }
    }

    public class Maze
    {
        private HashSet<int> tiles;
        private int[] starts;
        private Dictionary<int, char> keys;
        private Dictionary<char, int> doors;
        private string _path;

        public Maze(HashSet<int> tiles, int[] starts, Dictionary<int, char> keys, Dictionary<char, int> doors, string path)
        {
            this.tiles = tiles;
            this.starts = starts;
            this.keys = keys;
            this.doors = doors;
            _path = path;
        }

        static Dictionary<string, int> _calculatedPaths = new Dictionary<string, int>();

        public int Walk()
        {
            if (keys.Count() == 0)
            {
                return 0;
            }
            var positions = starts.ToList();
            var keysLeft = keys.ToDictionary(x => x.Key, x => x.Value);
            var nextPositions = new List<int>();
            var alternatives = new List<int>();
            var tilesLeft = new HashSet<int>(tiles);
            var steps = 0;

            while (positions.Any() && keysLeft.Any())
            {
                steps++;
                foreach (var position in positions)
                {
                    foreach (var tryPos in new[] { position - 1, position + 1, position - 1000, position + 1000 })
                    {
                        if (keysLeft.TryGetValue(tryPos, out char key))
                        {
                            var testStarts = new[] { starts[0], starts[1], starts[2], starts[3] };
                            int getQuadrant(int p) => (p / 40000) * 2 + (p % 1000 / 40);
                            testStarts[getQuadrant(tryPos)] = tryPos;
                            string testPath = new string((_path + key).OrderBy(x => x).ToArray());
                            string testPathKey = testPath + string.Join("|", testStarts);
                            if (_calculatedPaths.TryGetValue(testPathKey, out int tmp))
                            {
                                alternatives.Add(steps + tmp);
                            }
                            else
                            {
                                Console.Write(key);
                                var testTiles = new HashSet<int>(tiles);
                                testTiles.Add(tryPos);
                                var doorName = (char)(key + 'A' - 'a');
                                if (doors.ContainsKey(doorName)) testTiles.Add(doors[doorName]);
                                var testKeys = keys.Where(x => x.Key != tryPos).ToDictionary(x => x.Key, x => x.Value);
                                var testDoors = doors.Where(x => x.Value != tryPos).ToDictionary(x => x.Key, x => x.Value);

                                var maze = new Maze(
                                    testTiles,
                                    testStarts,
                                    testKeys,
                                    testDoors,
                                    testPath
                                    );
                                var cost = maze.Walk();
                                alternatives.Add(steps + cost);
                                _calculatedPaths.Add(testPathKey, cost);
                                Console.Write("\b");
                            }
                        }
                        else if (tilesLeft.Remove(tryPos))
                        {
                            nextPositions.Add(tryPos);
                        }
                    }
                }
                positions = nextPositions;
                nextPositions = new List<int>();
            }

            if(alternatives.Any()==false)
            {
                foreach ( var tile in tilesLeft)
                {
                    Console.SetCursorPosition(tile % 1000, tile / 1000);
                    Console.Write(".");
                }
                foreach ( var bot in starts)
                {
                    Console.SetCursorPosition(bot % 1000, bot / 1000);
                    Console.Write("@");
                }
                foreach ( var key in keysLeft)
                {
                    Console.SetCursorPosition(key.Key % 1000, key.Key / 1000);
                    Console.Write(key.Value);
                }
                foreach (var door in doors)
                {
                    Console.SetCursorPosition(door.Value % 1000, door.Value / 1000);
                    Console.Write(door.Key);
                }
            }
            return alternatives.Any() ? alternatives.Min() : 1000000;
        }

        //private object Clone()
        //{
        //    return new Maze(
        //        new HashSet<int>(tiles),
        //        start,
        //        keys.ToDictionary(x => x.Key, x => x.Value),
        //        doors.ToDictionary(x => x.Key, x => x.Value)
        //        );
        //}

        public static Maze Parse(string text)
        {
            var sr = new StringReader(text);
            string line;
            int y = 0;
            var tiles = new HashSet<int>();
            var doors = new Dictionary<char, int>();
            var keys = new Dictionary<int, char>();
            var starts = new int[4];
            var startsFound = 0;
            while ((line = sr.ReadLine()) != null)
            {
                for (int x = 0; x < line.Length; x++)
                {
                    var pos = y * 1000 + x;
                    var c = line[x];
                    switch (c)
                    {
                        case '#':
                            // ignore
                            break;
                        case ' ':
                        case '.':
                            tiles.Add(pos);
                            break;
                        case '@':
                            tiles.Add(pos);
                            starts[startsFound++] = pos;
                            break;
                        default:
                            if (c < 'a')
                            {
                                doors.Add(c, pos);
                            }
                            else
                            {
                                keys.Add(pos, c);
                            }
                            break;
                    }
                }
                y++;
            }

            return new Maze(tiles, starts, keys, doors, "");
        }
    }
    public class Input
    {
        public static string Quiz =
            @"#################################################################################
#...#z..#.....#...#...#.........#...#...#.............................#...#.....#
#.#.#.#.#.#.#.#.#.#.#.#####.###.###.#.#.#####.#######################H#.#.###.#.#
#.#.#.#...#.#...#...#.....#.#.#...#f..#.#...#.#.......#...#.........#...#.....#.#
#.#.#.#####.#############.#.#.###.###.#.#.#.#.#.#####.#.#.###.#####.###########.#
#.#...#...#.#.........#.....#...#...#.#.#.#...#.....#...#...#.#...#...#...#.....#
#.#####.#.#.#.#######.#######.#####.#.#.#.###.#####.#######I#.#.#.#####X#.#.#####
#.#...#.#.#...#.....#...#...#.....#.#.#.#...#...#.#.#.....#.#.#.#.......#.#.#...#
#.#.#.###.#######.#.###.###.#.#.###.#.#.#.#.###.#.#.#.###.#.#.#.#########.#.#.#.#
#.#.#...#.#.......#...#.#...#.#...#.#.#.#.#.#...#w#b..#.#.#.#.....#...#...#.#.#.#
#.#.###.#.#.#.#######.#.#.###.###.#.###.###.#.###.#####.#.#.###.###Y#.#.#.#.#.###
#.#.#...#...#...#.....#.#.#...#.#...#...#...#.......#.....#...#.#...#.#.#.#.#...#
#.#.#.#########.#.#####.#.#.###.#####.#.#.###########.#####.#.#.#.###.#.###.###.#
#.#.#...#.......#.#.....#.....#.......#.#............j#...#.#.#.#...#.#.....#s..#
#.#.###.#.###.#####.#.#######.#####.###.###############.#.###.###.###.#.#####.#G#
#.C.#.#...#...#.....#.#.....#.....#...#.#.............#.#.....#...#...#.#.....#.#
#####.#####.###.#######.###.#####.###.#.#.###.#####.###.#.#####.###.###.#.#####.#
#...#...#...#...#.....#.#.#.#.......#.#.#.#.#.#.....#...#.#.....#.#...#...#...#.#
#.#.###.#.###.#.#.###.#.#.#.#######.#.#.#.#.#.#.#####.#####.#####.###.#######.#.#
#.#...#...#.#.#.#...#...#.#.#.....#...#.#.#.#.#.#.....#...........T.#.#...U.#.#.#
#.###.#.###.#.#.#.#####.#.#.#.###.#####.#.#.#.###.#.###.#############.#.###.#.#.#
#...#.#.....#.#.#.#...#...#.....#.....#.#...#.#...#.#...#.P...#.E...#...#...#...#
#.#.#.#####.#.###.#.###############.#.###.###.#.###.#.###.#####.###.#####.###.###
#.#.#.....#.#.#...#...............#.#...#.#...#...#.#.#.#...#...#.#.#.N.#...#...#
#.#.#####.###.#.#################.#.###.###.#.###R#.#.#.###.###.#.#.#.#.###.###.#
#.#.....#.#...#...#.........#...#.#...#.#...#.#...#.#.#...#p#...#.#e#.#.....#...#
#.#####.#.#.#####.###.#.#####.#.#.#####.#.#####.###.#V###.#.#.###.#.#.#######.###
#.#...#.#...#...#...#.#.#.....#.#.....#.#.....#.#...#...#.#...#...#...#...#...#.#
#.#.#.#.###.#.#.###.#.#.#.#####.#####.#.#.###.#.#######.#.#######.#######.#.###.#
#.#.#.#...#.#.#.....#.#.......#...#...#.#.#...#.........#...#.........#...#.....#
#.#.#.###.###.#######.#########.#.#.###.###.#.###########.#.#.#.#####.#.#.#####.#
#m#.#...#.....#...#.....#...#...#.#.....#...#...#...#.....#.#.#.#.......#...#...#
#.###O#.#######.#.#####.#.#.#.###.#######.#####.#.###.#.###.#.#.###########.#.###
#...#.#.#.......#.....#.#.#.#...#.....#.#.#...#...#..k#...#.#.#.#.....#...#.#.#.#
###.#.#.###.#########.###.#.###.#####.#.#.#.#.###.#.#####.###L#.#.###.#.#.###.#.#
#.#.#.#.....#l#.....#a..#.#.....#...#.#.#.#.#.#...#...#.#.#...#.#.#.#...#...#.#.#
#.#.#.#######.#.###.###.#.#######.###.#.#.#.#.#######.#.#.#.###.#.#.#######.#.#.#
#...#.#.#.K...#...#...#...#...........#.#...#.......#.#.#.#.#...#...#o....#.#...#
#Q###.#.#.#.#####.#.#######.###########.#.#######.###.#.#.#.#.#####.#.###.#.###.#
#.....#...#.......#............d................#.......#...#.........#...#.....#
#######################################.@.#######################################
#...........#.....#.....#...#.....#.......#.....#...D.......#...#.........#.....#
#.#.#######.#.###.#.###.#.#.###.#.###.#.#.#.#.###.###.#.#####.#.#.###.###.#.###.#
#g#.....#...#.#.#.#.#.....#.....#.#...#.#...#.....#.#.#.#.....#.....#.#.....#...#
#.#####.#.###.#.#.#.#############.#.###.#.#########.#.###.#########.#.#########.#
#.....#.#.#...#.....#...#...#...#.#...#.#.......#...#.....#...#...#.#.#.......#.#
#######.#.###.#######.#.#.#.#.#.#.#.#.#########.#.#########.#.#.#.###.#.#####.#.#
#.......#.....#.......#...#.#.#...#.#...#.....#.#.#.........#...#.....#...#.#.#.#
#.###########.#.###########.#.#########.#.#.###.#.#.#####################.#.#M#.#
#.#.........#.#.#.....#.....#...#.......#.#.#...#.#.....#.......#...........#.#.#
#.#.#######.#.#.#####.#.#######.###.###.#.#.#.###.#####.#.#####.#.###########.#.#
#.#.......#.#.#.....#.#.#.#...#...#.#.#.#.#.#.........#...#.#...#.....#.....#.#.#
#.#######.#.#######.#.#.#.#.#.###.#.#.#.#.#.#########.#####.#.###.#####.###.#.#.#
#.#.....S.#.......#.#.#.#.#.#.....#.#...#.#.........#...#...#.....#...#.#...#.#.#
#.#.#########.#.###.#.#.#.#.#######.#.###########.#.###.###.#######.#.#.#.###.#.#
#...#.......#.#.#.....#.#.#...#.....#...#.......#.#.......#...#.....#...#...#.#.#
#######.###.#.###.#####.#.###.#.#######.#.#####.#.#######.###.#.###########.#.#.#
#.....#.#...#.....#.....#...#...#...#...#.#.#...#.#.....#.....#.#.....#.....#.#.#
#.###.#.#.#########.#####.#########.#.###.#.#.#####.###.#.#####.#.###.#.#####.#A#
#.#.....#.....#...#...#...#.........#...#...#.....#.#...#.#...#.#...#.#.#.....#.#
#.#.###########.#.###.#.#.#.###.#.#####.###F#####.#.#.#####.#.#.###.#.#.###.###.#
#.#...#.........#...#.#.#...#...#.#.....#.#...#.#v..#....t#.#n#...#.#.#.#...#...#
#.#.###.###########.###.#####.#####.#####.###.#.#########.#.###.#.#.###.#.###.###
#.#.#.....#.........#.#.#...#...#...#...#...#.#.......#...#...#.#.#.#.....#.#.#.#
#.#.#.#####.#######.#.#.#.#####.#.#####.#.###.###.###.#.#.###.###.#.#.#####.#.#.#
#.#.#...#...#.....#...#.#.#...#.#.#...#.#...#.....#...#.#...#...#.#...#...#...#.#
#.#####.#.#.#.###.###.#.#.#.#.#.#.#.#.#.###.#####.#####.#######.#.###.#.#.###.#.#
#.#.B.#.#.#.#...#...#.#.#...#.#.#...#.#.#.....#...#..y#.#..u....#...#.#.#...#.#.#
#.#.#.#.#.#.###.###.###.#####.#.#####.#.#.###.#.###.#.#.#.#######.#.###.###.#.#.#
#.#.#.#.#.#.#.....#...#.......#.....#...#.#.#.#.#.#.#...#.#.......#.......#.#...#
#.#.#.#.#.###.#######.#######.#####.###.#.#.#.#.#.#.#####.#.#############.#.###.#
#...#...#.....#.....#.......#.#.#...#.#.#.#...#...#.......#.#.........#...#c#.#.#
#.#############.###.#######.#.#.#.#.#.#.#.#.#####.#########.#.#######.#.###.#.#.#
#.#...........#...#...#...#.#.#.#.#...#.#.#.#...#.#.#.....Z.#.......#.#.#...#...#
#.#########.#####.###.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#############.#.###.###.###
#i#.......#.#...#x#..h..#...#.#.#.#.#...#.#...#r#.#...#.....#.J.....#...#...#...#
#.#.#####.#.#.#.#.#########.#.#.#.###.###.###.###.#####.###.#.#########.###.###.#
#.#...#...#.#.#...#...#...#.#.#.#.#...#.#...#.#...#...#.#.#...#.......#...#.#q..#
#.###.#.###.#.#####.#.#.#.###.#.#.#.###.#.#.###.###.#.#.#.#####.###.#####.#.#.###
#.....#.....#.......#...#.......#.......#.#.........#...#...W.....#.........#...#
#################################################################################";
        public static string Quiz2 =
            @"#################################################################################
#   #z  #     #   #   #         #   #   #                             #   #     #
# # # # # # # # # # # ##### ### ### # # ##### #######################H# # ### # #
# # # #   # #   #   #     # # #   #f  # #   # #       #   #         #   #     # #
# # # ##### ############# # # ### ### # # # # # ##### # # ### ##### ########### #
# #   #   # #         #     #   #   # # # #   #     #   #   # #   #   #   #     #
# ##### # # # ####### ####### ##### # # # ### ##### #######I# # # #####X# # #####
# #   # # #   #     #   #   #     # # # #   #   # # #     # # # #       # # #   #
# # # ### ####### # ### ### # # ### # # # # ### # # # ### # # # ######### # # # #
# # #   # #       #   # #   # #   # # # # # #   #w#b  # # # #     #   #   # # # #
# # ### # # # ####### # # ### ### # ### ### # ### ##### # # ### ###Y# # # # # ###
# # #   #   #   #     # # #   # #   #   #   #       #     #   # #   # # # # #   #
# # # ######### # ##### # # ### ##### # # ########### ##### # # # ### # ### ### #
# # #   #       # #     #     #       # #            j#   # # # #   # #     #s  #
# # ### # ### ##### # ####### ##### ### ############### # ### ### ### # ##### #G#
# C # #   #   #     # #     #     #   # #             # #     #   #   # #     # #
##### ##### ### ####### ### ##### ### # # ### ##### ### # ##### ### ### # ##### #
#   #   #   #   #     # # # #       # # # # # #     #   # #     # #   #   #   # #
# # ### # ### # # ### # # # ####### # # # # # # ##### ##### ##### ### ####### # #
# #   #   # # # #   #   # # #     #   # # # # # #     #           T # #   U # # #
# ### # ### # # # ##### # # # ### ##### # # # ### # ### ############# # ### # # #
#   # #     # # # #   #   #     #     # #   # #   # #   # P   # E   #   #   #   #
# # # ##### # ### # ############### # ### ### # ### # ### ##### ### ##### ### ###
# # #     # # #   #               # #   # #   #   # # # #   #   # # # N #   #   #
# # ##### ### # ################# # ### ### # ###R# # # ### ### # # # # ### ### #
# #     # #   #   #         #   # #   # #   # #   # # #   #p#   # #e# #     #   #
# ##### # # ##### ### # ##### # # ##### # ##### ### #V### # # ### # # ####### ###
# #   # #   #   #   # # #     # #     # #     # #   #   # #   #   #   #   #   # #
# # # # ### # # ### # # # ##### ##### # # ### # ####### # ####### ####### # ### #
# # # #   # # #     # #       #   #   # # #   #         #   #         #   #     #
# # # ### ### ####### ######### # # ### ### # ########### # # # ##### # # ##### #
#m# #   #     #   #     #   #   # #     #   #   #   #     # # # #       #   #   #
# ###O# ####### # ##### # # # ### ####### ##### # ### # ### # # ########### # ###
#   # # #       #     # # # #   #     # # #   #   #  k#   # # # #     #   # # # #
### # # ### ######### ### # ### ##### # # # # ### # ##### ###L# # ### # # ### # #
# # # #     #l#     #a  # #     #   # # # # # #   #   # # #   # # # #   #   # # #
# # # ####### # ### ### # ####### ### # # # # ####### # # # ### # # ####### # # #
#   # # # K   #   #   #   #           # #   #       # # # # #   #   #o    # #   #
#Q### # # # ##### # ####### ########### # ####### ### # # # # ##### # ### # ### #
#     #   #       #            d       @#@      #       #   #         #   #     #
#################################################################################
#           #     #     #   #     #    @#@#     #   D       #   #         #     #
# # ####### # ### # ### # # ### # ### # # # # ### ### # ##### # # ### ### # ### #
#g#     #   # # # # #     #     # #   # #   #     # # # #     #     # #     #   #
# ##### # ### # # # ############# # ### # ######### # ### ######### # ######### #
#     # # #   #     #   #   #   # #   # #       #   #     #   #   # # #       # #
####### # ### ####### # # # # # # # # ######### # ######### # # # ### # ##### # #
#       #     #       #   # # #   # #   #     # # #         #   #     #   # # # #
# ########### # ########### # ######### # # ### # # ##################### # #M# #
# #         # # #     #     #   #       # # #   # #     #       #           # # #
# # ####### # # ##### # ####### ### ### # # # ### ##### # ##### # ########### # #
# #       # # #     # # # #   #   # # # # # #         #   # #   #     #     # # #
# ####### # ####### # # # # # ### # # # # # ######### ##### # ### ##### ### # # #
# #     S #       # # # # # #     # #   # #         #   #   #     #   # #   # # #
# # ######### # ### # # # # ####### # ########### # ### ### ####### # # # ### # #
#   #       # # #     # # #   #     #   #       # #       #   #     #   #   # # #
####### ### # ### ##### # ### # ####### # ##### # ####### ### # ########### # # #
#     # #   #     #     #   #   #   #   # # #   # #     #     # #     #     # # #
# ### # # ######### ##### ######### # ### # # ##### ### # ##### # ### # ##### #A#
# #     #     #   #   #   #         #   #   #     # #   # #   # #   # # #     # #
# # ########### # ### # # # ### # ##### ###F##### # # ##### # # ### # # ### ### #
# #   #         #   # # #   #   # #     # #   # #v  #    t# #n#   # # # #   #   #
# # ### ########### ### ##### ##### ##### ### # ######### # ### # # ### # ### ###
# # #     #         # # #   #   #   #   #   # #       #   #   # # # #     # # # #
# # # ##### ####### # # # ##### # ##### # ### ### ### # # ### ### # # ##### # # #
# # #   #   #     #   # # #   # # #   # #   #     #   # #   #   # #   #   #   # #
# ##### # # # ### ### # # # # # # # # # ### ##### ##### ####### # ### # # ### # #
# # B # # # #   #   # # #   # # #   # # #     #   #  y# #  u    #   # # #   # # #
# # # # # # ### ### ### ##### # ##### # # ### # ### # # # ####### # ### ### # # #
# # # # # # #     #   #       #     #   # # # # # # #   # #       #       # #   #
# # # # # ### ####### ####### ##### ### # # # # # # ##### # ############# # ### #
#   #   #     #     #       # # #   # # # #   #   #       # #         #   #c# # #
# ############# ### ####### # # # # # # # # ##### ######### # ####### # ### # # #
# #           #   #   #   # # # # #   # # # #   # # #     Z #       # # #   #   #
# ######### ##### ### # # # # # # # ### # # # # # # # ############# # ### ### ###
#i#       # #   #x#  h  #   # # # # #   # #   #r# #   #     # J     #   #   #   #
# # ##### # # # # ######### # # # ### ### ### ### ##### ### # ######### ### ### #
# #   #   # # #   #   #   # # # # #   # #   # #   #   # # #   #       #   # #q  #
# ### # ### # ##### # # # ### # # # ### # # ### ### # # # ##### ### ##### # # ###
#     #     #       #   #       #       # #         #   #   W     #         #   #
#################################################################################";
    }
}
