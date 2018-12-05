using System;
using System.Collections.Generic;

namespace adventofcode2018
{
    static class Day4
    {
        static string[] _lines = System.IO.File.ReadAllLines("Input/4/input.txt");

        class Entry
        {
            public DateTime _timestamp;
            public int _id;

            public enum EType
            {
                eBeginsShift,
                eFallsAsleep,
                eWakesUp
            }
            public EType _type;
        }

        static List<Entry> _entries = new List<Entry>();

        static public void Part1()
        {
            Parse();
            Sort();
        }

        static void Parse()
        {
            const string formatString = "yyyy-MM-dd HH:mm";

            foreach (string line in _lines)
            {
                string tsString = line.Substring(1, 16);
                if (!DateTime.TryParseExact(tsString, formatString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AllowInnerWhite, out DateTime timestamp))
                {
                    Console.WriteLine("TryParseExact failed '" + tsString + "'");
                }

                Entry entry = new Entry();
                entry._timestamp = timestamp;

                int idHashIndex = line.IndexOf('#');
                if (idHashIndex >= 0)
                {
                    int beginsIndex = line.IndexOf("begins");
                    if (beginsIndex >= 0)
                    {
                        string idString = line.Substring(idHashIndex + 1, beginsIndex - idHashIndex - 1);
                        entry._id = System.Convert.ToInt32(idString);
                        entry._type = Entry.EType.eBeginsShift;
                    }
                    else
                    {
                        Console.WriteLine("Parse error: '" + line + "'");
                    }
                }
                else
                {
                    if (line.Contains("wakes"))
                    {
                        entry._type = Entry.EType.eWakesUp;
                    }
                    else
                    {
                        if (!line.Contains("falls"))
                        {
                            Console.WriteLine("Parse error: '" + line + "'");
                        }

                        entry._type = Entry.EType.eFallsAsleep;
                    }
                }

                _entries.Add(entry);
            }
        }

        static void Sort()
        {
            _entries.Sort((a, b) => DateTime.Compare(a._timestamp, b._timestamp));
        }
    }
}
