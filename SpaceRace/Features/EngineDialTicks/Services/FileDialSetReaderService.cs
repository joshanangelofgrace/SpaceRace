using SpaceRace.Features.EngineDialTicks.Models;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>
/// Reads dial sets from a text file. Each set is a pair of whitespace-separated
/// number lines (current readings followed by expected readings); blank lines are
/// ignored.
/// </summary>
public sealed class FileDialSetReaderService : IDialSetReaderService
{
    public IReadOnlyList<DialSet> Read()
    {
        string? path = FindInputFile();
        if (!File.Exists(path))
            throw new FileNotFoundException($"Input file not found: {path}", path);

        return DialSetParserService.Parse(File.ReadLines(path));
    }

    // Looks for a single .txt input file without requiring a command-line argument.
    // Searches the current working directory first, then the directory the executable
    // lives in. A file whose name contains "input" wins; otherwise, if exactly one
    // .txt file is present it is used. Returns null when the choice is absent or
    // ambiguous, leaving the app to fall back to stdin / the built-in example.
    private string? FindInputFile()
    {
        foreach (string directory in EnumerateSearchDirectories())
        {
            string[] candidates;
            try
            {
                candidates = Directory.GetFiles(directory, "*.txt");
            }
            catch (DirectoryNotFoundException)
            {
                continue;
            }

            if (candidates.Length == 0)
                continue;

            string? preferred = candidates.FirstOrDefault(path =>
                Path.GetFileNameWithoutExtension(path)
                    .Contains("input", StringComparison.OrdinalIgnoreCase));

            if (preferred is not null)
                return preferred;

            if (candidates.Length == 1)
                return candidates[0];
        }

        return null;
    }

    private IEnumerable<string> EnumerateSearchDirectories()
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
