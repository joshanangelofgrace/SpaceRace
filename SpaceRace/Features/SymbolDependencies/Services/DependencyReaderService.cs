using SpaceRace.Features.SymbolDependencies.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>
/// Reads dependency lines from <c>symboldependencies.txt</c> (shipped with the solution
/// and copied to the output directory). Each non-blank line is one set of bracketed
/// dependency pairs, e.g. <c>[*,&lt;],[&lt;,*]</c>.
/// </summary>
public sealed class DependencyReaderService : IDependencyReaderService
{
    private const string FileName = "symboldependencies.txt";

    public IReadOnlyList<IReadOnlyList<Dependency>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        Console.WriteLine($"Reading dependencies from: {path}\n");
        return DependencyParserService.Parse(File.ReadLines(path));
    }
}
