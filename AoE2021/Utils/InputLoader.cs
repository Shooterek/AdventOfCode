using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoE2021.Utils
{
    public class InputLoader
    {
        public static readonly string App = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        private readonly string path;

        public InputLoader(string path)
        {
            this.path = Path.Combine(App, path);
        }

        public List<string> LoadStringListInput()
        {
            return File.ReadAllLines(this.path)
                .ToList();
        }

        internal List<long> LoadLongListInput()
        {
            List<long> input = new List<long>();
            foreach (var line in File.ReadAllLines(this.path))
            {
                input.Add(long.Parse(line));
            }

            return input;
        }

        public List<int> LoadIntListInput()
        {
            List<int> input = new List<int>();
            foreach (var line in File.ReadAllLines(this.path))
            {
                input.Add(int.Parse(line));
            }

            return input;
        }

        public List<int> LoadIntListFromOneLine(string separator = ",")
        {
            return File.ReadAllText(this.path).Split(separator).Select(int.Parse).ToList();
        }

        public List<string> LoadStringBatches()
        {
            var batches = File.ReadAllText(this.path)
                .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(batch => batch.Replace('\n', ' '))
                .ToList();
            return batches;
		}

		public string LoadEntireString()
		{
			return File.ReadAllText(this.path);
		}
	}
}
