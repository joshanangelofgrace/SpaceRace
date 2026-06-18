namespace SpaceRace.Features.GateSequence.Services;

/// <summary>
/// Reads integer sequences from <c>gatesequence.txt</c> (shipped with the solution and
/// copied to the output directory). Each non-blank line is one sequence, e.g.
/// <c>[5, 2, 6, 1]</c> or <c>5 2 6 1</c>.
/// </summary>
public sealed class SequenceReaderService : ISequenceReaderService
{
    private const string FileName = "gatesequence.txt";

    public IReadOnlyList<IReadOnlyList<int>> Read()
    {
        string? path = FindInputFile();
        if (path is null)
            throw new FileNotFoundException(
                $"Input file not found: {FileName}. Place it in the working directory or alongside the executable.",
                FileName);

        Console.WriteLine($"Reading sequences from: {path}\n");
        return SequenceParserService.Parse(File.ReadLines(path));
    }

    // Looks for gatesequence.txt in the current working directory first, then the
    // directory the executable lives in. Returns null when it cannot be found.
    private static string? FindInputFile()
    {
        foreach (string directory in EnumerateSearchDirectories())
        {
            string candidate = Path.Combine(directory, FileName);
            if (File.Exists(candidate))
                return candidate;
        }

        return null;
    }

    private static IEnumerable<string> EnumerateSearchDirectories()
    {
        string current = Directory.GetCurrentDirectory();
        yield return current;

        string baseDir = AppContext.BaseDirectory;
        if (!string.Equals(
                Path.GetFullPath(baseDir).TrimEnd(Path.DirectorySeparatorChar),
                Path.GetFullPath(current).TrimEnd(Path.DirectorySeparatorChar),
                StringComparison.OrdinalIgnoreCase))
        {
            yield return baseDir;
        }
    }
}
