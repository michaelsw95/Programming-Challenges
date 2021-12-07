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
        fuelCost += Convert.ToInt32(Math.Abs(crab - candidatePosition));
    }

    if (bestFoundPosition.fuelCost <= 0 || bestFoundPosition.fuelCost > fuelCost)
    {
        bestFoundPosition.position = candidatePosition;
        bestFoundPosition.fuelCost = fuelCost;
    }
}

Console.WriteLine($"Day 7 - Part 1: {bestFoundPosition.fuelCost}");
