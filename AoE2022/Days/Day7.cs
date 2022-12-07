using AoE2022.Utils;

public class Day7 : StringListDay
{
    const int MaxDirSize = 100_000;
    protected override object FirstTask()
    {
        var rootDir = BuildFileTree();
        return rootDir.GetAllDirsWithMaxSize(MaxDirSize);
    }

    protected override object SecondTask()
    {
        const int EntireSpace = 70_000_000;
        const int MinEmptySpace = 30_000_000;
        var root = BuildFileTree();
        var rootSize = root.GetSize();

        var dir = root
            .GetAllDirs()
            .Select(dir => new { dir, emptySpace = EntireSpace - rootSize + dir.GetSize() })
            .Where(d => d.emptySpace > MinEmptySpace)
            .MinBy(dir => dir.emptySpace);
        
        return dir.dir.GetSize();
    }

    private Directory BuildFileTree() {
        var rootDir = new Directory() {
        };
        var currentDir = rootDir;
        foreach (var instr in this.Input) {
            _ = instr.Split(" ").ToList() switch {
                ["$", "cd", var b] => Cd(rootDir, ref currentDir, b),
                ["$", "ls"] => null,
                ["dir", var name] => Expand(currentDir, name),
                [var size, var name] => Expand(currentDir, name, int.Parse(size)),
            };
        }

        return rootDir;
    }

    private object Cd(Directory root, ref Directory current, string target) {
        if (target == "/")
            current = root;
        else if (target == "..")
            current = current.Parent;
        else
            current = (Directory)current.Objects[target];

        return null;
    }

    private object Expand(Directory current, string target) {
        current.Objects.TryAdd(target, new Directory() { Parent = current });

        return null;
    }

    private object Expand(Directory current, string target, int size) {
        current.Objects.TryAdd(target, new File() { Size = size });

        return null;
    }

    private abstract class FileSystemObject {
        public abstract int GetSize();
    }

    private class Directory : FileSystemObject
    {
        public Directory? Parent { get; set; }
        public Dictionary<string, FileSystemObject> Objects { get; set; } = new();
        public override int GetSize() => this.Objects.Sum(o => o.Value.GetSize());

        public long GetAllDirsWithMaxSize(int maxSize) {
            var size = this.GetSize() <= maxSize ? this.GetSize() : 0;
            return size + this.Objects.Select(kv => kv.Value).OfType<Directory>().Select(d => d.GetAllDirsWithMaxSize(maxSize)).Sum();
        }

        public List<Directory> GetAllDirs() {
            return this.Objects.Select(kv => kv.Value).OfType<Directory>().SelectMany(dir => dir.GetAllDirs()).Append(this).ToList();
        }
    }

    private class File : FileSystemObject {
        public int Size { get; set; }
        public override int GetSize() => this.Size;
    }
}