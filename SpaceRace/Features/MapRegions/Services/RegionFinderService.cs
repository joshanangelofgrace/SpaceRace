using SpaceRace.Features.MapRegions.Models;

namespace SpaceRace.Features.MapRegions.Services;

/// <inheritdoc />
public sealed class RegionFinderService : IRegionFinderService
{
    // All eight neighbours: horizontal, vertical and diagonal.
    private static readonly (int RowDelta, int ColDelta)[] Directions =
    [
        (-1, -1), (-1, 0), (-1, 1),
        ( 0, -1),          ( 0, 1),
        ( 1, -1), ( 1, 0), ( 1, 1),
    ];

    public RegionReport FindRegions(MapGrid grid)
    {
        ArgumentNullException.ThrowIfNull(grid);

        IReadOnlyList<IReadOnlyList<int>> cells = grid.Cells;
        var visited = new bool[cells.Count][];
        for (int r = 0; r < cells.Count; r++)
            visited[r] = new bool[cells[r].Count];

        var sizes = new List<int>();
        for (int r = 0; r < cells.Count; r++)
        {
            for (int c = 0; c < cells[r].Count; c++)
            {
                if (cells[r][c] == 1 && !visited[r][c])
                    sizes.Add(Flood(cells, visited, r, c));
            }
        }

        int largest = sizes.Count == 0 ? 0 : sizes.Max();
        return new RegionReport(sizes, largest);
    }

    // Breadth-first flood fill from one occupied cell; returns the region's cell count.
    private static int Flood(IReadOnlyList<IReadOnlyList<int>> cells, bool[][] visited, int startRow, int startCol)
    {
        int size = 0;
        var queue = new Queue<(int Row, int Col)>();
        queue.Enqueue((startRow, startCol));
        visited[startRow][startCol] = true;

        while (queue.Count > 0)
        {
            (int row, int col) = queue.Dequeue();
            size++;

            foreach ((int dr, int dc) in Directions)
            {
                int nr = row + dr;
                int nc = col + dc;

                if (nr < 0 || nr >= cells.Count)
                    continue;
                if (nc < 0 || nc >= cells[nr].Count)
                    continue;
                if (visited[nr][nc] || cells[nr][nc] != 1)
                    continue;

                visited[nr][nc] = true;
                queue.Enqueue((nr, nc));
            }
        }

        return size;
    }
}
