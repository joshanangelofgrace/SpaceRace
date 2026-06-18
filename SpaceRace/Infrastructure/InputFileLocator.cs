namespace SpaceRace.Infrastructure;

/// <summary>
/// Locates an input file that ships with the solution. Searches the current working
/// directory first, then the directory the executable lives in.
/// </summary>
public static class InputFileLocator
{
    /// <summary>Returns the full path to <paramref name="fileName"/>, or null if not found.</summary>
    public static string? Find(string fileName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName);

        foreach (string directory in SearchDirectories())
        {
            string candidate = Path.Combine(directory, fileName);
            if (File.Exists(candidate))
                return candidate;
        }

        return null;
    }

    /// <summary>Returns the full path to <paramref name="fileName"/>, or throws
    /// <see cref="FileNotFoundException"/> with a helpful message when it is missing.</summary>
    public static string FindRequired(string fileName) =>
        Find(fileName) ?? throw new FileNotFoundException(
            $"Input file not found: {fileName}. Place it in the working directory or alongside the executable.",
            fileName);

    private static IEnumerable<string> SearchDirectories()
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
