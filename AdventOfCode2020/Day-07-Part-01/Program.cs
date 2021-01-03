using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

const string targetBag = "shiny gold";

var ruleDefinitions = File.ReadAllLines("input.txt").ToArray();
var rules = ruleDefinitions
    .Select(rule => rule.Split(" bags contain "))
    .ToDictionary(splitRule => splitRule.First(), splitRule => GetChildrenFromRule(splitRule.Last()));

var countOfTargetBag = rules.Keys
    .Where(bagName => bagName != targetBag)
    .Select(bagName => GetCountOfBag(targetBag, bagName, rules))
    .Where(count => count > 0)
    .Count();

Console.WriteLine($"Day 7 - Part 1: {countOfTargetBag}");

List<(string, int)> GetChildrenFromRule(string rule)
{
    if (rule == "no other bags.")
    {
        return new List<(string, int)>(0);
    }

    var countOfChildren = Regex.Matches(rule, "[0-9]+");
    var children = Regex.Matches(rule, @"\w+ \w+ (bags|bag)");
    var childrenNames = new List<(string, int)>(children.Count);

    for (var i = 0; i < children.Count; i++)
    {
        var child = children.ElementAt(i).ToString();
        var childCount = int.Parse(countOfChildren.ElementAt(i).ToString());

        var childName = child.ToString()
            .Replace("bags", string.Empty)
            .Replace("bag", string.Empty)
            .Trim();
        
        childrenNames.Add((childName, childCount));
    }

    return childrenNames;
}

int GetCountOfBag(string targetBag, string bagToSearch, Dictionary<string, List<(string Name, int Count)>> rules)
{
    var childrenOfSearchBag = rules[bagToSearch];

    if (childrenOfSearchBag.Count == 0)
    {
        return 0;
    }

    var countOfTargetBag = childrenOfSearchBag
        .Sum(child => child.Name == targetBag ? 1 : GetCountOfBag(targetBag, child.Name, rules));

    return countOfTargetBag;
}
