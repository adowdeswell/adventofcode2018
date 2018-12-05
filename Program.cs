using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            //Day1();
            //Day2();
            Day3();
        }

        static void Day1()
        {
            string[] lines = System.IO.File.ReadAllLines("Input/1/input.txt");

            int frequency = 0;
            List<int> frequencies = new List<int>();
            bool firstLoop = true;
            bool foundRepeat = false;

            do
            {
                Console.Write(".");

                foreach (string line in lines)
                {
                    frequency += Convert.ToInt32(line);

                    if (!foundRepeat)
                    {
                        if (frequencies.Contains(frequency))
                        {
                            // Part 2: What is the first frequency your device reaches twice?
                            Console.WriteLine("\nD01P2: " + frequency);
                            foundRepeat = true;
                        }
                        else
                        {
                            frequencies.Add(frequency);
                        }
                    }
                }

                if (firstLoop)
                {
                    // Part 1: What is the resulting frequency?
                    Console.WriteLine("\nD01P1: " + frequency);
                    firstLoop = false;
                }
            }
            while (!foundRepeat);
        }

        static void Day2()
        {
            string[] lines = System.IO.File.ReadAllLines("Input/2/input.txt");

            //Day2_Part1(lines);
            Day2_Part2(lines);
        }

        static void Day2_Part1(string[] lines)
        { 
            int numTwos = 0;
            int numThrees = 0;

            foreach (string line in lines)
            {
                int l = line.Length;

                bool foundTwo = false;
                bool foundThree = false;

                for (char c = 'a'; c <= 'z'; ++c)
                {
                    string replacedLine = line.Replace(c.ToString(), "");
                    if (replacedLine.Length == (l - 2))
                    {
                        if (!foundTwo)
                        {
                            ++numTwos;
                            foundTwo = true;
                        }
                    }
                    else if (replacedLine.Length == (l - 3))
                    {
                        if (!foundThree)
                        {
                            ++numThrees;
                            foundThree = true;
                        }
                    }

                    if (foundTwo && foundThree)
                    {
                        break;
                    }
                }

                //if (foundTwo || foundThree)
                //{
                //    Console.WriteLine("[info] '" + line + "' : foundTwo = " + foundTwo + " : foundThree = " + foundThree);
                //}
            }

            //Console.WriteLine("[info] numTwos = " + numTwos + " : numThrees = " + numThrees);

            int checksum = numTwos * numThrees;

            // Part 1: What is the checksum?
            Console.WriteLine("D02P1: " + checksum);
        }

        static void Day2_Part2(string[] lines)
        {
            foreach (string line1 in lines)
            {
                foreach (string line2 in lines)
                {
                    if (line1 != line2)
                    {
                        int numDifferences = 0;
                        int firstDifferenceIndex = 0;

                        for (int i = 0; i < line1.Length; ++i)
                        {
                            if (line1[i] != line2[i])
                            {
                                ++numDifferences;
                                firstDifferenceIndex = i;
                            }
                        }

                        if (numDifferences == 1)
                        {
                            Console.WriteLine("[info] '" + line1 + "' / '" + line2 + "' : numDifferences = " + numDifferences);
                            Console.WriteLine("[info] character " + firstDifferenceIndex + " = '" + line1[firstDifferenceIndex] + "' / '" + line2[firstDifferenceIndex] + "'");

                            string lineRemoved = line1.Remove(firstDifferenceIndex, 1);

                            // Part 2: What letters are common between the two correct box IDs?
                            Console.WriteLine("D2P2: " + lineRemoved);
                        }
                    }
                }
            }
        }

        static void Day3()
        {
            string[] lines = System.IO.File.ReadAllLines("Input/3/input.txt");

            int[,] fabric = new int[1000, 1000];
            List<int> intactIndices = new List<int>();

            for (int i = 0; i < lines.Length; ++i)
            {
                GetClaim(lines[i], out int id, out int coordX, out int coordY, out int dimX, out int dimY);
                
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
            Console.WriteLine("D3P1: " + numOverlaps);

            foreach (int index in intactIndices)
            {
                GetClaim(lines[index], out int id, out int coordX, out int coordY, out int dimX, out int dimY);
                
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
                    Console.WriteLine("D3P2: " + id);
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
