using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayFour : Problem
{
    private readonly List<Position> positions;

    internal DayFour(bool IsTest = false)
    {
        Title = IsTest ? "Day 4 Test" : "Day 4 Actual";
        var rawPath = "./Inputs/DayFour.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
        positions = IsTest
            ? [.. """
            ..@@.@@@@.
            @@@.@.@.@@
            @@@@@.@.@@
            @.@@@@..@.
            @@.@@@@.@@
            .@@@@@@@.@
            .@.@.@.@@@
            @.@@@.@@@@
            .@@@@@@@@.
            @.@.@@@.@.
            """.ToGrid().MapPositions()]
            : [.. File.ReadAllText(filePath).ToGrid().MapPositions()];
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne()
    {
        var map = positions.ToDictionary(p => p.Location, p => p.Contents);
        return positions
            .Count(pos => pos.IsPaper && pos.Location.IsAccessible(map))
            .ToString();
    }

    internal override string SolvePartTwo()
    {
        var map = positions.ToDictionary(p => p.Location, p => p.Contents);
        var count = 0;

        while (true)
        {
            var accessibleLocations = map
                .Where(kvp => kvp.Value == DayFourConstants.PAPER && kvp.Key.IsAccessible(map))
                .Select(kvp => kvp.Key)
                .ToList();

            if (accessibleLocations.Count == 0)
                break;

            foreach (var location in accessibleLocations)
            {
                map[location] = DayFourConstants.EMPTY;
            }

            count += accessibleLocations.Count;
        }

        return count.ToString();
    }
}

internal record Position(Point Location, char Contents);

internal static class DayFourConstants
{
    internal const char PAPER = '@';
    internal const char EMPTY = '.';
}

internal static class DayFourExtensions
{
    extension(Point location)
    {
        internal bool IsAccessible(Dictionary<Point, char> grid)
        {
            var paperCount = 0;
            for (var row = location.Row - 1; row <= location.Row + 1; row++)
            {
                for (var col = location.Col - 1; col <= location.Col + 1; col++)
                {
                    if (row == location.Row && col == location.Col)
                        continue;

                    var adjacentPoint = new Point(row, col);
                    if (grid.TryGetValue(adjacentPoint, out var contents) && contents == DayFourConstants.PAPER)
                    {
                        paperCount++;
                        if (paperCount >= 4)
                            return false;
                    }
                }
            }
            return true;
        }
    }

    extension(string str)
    {
        internal char[][] ToGrid() =>
            [.. str.Split(Environment.NewLine).Select(line => line.ToCharArray())];
    }

    extension(char[][] arr)
    {
        internal IEnumerable<Position> MapPositions() =>
            arr.SelectMany((row, rowIndex) =>
                row.Select((c, colIndex) => new Position(new(rowIndex, colIndex), c)));
    }

    extension(Position pos)
    {
        internal bool IsPaper =>
            pos.Contents is DayFourConstants.PAPER;
    }
}
