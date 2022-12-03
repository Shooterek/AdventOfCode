using AoE2022.Utils;

namespace AoE2022;

public class Day2 : StringListDay
{
    private const string Rock = "A";
    private const string Paper = "B";
    private const string Scissors = "C";

    protected override object FirstTask()
    {
        const string Rock2 = "X";
        const string Paper2 = "Y";
        const string Scissors2 = "Z";

        return this.Input.Select(l => {
            var plays = l.Split(" ");
            var elfPlay = plays[0];
            var myPlay = plays[1];

            var playPoints = myPlay switch {
                Rock2 => 1,
                Paper2 => 2,
                Scissors2 => 3,
                _ => throw new Exception(),
            };

            var outcomePoints = (elfPlay, myPlay) switch {
                ((Rock, Rock2) or (Paper, Paper2) or (Scissors, Scissors2)) => 3,
                ((Rock, Paper2) or (Paper, Scissors2) or (Scissors, Rock2)) => 6,
                ((Rock, Scissors2) or (Paper, Rock2) or (Scissors, Paper2)) => 0,
            };

            return playPoints + outcomePoints;
        }).Sum();
    }

    protected override object SecondTask()
    {
        const string Lost = "X";
        const string Draw = "Y";
        const string Win = "Z";
        var list = new LinkedList<string>(new string[] { Rock, Paper, Scissors });

        return this.Input.Select(l => {
            var plays = l.Split(" ");
            var elfPlay = plays[0];
            var requiredOutcome = plays[1];
            var node = list.Find(elfPlay);

            (var myPlay, var outcomePoints) = requiredOutcome switch {
                Lost => ((node.Previous ?? list.Last).Value, 0),
                Draw => (node.Value, 3),
                Win => ((node.Next ?? list.First).Value, 6)
            };

            var playPoints = myPlay switch {
                Rock => 1,
                Paper => 2,
                Scissors => 3,
                _ => throw new Exception(),
            };

            return playPoints + outcomePoints;
        }).Sum();
    }
}
