using System;
using System.Collections.Generic;
using System.Linq;

public class Day9 : Day
{
    private readonly int _preambleLength = 25;
    public Day9(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {       
        var input = _inputLoader.LoadLongListInput(_inputPath);
        var preamble = input.GetRange(0, _preambleLength);

        var invalidNumber = FindInvalidNumber(input, preamble);

        return invalidNumber.ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadLongListInput(_inputPath);
        var preamble = input.GetRange(0, _preambleLength);

        var invalidNumber = FindInvalidNumber(input, preamble);

        var numbersThatAddUpToInvalidNumber = FindNumbersThatAddUpToNumber(input, invalidNumber);

        var sumOfTheSmallestAndLargestValues = numbersThatAddUpToInvalidNumber.Min() + numbersThatAddUpToInvalidNumber.Max();

        return sumOfTheSmallestAndLargestValues.ToString();
    }

    private long FindInvalidNumber(List<long> input, List<long> preamble)
    {
        for(int i = _preambleLength; i < input.Count; i++){
            if(!IsValid(input[i], preamble)){
                return input[i];
            }
            else{
                preamble[i % _preambleLength] = input[i];
            }
        }

        return -1;
    }

    private bool IsValid(long searchedValue, List<long> preamble)
    {
        for(int i = 0; i < preamble.Count; i++){
            for(int j = 0; j < preamble.Count; j++){
                if(i != j && preamble[i] + preamble[j] == searchedValue){
                    return true;
                }
            }
        }

        return false;;
    }

    private List<long> FindNumbersThatAddUpToNumber(List<long> input, long searchedNumber)
    {
        for(int i = 0; i < input.Count; i++){
            long number = 0;
            var j = i;
            
            while(number <= searchedNumber && j < input.Count){
                number += input[j];
                j++;            
                //If contiguous numbers add to the searched value, then repeat the process of finding them, this time adding all numbers to the result
                if(number == searchedNumber){
                    var numbers = new List<long>();
                    j = i;
                    number = 0;
                    while(number < searchedNumber){
                        number += input[j];
                        numbers.Add(input[j]);
                        j++;
                    }

                    return numbers;
                }
            }

        }

        return null;
    }
}