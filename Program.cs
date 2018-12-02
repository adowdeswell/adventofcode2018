using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1();
        }

        static void Day1()
        {
            string[] lines = System.IO.File.ReadAllLines("1.1/input.txt");

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
                            // Part 2: What is the first repeated frequency?
                            Console.WriteLine("\nPart 2: first repeated frequency = " + frequency);
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
                    // Part 1: What is the resultant frequency?
                    Console.WriteLine("\nPart 1: frequency = " + frequency);
                    firstLoop = false;
                }
            }
            while (!foundRepeat);
        }
    }
}
