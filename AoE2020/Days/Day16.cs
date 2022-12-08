using System;
using System.Collections.Generic;
using System.Linq;

public class Day16 : Day
{
    public Day16(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadTicketRules(_inputPath);

        var sumOfInvalidValues = 0;
        foreach(var ticket in input.NearbyTickets){
            foreach(var ticketValue in ticket){
                var canBeValid = false;
                foreach(var rule in input.TicketRules){
                    if(rule.CheckIfValueIsValid(ticketValue)){
                        canBeValid = true;
                        break;
                    }
                }
                if(canBeValid == false){
                    sumOfInvalidValues += ticketValue;
                }
            }
        }

        return sumOfInvalidValues.ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadTicketRules(_inputPath);
        var invalidTickets = new List<List<int>>();
        foreach(var ticket in input.NearbyTickets){
            foreach(var ticketValue in ticket){
                var canBeValid = false;
                foreach(var rule in input.TicketRules){
                    if(rule.CheckIfValueIsValid(ticketValue)){
                        canBeValid = true;
                        break;
                    }
                }
                if(canBeValid == false){
                    invalidTickets.Add(ticket);
                }
            }
        }

        foreach(var ticket in invalidTickets){
            input.NearbyTickets.Remove(ticket);
        }

        long returnValue = 1;
        while(input.TicketRules.Count > 0){
            //for each position find the one and only rule that's fulfilled by value from this position from all tickets.
            for(int i = 0; i < input.NearbyTickets[0].Count; i++){
                var validRulesCount = new Dictionary<int, int>();
                for(int b = 0; b < input.TicketRules.Count; b++){
                    validRulesCount[b] = 0;
                }
                //Check every rule
                for(int j = 0; j < input.TicketRules.Count; j++){
                    var rule = input.TicketRules[j];
                    //Check the position specified by the i index from every ticket
                    for(int z = 0; z < input.NearbyTickets.Count; z++){
                        //If rule is fulfilled, increase corresponding counter.
                        if(rule.CheckIfValueIsValid(input.NearbyTickets[z][i])){
                            validRulesCount[j] = validRulesCount[j] + 1; 
                        }
                    }
                }

                //If after checking all rules there's only one with counter equal to the amount of tickets we can safely remove it from the list
                if(validRulesCount.Values.Where(v => v == input.NearbyTickets.Count).Count() == 1){
                    var rule = validRulesCount.First(x => x.Value == input.NearbyTickets.Count);
                    //If rule contains the word 'departure' we have to multiply the result.
                    if(input.TicketRules[rule.Key].PropertyName.Contains("departure")){
                        returnValue *= input.MyTicket[i];
                    }
                    input.TicketRules.RemoveAt(rule.Key);
                }
            }
        }


        return returnValue.ToString();
    }
}