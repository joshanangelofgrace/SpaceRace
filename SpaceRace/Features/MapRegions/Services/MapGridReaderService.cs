using SpaceRace.Features.MapRegions.Models;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>
/// Reads the map from <c>mapregions.txt</c> (shipped with the solution and copied to the
/// output directory). Each non-blank line is one row of cell values, e.g. <c>1 1 0 0</c>.
/// </summary>
public sealed class MapGridReaderService : IMapGridReaderService
{
    private const string FileName = "mapregions.txt";

    public MapGrid Read()
    {
        string? path = FindInputFile() ?? throw new FileNotFoundException(
                $"Input file not found: {FileName}. Place it in the working directory or alongside the executable.",
                FileName);
        Console.WriteLine($"Reading map from: {path}\n");
        return MapParserService.Parse(File.ReadLines(path));
    }

    // Looks for mapregions.txt in the current working directory first, then the
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
