using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DaySix : Problem
{
    /// <summary>
    /// fresh ID ranges (inclusive), blank line, available IDs
    /// </summary>
    private readonly string input;
    internal DaySix(bool IsTest = false)
    {
        Title = IsTest ? "Day 6 Test" : "Day 6 Actual";

        input = IsTest
            ? """
            
            """
            : File.ReadAllText("./Inputs/DaySix.txt");
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne()
    {
        return "NOT IMPLEMENTED";
    }

    internal override string SolvePartTwo()
    {
        return "NOT IMPLEMENTED";
    }
}

