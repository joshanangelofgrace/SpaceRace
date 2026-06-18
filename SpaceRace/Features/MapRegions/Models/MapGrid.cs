namespace SpaceRace.Features.MapRegions.Models;

/// <summary>The map: a grid of cells where 1 marks an occupied cell and 0 is empty.</summary>
public sealed record MapGrid(IReadOnlyList<IReadOnlyList<int>> Cells)
{
    public int RowCount => Cells.Count;
}

/// <summary>The sizes of every connected region found, plus the largest.</summary>
public sealed record RegionReport(IReadOnlyList<int> RegionSizes, int LargestRegionSize);
