using System;
using System.Collections.Generic;
using System.Linq;

public class Day2 : Day
{
    private readonly PasswordPolicyParser _policyParser; 
    public Day2(string inputPath) : base(inputPath)
    {
        _policyParser = new PasswordPolicyParser();
    }

    public override string FirstTask()
    {
        var policies = LoadPolicies();

        var counter = 0;
        foreach(var policy in policies){
            var occurrences = policy.Password.Count(letter => letter == policy.RequiredLetter);
            if(occurrences >= policy.FirstValue && occurrences <= policy.SecondValue){
                counter++;
            }
        }

        return counter.ToString();
    }

    public override string SecondTask()
    {
        var policies = LoadPolicies();

        var counter = 0;
        foreach(var policy in policies){
            var password = policy.Password;
            var letterUnderLowerIndex = password[policy.FirstValue - 1];
            var letterUnderHigherIndex = password[policy.SecondValue - 1];
            if(letterUnderHigherIndex != letterUnderLowerIndex && 
                (letterUnderHigherIndex == policy.RequiredLetter || letterUnderLowerIndex == policy.RequiredLetter)){
                counter++;
            }
        }

        return counter.ToString();
    }

    private List<PasswordPolicy> LoadPolicies(){
        var input = _inputLoader.LoadStringListInput(_inputPath);
        return _policyParser.Parse(input);
    }
}