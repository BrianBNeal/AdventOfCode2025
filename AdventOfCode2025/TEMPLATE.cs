using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class TEMPLATE : Problem
{
    /// <summary>
    /// fresh ID ranges (inclusive), blank line, available IDs
    /// </summary>
    private readonly string input;
    internal TEMPLATE(bool IsTest = false)
    {
        Title = IsTest ? "Day TEMPLATE Test" : "Day TEMPLATE Actual";
        var rawPath = "./Inputs/DayTEMPLATE.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/DayTEMPLATE.txt";
        input = IsTest
            ? """
            
            """
            : File.ReadAllText(filePath);
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

