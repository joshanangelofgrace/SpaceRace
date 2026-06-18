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
using SpaceRace.Features.EngineDialTicks.Functions;
using SpaceRace.Features.EngineDialTicks.Services;
using SpaceRace.Features.GateSequence.Functions;
using SpaceRace.Features.GateSequence.Services;
using SpaceRace.Features.MapRegions.Functions;
using SpaceRace.Features.MapRegions.Services;
using SpaceRace.Features.StonePiles.Functions;
using SpaceRace.Features.StonePiles.Services;
using SpaceRace.Features.SymbolDependencies.Functions;
using SpaceRace.Features.SymbolDependencies.Services;
using SpaceRace.Infrastructure;

var services = new ServiceCollection();

// --- Engine dial tick processing ---
services.AddSingleton<IDialSetReaderService, FileDialSetReaderService>();
services.AddSingleton<IEngineAdjustmentService, EngineAdjustmentService>();

// --- Gate sequence decoding ---
services.AddSingleton<ISequenceReaderService, SequenceReaderService>();
services.AddSingleton<ISequenceDecodeService, SequenceDecodeService>();

// --- Symbol dependency checking ---
services.AddSingleton<IDependencyReaderService, DependencyReaderService>();
services.AddSingleton<IDependencyCheckService, DependencyCheckService>();

// --- Stone pile splitting ---
services.AddSingleton<IStonePileReaderService, StonePileReaderService>();
services.AddSingleton<ISplitCalculatorService, SplitCalculatorService>();

// --- Map region analysis ---
services.AddSingleton<IMapGridReaderService, MapGridReaderService>();
services.AddSingleton<IRegionFinderService, RegionFinderService>();

// --- Menu functions (register additional IAppFunction implementations here) ---
services.AddSingleton<IAppFunction, TickProcessingFunction>();
services.AddSingleton<IAppFunction, SequenceDecodeFunction>();
services.AddSingleton<IAppFunction, DependencyCheckFunction>();
services.AddSingleton<IAppFunction, StonePileSplitFunction>();
services.AddSingleton<IAppFunction, LargestRegionFunction>();
services.AddSingleton<MenuApp>();

using ServiceProvider provider = services.BuildServiceProvider();

provider.GetRequiredService<MenuApp>().Run();
return 0;
