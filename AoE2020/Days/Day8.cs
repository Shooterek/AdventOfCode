using System;
using System.Collections.Generic;

public class Day8 : Day
{
    public Day8(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var acc = 0;
        var executedInstructionsSet = new HashSet<int>();
        var instrIndex = 0;
        while(executedInstructionsSet.Add(instrIndex)){
            var parts = input[instrIndex].Split(' ');
            var instruction = parts[0];
            var parameter = Int32.Parse(parts[1]);

            switch(instruction){
                case "nop":
                    instrIndex++;
                    break;
                case "jmp":
                    instrIndex += parameter;
                    break;
                case "acc":
                    acc += parameter;
                    instrIndex++;
                    break;
            }
        }

        return acc.ToString();
    }

    public override string SecondTask()
    {   
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var acc = 0;
        var executedInstructionsSet = new HashSet<int>();
        var instrIndex = 0;
        for(int i = 0; i < input.Count; i++){
            if(!IsLooped(i)){
                input[i] = ChangeInstruction(input[i]);
                while(instrIndex != input.Count){
                    var parts = input[instrIndex].Split(' ');
                    var instruction = parts[0];
                    var parameter = Int32.Parse(parts[1]);

                    switch(instruction){
                        case "nop":
                            instrIndex++;
                            break;
                        case "jmp":
                            instrIndex += parameter;
                            break;
                        case "acc":
                            acc += parameter;
                            instrIndex++;
                            break;
                    }
                }
                break;
            }
        }

        return acc.ToString();
    }

    private bool IsLooped(int changedInstructionIndex){
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var acc = 0;
        var executedInstructionsSet = new HashSet<int>();
        var instrIndex = 0;
        input[changedInstructionIndex] = ChangeInstruction(input[changedInstructionIndex]);

        while(instrIndex < input.Count){
            if(!executedInstructionsSet.Add(instrIndex)){
                return true;
            }
            var parts = input[instrIndex].Split(' ');
            var instruction = parts[0];
            var parameter = Int32.Parse(parts[1]);

            switch(instruction){
                case "nop":
                    instrIndex++;
                    break;
                case "jmp":
                    instrIndex += parameter;
                    break;
                case "acc":
                    acc += parameter;
                    instrIndex++;
                    break;
            }
        }

        return false;
    }

    private string ChangeInstruction(string instruction)
    {
        if(instruction.Contains("jmp")){
            return instruction.Replace("jmp", "nop");
        }
        else if(instruction.Contains("nop")){
            return instruction.Replace("nop", "jmp");
        }
        return instruction;
    }
}