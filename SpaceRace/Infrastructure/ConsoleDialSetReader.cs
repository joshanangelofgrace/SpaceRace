using SpaceRace.Domain;

namespace SpaceRace.Infrastructure;

/// <summary>
/// Reads dial sets from standard input as pairs of whitespace-separated number lines
/// (current readings followed by expected readings). Blank lines are ignored. When no
/// input is redirected, the worked example from the mission brief is used instead.
/// </summary>
public sealed class ConsoleDialSetReader : IDialSetReader
{
    private static readonly string[] ExampleInput =
    {
        "4 5 2 8",
        "2 4 9 1",
        "3 1 6 1",
        "4 2 0 9",
        "5 9 1 0",
        "8 8 9 1",
    };

    public IReadOnlyList<DialSet> Read()
    {
        List<string> lines = ReadStdinLines();

        if (lines.Count == 0)
        {
            Console.WriteLine("No input provided — running the built-in example.\n");
            lines.AddRange(ExampleInput);
        }

        return DialSetParser.Parse(lines);
    }

    private static List<string> ReadStdinLines()
    {
        var lines = new List<string>();

        if (Console.IsInputRedirected)
        {
            string? line;
            while ((line = Console.ReadLine()) != null)
                lines.Add(line);
        }

        return lines;
    }
}
