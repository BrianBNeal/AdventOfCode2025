using AdventOfCode2025.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode2025;

internal class DayTwo : Problem
{
    //private const string REGEX_PATTERN = @"(\d+)\1"; //matches to 100, but shouldn't
    private const string REGEX_PATTERN = @"(^\d+)\1+$"; //YAY! figured it out without AI using www.regex101.com
    private readonly string input;

    internal DayTwo(bool IsTest = false)
    {
        Title = IsTest ? "Day 2 Test" : "Day 2 Actual";
        var rawPath = "./Inputs/DayTwo.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/DayTEMPLATE.txt";
        input = IsTest
            ? "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
            : File.ReadAllText(filePath);
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne() =>
        input.Split(",")
            .Aggregate(0L, (acc, rangeString) =>
                acc += rangeString
                    .AsRangeOfIds()
                    .Where(id => id.HasEqualHalves())
                    .Sum())
            .ToString();

    internal override string SolvePartTwo() =>
        input.Split(",")
            .Aggregate(0L, (acc, rangeString) =>
                acc += rangeString
                    .AsRangeOfIds()
                    .Where(id => Regex.IsMatch(id.ToString(), REGEX_PATTERN))
                    .Sum())
            .ToString();
}

internal static class Day2Extensions
{
    // STRING EXTENSIONS
    extension(string str)
    {
        internal (string, string) SplitEvenly()
        {
            var halfLength = str.Length / 2;
            return (new string([.. str.Take(halfLength)]), new string([.. str.Skip(halfLength)]));
        }

        internal IEnumerable<long> AsRangeOfIds()
        {
            var bounds = str.Split('-');
            var start = long.Parse(bounds[0]);
            var end = long.Parse(bounds[1]);
            while (start <= end)
            {
                yield return start;
                start++;
            }
        }
    }

    // STRING[] EXTENSIONS
    extension(string[] arr)
    {
        internal bool AreEqual() => arr[0] == arr[1];
    }

    // Tuple (STRING, STRING) EXTENSIONS
    extension((string first, string second) tuple)
    {
        internal bool AreEqual() => tuple.first == tuple.second;
    }

    // LONG EXTENSIONS
    extension(long val)
    {
        internal bool HasEqualHalves()
        {
            var strVal = val.ToString();
            return strVal.Length % 2 == 0
                && strVal.SplitEvenly().AreEqual();
        }
    }

    // IENUMERABLE<LONG> EXTENSIONS
    extension(IEnumerable<long> vals)
    {
        internal IEnumerable<long> WithEqualHalves()
        {
            return vals.Where(val => val.HasEqualHalves());
        }
    }
}
