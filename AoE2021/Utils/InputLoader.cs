using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoE2021.Utils
{
    public class InputLoader
    {
        public static readonly string App = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        public List<string> LoadStringListInput(string filepath)
        {
            return File.ReadAllLines(Path.Combine(App, filepath))
                .ToList();
        }

        internal List<long> LoadLongListInput(string inputPath)
        {
            List<long> input = new List<long>();
            foreach (var line in File.ReadAllLines(Path.Combine(App, inputPath)))
            {
                input.Add(Int64.Parse(line));
            }

            return input;
        }

        public List<int> LoadIntListInput(string filepath)
        {
            List<int> input = new List<int>();
            foreach (var line in File.ReadAllLines(Path.Combine(App, filepath)))
            {
                input.Add(Int32.Parse(line));
            }

            return input;
        }

        public List<string> LoadStringBatches(string filepath)
        {
            var batches = File.ReadAllText(Path.Combine(App, filepath))
                .Split(new string[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(batch => batch.Replace('\n', ' '))
                .ToList();
            return batches;
        }
    }
}
