using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    static class Day2
    {
        static string[] _lines = System.IO.File.ReadAllLines("Input/2/input.txt");

        static public void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        static void SolvePart1()
        { 
            int numTwos = 0;
            int numThrees = 0;

            foreach (string line in _lines)
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

        static void SolvePart2()
        {
            foreach (string line1 in _lines)
            {
                foreach (string line2 in _lines)
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
                            //Console.WriteLine("[info] '" + line1 + "' / '" + line2 + "' : numDifferences = " + numDifferences);
                            //Console.WriteLine("[info] character " + firstDifferenceIndex + " = '" + line1[firstDifferenceIndex] + "' / '" + line2[firstDifferenceIndex] + "'");

                            string lineRemoved = line1.Remove(firstDifferenceIndex, 1);

                            // Part 2: What letters are common between the two correct box IDs?
                            Console.WriteLine("D02P2: " + lineRemoved);
                        }
                    }
                }
            }
        }
    }
}
