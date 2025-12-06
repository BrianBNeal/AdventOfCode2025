using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayFour : Problem
{
    private readonly List<Position> positions;
    private readonly int rowLength;

    public DayFour(bool IsTest = false)
    {
        Title = IsTest ? "Day 4 Test" : "Day 4 Actual";

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
            : [.. File.ReadAllText("./Inputs/DayFour.txt").ToGrid().MapPositions()];

        rowLength = positions.Count(p => p.Location.Row == 0);
    }

    public override string Title { get; init; }

    public override string SolvePartOne()
    {
        return positions
            .Count(pos => pos.IsPaper && pos.IsAccessible(positions))
            .ToString();
    }

    public override string SolvePartTwo()
    {
        var count = 0;

        var fullMap = positions
            .Select((p, index) => (index, p))
            .ToDictionary(t => t.index, t => t.p);
        var paperLocations = fullMap
            .Where(p => p.Value.IsPaper)
            .Select(kvp => (kvp.Key, kvp.Value))
            .ToList();

        while (true)
        {
            var accessiblePaper = paperLocations.Where(x => x.Value.IsAccessible(fullMap)).ToList();

            if (accessiblePaper.Count == 0)
                break;

            foreach ((int key, Position pos) item in accessiblePaper)
            {
                fullMap[item.key] = fullMap[item.key] with { Contents = DayFourConstants.EMPTY };
                paperLocations.Remove(item);
            }

            count += accessiblePaper.Count;
        }

        return count.ToString();
    }
}

internal record Point(int Row, int Col);
internal record Position(Point Location, char Contents);

internal static class DayFourConstants
{
    internal const char PAPER = '@';
    internal const char EMPTY = '.';
}

internal static class DayFourExtensions
{
    extension(IEnumerable<Position> positions)
    {
        internal IEnumerable<Position> ClearAccesiblePaper() =>
            positions.Where(pos => !(pos.IsPaper && pos.IsAccessible(positions)));
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

        internal bool IsAccessible(Dictionary<int, Position> map) =>
            pos.AdjacentPositions(map.Values.AsEnumerable()).Count(p => p.IsPaper) < 4;

        internal bool IsAccessible(IEnumerable<Position> grid) =>
            pos.AdjacentPositions(grid).Count(adj => adj.IsPaper) < 4;

        internal IEnumerable<Position> AdjacentPositions(IEnumerable<Position> grid) =>
            grid.Where(p =>
                (p.Location.Row == pos.Location.Row - 1 || p.Location.Row == pos.Location.Row || p.Location.Row == pos.Location.Row + 1)
                && (p.Location.Col == pos.Location.Col - 1 || p.Location.Col == pos.Location.Col || p.Location.Col == pos.Location.Col + 1)
                && p.Location != pos.Location);
    }
}
