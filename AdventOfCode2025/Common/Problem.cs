namespace AdventOfCode2025.Common;

internal abstract class Problem
{
    public abstract string Title { get; }
    public abstract string SolvePartOne();
    public abstract string SolvePartTwo();

    public override string ToString()
    {
        return $"""

        ==== {Title} ====
          Part One: {SolvePartOne()}
          Part Two: {SolvePartTwo()}

        """;
    }
}
