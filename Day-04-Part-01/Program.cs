using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day_04_Part_01
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputRecords = File.ReadAllLines(@"input.txt");
            var records = ParseInput(inputRecords);

            var guardThatWasAsleepForLongest = GetIdOfGuardWhoWasAsleepTheMost(records);
            var minuteMostOftenAsleep = GetMinuteMostOftenAsleep(records, guardThatWasAsleepForLongest);
            var result = guardThatWasAsleepForLongest * minuteMostOftenAsleep;

            Console.WriteLine("Advent of Code - Day 04 - Part 01: " + result);
        }

        static List<Record> ParseInput(string[] inputRecords)
        {
            const string betweenSquareBrackets = @"\[([^)]*)\]";
            const string containsAHash = @"(?<!\w)#\w+";

            var records = inputRecords
                .Select(o => 
                {
                    var recordTime = Regex.Match(o, betweenSquareBrackets).Groups[1].Value;
                    var recordText = Regex.Replace(o, betweenSquareBrackets, string.Empty).Trim();

                    var type = RecordType.GuardGoesToSleep;
                    if (Regex.Matches(recordText, containsAHash).Any())
                    {
                        type = RecordType.NewGuardShift;
                    }
                    else if (recordText.Contains("wake"))
                    {
                        type = RecordType.GuardWakesUp;
                    }

                    return new Record(DateTime.Parse(recordTime), recordText, type);
                })
                .OrderBy(o => o.TimeStamp)
                .ToList();

            var currentGuardId = 0;
            foreach (var record in records)
            {
                if (record.Type == RecordType.NewGuardShift)
                {
                    var guardId = Regex.Match(record.Text, containsAHash).Groups.First().Value.TrimStart('#');
                    currentGuardId = Convert.ToInt32(guardId);
                }

                record.GuardId = currentGuardId;
            }

            return records;
        }

        static int GetIdOfGuardWhoWasAsleepTheMost(List<Record> records)
        {
            var guardSleepTime = records
                .Select(o => o.GuardId)
                .Distinct()
                .ToDictionary(o => o, _ => 0);

            for (var i = 0; i < records.Count(); i++)
            {
                var currentRecord = records[i];

                if (currentRecord.Type == RecordType.GuardWakesUp)
                {
                    var timeAsleep = (records[i].TimeStamp - records[i - 1].TimeStamp).Minutes;
                    
                    guardSleepTime[currentRecord.GuardId] += timeAsleep;
                }
            }

            return guardSleepTime.GetKeyWithMaxValue();
        }

        static int GetMinuteMostOftenAsleep(List<Record> records, int guardId)
        {
            var minutesAsleep = Enumerable.Range(0, 60).ToList().ToDictionary(o => o, _ => 0);
            var recordsForTheTargetGuard = records
                .Where(o => o.GuardId == guardId)
                .ToList();
            
            for (var i = 0; i < recordsForTheTargetGuard.Count; i++)
            {
                if (recordsForTheTargetGuard[i].Type == RecordType.GuardWakesUp)
                {
                    var currentTime = recordsForTheTargetGuard[i - 1].TimeStamp;

                    while (currentTime != recordsForTheTargetGuard[i].TimeStamp)
                    {
                        minutesAsleep[currentTime.Minute] += 1;
                        currentTime = currentTime.AddMinutes(1);
                    }
                }
            }

            return minutesAsleep.GetKeyWithMaxValue();
        }
    }

    public static class DictionaryExtensions
    {
        public static int GetKeyWithMaxValue(this Dictionary<int, int> values)
        {
            return values.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        }    
    }
}
