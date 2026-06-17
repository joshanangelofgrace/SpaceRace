// USS MM Guide — terminal application.
//
// Composition root: registers the menu functions and their services in the
// dependency-injection container, then runs the interactive menu. All logic lives in
// the Domain, Application and Infrastructure layers.
//
// Add a new menu function by implementing IAppFunction and registering it below — the
// menu lists every registered IAppFunction automatically.
//
// Engine dial input:
//   SpaceRace <input.txt>   read dial sets from a specific text file
//   SpaceRace               auto-detect a .txt file in the working/app directory;
//                           otherwise fall back to the built-in example
//
// Input format: pairs of whitespace-separated number lines (current readings
// followed by expected readings). Blank lines are ignored.

using Microsoft.Extensions.DependencyInjection;
using SpaceRace.Application;
using SpaceRace.Infrastructure;

string? inputFile = args.Length > 0 ? args[0] : FindInputFile();

if (args.Length == 0 && inputFile is not null)
    Console.WriteLine($"Using input file: {inputFile}\n");

var services = new ServiceCollection();

// --- Engine dial tick processing ---
if (inputFile is not null)
    services.AddSingleton<IDialSetReader>(_ => new FileDialSetReader(inputFile));
else
    services.AddSingleton<IDialSetReader, ConsoleDialSetReader>();

services.AddSingleton<IEngineAdjustmentService, EngineAdjustmentService>();
services.AddSingleton<EngineAdjustmentApp>();

// --- Menu functions (register additional IAppFunction implementations here) ---
services.AddSingleton<IAppFunction, TickProcessingFunction>();

services.AddSingleton<MenuApp>();

using ServiceProvider provider = services.BuildServiceProvider();

provider.GetRequiredService<MenuApp>().Run();
return 0;

// Looks for a single .txt input file without requiring a command-line argument.
// Searches the current working directory first, then the directory the executable
// lives in. A file whose name contains "input" wins; otherwise, if exactly one
// .txt file is present it is used. Returns null when the choice is absent or
// ambiguous, leaving the app to fall back to stdin / the built-in example.
static string? FindInputFile()
{
    foreach (string directory in EnumerateSearchDirectories())
    {
        string[] candidates;
        try
        {
            candidates = Directory.GetFiles(directory, "*.txt");
        }
        catch (DirectoryNotFoundException)
        {
            continue;
        }

        if (candidates.Length == 0)
            continue;

        string? preferred = candidates.FirstOrDefault(path =>
            Path.GetFileNameWithoutExtension(path)
                .Contains("input", StringComparison.OrdinalIgnoreCase));

        if (preferred is not null)
            return preferred;

        if (candidates.Length == 1)
            return candidates[0];
    }

    return null;
}

static IEnumerable<string> EnumerateSearchDirectories()
{
    string current = Directory.GetCurrentDirectory();
    yield return current;

    string baseDir = AppContext.BaseDirectory;
    if (!string.Equals(
            Path.GetFullPath(baseDir).TrimEnd(Path.DirectorySeparatorChar),
            Path.GetFullPath(current).TrimEnd(Path.DirectorySeparatorChar),
            StringComparison.OrdinalIgnoreCase))
    {
        yield return baseDir;
    }
}
