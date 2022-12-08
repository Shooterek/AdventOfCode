using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class Day4 : Day
{
    public Day4(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var batches = _inputLoader.LoadStringBatches(_inputPath);
        var requiredFields = new List<string>(){"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        var correctPassports = 0;
        foreach(var batch in batches){  
            var fieldsAndValues = batch.Split(" ");
            if(CheckPassport(fieldsAndValues, requiredFields)){
                correctPassports++;
            }
        }

        return correctPassports.ToString();
    }

    public override string SecondTask()
    {
        var batches = _inputLoader.LoadStringBatches(_inputPath);
        var requiredFields = new List<string>(){"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

        var correctPassports = 0;
        foreach(var batch in batches){  
            var fieldsAndValues = batch.Split(" ");
            if(CheckPassport(fieldsAndValues, requiredFields)){
                //Create a field-data dictionary
                Dictionary<string, string> dict = fieldsAndValues.Select(fv => {
                    var temp = fv.Split(":");
                    return new KeyValuePair<string, string>(temp[0], temp[1]);
                })
                .ToDictionary(x => x.Key, y => y.Value);

                if(CheckPassportData(dict)){
                    correctPassports++;
                }
            }
        }

        return correctPassports.ToString();
    }

    private bool CheckPassport(IEnumerable<string> fieldsAndValues, List<string> requiredFields){
        var fields = fieldsAndValues.Select(fv => fv.Split(":")[0]);
        if(fields.ContainsAll(requiredFields)){
            return true;
        }
        else{
            return false;
        }
    }

    private bool CheckPassportData(Dictionary<string, string> dict){
        var eyeColors = new List<string>(){"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};
        foreach(var entry in dict){
            var value = entry.Value.RemoveWhitespaceCharacters();
            switch (entry.Key)
            {
                case "byr":
                    if(!IntegerValueBetween(int.Parse(value), 1920, 2002)){
                        return false;
                    }
                    break;

                case "iyr":
                    if(!IntegerValueBetween(int.Parse(value), 2010, 2020)){
                        return false;
                    }
                    break;

                case "eyr":
                    if(!IntegerValueBetween(int.Parse(value), 2020, 2030)){
                        return false;
                    }
                    break;

                case "hcl":
                    if(!Regex.IsMatch(value, "^#[0-9a-f]{6}")){
                        return false;
                    }
                    break;

                case "ecl":;
                    if(!eyeColors.Contains(value)){
                        return false;
                    }
                    break;

                case "pid":
                    if(!Regex.IsMatch(value, "^[0-9]{9}$")){
                        return false;
                    }
                    break;

                case "hgt":
                    if(!Regex.IsMatch(value, "(^(1[5-8][0-9]|19[0-3])cm$)|(^(59|6[0-9]|7[0-6])in$)")){
                        return false;
                    }
                    break;
            }
        }
        return true;
    }

    private bool IntegerValueBetween(int value, int min, int max){
        if(value > max || value < min){
            return false;
        }

        return true;
    }
}