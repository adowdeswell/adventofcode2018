using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    static class Day3
    {
        static string[] _lines = System.IO.File.ReadAllLines("Input/3/input.txt");

        static public void Parts1And2()
        {
            int[,] fabric = new int[1000, 1000];
            List<int> intactIndices = new List<int>();

            for (int i = 0; i < _lines.Length; ++i)
            {
                GetClaim(_lines[i], out int id, out int coordX, out int coordY, out int dimX, out int dimY);
                
                bool intact = true;
                for (int x = coordX; x < (coordX + dimX); ++x)
                {
                    for (int y = coordY; y < (coordY + dimY); ++y)
                    {
                        if (fabric[x, y] > 0)
                        {
                            intact = false;
                        }

                        ++fabric[x, y];
                    }
                }

                if (intact)
                {
                    intactIndices.Add(i);
                }
            }

            int numOverlaps = 0;
            for (int x = 0; x < 1000; ++x)
            {
                for (int y = 0; y < 1000; ++y)
                {
                    if (fabric[x, y] > 1)
                    {
                        ++numOverlaps;
                    }
                }
            }

            // Part 1: How many square inches of fabric are within two or more claims?
            Console.WriteLine("D03P1: " + numOverlaps);

            foreach (int index in intactIndices)
            {
                GetClaim(_lines[index], out int id, out int coordX, out int coordY, out int dimX, out int dimY);
                
                bool intact = true;
                for (int x = coordX; x < (coordX + dimX); ++x)
                {
                    for (int y = coordY; y < (coordY + dimY); ++y)
                    {
                        if (fabric[x, y] > 1)
                        {
                            intact = false;
                        }
                    }
                }

                if (intact)
                {
                    // Part 2: What is the ID of the only claim that doesn't overlap?
                    Console.WriteLine("D03P2: " + id);
                }
            }
        }

        static void GetClaim(string line, out int id, out int coordX, out int coordY, out int dimX, out int dimY)
        {
            string[] elements = line.Split(' ');

            id = System.Convert.ToInt32(elements[0].Substring(1));

            string[] coord = elements[2].Split(',');
            coordX = System.Convert.ToInt32(coord[0]);
            coordY = System.Convert.ToInt32(coord[1].Substring(0, coord[1].Length - 1));

            string[] dim = elements[3].Split('x');
            dimX = System.Convert.ToInt32(dim[0]);
            dimY = System.Convert.ToInt32(dim[1]);
        }
    }
}
