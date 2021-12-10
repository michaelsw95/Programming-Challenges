public static class SignalDecoder
{
    public static Dictionary<string, int> GetDecodedSignalToNumberMapping(SignalEntry lineToDecode)
    {
        var signalNumberMapper = GetInitialKnownSignals(lineToDecode);

        var signalsOfLengthSix = lineToDecode.SignalInput.Where(signal => signal.Length == 6);
        signalNumberMapper = DecodeComplexSignalForNumber(signalNumberMapper, signalsOfLengthSix, 4, 7, 0);

        var rangeToCheckForSix = signalsOfLengthSix.Where(signal => !signalNumberMapper.SignalIsEqual(signal, 0));
        signalNumberMapper = DecodeSignalForNumber(signalNumberMapper, rangeToCheckForSix, 6, 1);

        var nineByElimination = signalsOfLengthSix.Where(signal => !signalNumberMapper.SignalIsEqual(signal, 6) && !signalNumberMapper.SignalIsEqual(signal, 0)).First();
        signalNumberMapper.AddNewFoundEntry(9, nineByElimination);

        var signalsOfLengthFive = lineToDecode.SignalInput.Where(signal => signal.Length == 5);
        signalNumberMapper = DecodeComplexSignalForNumber(signalNumberMapper, signalsOfLengthFive, 9, 1, 3);

        var rangeToCheckForFive = signalsOfLengthFive.Where(signal => !signalNumberMapper.SignalIsEqual(signal, 3));
        signalNumberMapper = DecodeSignalForNumber(signalNumberMapper, rangeToCheckForFive, 5, 6);

        var twoByElimination = signalsOfLengthFive.Where(signal => !signalNumberMapper.SignalIsEqual(signal, 5) && !signalNumberMapper.SignalIsEqual(signal, 3)).First();
        signalNumberMapper.AddNewFoundEntry(2, twoByElimination);

        return signalNumberMapper.GetSignalToNumberMapping();
    }

    private static SignalNumberMapper GetInitialKnownSignals(SignalEntry entry)
    {
        var signalNumberMapper = new SignalNumberMapper();

        var uniqueSignalLengthsToTheirNumber = new (int length, int outputNumber)[4]
        {
            ( 2, 1 ),
            ( 4, 4 ),
            ( 3, 7 ),
            ( 7, 8 )
        };

        foreach ((int length, int outputNumber) in uniqueSignalLengthsToTheirNumber)
        {
            var foundSignal = entry.SignalInput.First(signal => signal.Length == length);

            signalNumberMapper.AddNewFoundEntry(outputNumber, foundSignal);
        }

        return signalNumberMapper;
    }

    private static SignalNumberMapper DecodeSignalForNumber(
        SignalNumberMapper mapper, IEnumerable<string> candidates, int targteNumber, int compareNumber)
    {
        foreach (var candidate in candidates)
        {
            var countOfMissingLinesFromTarget = 0;

            foreach (var charFromTarget in mapper.GetEncodedStringByNumber(compareNumber))
            {
                if (!candidate.Contains(charFromTarget))
                {
                    countOfMissingLinesFromTarget++;
                }
            }

            if (countOfMissingLinesFromTarget == 1)
            {
                mapper.AddNewFoundEntry(targteNumber, candidate);
                break;
            }
        }

        return mapper;
    }

    private static SignalNumberMapper DecodeComplexSignalForNumber(
        SignalNumberMapper mapper, IEnumerable<string> candidates, int targetNumberForMissingChars, int targetNumberForAllIncludedChars, int target)
    {
        foreach (var candidate in candidates)
        {
            var charsFromTargetAllCharsIncluded = mapper.GetEncodedStringByNumber(targetNumberForAllIncludedChars);

            foreach (var charFromTargetMissingChars in mapper.GetEncodedStringByNumber(targetNumberForMissingChars))
            {
                if (!candidate.Contains(charFromTargetMissingChars) && charsFromTargetAllCharsIncluded.All(targetPart => candidate.Contains(targetPart)))
                {
                    mapper.AddNewFoundEntry(target, candidate);
                }
            }
        }

        return mapper;
    }
}

public record SignalEntry(string[] SignalInput, string[] Output);
