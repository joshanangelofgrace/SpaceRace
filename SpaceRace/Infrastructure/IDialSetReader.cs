using SpaceRace.Domain;

namespace SpaceRace.Infrastructure;

/// <summary>Supplies the dial sets to be processed.</summary>
public interface IDialSetReader
{
    IReadOnlyList<DialSet> Read();
}
