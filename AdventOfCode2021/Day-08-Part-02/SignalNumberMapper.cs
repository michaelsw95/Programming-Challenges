public class SignalNumberMapper
{   
    public SignalNumberMapper()
    {
        _numberToSignal = new Dictionary<int, string>(10);
    }

    private readonly Dictionary<int, string> _numberToSignal;

    public void AddNewFoundEntry(int decodedNumber, string encodedString)
    {
        var sortedSignal = GetNormalisedSignal(encodedString);

        _numberToSignal.Add(decodedNumber, sortedSignal);
    }

    public Dictionary<string, int> GetSignalToNumberMapping()
    {
        var map = new Dictionary<string, int>(_numberToSignal.Count);

        foreach (var numberToSignalPair in _numberToSignal)
        {
            map.Add(numberToSignalPair.Value, numberToSignalPair.Key);
        }

        return map;
    }

    public string GetEncodedStringByNumber(int decodedNumber)
    {
        if (!_numberToSignal.ContainsKey(decodedNumber))
        {
            return null;
        }

        return _numberToSignal[decodedNumber];
    }

    public bool SignalIsEqual(string signal, int target) =>
        _numberToSignal[target] == GetNormalisedSignal(signal);
    
    public static string GetNormalisedSignal(string signal) =>
        new string(signal.ToCharArray().OrderBy(letter => letter).ToArray());
}
