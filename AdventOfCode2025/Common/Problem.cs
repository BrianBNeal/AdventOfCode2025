namespace AdventOfCode2025.Common;

internal abstract class Problem
{
    internal abstract string Title { get; init; }
    internal abstract string SolvePartOne();
    internal abstract string SolvePartTwo();

    public override string ToString()
    {
        return $"""

        ==== {Title} ====
          Part One: {SolvePartOne()}
          Part Two: {SolvePartTwo()}

        """;
    }
}
