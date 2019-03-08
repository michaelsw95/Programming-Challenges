using System;

namespace Day_04_Part_02
{
    class Record
    {
        public Record(DateTime timeStamp, string text, RecordType type)
        {
            TimeStamp = timeStamp;
            Text = text;
            Type = type;
        }

        public DateTime TimeStamp { get; }
        public string Text { get; }
        public RecordType Type { get; }
        public int GuardId;
    }
}