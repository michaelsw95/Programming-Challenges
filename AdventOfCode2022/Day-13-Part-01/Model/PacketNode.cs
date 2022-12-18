class PacketNode
{
    public PacketNode(PacketValueType type)
    {
        Type = type;

        if (type == PacketValueType.Collection)
        {
            ChildValues = new List<PacketNode>();
        }
    }

    public PacketNode(int value)
        : this(PacketValueType.Value)
    {
        Value = value;
    }

    public int? Value { get; set; }
    public List<PacketNode> ChildValues { get; set; }
    public PacketValueType Type { get; set; }
}
