using System;
using System.Collections.Generic;
using System.Linq;

public class Day13 : Day
{
    public Day13(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var earliestDepartureTime = Int32.Parse(input[0]);
        List<int> nums = new List<int>();

        foreach(var i in input[1].Split(',')){
            if(!i.Contains('x')){
                nums.Add(Int32.Parse(i));
            }
        }

        var numId = -1;
        var minWaitingTime = Int32.MaxValue;

        foreach(var id in nums){
            var waitingTime = id - earliestDepartureTime % id;
            if(waitingTime < minWaitingTime){
                minWaitingTime = waitingTime;
                numId = id;
            }
        }

        return (minWaitingTime * numId).ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var nums = new List<int>();
        var remainders = new List<int>();
        var ms = new List<double>();
        var mInverse = new List<double>();

        var ids = input[1].Split(',');
        long M = 1;
        for(int i = 0; i < ids.Length; i++){
            if(!ids[i].Contains('x')){
                var n = Int32.Parse(ids[i]);
                nums.Add(n);
                remainders.Add(n - i);
                M *= n;
            }
        }
        // nums.Add(3);
        // nums.Add(4);
        // nums.Add(5);
        // M = 60;
        // remainders.Add(2);
        // remainders.Add(3);
        // remainders.Add(1);

        long sum = 0;
        for (int i = 0; i < nums.Count; i++) 
        { 
            long pp = M / nums[i]; 
            sum += remainders[i] * inv(pp, nums[i]) * pp; 
        }


        return (sum % M).ToString();
    }

    long inv(long a, long m) 
    { 
        long m0 = m, t, q; 
        long x0 = 0, x1 = 1; 
    
        if (m == 1) 
        return 0; 
    
        // Apply extended Euclid Algorithm 
        while (a > 1) 
        { 
            q = a / m; 
            t = m; 

            m = a % m;
            a = t; 
    
            t = x0; 
            x0 = x1 - q * x0; 
    
            x1 = t; 
        } 
    
        // Make x1 positive 
        if (x1 < 0) 
        x1 += m0; 
    
        return x1; 
    } 

}