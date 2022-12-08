using System;
using System.Collections.Generic;
using System.Text;

public class Day23 : Day
{
    private CircularLinkedListNode<int>[] _lookUpArray;
    public Day23(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var maxValue = 9;
        _lookUpArray = new CircularLinkedListNode<int>[maxValue + 1];
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var line = input[0];

        var linkedList = new CircularLinkedList<int>();
        foreach(var ch in line.ToCharArray()){
            var node = linkedList.Insert((int)Char.GetNumericValue(ch));
            _lookUpArray[node.Value] = node;
        }
        var f = linkedList.Head;
        var maxMoves = 100;
        for(int i = 0; i < maxMoves; i++){
            PlayOneRound(linkedList, f, maxValue);
            f = f.Next;
        }

        while(f.Value != 1){
            f = f.Next;
        }

        f = f.Next;
        var resultBuilder = new StringBuilder();
        for(int j = 0; j < 8; j++){
            resultBuilder.Append(f.Value);
            f = f.Next;
        }

        return resultBuilder.ToString();
    }

    private void PlayOneRound(CircularLinkedList<int> linkedList, CircularLinkedListNode<int> f, int maxValue)
    {
        CircularLinkedListNode<int> dest = FindDestinationCup2(f, maxValue);
        var currentNode = f;
        var firstCupToSwap = f.Next;
        var lastCupToSwap = f.Next.Next.Next;
        var t = dest.Next;
        
        f.Next = lastCupToSwap.Next;
        f.Next.Previous = f;

        dest.Next = firstCupToSwap;
        firstCupToSwap.Previous = dest;

        lastCupToSwap.Next = t;
        t.Previous = lastCupToSwap;
    }

    private CircularLinkedListNode<int> FindDestinationCup2(CircularLinkedListNode<int> f, int maxValue)
    {
        var list = new List<int>(){f.Next.Value, f.Next.Next.Value, f.Next.Next.Next.Value};
        for(int i = 1; i < 5; i++){
            if(f.Value - i >= 1 && !list.Contains(f.Value - i)){
                return _lookUpArray[f.Value - i];
            }
        }

        for(int j = maxValue; j > 0; j--){
            if(!list.Contains(j)){
                return _lookUpArray[j];
            }
        }
        return null;
    }
    public override string SecondTask()
    {
        var maxValue = 1000000;
        _lookUpArray = new CircularLinkedListNode<int>[maxValue + 1];
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var line = input[0];

        var linkedList = new CircularLinkedList<int>();
        foreach(var ch in line.ToCharArray()){
            var node = linkedList.Insert((int)Char.GetNumericValue(ch));
            _lookUpArray[node.Value] = node;
        }
        for(int i = 10; i <= maxValue; i++){
            _lookUpArray[i]=linkedList.Insert(i);
        }
        
        var f = linkedList.Head;
        var maxMoves = 10000000;
        for(int i = 0; i < maxMoves; i++){
            PlayOneRound(linkedList, f, maxValue);
            f = f.Next;
        }

        f = linkedList.Head;
        while(f.Value != 1){
            f = f.Next;
        }
        long result = (long)f.Next.Value * (long)f.Next.Next.Value;
        return result.ToString();
    }
}