namespace Day_03_Part_02
{
    class Claim
    {
        public Claim(int id, int distanceFromLeft, int distanceFromTop, int width, int height)
        {
            Id = id;
            DistanceFromLeft = distanceFromLeft;
            DistanceFromTop = distanceFromTop;
            Width = width;
            Height = height;
        }

        public int Id { get; }
        public int DistanceFromLeft { get; }
        public int DistanceFromTop { get; }
        public int Width { get; }
        public int Height { get; }
    }
}