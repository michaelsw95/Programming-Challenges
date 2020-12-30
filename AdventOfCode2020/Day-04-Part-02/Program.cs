using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var validPassports = File
    .ReadAllText("input.txt")
    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
    .Where(PassportIsValid)
    .Count();

Console.WriteLine($"Day 4 - Part 2: {validPassports}");

bool PassportIsValid(string rawPassport)
{
    var passportValidators = new List<(string Key, Func<string, bool> Validator)>
    {
        ("byr", (string text) => HasValidYear(text, 1920, 2002)),
        ("iyr", (string text) => HasValidYear(text, 2010, 2020)),
        ("eyr", (string text) => HasValidYear(text, 2020, 2030)),
        ("hgt", (string text) => HasValidHeight(text)),
        ("hcl", (string text) => HasValidHairColour(text)),
        ("ecl", (string text) => HasValidEyeColour(text)),
        ("pid", (string text) => HasValidPassportId(text))
    };

    var passport = rawPassport.Replace(Environment.NewLine, " ");

    return passportValidators.Select(o => o.Key).All(o => passport.Contains(o)) && 
        passportValidators.All(o => o.Validator(ExtractValueFromPassport(passport, o.Key)));
}

string ExtractValueFromPassport(string passport, string key) =>
    Regex.Match($"{passport} ", $"{key}:.+?(?= )").Captures[0].Value.Split(':')[1];

bool HasValidYear(string yearText, int lowerBound, int upperBound) =>
    int.TryParse(yearText, out int year) ?
        year >= lowerBound && year <= upperBound :
        false;

bool HasValidHeight(string heightText)
{
    var numberPart = heightText.Substring(0, heightText.Length - 2);
    var unitPart = heightText.Substring(heightText.Length - 2);

    if (unitPart == "cm" && int.TryParse(numberPart, out int heightCM))
    {
        return heightCM >= 150 && heightCM <= 193;
    }
    else if (unitPart == "in" && int.TryParse(numberPart, out int heightIN))
    {
        return heightIN >= 59 && heightIN <= 76;
    }

    return false;
}

bool HasValidHairColour(string colourText) => Regex.IsMatch(colourText, "#([a-f0-9]{6})");

bool HasValidEyeColour(string eyeColour) => new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(eyeColour);

bool HasValidPassportId(string passportId) => passportId.Length == 9 && Regex.IsMatch(passportId, "[0-9]{9}");