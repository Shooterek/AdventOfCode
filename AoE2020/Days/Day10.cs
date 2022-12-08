using System;
using System.Collections.Generic;
using System.Linq;

public class Day10 : Day
{
    public Day10(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadIntListInput(_inputPath);
        input.Sort();

        return GetProductOfOneAndThreeJoltsAdapters(input).ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadIntListInput(_inputPath);
        input.Add(0);
        input.Add(input.Max() + 3);
        input.Sort();
        var currentJoltage = input[0];
        var length = 0;
        var dict = new Dictionary<int, int>();
        dict[2] = 0;
        dict[4] = 0;
        dict[7] = 0;
        for(int i = 1; i < input.Count; i++){
            if(input[i] - currentJoltage == 1){
                length++;
            }
            else{
                if(length == 4){
                    dict[7] += 1;
                }
                else if(length == 3){
                    dict[4] += 1;
                }
                else if(length == 2){
                    dict[2] += 1;
                }
                length = 0;
            }
            currentJoltage = input[i];
        }
        
        double result = Math.Pow(2, dict[2]) * Math.Pow(4, dict[4]) * Math.Pow(7, dict[7]);
        return result.ToString();
    }


    //One three jolts adapater difference comes from one adapter, which is always 3 jolts stronger than the strongest adapter from the list.
    private long GetProductOfOneAndThreeJoltsAdapters(List<int> adapters){
        
        int oneJoltDifferenceCounter = 0;
        int threeJoltsDifferenceCounter = 0;
        var currentJoltage = 0;

        for(int i = 0; i < adapters.Count; i++){
            if(adapters[i] - currentJoltage == 1){
                oneJoltDifferenceCounter++;
            }
            else if(adapters[i] - currentJoltage == 3){
                threeJoltsDifferenceCounter++;
            }

            currentJoltage = adapters[i];
        }

        return oneJoltDifferenceCounter * (threeJoltsDifferenceCounter + 1);
    }

    private long GetAllArrangements(List<int> adapters){
        long counter = 1;

        for(int i = 0; i < adapters.Count; i++){
            Console.Write(adapters[i] + " ");
        }
        Console.Write(Environment.NewLine);
        var requiredAdaptersIndexes = GetRequiredAdaptersIndexes(adapters);
        for(int i = 0; i < adapters.Count;){
            for(int j = 0; j < 4 && j + i < adapters.Count; j++){
                if(requiredAdaptersIndexes.Contains(i + j)){
                    i += j == 0 ? 1 : j;
                    if(j == 2){
                        counter *= 4;
                    }
                    else if(j == 1){
                        counter *= 2;
                    }
                    break;
                }
            }
        }
        return counter;
    }

    private List<int> GetRequiredAdaptersIndexes(List<int> adapters)
    {
        var indexes = new List<int>();
        var currentJoltage = -9999;
        
        for(int i = 0; i < adapters.Count - 1; i++){
            if(adapters[i + 1] - currentJoltage > 3){
                currentJoltage = adapters[i];
                indexes.Add(i);
                Console.Write(adapters[i] + " ");
            }
        }
        Console.Write(Environment.NewLine);
        indexes.Add(adapters.Count - 1);

        return indexes;
    }
}