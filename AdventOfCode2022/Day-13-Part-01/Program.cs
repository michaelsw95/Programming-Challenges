var input = File.ReadAllLines("input.txt");

var successIndices = new List<int>();
var runningPosition = 1;

for (int i = 0; i < input.Length; i += 3)
{
    var left = new Packet(input[i]);
    var right = new Packet(input[i + 1]);

    if (CollectionPacketNodesAreInOrder(left.PacketNodes, right.PacketNodes) == NodeCompareResult.Success)
    {
        successIndices.Add(runningPosition);
    }

    runningPosition++;
}

Console.WriteLine($"Day 13 - Part 1: {successIndices.Sum()}");

bool BothPacketsAreValueNodes(PacketNode left, PacketNode right) =>
    left.Type == PacketValueType.Value && right.Type == PacketValueType.Value;

bool BothPacketsAreCollectionNodes(PacketNode left, PacketNode right) =>
    left.Type == PacketValueType.Collection && right.Type == PacketValueType.Collection;

bool OnePacketIsValueNodeOneIsCollectionNode(PacketNode left, PacketNode right) =>
    (left.Type == PacketValueType.Value && right.Type == PacketValueType.Collection) ||
    (left.Type == PacketValueType.Collection && right.Type == PacketValueType.Value);

bool ValuePacketNodesAreEqual(PacketNode left, PacketNode right) =>
    left.Value == right.Value;

NodeCompareResult ValuePacketNodesAreInOrder(PacketNode left, PacketNode right) =>
    left.Value < right.Value ? NodeCompareResult.Success : NodeCompareResult.Failure;

NodeCompareResult CollectionPacketNodesAreInOrder(List<PacketNode> left, List<PacketNode> right)
{
    for (var i = 0; i < left.Count; i++)
    {
        if (i >= right.Count)
        {
            return NodeCompareResult.Failure;
        }

        if (BothPacketsAreValueNodes(left[i], right[i]))
        {
            if (ValuePacketNodesAreEqual(left[i], right[i]))
            {
                continue;
            }

            return ValuePacketNodesAreInOrder(left[i], right[i]);
        }
        else if (BothPacketsAreCollectionNodes(left[i], right[i]))
        {
            var result = CollectionPacketNodesAreInOrder(left[i].ChildValues, right[i].ChildValues);

            if (result == NodeCompareResult.Unknown)
            {
                continue;
            }

            return result;
        }
        else if (OnePacketIsValueNodeOneIsCollectionNode(left[i], right[i]))
        {
            var leftCollection = left[i].Type == PacketValueType.Collection ? left[i].ChildValues : new List<PacketNode> { left[i] };
            var rightCollection = right[i].Type == PacketValueType.Collection ? right[i].ChildValues : new List<PacketNode> { right[i] };

            var result = CollectionPacketNodesAreInOrder(leftCollection, rightCollection);

            if (result == NodeCompareResult.Unknown)
            {
                continue;
            }

            return result;
        }
    }

    return left.Count < right.Count ? NodeCompareResult.Success : NodeCompareResult.Unknown;
}
