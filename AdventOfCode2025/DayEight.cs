using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayEight : Problem
{
    private readonly string input;
    internal DayEight(bool IsTest = false)
    {
        Title = IsTest ? "Day 8 Test" : "Day 8 Actual";
        var rawPath = "./Inputs/DayEight.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
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