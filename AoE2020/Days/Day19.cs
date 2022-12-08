using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class Day19 : Day
{
    public Dictionary<string, string> ResolvedRules { get; set; }
    public Dictionary<string, string> Rules { get; set; }
    public List<string> Messages { get; set; }
    public Day19(string inputPath) : base(inputPath)
    {
        ResolvedRules = new Dictionary<string, string>();
        Rules = new Dictionary<string, string>();
        Messages = new List<string>();
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        LoadRulesAndMessages(input);
        foreach(var rule in Rules){
            ResolveRule(rule.Key);
        }
        
        var zeroRule = "^"+ResolvedRules["0"]+"$";
        var counter = 0;
        foreach(var message in Messages){
            if(Regex.Match(message, zeroRule).Success){
                counter++;
            }
        }

        return counter.ToString();
    }

    private string ResolveRule(string ruleNumber)
    {
        if(ResolvedRules.ContainsKey(ruleNumber)){
            return ResolvedRules[ruleNumber];
        }

        var ruleText = Rules[ruleNumber];
        var ruleSections = ruleText.Split(" | ");
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("(");
        for(int i = 0; i < ruleSections.Length; i++){
            var rules = ruleSections[i].Split(" ");
            foreach(var rule in rules){
                stringBuilder.Append(ResolveRule(rule));
            }
            if(i < ruleSections.Length - 1){
                stringBuilder.Append("|");
            }
        }

        stringBuilder.Append(")");
        ResolvedRules.Add(ruleNumber, stringBuilder.ToString());
        
        return stringBuilder.ToString();
    }

    private void LoadRulesAndMessages(List<string> input)
    {
        var index = 0;
        for(int i = 0; i < input.Count; i++, index++){
            if(input[i].Length == 0){
                index++;
                break;
            }
            else{
                var parts = input[i].Split(": ");
                var ruleNumber = parts[0];
                Rules.Add(parts[0], parts[1]);
                if(parts[1].Contains("a") || parts[1].Contains("b")){
                    ResolvedRules.Add(parts[0], parts[1].Replace("\"", ""));
                }
            }
        }

        for(int i = index; i < input.Count; i++){
            Messages.Add(input[i]);
        }
    }

    public override string SecondTask()
    {
        throw new System.NotImplementedException();
    }
}