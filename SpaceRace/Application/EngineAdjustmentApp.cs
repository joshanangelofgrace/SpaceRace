using System.Numerics;
using SpaceRace.Domain;
using SpaceRace.Infrastructure;

namespace SpaceRace.Application;

/// <summary>
/// Orchestrates a run: reads the dial sets, computes each set's ticks and the final
/// product, and reports the outcome.
/// </summary>
public sealed class EngineAdjustmentApp
{
    private readonly IDialSetReader _reader;
    private readonly IEngineAdjustmentService _service;

    public EngineAdjustmentApp(IDialSetReader reader, IEngineAdjustmentService service)
    {
        _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public BigInteger Run()
    {
        IReadOnlyList<DialSet> sets = _reader.Read();

        int setNumber = 1;
        foreach (DialSet set in sets)
        {
            int ticks = _service.CalculateSetTicks(set);
            Console.WriteLine(
                $"Set {setNumber++}: [{string.Join(' ', set.Current)}] -> " +
                $"[{string.Join(' ', set.Expected)}] = {ticks} ticks");
        }

        BigInteger finalResult = _service.CalculateFinalResult(sets);

        Console.WriteLine();
        Console.WriteLine($"Final result: {finalResult}");
        return finalResult;
    }
}
