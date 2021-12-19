using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;
using static AoE2021.Day19;

namespace AoE2021
{
	public class Day19 : Day
    {
		public Day19() : base("day19")
        {
        }

        protected override object FirstTask()
		{
			var input = this._inputLoader.LoadStringBatches();
			var scans = input.Select(x => x.Split('\r').ToList());
			var scansDictionary = new Dictionary<string, List<Point>>();

			foreach (var scan in scans)
			{
				var points = scan.Skip(1);
				var list = new List<Coordinates>();
				var listOfPoints = new List<Point>();

				foreach (var point in points)
				{
					var split = point.Split(',');
					list.Add(new(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2])));
				}

				foreach (var point in list)
				{
					var distances = new List<Distance>();
					foreach (var point2 in list.Where(x => x != point))
					{
						distances.Add(point.GetDistance(point2));
					}

					listOfPoints.Add(new(point, distances));
				}

				scansDictionary.Add(scan[0], listOfPoints);
			}

			var s0 = scansDictionary.First(x => x.Key.Contains('0'));
			var s1 = scansDictionary.First(x => x.Key.Contains('1'));

			Point? mainScanP1 = null, newScanP1 = null, mainScanP2 = null, newScanP2 = null;
			foreach (var p1 in s1.Value)
			{
				foreach (var p0 in s0.Value)
				{
					var ins = p0.DistancesToOtherPoints.Intersect(p1.DistancesToOtherPoints, new DistanceEqualityComparer()).ToList();
					if (ins.Count() >= 11)
					{
						if (mainScanP1 == null)
						{
							mainScanP1 = p0;
							newScanP1 = p1;
						}
						else if (mainScanP2 == null)
						{
							mainScanP2 = p0;
							newScanP2 = p1;
						}
						Console.WriteLine($"scan 0: {p0.Coordinates}, scan 1: {p1.Coordinates}");
					}
				}
			}

			int x1Dif, y1Dif, z1Dif, x2Dif, y2Dif, z2Dif;
			if (mainScanP1 != null)
			{
				x1Dif = mainScanP1.Coordinates.X - mainScanP2.Coordinates.X;
				x2Dif = newScanP1.Coordinates.X - newScanP2.Coordinates.X;

				y1Dif = mainScanP1.Coordinates.Y - mainScanP2.Coordinates.Y;
				y2Dif = newScanP1.Coordinates.Y- newScanP2.Coordinates.Y;

				z1Dif = mainScanP1.Coordinates.Z - mainScanP2.Coordinates.Z;
				z2Dif = newScanP1.Coordinates.Z - newScanP2.Coordinates.Z;

				var map = new Dictionary<string, string>();
				string xMap = null, yMap = null, zMap = null;
				#region Create map
				if (Math.Abs(x1Dif) == Math.Abs(x2Dif))
				{
					if (x1Dif == x2Dif)
					{
						xMap = "x";
					}
					else
					{
						xMap = "-x";
					}
				}
				else if (Math.Abs(x1Dif) == Math.Abs(y2Dif))
				{
					if (x1Dif == y2Dif)
					{
						xMap = "y";
					}
					else
					{
						xMap = "-y";
					}
				}
				else if (Math.Abs(x1Dif) == Math.Abs(z2Dif))
				{
					if (x1Dif == z2Dif)
					{
						xMap = "z";
					}
					else
					{
						xMap = "-z";
					}
				}

				if (Math.Abs(y1Dif) == Math.Abs(x2Dif))
				{
					if (y1Dif == x2Dif)
					{
						yMap = "x";
					}
					else
					{
						yMap = "-x";
					}
				}
				else if (Math.Abs(y1Dif) == Math.Abs(y2Dif))
				{
					if (y1Dif == y2Dif)
					{
						yMap = "y";
					}
					else
					{
						yMap = "-y";
					}
				}
				else if (Math.Abs(y1Dif) == Math.Abs(z2Dif))
				{
					if (y1Dif == z2Dif)
					{
						yMap = "z";
					}
					else
					{
						yMap = "-z";
					}
				}

				if (Math.Abs(z1Dif) == Math.Abs(x2Dif))
				{
					if (z1Dif == x2Dif)
					{
						zMap = "x";
					}
					else
					{
						zMap = "-x";
					}
				}
				else if (Math.Abs(z1Dif) == Math.Abs(y2Dif))
				{
					if (z1Dif == y2Dif)
					{
						zMap = "y";
					}
					else
					{
						zMap = "-y";
					}
				}
				else if (Math.Abs(z1Dif) == Math.Abs(z2Dif))
				{
					if (z1Dif == z2Dif)
					{
						zMap = "z";
					}
					else
					{
						zMap = "-z";
					}
				}
				#endregion

				var newPointCoords = new Coordinates(newScanP1.Coordinates.GetByAxis(xMap), newScanP1.Coordinates.GetByAxis(yMap), newScanP1.Coordinates.GetByAxis(zMap));
				var mainPointCoords = mainScanP1.Coordinates;
				var xDiff = newPointCoords.X - mainPointCoords.X;
				var yDiff = newPointCoords.Y - mainPointCoords.Y;
				var zDiff = newPointCoords.Z - mainPointCoords.Z;
				var mainScanPoints = s0.Value.Select(x => x.Coordinates);
				var pointsToAdd = new List<Coordinates>();

				foreach (var point in s1.Value.Select(x => x.Coordinates))
				{
					var z = new Coordinates(point.GetByAxis(xMap) - xDiff, point.GetByAxis(yMap) - yDiff, point.GetByAxis(zMap) - zDiff);
					pointsToAdd.Add(z);
				}

				var all = mainScanPoints.Union(pointsToAdd);
			}


			return "";
		}

		protected override object SecondTask()
		{
			return "";
		}

		public record Point(Coordinates Coordinates, List<Distance> DistancesToOtherPoints);

		public record Coordinates(int X, int Y, int Z)
		{
			public Distance GetDistance(Coordinates secondPoint)
			{
				return new(this.X - secondPoint.X, this.Y - secondPoint.Y, this.Z - secondPoint.Z);
			}

			public int GetByAxis(string axis)
			{
				var val = axis[^1..] switch
				{
					"x" => this.X,
					"y" => this.Y,
					"z" => this.Z
				};

				return val * (axis.Contains('-') ? -1 : 1);
			}
		}

		public class Distance
		{
			public Distance(int first, int second, int third)
			{
				this.Distances = new List<int>() { Math.Abs(first), Math.Abs(second), Math.Abs(third) };
			}

			public List<int> Distances { get; set; }
		}
	}

	public class DistanceEqualityComparer : IEqualityComparer<Distance>
	{
		public bool Equals(Distance? x, Distance? y)
		{
			return x.Distances.Select(r => Math.Abs(r)).Intersect(y.Distances.Select(r => Math.Abs(r))).Count() == 3;
		}

		public int GetHashCode([DisallowNull] Distance obj)
		{
			return obj.Distances[0].GetHashCode() + obj.Distances[1].GetHashCode() + obj.Distances[2].GetHashCode();
		}
	}
}
