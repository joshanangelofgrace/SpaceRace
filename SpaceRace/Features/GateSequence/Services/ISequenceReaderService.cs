namespace SpaceRace.Features.GateSequence.Services;

/// <summary>Supplies the integer sequences to be decoded.</summary>
public interface ISequenceReaderService
{
    IReadOnlyList<IReadOnlyList<int>> Read();
}
