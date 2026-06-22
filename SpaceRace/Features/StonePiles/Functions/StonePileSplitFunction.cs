using SpaceRace.Features.StonePiles.Models;
using SpaceRace.Features.StonePiles.Services;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.StonePiles.Functions;

/// <summary>
/// Menu function for the stone-pile puzzle: reads each pile and its factor set, finds
/// the maximum number of splits achievable per pile, and concatenates the per-row
/// results into the final answer.
/// </summary>
public sealed class StonePileSplitFunction(IStonePileReaderService reader, ISplitCalculatorService service) : IAppFunction
{
    private readonly IStonePileReaderService _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly ISplitCalculatorService _service = service ?? throw new ArgumentNullException(nameof(service));

    public string Title => "Maximise stone-pile splits (concatenated results)";

    public void Run()
    {
        ReadResult<IReadOnlyList<StonePile>> input = _reader.Read();
        Console.WriteLine($"Reading stone piles from: {input.Source}\n");
        PileSplitReport report = _service.Evaluate(input.Data);

        int rowNumber = 1;
        foreach (PileSplit row in report.Rows)
        {
            Console.WriteLine(
                $"Row {rowNumber++}: {row.Pile.Size} [{string.Join(",", row.Pile.Set)}] -> " +
                $"{row.MaxSplits} splits");
        }

        Console.WriteLine();
        Console.WriteLine($"Final result (concatenated): {report.Result}");
    }
}
