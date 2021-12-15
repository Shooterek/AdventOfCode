using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace AoE2021
{
    public class Day15 : Day
    {
        public Day15() : base("day15")
        {
        }

        protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray().Select(r => (int)char.GetNumericValue(r)).ToArray()).ToArray();
			var q = new List<Vertex>();
			var s = new List<Vertex>();

			for (int i = 0; i < input.Length; i++)
			{
				for (int j = 0; j < input[i].Length; j++)
				{
					var v = new Vertex(i, j);
					if (i == 0 && j == 0)
						v.Du = 0;

					q.Add(v);
				}
			}
			Dijkstra();

			return s.Single(x => x.Y == input.Length - 1 && x.X == input[0].Length - 1).Du;

			void Dijkstra()
			{
				while (q.Any())
				{
					var currVertex = q.OrderBy(x => x.Du).First();
					q.Remove(currVertex);
					s.Add(currVertex);

					var neighbours = q.Where(x => x.IsNeighbour(currVertex));
					foreach (var n in neighbours)
					{
						var dist = input[n.Y][n.X];
						if (currVertex.Du + dist < n.Du)
						{
							n.Du = currVertex.Du + dist;
							n.PreviousVertex = currVertex;
						}
					}
				}
			}
		}

		protected override object SecondTask()
		{
			var input = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray().Select(r => (int)char.GetNumericValue(r)).ToArray()).ToArray();
			var q = new List<Vertex>();
			var s = new List<Vertex>();

			var inputLength = input.Length;
			for (int i = 0; i < inputLength * 5; i++)
			{
				for (int j = 0; j < inputLength * 5; j++)
				{
					var v = new Vertex(i, j);
					if (i == 0 && j == 0)
						v.Du = 0;

					var cost = (input[j % inputLength][i % inputLength] + i / input.Length + j / inputLength);
					cost = cost > 9 ? cost % 9 : cost;
					v.Cost = cost;
					q.Add(v);
				}
			}
			Dijkstra();

			return s.Single(x => x.Y == inputLength * 5 - 1 && x.X == inputLength * 5 - 1).Du;

			void Dijkstra()
			{
				while (q.Any())
				{
					if (q.Count % 1000 == 0)
					{
						Console.WriteLine(q.Count);
					}
					var min = q.Min(x => x.Du);
					var currVertex = q.First(x => x.Du == min);
					q.Remove(currVertex);
					s.Add(currVertex);

					var neighbours = q.Where(x => x.IsNeighbour(currVertex));
					foreach (var n in neighbours)
					{
						var dist = n.Cost;
						if (currVertex.Du + dist < n.Du)
						{
							n.Du = currVertex.Du + dist;
							n.PreviousVertex = currVertex;
						}
					}
				}
			}
		}

		private class Vertex
		{
			public Vertex(int x, int y)
			{
				this.X = x;
				this.Y = y;
			}

			public int X { get; set; }

			public int Y { get; set; }

			public int Cost { get; set; }

			public int Du { get; set; } = int.MaxValue;

			public Vertex? PreviousVertex { get; set; }

			public bool IsNeighbour(Vertex v2) => (Math.Abs(v2.X - this.X) == 0 && Math.Abs(v2.Y - this.Y) == 1) || (Math.Abs(v2.Y - this.Y) == 0 && Math.Abs(v2.X - this.X) == 1);
		}
    }
}
