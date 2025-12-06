using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayFour : Problem
{
    private const char PAPER = '@';
    private const char EMPTY = '.';
    private readonly string input;
    private readonly string title;

    public DayFour(bool IsTest = false)
    {
        title = IsTest ? "Day 4 Test" : "Day 4 Actual";

        input = IsTest
            ? """
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
            """
            : File.ReadAllText("./Inputs/DayFour.txt");
    }

    public override string Title => title;

    public override string SolvePartOne() =>
        "NOT IMPLEMENTED";

    public override string SolvePartTwo() =>
       "NOT IMPLEMENTED";

}
