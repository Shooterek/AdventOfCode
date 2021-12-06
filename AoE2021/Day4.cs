using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoE2021
{
    public class Day4 : Day
    {
        public Day4() : base("day4")
        {
        }

        protected override object FirstTask()
        {
            var input = _inputLoader.LoadStringBatches();
            var numbers = input[0].Split(',')
                .Select(int.Parse)
                .ToList();
            var boards = CreateAllBoards(input.Skip(1));

            foreach (var inputNumber in numbers)
            {
                foreach (var board in boards)
                {
                    board.Mark(inputNumber);
                    if (board.Score != null)
                        return board.Score.Value;
                }
            }
            
            return "";
        }

        protected override object SecondTask()
        {
            var input = _inputLoader.LoadStringBatches();
            var numbers = input[0].Split(',')
                .Select(int.Parse)
                .ToList();
            var boards = CreateAllBoards(input.Skip(1));

            var i = 0;
            while (boards.Count > 1)
            {
                foreach (var board in boards)
                {
                    board.Mark(numbers[i]);
                }

                boards = boards.Where(b => b.Score == null).ToList();
                i++;
            }
                
            var lastBoard = boards.First();
            while (lastBoard.Score == null)
            {
                lastBoard.Mark(numbers[i]);
                i++;
            }

            return lastBoard.Score.Value;
        }

        private List<BingoBoard> CreateAllBoards(IEnumerable<string> batches)
            => batches
                .Select(boardRows => boardRows.Split("\r")
                    .Select(boardRow => boardRow.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse).ToArray()))
                .Select(x => new BingoBoard(x)).ToList();
    }
}
