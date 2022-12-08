using System.Collections.Generic;

public class PasswordPolicyParser{
    public List<PasswordPolicy> Parse(List<string> input){
        var policies = new List<PasswordPolicy>();
        foreach(var line in input){
            var parsedPolicy = new PasswordPolicy();
            var parts = line.Split(':');

            parsedPolicy.Password = parts[1].Replace(" ", "");

            var policyParts = parts[0].Split(' ');
            var occurrencesPart = policyParts[0].Split('-');
            
            parsedPolicy.FirstValue = int.Parse(occurrencesPart[0]);
            parsedPolicy.SecondValue = int.Parse(occurrencesPart[1]);
            parsedPolicy.RequiredLetter = policyParts[1].ToCharArray()[0];
            policies.Add(parsedPolicy);
        }

        return policies;
    }
}