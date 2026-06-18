using SpaceRace.Features.StonePiles.Models;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>
/// Reads stone piles from <c>stonepiles.txt</c> (shipped with the solution and copied to
/// the output directory). Each non-blank line is a pile size and a bracketed factor set,
/// e.g. <c>12 [3,6,5]</c>.
/// </summary>
public sealed class StonePileReaderService : IStonePileReaderService
{
    private const string FileName = "stonepiles.txt";

    public IReadOnlyList<StonePile> Read()
    {
        string? path = FindInputFile() ?? throw new FileNotFoundException(
                $"Input file not found: {FileName}. Place it in the working directory or alongside the executable.",
                FileName);
        Console.WriteLine($"Reading stone piles from: {path}\n");
        return StonePileParserService.Parse(File.ReadLines(path));
    }

    // Looks for stonepiles.txt in the current working directory first, then the
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
