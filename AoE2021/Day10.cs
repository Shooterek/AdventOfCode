using AoE2021.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AoE2021
{
    public class Day10 : Day
    {
        public Day10() : base("day10")
        {
        }

        protected override object FirstTask()
		{
			var openingBrackets = new char[] { '(', '[', '{', '<' };
			var closingBrackets = new char[] { ')', ']', '}', '>' };
			var map = new Dictionary<char, (char character, int val)>()
			{
				[')'] = ('(', 3),
				[']'] = ('[', 57),
				['}'] = ('{', 1197),
				['>'] = ('<', 25137),
			};
			var errorSize = 0;

			var lines = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray());
			foreach (var line in lines)
			{
				var stack = new Stack<char>();
				foreach (var charValue in line)
				{
					if (openingBrackets.Contains(charValue))
						stack.Push(charValue);
					else if (closingBrackets.Contains(charValue))
					{
						var poppedChar = stack.Pop();
						if (poppedChar != map[charValue].character)
						{
							errorSize += map[charValue].val;
							break;
						}
					}
				}
			}


            return errorSize;
        }

        protected override object SecondTask()
		{
			var openingBrackets = new char[] { '(', '[', '{', '<' };
			var closingBrackets = new char[] { ')', ']', '}', '>' };
			var map = new Dictionary<char, (char character, int val)>()
			{
				[')'] = ('(', 1),
				[']'] = ('[', 2),
				['}'] = ('{', 3),
				['>'] = ('<', 4),
			};

			var lines = this._inputLoader.LoadStringListInput().Select(x => x.ToCharArray());
			var scores = new List<long>();
			foreach (var line in lines)
			{
				var stack = new Stack<char>();
				var wasCorrupted = false;
				foreach (var charValue in line)
				{
					if (openingBrackets.Contains(charValue))
						stack.Push(charValue);
					else if (closingBrackets.Contains(charValue))
					{
						var poppedChar = stack.Pop();
						if (poppedChar != map[charValue].character)
						{
							wasCorrupted = true;
							break;
						}
					}
				}

				if (!wasCorrupted && stack.Count > 0)
				{
					var array = stack.ToArray();
					long score = 0;
					foreach (var character in array)
					{
						score *= 5;
						score += map.First(x => x.Value.character == character).Value.val;
					}
					scores.Add(score);
				}
			}

			var x = scores.OrderByDescending(x => x);
			return x.ElementAt(scores.Count / 2);
		}
    }        
}
