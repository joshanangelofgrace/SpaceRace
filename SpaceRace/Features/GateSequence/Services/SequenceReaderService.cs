using SpaceRace.Infrastructure;

namespace SpaceRace.Features.GateSequence.Services;

/// <summary>
/// Reads integer sequences from <c>gatesequence.txt</c> (shipped with the solution and
/// copied to the output directory). Each non-blank line is one sequence, e.g.
/// <c>[5, 2, 6, 1]</c> or <c>5 2 6 1</c>.
/// </summary>
public sealed class SequenceReaderService : ISequenceReaderService
{
    private const string FileName = "gatesequence.txt";

    public ReadResult<IReadOnlyList<IReadOnlyList<int>>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<IReadOnlyList<IReadOnlyList<int>>>(path, SequenceParserService.Parse(File.ReadLines(path)));
    }
}
