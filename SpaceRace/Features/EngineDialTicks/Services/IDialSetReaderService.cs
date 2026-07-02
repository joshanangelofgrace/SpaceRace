using SpaceRace.Features.EngineDialTicks.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>Supplies the dial sets to be processed, with a description of their source.</summary>
public interface IDialSetReaderService
{
    ReadResult<List<DialSet>> Read();
}
