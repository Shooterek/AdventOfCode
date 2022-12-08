using System.Collections.Generic;

public class Day1 : Day{
    private readonly int _searchedValue = 2020;

    public Day1(string inputPath) : base(inputPath)
    {

    }
    public override string FirstTask(){
        var input = _inputLoader.LoadIntListInput(_inputPath);
        input.Sort();

        var result = FindProductOfTwoNumbersThatAddUpTo(input, _searchedValue);
        return result.ToString();
    }

    public override string SecondTask(){
        var input = _inputLoader.LoadIntListInput(_inputPath);
        input.Sort();

        int result = 0;
        for(int i = 0; i < input.Count; i++){
            var tempList = new List<int>(input);
            tempList.RemoveAt(i);
            var partialProduct = FindProductOfTwoNumbersThatAddUpTo(tempList, _searchedValue - input[i]);
            if(partialProduct != 0){
                result = input[i] * partialProduct;
            }
        }

        return result.ToString();
    }


    //returns 0 if there are no two numbers that add up to the searched value
    int FindProductOfTwoNumbersThatAddUpTo(List<int> numbers, int searchedValue){
        int i = 0;
        int j = numbers.Count - 1;

        int result = 0;
        while(i < j){
            var sum = numbers[i] + numbers[j];
            if(sum == searchedValue){
                result = numbers[i] * numbers[j];
                break;
            }
            if(sum < searchedValue){
                i++;
            }
            else if(sum > searchedValue){
                j--;
            }
        }

        return result;
    }
}