using System;
using System.IO;
using System.Linq;

var validPassports = File
    .ReadAllText("input.txt")
    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
    .Where(PassportHasRequiredFields)
    .Count();

Console.WriteLine($"Day 4 - Part 1: {validPassports}");

bool PassportHasRequiredFields(string passport)
{
    var requiredFields = new string[7] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

    return requiredFields.All(o => passport.Contains(o));
}