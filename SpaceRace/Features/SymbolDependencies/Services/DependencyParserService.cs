using SpaceRace.Features.SymbolDependencies.Models;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>
/// Parses raw text lines into dependency lists. Each non-blank line holds one or more
/// bracketed pairs, e.g. <c>[*,&lt;],[&lt;,*]</c>. Within a pair the first character is the
/// symbol and the last character is its prerequisite, so single-character symbols that
/// happen to be commas or dots (e.g. <c>[.,,]</c>) are handled correctly.
/// </summary>
public static class DependencyParserService
{
    public static IReadOnlyList<IReadOnlyList<Dependency>> Parse(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var result = new List<IReadOnlyList<Dependency>>();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            result.Add(ParseLine(line));
        }

        return result;
    }

    private static IReadOnlyList<Dependency> ParseLine(string line)
    {
        var dependencies = new List<Dependency>();

        int i = 0;
        while (i < line.Length)
        {
            if (line[i] != '[')
            {
                i++;
                continue;
            }

            int close = line.IndexOf(']', i + 1);
            if (close < 0)
                throw new FormatException($"Unterminated '[' in line: {line}");

            // Content is "<symbol>,<prerequisite>" — take the first and last characters so
            // that comma/dot symbols are not mistaken for the separator.
            string content = line.Substring(i + 1, close - i - 1);
            if (content.Length < 3)
                throw new FormatException($"Each dependency needs two symbols: '[{content}]'.");

            dependencies.Add(new Dependency(Symbol: content[0], Prerequisite: content[^1]));
            i = close + 1;
        }

        if (dependencies.Count == 0)
            throw new FormatException($"No dependencies found in line: {line}");

        return dependencies;
    }
}
