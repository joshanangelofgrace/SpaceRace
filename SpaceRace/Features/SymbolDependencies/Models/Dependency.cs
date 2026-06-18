namespace SpaceRace.Features.SymbolDependencies.Models;

/// <summary>One dependency <c>[Symbol, Prerequisite]</c>: <see cref="Prerequisite"/>
/// must be pressed before <see cref="Symbol"/>.</summary>
public sealed record Dependency(char Symbol, char Prerequisite);

/// <summary>One evaluated dependency line and whether a valid press order exists.</summary>
public sealed record DependencyCheck(IReadOnlyList<Dependency> Dependencies, bool IsFeasible)
{
    /// <summary>1 when a valid press order exists, otherwise 0.</summary>
    public int Result => IsFeasible ? 1 : 0;
}

/// <summary>The evaluated lines plus the sum of their results.</summary>
public sealed record DependencyReport(IReadOnlyList<DependencyCheck> Lines, long Sum);
