var priorityRange = Enumerable
    .Range('a', 26)
    .Select(letter => (char)letter)
    .ToArray();

var prioritySum = File.ReadAllLines("./input.txt")
    .Select((string line) =>
    {
        var compartmentSize = line.Length / 2;

        var compartmentOne = line.Substring(0, compartmentSize).ToHashSet();
        var compartmentTwo = line.Substring(compartmentSize).ToHashSet();

        var commonItem = compartmentOne.Single(letter => compartmentTwo.Contains(letter));

        return char.IsUpper(commonItem) ? 
            (Array.IndexOf(priorityRange, char.ToLower(commonItem)) + 1) + priorityRange.Length :
            Array.IndexOf(priorityRange, commonItem) + 1;
    })
    .Sum();
    
Console.WriteLine($"Day 3 - Part 1: {prioritySum}");
