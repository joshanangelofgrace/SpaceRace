using SpaceRace.Features.StonePiles.Models;
using System.Numerics;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>
/// Parses raw text lines into stone piles. Each non-blank line is a pile size followed
/// by a bracketed set of distinct factors, e.g. <c>12 [3,6,5]</c>. Set members may be
/// separated by commas and/or whitespace.
/// </summary>
public static class StonePileParserService
{
    private static readonly char[] SetSeparators = [',', ' ', '\t'];

    public static IReadOnlyList<StonePile> Parse(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var piles = new List<StonePile>();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            piles.Add(ParseLine(line));
        }

        return piles;
    }

    private static StonePile ParseLine(string line)
    {
        int open = line.IndexOf('[');
        int close = line.IndexOf(']', open + 1);
        if (open < 0 || close < 0)
            throw new FormatException($"Expected '<size> [factors]' but found: {line}");

        string sizePart = line[..open].Trim();
        if (!BigInteger.TryParse(sizePart, out BigInteger size))
            throw new FormatException($"'{sizePart}' is not a valid pile size.");

        string setPart = line.Substring(open + 1, close - open - 1);
        string[] tokens = setPart.Split(SetSeparators, StringSplitOptions.RemoveEmptyEntries);

        var set = new List<BigInteger>();
        foreach (string token in tokens)
        {
            if (!BigInteger.TryParse(token, out BigInteger value))
                throw new FormatException($"'{token}' is not a valid factor.");
            if (!set.Contains(value))
                set.Add(value);
        }

        if (set.Count == 0)
            throw new FormatException($"The factor set is empty in line: {line}");

        return new StonePile(size, set);
    }
}
