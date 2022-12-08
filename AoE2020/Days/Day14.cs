using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class Day14 : Day
{
    public Day14(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var memory = new Dictionary<int, long>();
        string mask = null;

        for(int i = 0; i < input.Count; i++){
            var line = input[i].Split('=');
            if(line[0].Contains("mask")){
                mask = line[1].RemoveWhitespaceCharacters();
            }
            else if(line[0].Contains("mem")){
                var memAdress = Int32.Parse(Regex.Match(line[0], @"\d+").Value);
                var valueString = Convert.ToString(Int32.Parse(Regex.Match(line[1], @"\d+").Value), 2);
                if(valueString.Length < 36){
                    valueString = valueString.PadLeft(36, '0');
                }
                valueString = ApplyMask(valueString.ToCharArray(), mask);
                var finalValue = Convert.ToInt64(valueString, 2);
                memory[memAdress] = finalValue;
            }
        }

        long sum = 0;
        foreach(var item in memory){
            sum += item.Value;
        }

        return sum.ToString();
    }

    private string ApplyMask(char[] value, string mask)
    {
        for(int i = mask.Length - 1, j = value.Length - 1; i >= 0 && j >= 0; i--, j--){
            if(mask[i] == '1'){
                value[j] = '1';
            }
            else if(mask[i] == '0'){
                value[j] = '0';
            }
        }

        return new string(value);
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var memory = new Dictionary<long, long>();
        string mask = null;

        for(int i = 0; i < input.Count; i++){
            var line = input[i].Split('=');
            Console.WriteLine(i);
            if(line[0].Contains("mask")){
                mask = line[1].RemoveWhitespaceCharacters();
            }
            else if(line[0].Contains("mem")){
                var value = Int32.Parse(Regex.Match(line[1], @"\d+").Value);
                var memAdress = Convert.ToString(Int32.Parse(Regex.Match(line[0], @"\d+").Value), 2);
                if(memAdress.Length < 36){
                    memAdress = memAdress.PadLeft(36, '0');
                }
                memAdress = ApplyMaskToAddress(memAdress.ToCharArray(), mask);
                ChangeMemory(memory, memAdress, value);
            }
        }
        long sum = 0;
        foreach(var item in memory){
            sum += item.Value;
        }

        return sum.ToString();
    }

    private void ChangeMemory(Dictionary<long, long> memory, string memAdress, int value)
    {
        if(!memAdress.Contains('X')){
                var finalValue = Convert.ToInt64(memAdress, 2);
                memory[finalValue] = value;
                return;
        }
        for(int i = memAdress.Length - 1; i >= 0; i--){
            if(memAdress[i] == 'X'){
                StringBuilder sb = new StringBuilder(memAdress);
                sb[i] = '0';
                ChangeMemory(memory, sb.ToString(), value);
                sb[i] = '1';
                ChangeMemory(memory, sb.ToString(), value);
            }
        }
    }

    private string ApplyMaskToAddress(char[] address, string mask)
    {
        for(int i = mask.Length - 1, j = address.Length - 1; i >= 0 && j >= 0; i--, j--){
            if(mask[i] == '1'){
                address[j] = '1';
            }
            else if(mask[i] == 'X'){
                address[j] = 'X';
            }
        }

        return new string(address);
    }
}