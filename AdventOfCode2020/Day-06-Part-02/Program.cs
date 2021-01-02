using System;
using System.IO;
using System.Linq;

var sumOfAnswers = File
    .ReadAllText("input.txt")
    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
    .Select(CountSharedAnswers)
    .Sum();

Console.WriteLine($"Day 6 - Part 2: {sumOfAnswers}");

int CountSharedAnswers(string groupAnswers)
{
    var answersPerPerson = groupAnswers.Split(Environment.NewLine);

    var answersInContention = answersPerPerson
        .First()
        .ToCharArray()
        .ToHashSet();

    foreach (var answer in answersPerPerson.First())
    {
        for (var i = 1; i < answersPerPerson.Length; i++)
        {
            if (!answersPerPerson[i].Contains(answer))
            {
                answersInContention.Remove(answer);
                break;
            }
        }
    }

    return answersInContention.Count;
}
