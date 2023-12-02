using AoE2023.Utils;
using System.Text.RegularExpressions;

namespace AoE2023;

public class Day2 : StringListDay
{
    private List<Game> games;
    private Regex numberRegex = new Regex(@"\d+");

    public override void LoadInput() {
        base.LoadInput();

        
        this.games = this.Input.Select(line => {
            var gameAndSets = line.Replace(" ", "").Split(':');
            var id = int.Parse(gameAndSets[0].Replace("Game", ""));
            var sets = gameAndSets[1].Split(';').Select(s => {
                var cubes = s.Split(',').Select(c => {
                    var number = int.Parse(numberRegex.Match(c).Value);
                    var color = c.Replace($"{number}", "");
                    return (number, color);
                });

                int r = 0, g = 0, b = 0;
                foreach (var cube in cubes) {
                    if (cube.color == "green")
                        g = cube.number;
                    if (cube.color == "blue")
                        b = cube.number;
                    if (cube.color == "red")
                        r = cube.number;
                }

                return new Set(r, g, b);
            }).ToList();

            return new Game(id, sets);
        }).ToList();
    }

    protected override object FirstTask()
    {
        var gameRules = new GameRules(12, 13, 14);

        var correct = this.games.Where(gameRules.GameIsCorrect).ToArray();
        return correct.Sum(g => g.Id);
    }

    protected override object SecondTask()
    {
        return this.games.Sum(g => {
            return g.Sets.Select(s => s.Blue).Max() * g.Sets.Select(s => s.Green).Max() * g.Sets.Select(s => s.Red).Max();
        });
    }
}

public record Game(int Id, List<Set> Sets);

public record Set(int Red, int Green, int Blue);

public record GameRules(int MaxRed, int MaxGreen, int MaxBlue) {
    public bool GameIsCorrect(Game game)
        => game.Sets.All(s => s.Red <= this.MaxRed && s.Blue <= this.MaxBlue && s.Green <= this.MaxGreen);
}
