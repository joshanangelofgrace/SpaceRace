namespace SpaceRace.Infrastructure;

/// <summary>
/// Central place for the app's green "spacey" console styling. Colours degrade
/// gracefully when output is redirected (no console attached).
/// </summary>
public static class ConsoleTheme
{
    /// <summary>Main text colour.</summary>
    public const ConsoleColor Primary = ConsoleColor.Green;

    /// <summary>Brighter colour for titles and the active prompt.</summary>
    public const ConsoleColor Highlight = ConsoleColor.White;

    /// <summary>Dimmer colour for the starfield and secondary detail.</summary>
    public const ConsoleColor Dim = ConsoleColor.DarkGreen;

    public const ConsoleColor Background = ConsoleColor.Black;
    public const ConsoleColor Error = ConsoleColor.Red;

    /// <summary>Applies the base colours and clears the screen. Safe to call when no
    /// console is attached (e.g. output redirected) — it simply does nothing.</summary>
    public static void Apply()
    {
        try
        {
            Console.BackgroundColor = Background;
            Console.ForegroundColor = Primary;
            Console.Clear();
        }
        catch (IOException)
        {
            // No interactive console (redirected/piped) — skip styling.
        }
    }

    /// <summary>Writes text in the given colour, then restores the primary colour.</summary>
    public static void Write(string text, ConsoleColor color)
    {
        ConsoleColor previous = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(text);
        Console.ForegroundColor = previous;
    }

    /// <summary>Writes a line in the given colour, then restores the primary colour.</summary>
    public static void WriteLine(string text, ConsoleColor color)
    {
        ConsoleColor previous = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = previous;
    }

    /// <summary>Prints the spacey banner shown above the menu.</summary>
    public static void WriteBanner()
    {
        WriteLine(@"  .      *      .     +      .        *       .     .   *", Dim);
        WriteLine(@"      .    ___ ___ ___   __  __ __  __     .      *     . ", Highlight);
        WriteLine(@"   *     | | | / __/ __| |  \/  |  \/  |       .         .", Highlight);
        WriteLine(@" .       | |_| \__ \__ \ | |\/| | |\/| |   *        .   * ", Highlight);
        WriteLine(@"     *    \___/|___/___/ |_|  |_|_|  |_|      .    *      ", Highlight);
        WriteLine(@"   .        .     U S S   M M   G U I D E       .      .  ", Primary);
        WriteLine(@"      *   .      +      .    *      .       +       .   * ", Dim);
    }
}
