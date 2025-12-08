using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayThree : Problem
{
    private readonly string input;

    internal DayThree(bool IsTest = false)
    {
        Title = IsTest ? "Day 3 Test" : "Day 3 Actual";
        var rawPath = "./Inputs/DayThree.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
        input = IsTest
            ? """
            987654321111111
            811111111111119
            234234234234278
            818181911112111
            """
            : File.ReadAllText(filePath);
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne() =>
        input.Split(Environment.NewLine)
            .Select(line => new Bank([.. line.Select((c, idx) => new Battery(idx, c - '0'))]))
            .ToArray()
            .Select(bank => bank.FindLargestJoltageOfSize(2))
            .Sum()
            .ToString();

    internal override string SolvePartTwo() =>
        input.Split(Environment.NewLine)
            .Select(line => new Bank([.. line.Select((c, idx) => new Battery(idx, c - '0'))]))
            .ToArray()
            .Select(bank => bank.FindLargestJoltageOfSize(12))
            .Sum()
            .ToString();

}
internal record Battery(int Index, int Joltage);

internal record Bank(Battery[] Batteries)
{
    internal long FindLargestJoltageOfSize(int size)
    {
        int previousJoltageIndex = -1;
        string joltages = "";
        for (int i = 1; i <= size; i++)
        {
            var skipAmount = previousJoltageIndex + 1;
            var takeAmount = Batteries.Length - skipAmount - (size - i);
            var battery = Batteries
                .Skip(skipAmount)
                .Take(takeAmount)
                .MaxBy(b => b.Joltage);
            joltages += battery?.Joltage.ToString();
            previousJoltageIndex = battery?.Index ?? 0;
        }
        return long.Parse(joltages);
    }
};