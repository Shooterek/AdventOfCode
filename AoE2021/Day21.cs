using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using AoE2021.Utils;

namespace AoE2021
{
	public class Day21 : Day
	{
		public Day21() : base("day21")
		{
		}
		protected override object FirstTask()
		{
			var input = this._inputLoader.LoadEntireString();

			var matches = Regex.Matches(input, @"\d+");
			Console.WriteLine(string.Join(", ", matches.Select(x => x.Value)));
			return "";
		}

		protected override object SecondTask()
		{
			throw new NotImplementedException();
		}
	}
}
