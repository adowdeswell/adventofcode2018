using System;

namespace adventofcode2018
{
    static class Day5
    {
        static string _input = System.IO.File.ReadAllText("Input/5/input.txt");

        static public void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        static void SolvePart1()
        {
            int reactedLength = GetReactedPolymerLength(_input, ' ');

            // Part 1: How many units remain after fully reacting the polymer you scanned?
            Console.WriteLine("D05P1: " + reactedLength);
        }

        static void SolvePart2()
        {
            int minReactedLength = _input.Length;
            int minReactedUnit = ' ';

            for (char c = 'a'; c <= 'z'; ++c)
            {
                int reactedLength = GetReactedPolymerLength(_input, c);
                if (reactedLength < minReactedLength)
                {
                    minReactedLength = reactedLength;
                    minReactedUnit = c;
                }
            }

            // Part 2: What is the length of the shortest polymer you can produce?
            Console.WriteLine("D05P2: " + minReactedLength);
        }

        static int GetReactedPolymerLength(string input, char unitToRemove)
        {
            bool anyReaction;

            if (unitToRemove != ' ')
            {
                input = input.Replace(unitToRemove.ToString().ToUpper(), "");
                input = input.Replace(unitToRemove.ToString().ToLower(), "");
            }

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

            return input.Length;
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
