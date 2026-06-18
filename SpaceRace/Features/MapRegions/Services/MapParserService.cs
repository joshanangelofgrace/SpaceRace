using SpaceRace.Features.MapRegions.Models;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>
/// Parses raw text lines into a <see cref="MapGrid"/>. Each non-blank line is one row of
/// cell values (e.g. <c>1 1 0 0</c>); values may be separated by commas and/or whitespace.
/// </summary>
public static class MapParserService
{
    private static readonly char[] Separators = [',', ' ', '\t'];

    public static MapGrid Parse(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var rows = new List<IReadOnlyList<int>>();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            string[] tokens = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            var row = new int[tokens.Length];
            for (int i = 0; i < tokens.Length; i++)
            {
                if (!int.TryParse(tokens[i], out row[i]))
                    throw new FormatException($"'{tokens[i]}' is not a valid cell value.");
            }

            rows.Add(row);
        }

        if (rows.Count == 0)
            throw new FormatException("The map is empty.");

        return new MapGrid(rows);
    }
}
