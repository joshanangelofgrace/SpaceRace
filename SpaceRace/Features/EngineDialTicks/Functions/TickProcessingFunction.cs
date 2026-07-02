using SpaceRace.Features.EngineDialTicks.Models;
using SpaceRace.Features.EngineDialTicks.Services;
using SpaceRace.Infrastructure;
using System.Numerics;

namespace SpaceRace.Features.EngineDialTicks.Functions;

/// <summary>
/// Menu function that reads the dial sets and reports each set's ticks plus the final
/// product. Adapts <see cref="EngineAdjustmentApp"/> to the <see cref="IAppFunction"/>
/// menu contract.
/// </summary>
public sealed class TickProcessingFunction(IDialSetReaderService reader, IEngineAdjustmentService service) : IAppFunction
{
    private readonly IDialSetReaderService _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IEngineAdjustmentService _service = service ?? throw new ArgumentNullException(nameof(service));

    public string Title => "Process engine dial ticks";

    public void Run()
    {
        ReadResult<List<DialSet>> input = _reader.Read();
        Console.WriteLine($"Reading dial sets from: {input.Source}\n");
        IReadOnlyList<DialSet> sets = input.Data;

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
    }
}
