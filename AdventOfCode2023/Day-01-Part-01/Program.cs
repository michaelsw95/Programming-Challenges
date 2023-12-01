using System.Linq;

var sumOfCalibrationValues = File.ReadAllLines("./input.txt")
    .Select(calibration => {
        var digits = calibration.Where(value => char.IsDigit(value));

        return $"{digits.First()}{digits.Last()}";
    })
    .Sum(calibartionValue => int.Parse(calibartionValue));

Console.WriteLine($"Day 1 - Part 1: {sumOfCalibrationValues}");