var priorityRange = Enumerable
    .Range('a', 26)
    .Select(letter => (char)letter)
    .ToArray();

var allRucksacks = File.ReadAllLines("./input.txt");
var prioritySum = 0;

for (var i = 0; i < allRucksacks.Length; i += 3)
{
    var rucksackOne = allRucksacks[i].ToHashSet();
    var rucksackTwo = allRucksacks[i + 1].ToHashSet();
    var rucksackThree = allRucksacks[i + 2].ToHashSet();

    var commonItem = rucksackOne
        .Single(item => rucksackTwo.Contains(item) && rucksackThree.Contains(item));

    var groupPriority = char.IsUpper(commonItem) ? 
        (Array.IndexOf(priorityRange, char.ToLower(commonItem)) + 1) + priorityRange.Length :
        Array.IndexOf(priorityRange, commonItem) + 1;

    prioritySum += groupPriority;
}

Console.WriteLine($"Day 3 - Part 2: {prioritySum}");
