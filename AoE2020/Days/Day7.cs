using System;
using System.Collections.Generic;
using System.Linq;

public class Day7 : Day
{
    private readonly LuggageRuleParser _ruleParser;
    public Day7(string inputPath) : base(inputPath)
    {
        _ruleParser = new LuggageRuleParser();
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var rules = _ruleParser.ParseRules(input);
        var bagColor = "shiny gold";

        var result = GetAllBagsThatCanCarryBagOfSpecifiedColor(bagColor, rules);
        return result.Count.ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var rules = _ruleParser.ParseRules(input);
        var bagColor = "shiny gold";
        var bagRule = rules.First(r => r.BagColor.Equals(bagColor));
        
        var result = CountAllRequiredBagsToEquip(bagRule, rules);
        return result.ToString();
    }

    private List<string> GetAllBagsThatCanCarryBagOfSpecifiedColor(string bagColor, List<LuggageRule> rules){
        var bagsThatCanCarry = new List<string>();
        var result = new List<String>();
        foreach(var rule in rules){
            if(rule.RequiredContent.Any(b => b.BagColor.Equals(bagColor))){
                bagsThatCanCarry.Add(rule.BagColor);
                result.Add(rule.BagColor);
            }
        }
        foreach(var containerBag in bagsThatCanCarry){
            result.AddRange(GetAllBagsThatCanCarryBagOfSpecifiedColor(containerBag, rules));
        }
        return result.Distinct().ToList();
    }

    private int CountAllRequiredBagsToEquip(LuggageRule bagRule, List<LuggageRule> rules){
        if(bagRule.RequiredContent.Count == 0){
            return 0;
        }

        var counter = 0;
        foreach(var requiredBag in bagRule.RequiredContent){
            counter += requiredBag.RequiredQuantity;
        }

        foreach(var requiredBag in bagRule.RequiredContent){
            var rule = rules.First(r => r.BagColor.Equals(requiredBag.BagColor));
            counter += (CountAllRequiredBagsToEquip(rule, rules) * requiredBag.RequiredQuantity);
        }

        return counter;
    }
}