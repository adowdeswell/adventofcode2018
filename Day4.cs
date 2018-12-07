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

            public enum EState
            {
                eNone,
                eBeginsShift,
                eFallsAsleep,
                eWakesUp
            }
            public EState _state;
        }

        static List<Entry> _entries = new List<Entry>();

        static public void Parts1And2()
        {
            Parse();
            Sort();
            Solve();
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
                        entry._state = Entry.EState.eBeginsShift;
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
                        entry._state = Entry.EState.eWakesUp;
                    }
                    else
                    {
                        if (!line.Contains("falls"))
                        {
                            Console.WriteLine("Parse error: '" + line + "'");
                        }

                        entry._state = Entry.EState.eFallsAsleep;
                    }
                }

                _entries.Add(entry);
            }
        }

        static void Sort()
        {
            _entries.Sort((a, b) => DateTime.Compare(a._timestamp, b._timestamp));
        }

        static void Solve()
        {
            Dictionary<int, int>[] sumPerIDPerMinute = new Dictionary<int, int>[60];
            Dictionary<int, int> minutesPerID = new Dictionary<int, int>();

            for (int m = 0; m < 60; ++m)
            {
                sumPerIDPerMinute[m] = new Dictionary<int, int>();
            }

            Entry.EState currentState = Entry.EState.eNone;
            int currentID = 0;
            int currentSleepStartMinute = 0;

            foreach (Entry entry in _entries)
            {
                switch (entry._state)
                {
                    case Entry.EState.eBeginsShift:
                    {
                        if ((currentState == Entry.EState.eFallsAsleep))
                        {
                            Console.WriteLine("Logic error: currentState = " + currentState + " : _state = " + entry._state);
                        }

                        currentID = entry._id;
                        break;
                    }
                    case Entry.EState.eFallsAsleep:
                    {
                        if ((currentState != Entry.EState.eBeginsShift) && (currentState != Entry.EState.eWakesUp))
                        {
                            Console.WriteLine("Logic error: currentState = " + currentState + " : _state = " + entry._state);
                        }

                        currentSleepStartMinute = entry._timestamp.Minute;
                        break;
                    }
                    case Entry.EState.eWakesUp:
                    {
                        if (currentState != Entry.EState.eFallsAsleep)
                        {
                            Console.WriteLine("Logic error: currentState = " + currentState + " : _state = " + entry._state);
                        }

                        for (int m = entry._timestamp.Minute - 1; m >= currentSleepStartMinute; --m)
                        {
                            if (sumPerIDPerMinute[m].ContainsKey(currentID))
                            {
                                sumPerIDPerMinute[m][currentID]++;
                            }
                            else
                            {
                                sumPerIDPerMinute[m][currentID] = 1;
                            }

                            if (minutesPerID.ContainsKey(currentID))
                            {
                                minutesPerID[currentID]++;
                            }
                            else
                            {
                                minutesPerID[currentID] = 1;
                            }
                        }

                        break;
                    }
                }

                currentState = entry._state;
            }

            int maxCount = 0;
            int maxCountID = 0;

            foreach (KeyValuePair<int, int> kvp in minutesPerID)
            {
                if (kvp.Value > maxCount)
                {
                    maxCount = kvp.Value;
                    maxCountID = kvp.Key;
                }
            }

            //Console.WriteLine("Guard that has the most minutes asleep = " + maxCountID);

            maxCount = 0;
            int maxCountIDMinute = 0;

            for (int m = 0; m < 60; ++m)
            {
                if (sumPerIDPerMinute[m].ContainsKey(maxCountID))
                {
                    if (sumPerIDPerMinute[m][maxCountID] > maxCount)
                    {
                        maxCount = sumPerIDPerMinute[m][maxCountID];
                        maxCountIDMinute = m;
                    }
                }
            }

            //Console.WriteLine("Minute guard spends asleep the most = " + maxCountIDMinute);

            // Part 1: What is the ID of the guard you chose multiplied by the minute you chose?
            Console.WriteLine("D04P1: " + (maxCountID * maxCountIDMinute));

            int maxMinutes = 0;
            int maxMinutesID = 0;
            int maxMinutesMinute = 0;

            for (int m = 0; m < 60; ++m)
            {
                foreach (KeyValuePair<int, int> kvp in sumPerIDPerMinute[m])
                {
                    if (kvp.Value > maxMinutes)
                    {
                        maxMinutes = kvp.Value;
                        maxMinutesID = kvp.Key;
                        maxMinutesMinute = m;
                    }
                }
            }

            // Part 2: What is the ID of the guard you chose multiplied by the minute you chose?
            Console.WriteLine("D04P2: " + (maxMinutesID * maxMinutesMinute));
        }
    }
}
