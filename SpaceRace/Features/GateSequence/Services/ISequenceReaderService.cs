using SpaceRace.Infrastructure;

namespace SpaceRace.Features.GateSequence.Services;

/// <summary>Supplies the integer sequences to be decoded, with a description of their source.</summary>
public interface ISequenceReaderService
{
    ReadResult<IReadOnlyList<IReadOnlyList<int>>> Read();
}
