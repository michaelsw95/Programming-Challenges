var input = File.ReadAllLines("./input.txt").Where(line => !string.IsNullOrEmpty(line));

var seeds = input.First().Split(':').Last().Trim().Split(' ').Select(uint.Parse);

var maps = new List<Map>();

foreach (var line in input.Skip(1))
{
    if (char.IsLetter(line[0]))
    {
        var nameParts = line.Split('-', ' ');

        maps.Add(new Map(nameParts[0], nameParts[2], new List<MapRange>()));
    }
    else if (char.IsDigit(line[0]))
    {
        var rangeParts = line.Split(' ').Select(uint.Parse).ToArray();
        maps.Last().Ranges.Add(new MapRange(rangeParts[0], rangeParts[1], rangeParts[2]));
    }
}

const string sourceMap = "seed";
const string targetMap = "location";
var valuesAtTarget = seeds.ToDictionary(seed => seed, _ => default(uint));

var mapLookup = maps.ToDictionary(map => map.FromName, map => map);

foreach (var seed in seeds)
{
    var currentMap = maps.Single(map => map.FromName == sourceMap);
    var currentValue = seed;
    
    do
    {
        currentValue = FindValueFromMap(currentValue, currentMap);
        currentMap = mapLookup[currentMap.ToName];
    } while (currentMap.ToName != targetMap);

    valuesAtTarget[seed] = FindValueFromMap(currentValue, currentMap);
}

Console.WriteLine($"Day 5 - Part 1: {valuesAtTarget.Values.Min()}");

uint FindValueFromMap(uint mapInput, Map map)
{
    var activeRange = map.Ranges.SingleOrDefault(range => range.SourceStart < mapInput && range.SourceStart + range.Length > mapInput);

    return activeRange == null ? mapInput : activeRange.DestinationStart + (mapInput - activeRange.SourceStart);
}

record Map(string FromName, string ToName, List<MapRange> Ranges);
record MapRange(uint DestinationStart, uint SourceStart, uint Length);