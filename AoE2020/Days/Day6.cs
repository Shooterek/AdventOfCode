using System.Collections.Generic;
using System.Linq;

public class Day6 : Day
{
    public Day6(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var groups = _inputLoader.LoadStringBatches(_inputPath);
        var sumOfCounts = 0;

        foreach(var group in groups){
            sumOfCounts += CountDistinctQuestionsWithPositiveAnswers(group);
        }

        return sumOfCounts.ToString();
    }

    public override string SecondTask()
    {
        var groups = _inputLoader.LoadStringBatches(_inputPath);
        var sumOfCounts = 0;

        foreach(var group in groups){
            sumOfCounts += CountQuestionsToWhichAllPeopleAnsweredYes(group);
        }

        return sumOfCounts.ToString();
    }

    private int CountDistinctQuestionsWithPositiveAnswers(string group){
        var peoplesAnswers = group.Split(" ");
        var distinctPositiveAnswers = new HashSet<char>();
        foreach(var singlePersonAnswers in peoplesAnswers){
            foreach(var singlePositiveAnswer in singlePersonAnswers.RemoveWhitespaceCharacters()){
                distinctPositiveAnswers.Add(singlePositiveAnswer);
            }
        }

        return distinctPositiveAnswers.Count;
    }

    private int CountQuestionsToWhichAllPeopleAnsweredYes(string group){
        int peopleInTheGroup = group.Split(" ").Length;

        int counter = 0;
        var aCharactersAscii = (char)'a';
        var zCharactersAscii = (char)'z';
        for(int i = aCharactersAscii; i <= zCharactersAscii; i++){
            var letterQuantity = group.Count(c => c == (char)i);
            if(letterQuantity == peopleInTheGroup){
                counter++;
            }
        }

        return counter;
    }
}