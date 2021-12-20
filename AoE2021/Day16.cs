using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AoE2021.Utils;

namespace AoE2021
{
    public class Day16 : Day
    {
		private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
			{ '0', "0000" },
			{ '1', "0001" },
			{ '2', "0010" },
			{ '3', "0011" },
			{ '4', "0100" },
			{ '5', "0101" },
			{ '6', "0110" },
			{ '7', "0111" },
			{ '8', "1000" },
			{ '9', "1001" },
			{ 'a', "1010" },
			{ 'b', "1011" },
			{ 'c', "1100" },
			{ 'd', "1101" },
			{ 'e', "1110" },
			{ 'f', "1111" }
		};

		public Day16() : base("day16")
        {
        }

        protected override object FirstTask()
		{
			var versionScore = 0;
			var input = this._inputLoader.LoadEntireString();
			var binaryString = HexStringToBinary(input);

			ProcessPackage(binaryString);

			return versionScore;

			string ProcessPackage(string s)
			{
				var version = s[..3];
				s = s[3..];

				var type = s[..3];
				s = s[3..];
				versionScore += Convert.ToInt32(version, 2);
				if (type == "100")
				{
					while (true)
					{
						var part = s[..5];
						s = s[5..];
						if (part.StartsWith('0'))
							break;
					}

					return s;
				}
				else
				{
					var lengthBit = s[..1];
					s = s[1..];

					int totalLength;
					int numberOfSubPackets;
					if (lengthBit == "0")
					{
						totalLength = Convert.ToInt32(s[..15], 2);
						s = s[15..];

						while (totalLength > 0)
						{
							var returnedString = ProcessPackage(s);
							var diffLength = s.Length - returnedString.Length;
							s = s[diffLength..];
							totalLength -= diffLength;
						}
					}
					else
					{
						numberOfSubPackets = Convert.ToInt32(s[..11], 2);
						s = s[11..];

						for (int i = 0; i < numberOfSubPackets; i++)
						{
							var returnedString = ProcessPackage(s);
							var diffLength = s.Length - returnedString.Length;
							s = s[diffLength..];
						}
					}
					return s;
				}
			}
		}

		protected override object SecondTask()
		{
			long versionScore = 0;
			var input = this._inputLoader.LoadEntireString();
			var binaryString = HexStringToBinary(input);

			var x = ProcessPackage(binaryString);

			return x.value;

			(string resultString, long value) ProcessPackage(string s)
			{
				var results = new List<long>();
				var version = s[..3];
				s = s[3..];

				var type = s[..3];
				s = s[3..];
				versionScore += Convert.ToInt64(version, 2);
				if (type == "100")
				{
					var z = "";
					while (true)
					{
						var part = s[..5];
						s = s[5..];
						z += part[1..];
						if (part.StartsWith('0'))
							break;
					}
					results.Add(Convert.ToInt64(z, 2));
				}
				else
				{
					var lengthBit = s[..1];
					s = s[1..];

					long totalLength;
					long numberOfSubPackets;
					if (lengthBit == "0")
					{
						totalLength = Convert.ToInt64(s[..15], 2);
						s = s[15..];

						while (totalLength > 0)
						{
							var result = ProcessPackage(s);
							var diffLength = s.Length - result.resultString.Length;
							s = s[diffLength..];
							results.Add(result.value);
							totalLength -= diffLength;
						}
					}
					else
					{
						numberOfSubPackets = Convert.ToInt64(s[..11], 2);
						s = s[11..];

						for (int i = 0; i < numberOfSubPackets; i++)
						{
							var result = ProcessPackage(s);
							var diffLength = s.Length - result.resultString.Length;
							s = s[diffLength..];
							results.Add(result.value);
						}
					}
				}
				var res = type switch
				{
					"000" => (s, results.Sum()),
					"001" => (s, results.Aggregate((long)1, (next, curr) => next * curr)),
					"010" => (s, results.Min()),
					"011" => (s, results.Max()),
					"100" => (s, results.Single()),
					"101" => (s, results[0] > results[1] ? 1 : 0),
					"110" => (s, results[0] < results[1] ? 1 : 0),
					"111" => (s, results[0] == results[1] ? 1 : 0),
					_ => throw new Exception()
				};
				Console.WriteLine($"OP: {type} -- {string.Join(", ", results)}");
				return res;
			}
		}

		private string HexStringToBinary(string hex)
		{
			StringBuilder result = new StringBuilder();
			foreach (char c in hex)
			{
				result.Append(hexCharacterToBinary[char.ToLower(c)]);
			}
			return result.ToString();
		}
	}
}
