const int packetIdLength = 4;

var input = File.ReadAllText("./input.txt");

var candidatePacketId = new HashSet<char>(packetIdLength);
var packetStartPosition = 0;

for (var i = packetIdLength; i < input.Length; i++)
{
    for (var j = 0; j < packetIdLength && candidatePacketId.Add(input[i - j]); j++);

    if (candidatePacketId.Count == packetIdLength)
    {
        packetStartPosition = i + 1;

        break;
    }

    candidatePacketId.Clear();
}

Console.WriteLine($"Day 6 - Part 1: {packetStartPosition}");
