using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Day22 : Day
{
    public Day22(string inputPath) : base(inputPath)
    {
    }

    public override string FirstTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var loadedDecks = LoadDecks(input);

        var playersOneDeck = loadedDecks.Item1;
        var playersTwoDeck = loadedDecks.Item2;

        while(playersOneDeck.Count > 0 && playersTwoDeck.Count > 0){
            var firstNumber = playersOneDeck[0];
            var secondNumber = playersTwoDeck[0];

            playersOneDeck.RemoveAt(0);
            playersTwoDeck.RemoveAt(0);

            if(firstNumber > secondNumber){
                playersOneDeck.Add(firstNumber);
                playersOneDeck.Add(secondNumber);
            }
            else if(secondNumber > firstNumber){
                playersTwoDeck.Add(secondNumber);
                playersTwoDeck.Add(firstNumber);
            }
        }

        
        
        return playersOneDeck.Count > 0 ? CalculateScore(playersOneDeck) : CalculateScore(playersTwoDeck);
    }

    private Tuple<List<int>, List<int>> LoadDecks(List<string> input)
    {
        var playersOneDeck = new List<int>();
        var playersTwoDeck = new List<int>();
        var ind = 0;
        for(int i = 1; i < input.Count; i++){
            if(input[i + 1].Contains("Player")){
                ind = i + 2;
                break;
            }
            else{
                playersOneDeck.Add(Int32.Parse(input[i]));
            }
        }
        for(int i = ind; i < input.Count; i++){
            playersTwoDeck.Add(Int32.Parse(input[i]));
        }

        return new Tuple<List<int>, List<int>>(playersOneDeck, playersTwoDeck);
    }

    private string CalculateScore(List<int> deck)
    {
        var result = 0;
        while(deck.Count > 0){
            result += deck[0] * deck.Count;
            deck.RemoveAt(0);
        }

        return result.ToString();
    }

    public override string SecondTask()
    {
        var input = _inputLoader.LoadStringListInput(_inputPath);
        var loadedDecks = LoadDecks(input);

        var playerOneDeck = loadedDecks.Item1;
        var playerTwoDeck = loadedDecks.Item2;

        var result = RecursiveCombat(playerOneDeck, playerTwoDeck);

        return result == 1 ? CalculateScore(playerOneDeck) : CalculateScore(playerTwoDeck);
    }

    private int RecursiveCombat(List<int> pOneDeck, List<int> pTwoDeck)
    {
        var pOneHist = new HashSet<string>();
        var pTwoHist = new HashSet<string>();
        while(pOneDeck.Count > 0 && pTwoDeck.Count > 0){
            var serializedDeck1 = SerializeDeck(pOneDeck);
            var serializedDeck2 = SerializeDeck(pTwoDeck);

            if(pOneHist.Contains(serializedDeck1) || pTwoHist.Contains(serializedDeck2)){
                return 1;
            }
            var firstPlayerCard = pOneDeck[0];
            var secondPlayerCard = pTwoDeck[0];
            // Console.WriteLine("P1 Card: " + firstPlayerCard + ". P2 Card: " + secondPlayerCard);
            pOneHist.Add(serializedDeck1);
            pTwoHist.Add(serializedDeck2);

            pOneDeck.RemoveAt(0);
            pTwoDeck.RemoveAt(0);

            if(pOneDeck.Count >= firstPlayerCard && pTwoDeck.Count >= secondPlayerCard){
                var subGameResult = RecursiveCombat(pOneDeck.Take(firstPlayerCard).ToList(), pTwoDeck.Take(secondPlayerCard).ToList());
                if(subGameResult == 1){
                    pOneDeck.Add(firstPlayerCard);
                    pOneDeck.Add(secondPlayerCard);
                }
                else if(subGameResult == 2){
                    pTwoDeck.Add(secondPlayerCard);
                    pTwoDeck.Add(firstPlayerCard);
                }
            }
            else{
                if(firstPlayerCard > secondPlayerCard){
                    pOneDeck.Add(firstPlayerCard);
                    pOneDeck.Add(secondPlayerCard);
                }
                else{
                    pTwoDeck.Add(secondPlayerCard);
                    pTwoDeck.Add(firstPlayerCard);
                }
            }
        }

        return pOneDeck.Count > 0 ? 1 : 2;
    }

    private string SerializeDeck(List<int> deck)
    {
        var stringBuilder = new StringBuilder();

        foreach(var n in deck){
            stringBuilder.Append(n);
            stringBuilder.Append(' ');
        }
        return stringBuilder.ToString();
    }
}