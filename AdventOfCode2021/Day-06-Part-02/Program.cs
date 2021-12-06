const int newFishTimer = 8;
const int targetGeneration = 256;

var allFish = File.ReadAllText("input.txt")
    .Split(",")
    .Select(fishTimer => int.Parse(fishTimer))
    .ToList();

var fishBuckets = Enumerable.Range(0, newFishTimer + 1)
    .ToDictionary(bucket => bucket, _ => 0L);

foreach (var fish in allFish)
{
    fishBuckets[fish]++;
}

for (var generation = 0; generation < targetGeneration; generation++)
{
    var updatedBuckets = new Dictionary<int, long>();

    for (var i = 0; i < newFishTimer; i++)
    {
        updatedBuckets[i] = fishBuckets[i + 1];
    }

    updatedBuckets[newFishTimer] = fishBuckets[0];
    updatedBuckets[newFishTimer - 2] += fishBuckets[0];

    fishBuckets = new Dictionary<int, long>(updatedBuckets);
}

var total = fishBuckets.Select(bucket => bucket.Value).Sum();

Console.WriteLine($"Day 6 - Part 2: {total}");
