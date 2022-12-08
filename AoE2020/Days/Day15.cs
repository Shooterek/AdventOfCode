using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class Day15 : Day
{
    public Day15(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = new List<int>(){
            19,20,14,0,9,1
        };

        var spokenNumber = 0;
        var dict = new Dictionary<int, int>();
        var introductionOfNumber = new Dictionary<int, int>();
        var wasFirstTime = false;
        for(int i = 0; i < input.Count; i++){
            spokenNumber = input[i];
            dict[spokenNumber] = i;
            if(!introductionOfNumber.ContainsKey(spokenNumber)){
                wasFirstTime = true;
                introductionOfNumber[input[i]] = i;
            }
            else{
                wasFirstTime = false;
            }
            dict[input[i]] = i;
        }
        var counter = input.Count;
        while(counter < 2020){
            if(wasFirstTime){
                dict[0] = counter;
                spokenNumber = 0;
                if(introductionOfNumber.ContainsKey(0)){
                    wasFirstTime = false;
                }
                else{
                    introductionOfNumber[0] = counter;
                }
            }
            else{
                var lastTime = introductionOfNumber[spokenNumber];
                var diff = dict[spokenNumber] - lastTime;
                introductionOfNumber[spokenNumber] = dict[spokenNumber];
                if(introductionOfNumber.ContainsKey(diff)){
                    wasFirstTime = false;
                }
                dict[diff] = counter;                
                spokenNumber = diff;
                if(!introductionOfNumber.ContainsKey(spokenNumber)){
                    wasFirstTime = true;
                    introductionOfNumber[spokenNumber] = counter;
                }
            }
            counter++;
        }
        return spokenNumber.ToString();
    }

    public override string SecondTask()
    {
        var input = new List<int>(){
            19,20,14,0,9,1
        };

        var spokenNumber = 0;
        var dict = new Dictionary<int, int>();
        var introductionOfNumber = new Dictionary<int, int>();
        var wasFirstTime = false;
        for(int i = 0; i < input.Count; i++){
            spokenNumber = input[i];
            dict[spokenNumber] = i;
            if(!introductionOfNumber.ContainsKey(spokenNumber)){
                wasFirstTime = true;
                introductionOfNumber[input[i]] = i;
            }
            else{
                wasFirstTime = false;
            }
            dict[input[i]] = i;
        }
        var counter = input.Count;
        while(counter < 30000000){
            if(wasFirstTime){
                dict[0] = counter;
                spokenNumber = 0;
                if(introductionOfNumber.ContainsKey(0)){
                    wasFirstTime = false;
                }
                else{
                    introductionOfNumber[0] = counter;
                }
            }
            else{
                var lastTime = introductionOfNumber[spokenNumber];
                var diff = dict[spokenNumber] - lastTime;
                introductionOfNumber[spokenNumber] = dict[spokenNumber];
                if(introductionOfNumber.ContainsKey(diff)){
                    wasFirstTime = false;
                }
                dict[diff] = counter;                
                spokenNumber = diff;
                if(!introductionOfNumber.ContainsKey(spokenNumber)){
                    wasFirstTime = true;
                    introductionOfNumber[spokenNumber] = counter;
                }
            }
            counter++;
        }
        return spokenNumber.ToString();
    }
}