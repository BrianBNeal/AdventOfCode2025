using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayThree : Problem
{
    private readonly string input;

    public DayThree(bool IsTest = false)
    {
        Title = IsTest ? "Day 3 Test" : "Day 3 Actual";

        input = IsTest
            ? """
            987654321111111
            811111111111119
            234234234234278
            818181911112111
            """
            : File.ReadAllText("./Inputs/DayThree.txt");
    }

    public override string Title { get; init; }

    public override string SolvePartOne() =>
        input.Split(Environment.NewLine)
            .Select(line => new Bank([.. line.Select((c, idx) => new Battery(idx, c - '0'))]))
            .ToArray()
            .Select(bank => bank.FindLargestJoltageOfSize(2))
            .Sum()
            .ToString();

    public override string SolvePartTwo() =>
        input.Split(Environment.NewLine)
            .Select(line => new Bank([.. line.Select((c, idx) => new Battery(idx, c - '0'))]))
            .ToArray()
            .Select(bank => bank.FindLargestJoltageOfSize(12))
            .Sum()
            .ToString();

}
public record Battery(int Index, int Joltage);

public record Bank(Battery[] Batteries)
{
    public long FindLargestJoltageOfSize(int size)
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