using SpaceRace.Features.EngineDialTicks.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>
/// Reads dial sets from <c>dialticks.txt</c> (shipped with the solution and copied to the
/// output directory). Each set is a pair of whitespace-separated number lines (current
/// readings followed by expected readings); blank lines are ignored.
/// </summary>
public sealed class FileDialSetReaderService : IDialSetReaderService
{
    private const string FileName = "dialticks.txt";

    public IReadOnlyList<DialSet> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        Console.WriteLine($"Reading dial sets from: {path}\n");
        return DialSetParserService.Parse(File.ReadLines(path));
    }
}
