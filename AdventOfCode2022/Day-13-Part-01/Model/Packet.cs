class Packet
{
    public Packet(string packetInput)
    {
        PacketNodes = new List<PacketNode>();
        var nodeTree = new Stack<PacketNode>();
        var depth = 0;

        for (var i = 0; i < packetInput.Length; i++)
        {
            var current = packetInput[i];

            if (current == '[')
            {
                if (!nodeTree.Any() && depth > 0)
                {
                    PacketNodes.Add(new PacketNode(PacketValueType.Collection));
                    nodeTree.Push(PacketNodes.Last());
                }
                else if (nodeTree.Any())
                {
                    var workingNode = nodeTree.Peek();
                    var newNode = new PacketNode(PacketValueType.Collection);

                    workingNode.ChildValues.Add(newNode);
                    nodeTree.Push(newNode);
                }

                depth++;
            }
            else if (int.TryParse(current.ToString(), out var startOfNumber))
            {
                var inputNumber = $"{startOfNumber}";
                while (i + 1 < packetInput.Length && int.TryParse(packetInput[i + 1].ToString(), out var nextPacketValue))
                {
                    inputNumber += nextPacketValue;
                    i++;
                }

                var packetValue = int.Parse(inputNumber);

                var workingNode = nodeTree.Any() ? nodeTree.Peek() : null;
                if (workingNode == null)
                {
                    PacketNodes.Add(new PacketNode(packetValue));
                }
                else
                {
                    workingNode.ChildValues.Add(new PacketNode(packetValue));
                }
            }
            else if (current == ']')
            {
                if (nodeTree.Any())
                {
                    nodeTree.Pop();
                }

                depth--;
            }
        }
    }

    public List<PacketNode> PacketNodes { get; set; }
}
