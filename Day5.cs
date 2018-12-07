using System;

namespace adventofcode2018
{
    static class Day5
    {
        static string input = System.IO.File.ReadAllText("Input/5/input.txt");

        static public void Part1()
        {
            Solve_Part1();
        }

        static void Solve_Part1()
        {
            bool anyReaction;

            do
            {
                anyReaction = false;

                for (int i = 0; i < (input.Length - 1); ++i)
                {
                    char unit1 = input[i];
                    char unit2 = input[i + 1];

                    bool reaction = IsReaction(unit1, unit2);
                    if (reaction)
                    {
                        input = input.Substring(0, i) + input.Substring(i + 2);
                        anyReaction = true;
                    }
                }
            }
            while (anyReaction);

            Console.WriteLine("D05P1: " + input.Length);
        }

        static bool IsReaction(char unit1, char unit2)
        {
            const int charDifference = 'a' - 'A';

            if (Math.Abs(unit2 - unit1) == charDifference)
            {
                return true;
            }

            return false;
        }
    }
}
