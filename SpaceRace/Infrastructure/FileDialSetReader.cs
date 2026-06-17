using SpaceRace.Domain;

namespace SpaceRace.Infrastructure;

/// <summary>
/// Reads dial sets from a text file. Each set is a pair of whitespace-separated
/// number lines (current readings followed by expected readings); blank lines are
/// ignored.
/// </summary>
public sealed class FileDialSetReader : IDialSetReader
{
    private readonly string _path;

    public FileDialSetReader(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("A file path is required.", nameof(path));

        _path = path;
    }

    public IReadOnlyList<DialSet> Read()
    {
        if (!File.Exists(_path))
            throw new FileNotFoundException($"Input file not found: {_path}", _path);

        return DialSetParser.Parse(File.ReadLines(_path));
    }
}
