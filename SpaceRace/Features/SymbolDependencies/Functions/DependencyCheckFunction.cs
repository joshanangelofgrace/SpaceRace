using SpaceRace.Features.SymbolDependencies.Models;
using SpaceRace.Features.SymbolDependencies.Services;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.SymbolDependencies.Functions;

/// <summary>
/// Menu function for the symbol-dependency panel: reads dependency lines, decides for
/// each whether all symbols can be pressed without violating a prerequisite (a feasible
/// topological order, i.e. no cycle), and reports the sum of the per-line results.
/// </summary>
public sealed class DependencyCheckFunction(IDependencyReaderService reader, IDependencyCheckService service) : IAppFunction
{
    private readonly IDependencyReaderService _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IDependencyCheckService _service = service ?? throw new ArgumentNullException(nameof(service));

    public string Title => "Check symbol dependencies (sum of feasible lines)";

    public void Run()
    {
        IReadOnlyList<IReadOnlyList<Dependency>> lines = _reader.Read();
        DependencyReport report = _service.Evaluate(lines);

        int lineNumber = 1;
        foreach (DependencyCheck check in report.Lines)
        {
            string pairs = string.Join(", ", check.Dependencies.Select(d => $"[{d.Symbol},{d.Prerequisite}]"));
            string verdict = check.IsFeasible ? "feasible" : "not feasible";
            Console.WriteLine($"Line {lineNumber++}: {pairs} -> {verdict} ({check.Result})");
        }

        Console.WriteLine();
        Console.WriteLine($"Final result (sum of feasible lines): {report.Sum}");
    }
}
