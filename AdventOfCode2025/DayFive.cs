using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayFive : Problem
{
    /// <summary>
    /// fresh ID ranges (inclusive), blank line, available IDs
    /// </summary>
    private readonly string input;
    internal DayFive(bool IsTest = false)
    {
        Title = IsTest ? "Day 5 Test" : "Day 5 Actual";

        input = IsTest
            ? """
            3-5
            10-14
            16-20
            12-18

            1
            5
            8
            11
            17
            32
            """
            : File.ReadAllText("./Inputs/DayFive.txt");
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne()
    {
        var split = input.Split(Environment.NewLine + Environment.NewLine);
        var ranges = split[0].Split(Environment.NewLine).Select(line => line.ToIdRange());
        return split[1].Split(Environment.NewLine)
            .Select(str => long.Parse(str))
            .Count(id => ranges.Any(r => id >= r.lower && id <= r.upper))
            .ToString();
    }

    internal override string SolvePartTwo()
    {
        return "NOT IMPLEMENTED";
    }
}

internal record IdRange(long lower, long upper);

internal static class DayFiveExtensions
{
    extension(string line)
    {
        internal IdRange ToIdRange()
        {
            var bounds = line.Split('-');
            return new IdRange(long.Parse(bounds[0]), long.Parse(bounds[1]));
        }
    }
}
