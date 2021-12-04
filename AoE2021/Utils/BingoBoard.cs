using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AoE2021.Utils
{
    public class BingoBoard
    {
        private BoardSquare[][] board;
        
        public BingoBoard(IEnumerable<int[]> boardLines)
        {
            var linesCount = boardLines.Count();
            this.board = new BoardSquare[linesCount][];
            for (int i = 0; i < linesCount; i++)
            {
                this.board[i] = boardLines.ElementAt(i).Select(x => new BoardSquare(x)).ToArray();
            }
        }

        public int? Score { get; private set; } = null;

        public void Mark(int number)
        {
            foreach (var row in board)
            {
                foreach (var value in row.Where(x => x.Number == number))
                {
                    value.IsMarked = true;
                }
            }
            
            foreach (var row in board)
            {
                if (row.All(x => x.IsMarked))
                {
                    var sum = board.Aggregate(0, (i, sq) => i + sq.Sum(x => x.IsMarked ? 0 : x.Number));
                    this.Score = number * sum;
                    return;
                }
            }

            for (int i = 0; i < board.Length; i++)
            {
                if (board.All(x => x.ElementAt(i).IsMarked))
                {
                    var sum = board.Aggregate(0, (i, sq) => i + sq.Sum(x => x.IsMarked ? 0 : x.Number));
                    this.Score = number * sum;
                    return;
                }
            }
        }

        private class BoardSquare
        {
            public BoardSquare(int number)
            {
                this.Number = number;
            }
        
            public int Number { get; set; }

            public bool IsMarked { get; set; }
        }
    }
}