using SpaceRace.Domain;

namespace SpaceRace.Infrastructure;

/// <summary>
/// Parses raw text lines into dial sets. Each set is a pair of whitespace-separated
/// number lines: the current readings followed by the expected readings. Blank lines
/// are ignored.
/// </summary>
public static class DialSetParser
{
    public static IReadOnlyList<DialSet> Parse(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        List<string> nonBlank = [.. lines.Where(line => !string.IsNullOrWhiteSpace(line))];

        if (nonBlank.Count % 2 != 0)
            throw new FormatException(
                "Expected an even number of non-blank lines (a current/expected pair per dial set).");

        var sets = new List<DialSet>(nonBlank.Count / 2);
        for (int i = 0; i < nonBlank.Count; i += 2)
        {
            IReadOnlyList<Dial> current = ParseLine(nonBlank[i]);
            IReadOnlyList<Dial> expected = ParseLine(nonBlank[i + 1]);
            sets.Add(new DialSet(current, expected));
        }

        return sets;
    }

    private static IReadOnlyList<Dial> ParseLine(string line) =>
        [.. line.Select(token => new Dial(int.Parse(token.ToString())))];
}
