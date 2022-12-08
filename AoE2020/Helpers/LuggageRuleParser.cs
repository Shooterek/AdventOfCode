using System;
using System.Collections.Generic;

public class LuggageRuleParser{
    public List<LuggageRule> ParseRules(List<string> rules){
        var luggageRules = new List<LuggageRule>();

        foreach(var line in rules){
            var parts = line.Split("bags contain");
            var color = parts[0].Trim();
            var luggageRule = new LuggageRule(color);
            var requiredContent = parts[1].Split(",");
            foreach(var requiredBag in requiredContent){
                if(requiredBag.Contains("no other bags")){
                    continue;
                }
                var bagParts = requiredBag.Trim(' ').Split(" ");
                var requiredBagQuantity = Int32.Parse(bagParts[0]);
                var bagColor = bagParts[1] + " " + bagParts[2];

                var bagContent = new BagContent(bagColor, requiredBagQuantity);
                luggageRule.RequiredContent.Add(bagContent);
            }
            luggageRules.Add(luggageRule);
        }

        return luggageRules;
    }
}