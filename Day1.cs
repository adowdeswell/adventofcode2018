using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    static class Day1
    {
        static string[] _lines = System.IO.File.ReadAllLines("Input/1/input.txt");

        static public void Solve()
        {
            SolveParts1And2();
        }

        static void SolveParts1And2()
        {
            int frequency = 0;
            List<int> frequencies = new List<int>();
            bool firstLoop = true;
            bool foundRepeat = false;

            do
            {
                //Console.Write(".");

                foreach (string line in _lines)
                {
                    frequency += Convert.ToInt32(line);

                    if (!foundRepeat)
                    {
                        if (frequencies.Contains(frequency))
                        {
                            // Part 2: What is the first frequency your device reaches twice?
                            Console.WriteLine("D01P2: " + frequency);
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
                    Console.WriteLine("D01P1: " + frequency);
                    firstLoop = false;
                }
            }
            while (!foundRepeat);
        }
    }
}
