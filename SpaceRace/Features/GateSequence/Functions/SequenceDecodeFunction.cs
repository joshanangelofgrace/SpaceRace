using SpaceRace.Features.GateSequence.Models;
using SpaceRace.Features.GateSequence.Services;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.GateSequence.Functions;

/// <summary>
/// Menu function for the gate puzzle: reads integer sequences, counts how many numbers
/// to the right of each element are strictly smaller, then reports the sum of the
/// rounded-up medians of those counts.
/// </summary>
public sealed class SequenceDecodeFunction(ISequenceReaderService reader, ISequenceDecodeService service) : IAppFunction
{
    private readonly ISequenceReaderService _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly ISequenceDecodeService _service = service ?? throw new ArgumentNullException(nameof(service));

    public string Title => "Decode gate sequence (sum of rounded-up medians)";

    public void Run()
    {
        IReadOnlyList<IReadOnlyList<int>> sequences = _reader.Read();
        SequenceDecodeReport report = _service.Decode(sequences);

        int lineNumber = 1;
        foreach (SequenceDecode line in report.Lines)
        {
            Console.WriteLine(
                $"Line {lineNumber++}: [{string.Join(", ", line.Source)}] -> " +
                $"counts [{string.Join(", ", line.Counts)}] -> rounded-up median {line.RoundedMedian}");
        }

        Console.WriteLine();
        Console.WriteLine($"Final result (sum of rounded-up medians): {report.Sum}");
    }
}
