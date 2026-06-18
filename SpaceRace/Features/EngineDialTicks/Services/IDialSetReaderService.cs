using SpaceRace.Features.EngineDialTicks.Models;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>Supplies the dial sets to be processed.</summary>
public interface IDialSetReaderService
{
    IReadOnlyList<DialSet> Read();
}
