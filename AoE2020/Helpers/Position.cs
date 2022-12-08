public class Position{
    public Position()
    {
        NorthSouthPosition = 0;
        EastWestPosition = 0;
        Direction = Direction.East;
    }

    public Position(int northSouth, int eastWest)
    {
        NorthSouthPosition = northSouth;
        EastWestPosition = eastWest;
        Direction = Direction.East;
    }
    // North - positive value, south - negative value
    public int NorthSouthPosition { get; set; }
    // East - positive value, west - negative value
    public int EastWestPosition { get; set; }
    public Direction Direction { get; set; }
}