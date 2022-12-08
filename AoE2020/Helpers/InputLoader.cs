using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class InputLoader{
    public List<string> LoadStringListInput(string filepath){
        return File.ReadAllLines(filepath)
            .ToList();
    }

    internal List<long> LoadLongListInput(string inputPath)
    {
        List<long> input = new List<long>();
        foreach(var line in File.ReadAllLines(inputPath)){
            input.Add(Int64.Parse(line));
        }

        return input;
    }

    public List<int> LoadIntListInput(string filepath){
        List<int> input = new List<int>();
        foreach(var line in File.ReadAllLines(filepath)){
            input.Add(Int32.Parse(line));
        }

        return input;
    }

    public List<string> LoadStringBatches(string filepath){
        var batches = File.ReadAllText(filepath)
            .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
            .Select(batch => batch.Replace('\n', ' '))
            .ToList();
        return batches;    
    }

    public TrainTicketInput LoadTicketRules(string filepath){
        var lines = File.ReadAllLines(filepath);
        var rules = new List<TicketRule>();
        var myTicketIndex = 0;
        var trainTicketInput = new TrainTicketInput();
        for(int i = 0; i < lines.Length; i++){
            var line = lines[i];
            if(line.Length == 0){
                myTicketIndex = i + 2;
                break;
            }
            var parts = line.Split(':');
            var propertyName = parts[0];
            var ticketRule = new TicketRule(propertyName);
            foreach(var rule in parts[1].Split("or")){
                var values = rule.Split('-');
                var minValue = Int32.Parse(values[0]);
                var maxValue = Int32.Parse(values[1]);
                ticketRule.PropertyValidRanges.Add(new PropertyValueRange(minValue, maxValue));
            }
            rules.Add(ticketRule);
        }

        var myTicketValues = new List<int>();
        foreach(var value in lines[myTicketIndex].Split(',')){
            myTicketValues.Add(Int32.Parse(value));
        }

        var nearbyTickets = new List<List<int>>();
        for(int i = myTicketIndex + 3; i < lines.Length; i++){
            var ticketValuesLine = new List<int>();
            var line = lines[i];
            foreach(var value in line.Split(',')){
                ticketValuesLine.Add(Int32.Parse(value));
            }
            nearbyTickets.Add(ticketValuesLine);
        }

        trainTicketInput.NearbyTickets = nearbyTickets;
        trainTicketInput.TicketRules = rules;
        trainTicketInput.MyTicket = myTicketValues;

        return trainTicketInput;
    }
}