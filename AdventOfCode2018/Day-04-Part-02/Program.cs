using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Day_04_Part_02
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputRecords = File.ReadAllLines(@"input.txt");
            var records = ParseInput(inputRecords);

            var allGuardIds  = records.Select(o => o.GuardId).Distinct().ToList();

            var maxNumberOfTimesAsleepOnAMinute = 0;
            var resultGuardId = 0;
            var resultMinute = 0;

            foreach (var guard in allGuardIds)
            {
                var minuteMostOftenAsleep = GetMinuteMostOftenAsleep(records, guard);

                if (minuteMostOftenAsleep.numberOfTimesAsleep > maxNumberOfTimesAsleepOnAMinute)
                {
                    maxNumberOfTimesAsleepOnAMinute = minuteMostOftenAsleep.numberOfTimesAsleep;
                    resultGuardId = guard;
                    resultMinute = minuteMostOftenAsleep.minute;
                }
            }

            var result = resultGuardId * resultMinute;

            Console.WriteLine("Advent of Code - Day 04 - Part 02: " + result);
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

        static (int minute, int numberOfTimesAsleep) GetMinuteMostOftenAsleep(List<Record> records, int guardId)
        {
            var minutesAndTimesAsleep = Enumerable.Range(0, 60).ToList().ToDictionary(o => o, _ => 0);
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
                        minutesAndTimesAsleep[currentTime.Minute] += 1;
                        currentTime = currentTime.AddMinutes(1);
                    }
                }
            }

            var minuteMostOftenAsleep = minutesAndTimesAsleep.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;

            return (minuteMostOftenAsleep, minutesAndTimesAsleep[minuteMostOftenAsleep]);
        }
    }
}
