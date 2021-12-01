var truncatedSum = File.ReadAllLines("./input.txt")
    .Select(line => long.Parse(line.Substring(0, 11)))
    .Sum()
    .ToString()
    .Substring(0, 10);

Console.WriteLine($"Project Euler - Problem 12: {truncatedSum}");
