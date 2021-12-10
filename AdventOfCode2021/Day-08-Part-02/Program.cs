var rawSignalEntries = File.ReadAllLines("./input.txt");
var signalEntries = new SignalEntry[rawSignalEntries.Length];

for (int i = 0; i < rawSignalEntries.Length; i++)
{
    string[] GetFormattedSignalArray(string line) => line
        .Split(' ')
        .Where(part => !string.IsNullOrWhiteSpace(part))
        .ToArray();

    var lineSplit = rawSignalEntries[i].Split('|');

    var signals = GetFormattedSignalArray(lineSplit.First());
    var outputs = GetFormattedSignalArray(lineSplit.Last());

    signalEntries[i] = new SignalEntry(signals, outputs);
}

var decodedOutputs = new List<string>(signalEntries.Length);
foreach (var entry in signalEntries)
{
    var signalToNumberMap = SignalDecoder.GetDecodedSignalToNumberMapping(entry);

    var decodedEntryOutput = string.Empty;
    foreach (var signal in entry.Output)
    {
        var normalisedSignal = SignalNumberMapper.GetNormalisedSignal(signal);

        var decodedNumber = signalToNumberMap[normalisedSignal];

        decodedEntryOutput += decodedNumber.ToString();
    }

    decodedOutputs.Add(decodedEntryOutput);
}

var sumOfDecodedOutputs = decodedOutputs
    .Select(output => int.Parse(output))
    .Sum();

Console.WriteLine($"Day 8 - Part 2: {sumOfDecodedOutputs}");
