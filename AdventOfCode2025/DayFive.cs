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
        return input
            .Split(Environment.NewLine + Environment.NewLine)
            .First()
            .Split(Environment.NewLine)
            .Select(line => line.ToIdRange())
            .OrderBy(r => r.lower)
            .ThenBy(r => r.upper)
            .ToArray()
            .Merge()
            .Sum(range => range.upper - range.lower + 1) //range 3-5 = [3,4,5] so upper-lower+1 = count
            .ToString();
    }
}

internal record IdRange(long lower, long upper);

internal static class DayFiveExtensions
{
    extension(IdRange[] ranges)
    {
        internal IEnumerable<IdRange> Merge()
        {
            List<IdRange> retVal = new();
            for (int i = 0; i < ranges.Length; i++)
            {
                //always add last one, nothing to compare it to
                if (i == ranges.Length - 1)
                {
                    retVal.Add(ranges[i]);
                    break;
                }

                var (current, next) = (ranges[i], ranges[i + 1]);
                if (next.lower > current.upper)
                {
                    //no overlap, keep it
                    retVal.Add(current);
                }
                else
                {
                    //merge and set as next value in array for next compare
                    var merged = new IdRange(Math.Min(current.lower, next.lower), Math.Max(current.upper, next.upper));
                    ranges[i + 1] = merged;
                }
            }
            return retVal;
        }
    }

    extension(string line)
    {
        internal IdRange ToIdRange()
        {
            var bounds = line.Split('-');
            return new IdRange(long.Parse(bounds[0]), long.Parse(bounds[1]));
        }
    }
}
