var crabPositions = File.ReadAllText("./input.txt")
    .Split(",")
    .Select(position => int.Parse(position))
    .ToArray();

(int position, int fuelCost) bestFoundPosition = (0, 0);
for (var candidatePosition = 0; candidatePosition < crabPositions.Max(); candidatePosition++)
{
    var fuelCost = 0;
    foreach (var crab in crabPositions)
    {
        var lower = Math.Min(crab, candidatePosition);
        var upper = Math.Max(crab, candidatePosition);

        var steps = Enumerable.Range(0, (upper - lower) + 1);

        fuelCost += steps.Sum();
    }

    if (bestFoundPosition.fuelCost <= 0 || bestFoundPosition.fuelCost > fuelCost)
    {
        bestFoundPosition.position = candidatePosition;
        bestFoundPosition.fuelCost = fuelCost;
    }
}

Console.WriteLine($"Day 7 - Part 2: {bestFoundPosition.fuelCost}");
