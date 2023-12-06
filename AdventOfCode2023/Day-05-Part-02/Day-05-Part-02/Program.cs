var input = File.ReadAllLines("./input.txt").Where(line => !string.IsNullOrEmpty(line));

var seeds = GetSeedRangePairs(input.First());

var maps = GetMaps(input);

const string sourceMap = "seed";
const string targetMap = "location";
const int blockIteration = 5000;
var mapLookup = maps.ToDictionary(map => map.FromName, map => map);

var roughMinimum = (MinimumValueFound: long.MaxValue, BlockMinimumWasFoundIn: default(long));
foreach (var seedRangePair in seeds)
{
    var newMinimumFound = FindMinimumForSeedRange(seedRangePair.Start, seedRangePair.Range, blockIteration);

    if (newMinimumFound.MinimumValueFound < roughMinimum.MinimumValueFound)
    {
        roughMinimum.MinimumValueFound = newMinimumFound.MinimumValueFound;
        roughMinimum.BlockMinimumWasFoundIn = newMinimumFound.BlockFoundIn;
    }
}

var refinedMinimum = FindMinimumForSeedRange(roughMinimum.BlockMinimumWasFoundIn - blockIteration, blockIteration * 2, 1);

Console.WriteLine($"Day 5 - Part 2: {refinedMinimum.MinimumValueFound}");

long FindValueFromMap(long mapInput, Map map)
{
    var activeRange = map.Ranges.SingleOrDefault(range => range.SourceStart <= mapInput && range.SourceStart + range.Length > mapInput);

    return activeRange == null ? mapInput : activeRange.DestinationStart + (mapInput - activeRange.SourceStart);
}

(long Start, long Range)[] GetSeedRangePairs(string seedInputLine)
{
    var seedInitializer = seedInputLine.Split(':').Last().Trim().Split(' ').Select(long.Parse).ToArray();
    
    var seedRangePairs = new (long Start, long Range)[seedInitializer.Length / 2];
    
    for (var i = 0; i < seedInitializer.Length; i += 2)
    {
        var currentSeed = seedInitializer[i];
        var range = seedInitializer[i + 1];

        seedRangePairs[i / 2] = (currentSeed, range);
    }

    return seedRangePairs;
}

List<Map> GetMaps(IEnumerable<string> inputLines)
{
    var list = new List<Map>();

    foreach (var line in inputLines.Skip(1))
    {
        if (char.IsLetter(line[0]))
        {
            var nameParts = line.Split('-', ' ');

            list.Add(new Map(nameParts[0], nameParts[2], new List<MapRange>()));
        }
        else if (char.IsDigit(line[0]))
        {
            var rangeParts = line.Split(' ').Select(long.Parse).ToArray();
            list.Last().Ranges.Add(new MapRange(rangeParts[0], rangeParts[1], rangeParts[2]));
        }
    }

    return list;
}

(long MinimumValueFound, long BlockFoundIn) FindMinimumForSeedRange(long start, long range, int seedSearchIteration)
{
    var result = (MinimumValueFound: long.MaxValue, BlockFoundIn: default(long));
    for (var i = 0; i < range; i += seedSearchIteration)
    {
        var currentSeed = start + i;
    
        var currentMap = maps.Single(map => map.FromName == sourceMap);
        var currentValue = currentSeed;

        do
        {
            currentValue = FindValueFromMap(currentValue, currentMap);
            currentMap = mapLookup[currentMap.ToName];
        } while (currentMap.ToName != targetMap);

        var currentMappedValue = FindValueFromMap(currentValue, currentMap);
        
        if (result.MinimumValueFound > currentMappedValue)
        {
            result.MinimumValueFound = currentMappedValue;
            result.BlockFoundIn = start + i;
        }
    }

    return result;
}

record Map(string FromName, string ToName, List<MapRange> Ranges);
record MapRange(long DestinationStart, long SourceStart, long Length);
