var fish = File.ReadAllText("input.txt")
    .Split(",")
    .Select(fishTimer => int.Parse(fishTimer))
    .ToList();

for (var generation = 0; generation < 80; generation++)
{
    var newFishToAddAfterGeneration = 0;
    for (var i = 0; i < fish.Count; i++)
    {
        if (fish[i] == 0)
        {
            fish[i] = 7;

            newFishToAddAfterGeneration++;
        }

        fish[i] = fish[i] - 1;
    }

    for (var i = 0; i < newFishToAddAfterGeneration; i++)
    {
        fish.Add(8);
    }
}

Console.WriteLine($"Day 6 - Part 1: {fish.Count}");
