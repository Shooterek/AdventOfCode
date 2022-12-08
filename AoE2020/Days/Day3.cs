using System;
using System.Collections.Generic;
using System.Linq;

public class Day3 : Day
{
    public Day3(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var map = input.Select(line => line.ToCharArray()).ToList();
        var encounteredTrees = CountEncounteredTrees(map, 3, 1);

        return encounteredTrees.ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var map = input.Select(line => line.ToCharArray()).ToList();
        var slopes = new List<Tuple<int, int>>();

        slopes.Add(new Tuple<int, int>(1, 1));
        slopes.Add(new Tuple<int, int>(3, 1));
        slopes.Add(new Tuple<int, int>(5, 1));
        slopes.Add(new Tuple<int, int>(7, 1));
        slopes.Add(new Tuple<int, int>(1, 2));

        var results = new List<int>();

        foreach(var slope in slopes){
            results.Add(CountEncounteredTrees(map, slope.Item1, slope.Item2));
        }

        return results.Aggregate((double)1, (x, y) => x * y).ToString();
    }

    private int CountEncounteredTrees(List<char[]> map, int xStep, int yStep){
        int encounteredTrees = 0;
        int xPos = 0;
        int maxXPos = map[0].Length;
        for(int yPos = 0; yPos < map.Count; yPos += yStep, xPos += xStep){
            xPos %= maxXPos;
            if(map[yPos][xPos] == '#'){
                encounteredTrees++;
            }
        }

        return encounteredTrees;
    }
}