using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AoE2021
{
    public class Day4 : Day
    {
        public Day4(string inputPath) : base(inputPath)
        {

        }
        public override string FirstTask()
        {
            var input = _inputLoader.LoadStringBatches(this._inputPath);

            var numbers = input[0];
            var boards = new List<BingoBoard>();

            for (var i = 1; i < input.Count; i++)
            {
                var boardLines = input[i].Split("\r")
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => x.Select(int.Parse).ToArray());
                boards.Add(new BingoBoard(boardLines));
            }

            foreach (var inputNumber in numbers.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToList())
            {
                foreach (var board in boards)
                {
                    board.Mark(inputNumber);
                    if (board.Score != null)
                    {
                        return board.Score.Value.ToString();
                    }
                }
            }
            
            return "";
        }

        public override string SecondTask()
        {
            var input = _inputLoader.LoadStringBatches(this._inputPath);

            var numbers = input[0].Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse)
                .ToList();
            var boards = new List<BingoBoard>();

            for (var i = 1; i < input.Count; i++)
            {
                var boardLines = input[i].Split("\r")
                    .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    .Select(x => x.Select(int.Parse).ToArray());
                boards.Add(new BingoBoard(boardLines));
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                while (boards.Count() > 1)
                {
                    foreach (var board in boards)
                    {
                        board.Mark(numbers[i]);
                    }

                    boards = boards.Where(b => b.Score == null).ToList();
                    i++;
                }
                
                var lastBoard = boards.First();
                lastBoard.Mark(numbers[i]);

                if (lastBoard.Score != null)
                    return lastBoard.Score.Value.ToString();
            }
            return "";
        }
    }
}
