var input = File.ReadAllLines("input.txt").Where(packetLine => !string.IsNullOrEmpty(packetLine));

var allPackets = new List<Packet>();
foreach (var packetLine in input)
{
    var packet = new Packet(packetLine);
    allPackets.Add(packet);
}

var dividerPacketOne = new Packet("[[2]]");
var dividerPacketTwo = new Packet("[[6]]");
allPackets.Add(dividerPacketOne);
allPackets.Add(dividerPacketTwo);

allPackets.Sort(new PacketCompare());

var positionOfDividerPacketOne = allPackets.IndexOf(dividerPacketOne) + 1;
var positionOfDividerPacketTwo = allPackets.IndexOf(dividerPacketTwo) + 1;

Console.WriteLine($"Day 13 - Part 2 {positionOfDividerPacketOne * positionOfDividerPacketTwo}");

class PacketCompare : IComparer<Packet>
{
    public int Compare(Packet packetOne, Packet packetTwo)
    {
        return CollectionPacketNodesAreInOrder(packetOne.PacketNodes, packetTwo.PacketNodes) == NodeCompareResult.Success ? -1 : 1;
    }

    private bool BothPacketsAreValueNodes(PacketNode left, PacketNode right) =>
    left.Type == PacketValueType.Value && right.Type == PacketValueType.Value;

    private bool BothPacketsAreCollectionNodes(PacketNode left, PacketNode right) =>
        left.Type == PacketValueType.Collection && right.Type == PacketValueType.Collection;

    private bool OnePacketIsValueNodeOneIsCollectionNode(PacketNode left, PacketNode right) =>
        (left.Type == PacketValueType.Value && right.Type == PacketValueType.Collection) ||
        (left.Type == PacketValueType.Collection && right.Type == PacketValueType.Value);

    private bool ValuePacketNodesAreEqual(PacketNode left, PacketNode right) =>
        left.Value == right.Value;

    private NodeCompareResult ValuePacketNodesAreInOrder(PacketNode left, PacketNode right) =>
        left.Value < right.Value ? NodeCompareResult.Success : NodeCompareResult.Failure;

    private NodeCompareResult CollectionPacketNodesAreInOrder(List<PacketNode> left, List<PacketNode> right)
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
}
