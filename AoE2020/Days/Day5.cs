using System;
using System.Collections.Generic;
using System.Linq;

public class Day5 : Day
{
    public Day5(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var seatIds = new List<int>();

        foreach(var line in input){
            seatIds.Add(CalculateSeatId(line));
        }

        return seatIds.Max().ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var seatIds = new List<int>();

        foreach(var line in input){
            seatIds.Add(CalculateSeatId(line));
        }

        for(int i = 1; i < seatIds.Count - 2; i++){
            if(!seatIds.Contains(i) && seatIds.Contains(i - 1) && seatIds.Contains(i + 1)){
                return i.ToString();
            }
        }

        return "0";
    }

    private int CalculateSeatId(string instructions){
        var rowPartitioning = instructions.Substring(0, 7);
        var columnPartitioning = instructions.Substring(7, 3);

        var row = CalculateSeatRow(rowPartitioning);
        var column = CalculateSeatColumn(columnPartitioning);

        var seatId = row * 8 + column;
        return seatId;
    }

    private int CalculateSeatRow(string rowPartitioning){
        var minIndex = 0;
        var maxIndex = 127;
        foreach(var character in rowPartitioning){
            if(character == 'F'){
                maxIndex -= (maxIndex - minIndex) / 2;
            }
            else if(character == 'B'){
                minIndex += (maxIndex - minIndex + 1) / 2;
            }
        }

        return minIndex;
    }

    private int CalculateSeatColumn(string columnPartitioning){
        var minIndex = 0;
        var maxIndex = 7;
        foreach(var character in columnPartitioning){
            if(character == 'L'){
                maxIndex -= (maxIndex - minIndex) / 2;
            }
            else if(character == 'R'){
                minIndex += (maxIndex - minIndex + 1) / 2;
            }
        }

        return minIndex;
    }
}