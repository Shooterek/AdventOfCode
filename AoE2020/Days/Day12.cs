using System;

public class Day12 : Day
{
    public Day12(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var instructions = _inputLoader.LoadStringListInput(_inputPath);
        
        var currentPosition = new Position();

        foreach(var instruction in instructions){
            ChangePosition(currentPosition, instruction);
        }

        var manhattanDistance = Math.Abs(currentPosition.NorthSouthPosition) + Math.Abs(currentPosition.EastWestPosition);

        return manhattanDistance.ToString();
    }

    public override string SecondTask()
    {
        var instructions = _inputLoader.LoadStringListInput(_inputPath);
        
        var currentPosition = new Position();
        var waypointPosition = new Position(1, 10);

        foreach(var instruction in instructions){
            ChangePosition(currentPosition, waypointPosition, instruction);
        }

        var manhattanDistance = Math.Abs(currentPosition.NorthSouthPosition) + Math.Abs(currentPosition.EastWestPosition);

        return manhattanDistance.ToString();
    }

    private void ChangePosition(Position currentPosition, Position waypointPosition, string instruction)
    {
        var action = instruction[0];
        var value = Int32.Parse(instruction.Substring(1, instruction.Length - 1));

        if(action == 'F'){
            MoveToWaypoint(currentPosition, waypointPosition, value);
        }
        else{
            ChangeWaypointPosition(waypointPosition, action, value);
        }

        Console.WriteLine("Current: " + currentPosition.EastWestPosition + " " + currentPosition.NorthSouthPosition + "  " + waypointPosition.EastWestPosition + " " + waypointPosition.NorthSouthPosition + " " + instruction);
    }

    private void ChangeWaypointPosition(Position waypointPosition, char action, int value)
    {
        switch(action){
            case 'E':
                waypointPosition.EastWestPosition += value;
                break;

            case 'S':
                waypointPosition.NorthSouthPosition -= value;
                break;

            case 'W':
                waypointPosition.EastWestPosition -= value;
                break;

            case 'N':
                waypointPosition.NorthSouthPosition += value;
                break;

            case 'R':
            case 'L':
                RotateWaypoint(waypointPosition, action, value);
                break;
        }
    }

    private void RotateWaypoint(Position waypointPosition, char action, int value)
    {
        int newNorthSouth;
        int newEastWest;
        if(value == 180){
            newNorthSouth = -waypointPosition.NorthSouthPosition;
            newEastWest = -waypointPosition.EastWestPosition;
            waypointPosition.NorthSouthPosition = newNorthSouth;
            waypointPosition.EastWestPosition = newEastWest;
        }
        if((action == 'L' && value == 90) || (action == 'R' && value == 270)){
            newNorthSouth = waypointPosition.EastWestPosition;
            newEastWest = -waypointPosition.NorthSouthPosition;
            waypointPosition.EastWestPosition = newEastWest;
            waypointPosition.NorthSouthPosition = newNorthSouth;
        }
        if((action == 'L' && value == 270) || (action == 'R' && value == 90)){
            newNorthSouth = -waypointPosition.EastWestPosition;
            newEastWest = waypointPosition.NorthSouthPosition;
            waypointPosition.EastWestPosition = newEastWest;
            waypointPosition.NorthSouthPosition = newNorthSouth;
        }
    }

    private void ChangePosition(Position currentPosition, string instruction)
    {
        var action = instruction[0];
        var value = Int32.Parse(instruction.Substring(1, instruction.Length - 1));
        var currentDirectionValue = (int)currentPosition.Direction;

        switch(action){
            case 'L':
                currentPosition.Direction = (Direction)Mod(currentDirectionValue - value / 90, 4);
                break;

            case 'R':
                currentPosition.Direction = (Direction)Mod(currentDirectionValue + value / 90, 4);
                break;

            case 'F':
                MoveInDirection(currentPosition, currentPosition.Direction, value);
                break;
            
            case 'N':
            case 'S':
            case 'E':
            case 'W':
                var direction = ConverCharToDirection(action);
                MoveInDirection(currentPosition, direction, value);
                break;
        }
    }

    private Direction ConverCharToDirection(char action)
    {
        switch(action){
            case 'E':
                return Direction.East;

            case 'S':
                return Direction.South;

            case 'W':
                return Direction.West;

            case 'N':
                return Direction.North;
        }

        throw new ArgumentException("Incorrect direction");
    }

    private void MoveInDirection(Position currentPosition, Direction ferryDirection, int value)
    {
        switch(ferryDirection){
            case Direction.East:
                currentPosition.EastWestPosition += value;
                break;
                
            case Direction.West:
                currentPosition.EastWestPosition -= value;
                break;
                
            case Direction.North:
                currentPosition.NorthSouthPosition += value;
                break;
                
            case Direction.South:
                currentPosition.NorthSouthPosition -= value;
                break;
        }
    }

    private int Mod(int x, int m) {
        return (x%m + m)%m;
    }
    
    private void MoveToWaypoint(Position currentPosition, Position waypointPosition, int value)
    {
        for(int i = 0; i < value; i++){
            currentPosition.EastWestPosition += waypointPosition.EastWestPosition;
            currentPosition.NorthSouthPosition += waypointPosition.NorthSouthPosition;
        }
    }
}
