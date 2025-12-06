using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayThree : Problem
{
    private readonly string input;
    private readonly string title;

    public DayThree(bool IsTest = false)
    {
        title = IsTest ? "Day 3 Test" : "Day 3 Actual";

        input = IsTest
            ? ""
            : File.ReadAllText("./Inputs/DayThree.txt");
    }

    public override string Title => title;

    public override string SolvePartOne() =>
        "NOT IMPLEMENTED";

    public override string SolvePartTwo() =>
        "NOT IMPLEMENTED";
}

public static class Day3Extensions
{

}
