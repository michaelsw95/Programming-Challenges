var circuit = File.ReadAllLines("./input.txt");

var circuitValues = new Dictionary<string, int>();

foreach (var line in circuit)
{
    var identifier = GetOperationIdentifierFromLine(line);

    var type = GetOperationTypeFromIdentifier(identifier);

    var parsedOperation = GetOperation(identifier, type, line);

    circuitValues.TryAdd(parsedOperation.Target, 0);

    switch (parsedOperation.Identifier)
    {
        case OperationIdentifier.ProvideSignal:
            circuitValues[parsedOperation.Target] = parsedOperation.Value;
            break;
        case OperationIdentifier.And:
            circuitValues[parsedOperation.Target] = circuitValues[parsedOperation.VariableOne] &
                                                    circuitValues[parsedOperation.VariableTwo];
            break;
        case OperationIdentifier.Or:
            circuitValues[parsedOperation.Target] = circuitValues[parsedOperation.VariableOne] |
                                                    circuitValues[parsedOperation.VariableTwo];
            break;
        case OperationIdentifier.Not:
            int originalValue = circuitValues[parsedOperation.VariableOne];
            ushort maxValue = ushort.MaxValue;
            ushort complementedValue = (ushort)(maxValue - originalValue);
            int result = complementedValue;
            
            circuitValues[parsedOperation.Target] = result;
            break;
        case OperationIdentifier.LShift:
            circuitValues[parsedOperation.Target] = circuitValues[parsedOperation.VariableOne] <<
                                                    parsedOperation.Value;
            break;
        case OperationIdentifier.RShift:
            circuitValues[parsedOperation.Target] = circuitValues[parsedOperation.VariableOne] >>
                                                    parsedOperation.Value;
            break;
    }
}

Console.WriteLine($"Day 7 - Part 1: {circuitValues["A"]}");

OperationType GetOperationTypeFromIdentifier(OperationIdentifier operationIdentifier)
{
    return operationIdentifier switch
    {
        OperationIdentifier.And => OperationType.MultiIdentifierPlusOperation,
        OperationIdentifier.Or => OperationType.MultiIdentifierPlusOperation,
        OperationIdentifier.LShift => OperationType.SingleIdentifierPlusOperationAndValue,
        OperationIdentifier.RShift => OperationType.SingleIdentifierPlusOperationAndValue,
        OperationIdentifier.Not => OperationType.SingleIdentifierPlusOperation,
        OperationIdentifier.ProvideSignal => OperationType.SingleIdentifierPlusValue,
        _ => throw new NotSupportedException()
    };
}

OperationIdentifier GetOperationIdentifierFromLine(string line)
{
    var lineParts = line.Split("->");

    var operation = lineParts[0]
        .Where(char.IsUpper)
        .Aggregate(string.Empty, (current, element) => current + element);

    var operationMapping = new Dictionary<string, OperationIdentifier>
    {
        { "AND", OperationIdentifier.And },
        { "OR", OperationIdentifier.Or },
        { "LSHIFT", OperationIdentifier.LShift },
        { "RSHIFT", OperationIdentifier.RShift },
        { "NOT", OperationIdentifier.Not },
    };

    return operationMapping.TryGetValue(operation, out var operationType) ?
        operationType :
        OperationIdentifier.ProvideSignal;
}

Operation GetOperation(OperationIdentifier identifier, OperationType type, string line)
{
    var lineParts = line.Split("->").Select(part => part.Trim()).ToArray();
    
    if (type == OperationType.SingleIdentifierPlusValue)
    {
        var value = int.Parse(lineParts[0]);

        return new Operation
        {
            Target = lineParts[1],
            Identifier = identifier,
            Value = value
        };
    }
    else if (type == OperationType.SingleIdentifierPlusOperation)
    {
        return new Operation
        {
            Target = lineParts[1],
            Identifier = identifier,
            VariableOne = lineParts[0].Split(' ')[1]
        };
    }
    else if (type == OperationType.MultiIdentifierPlusOperation)
    {
        var operativeLinePart = lineParts[0].Split(' ');
        return new Operation
        {
            Target = lineParts[1],
            Identifier = identifier,
            VariableOne = operativeLinePart[0],
            VariableTwo = operativeLinePart[2]
        };
    }
    else if (type == OperationType.SingleIdentifierPlusOperationAndValue)
    {
        var operativeLinePart = lineParts[0].Split(' ');
        return new Operation
        {
            Target = lineParts[1],
            Identifier = identifier,
            VariableOne = operativeLinePart[0],
            Value = int.Parse(operativeLinePart[2])
        };
    }

    throw new NotSupportedException();
}

enum OperationIdentifier
{
    ProvideSignal,
    And,
    Or,
    LShift,
    RShift,
    Not
}

enum OperationType
{
    SingleIdentifierPlusOperationAndValue,
    MultiIdentifierPlusOperation,
    SingleIdentifierPlusOperation,
    SingleIdentifierPlusValue
}

class Operation
{
    public int Value { get; init; }
    public string VariableOne { get; init; }
    public OperationIdentifier Identifier { get; init; }
    public string VariableTwo { get; init; }
    public string Target { get; init; }
}
