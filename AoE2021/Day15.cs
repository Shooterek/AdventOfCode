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
			return "";
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

			public int Du { get; set; } = int.MaxValue;

			public Vertex? PreviousVertex { get; set; }

			public bool IsNeighbour(Vertex v2) => (Math.Abs(v2.X - this.X) == 0 && Math.Abs(v2.Y - this.Y) == 1) || (Math.Abs(v2.Y - this.Y) == 0 && Math.Abs(v2.X - this.X) == 1);
		}
    }
}
