namespace SpaceRace.Infrastructure;

/// <summary>
/// Data loaded by a reader service together with a human-readable description of where it
/// came from (a file path, "standard input", and so on).
///
/// This keeps reader services free of console output: they return <em>what</em> was read
/// and <em>from where</em>, and the presentation layer decides how — or whether — to show
/// <see cref="Source"/> to the user.
/// </summary>
/// <typeparam name="T">The type of data that was read.</typeparam>
public sealed record ReadResult<T>(string Source, T Data);
