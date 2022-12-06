const int packetIdLength = 14;

var input = File.ReadAllText("./input.txt");

var candidatePacketId = new HashSet<char>(packetIdLength);
var packetStartPosition = 0;

for (var i = packetIdLength; i < input.Length; i++)
{
    candidatePacketId.Clear();

    for (var j = 0; j < packetIdLength && candidatePacketId.Add(input[i - j]); j++);

    if (candidatePacketId.Count == packetIdLength)
    {
        packetStartPosition = i + 1;
        break;
    }
}

Console.WriteLine($"Day 6 - Part 2: {packetStartPosition}");
