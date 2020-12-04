using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24_Bugs
{
    class Program
    {
        static void Main(string[] args)
        {
            var grids = new List<bool[,]> { Parse(Input.Puzzle) };
            //var seenRatings = new HashSet<int>();
            //var iterations = 0;
            for (int i = 0; i < 200; i++)
            {
                if (GetRating(grids[0]) > 0) grids.Insert(0, new bool[5, 5]);
                if (GetRating(grids.Last()) > 0) grids.Add(new bool[5, 5]);
                //Print(grid);
                //Console.ReadLine();
                //var rating = GetRating(grid);
                //if (seenRatings.Add(rating) == false)
                //{
                //    Console.WriteLine($"Found duplicate rating {rating} at iteration {iterations}");
                //    Print(grid);
                //    break;
                //}
                grids = TransformGrid(grids);
                //if (iterations % 1000 == 0)
                //{
                //    Console.WriteLine("Iterations: " + iterations);
                //}
                //iterations++;
            }
            var bugs = grids.Sum(CountBugs);
            Console.WriteLine("Number of bugs after 200 iterations: " + bugs);
        }

        private static void Print(bool[,] grid)
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    Console.Write(grid[y, x] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        private static List<bool[,]> TransformGrid(List<bool[,]> grids)
        {
            var newGrids = grids.Select(x => new bool[5, 5]).ToList();
            for (int z = 0; z < grids.Count; z++)
            {
                for (int y = 0; y < 5; y++)
                {
                    for (int x = 0; x < 5; x++)
                    {
                        if (y == 2 && x == 2) continue;

                        var aboveOut = y == 0 && z > 0 && grids[z - 1][1, 2] ? 1 : 0;
                        var leeftOut = x == 0 && z > 0 && grids[z - 1][2, 1] ? 1 : 0;
                        var belowOut = y == 4 && z > 0 && grids[z - 1][3, 2] ? 1 : 0;
                        var rightOut = x == 4 && z > 0 && grids[z - 1][2, 3] ? 1 : 0;

                        var aboveIn = y == 3 && x == 2 && z < grids.Count - 1 ? Enumerable.Range(0, 5).Sum(a => grids[z + 1][4, a] ? 1 : 0) : 0;
                        var leeftIn = y == 2 && x == 1 && z < grids.Count - 1 ? Enumerable.Range(0, 5).Sum(a => grids[z + 1][a, 0] ? 1 : 0) : 0;
                        var belowIn = y == 1 && x == 2 && z < grids.Count - 1 ? Enumerable.Range(0, 5).Sum(a => grids[z + 1][0, a] ? 1 : 0) : 0;
                        var rightIn = y == 2 && x == 3 && z < grids.Count - 1 ? Enumerable.Range(0, 5).Sum(a => grids[z + 1][a, 4] ? 1 : 0) : 0;

                        var above = y > 0 && grids[z][y - 1, x] ? 1 : 0;
                        var leeft = x > 0 && grids[z][y, x - 1] ? 1 : 0;
                        var below = y < 4 && grids[z][y + 1, x] ? 1 : 0;
                        var right = x < 4 && grids[z][y, x + 1] ? 1 : 0;

                        var bugs = above + leeft + below + right +
                            aboveOut + leeftOut + belowOut + rightOut +
                            aboveIn + leeftIn + belowIn + rightIn;

                        if (grids[z][y, x])
                        {
                            newGrids[z][y, x] = bugs == 1;
                        }
                        else
                        {
                            newGrids[z][y, x] = bugs >= 1 && bugs <= 2;
                        }
                    }
                }
            }
            return newGrids;
        }

        private static int GetRating(bool[,] grid)
        {
            var rating = 0;
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    rating <<= 1;
                    if (grid[4 - y, 4 - x]) rating += 1;
                }
            return rating;
        }

        private static int CountBugs(bool[,] grid)
        {
            var count = 0;
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 5; x++)
                {
                    count += grid[y, x] ? 1 : 0;
                }
            return count;
        }

        private static bool[,] Parse(string text)
        {
            var sr = new StringReader(text);
            string line;
            int row = 0;
            var grid = new bool[5, 5];
            while ((line = sr.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                for (int i = 0; i < 5; i++)
                {
                    grid[row, i] = line[i] == '#';
                }
                row++;
            }
            return grid;
        }
    }

    public static class Input
    {
        public static string Test1 =
            @"
....#
#..#.
#..##
..#..
#....
";
        public static string Puzzle =
            @"
##.#.
.#.##
.#...
#..#.
.##..";
    }
}
