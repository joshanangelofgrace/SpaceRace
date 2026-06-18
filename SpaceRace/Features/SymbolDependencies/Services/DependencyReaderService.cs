using SpaceRace.Features.SymbolDependencies.Models;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>
/// Reads dependency lines from <c>symboldependencies.txt</c> (shipped with the solution
/// and copied to the output directory). Each non-blank line is one set of bracketed
/// dependency pairs, e.g. <c>[*,&lt;],[&lt;,*]</c>.
/// </summary>
public sealed class DependencyReaderService : IDependencyReaderService
{
    private const string FileName = "symboldependencies.txt";

    public IReadOnlyList<IReadOnlyList<Dependency>> Read()
    {
        string? path = FindInputFile();
        if (path is null)
            throw new FileNotFoundException(
                $"Input file not found: {FileName}. Place it in the working directory or alongside the executable.",
                FileName);

        Console.WriteLine($"Reading dependencies from: {path}\n");
        return DependencyParserService.Parse(File.ReadLines(path));
    }

    // Looks for symboldependencies.txt in the current working directory first, then the
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
