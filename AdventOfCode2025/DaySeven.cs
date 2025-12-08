using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DaySeven : Problem
{
    /// <summary>
    /// fresh ID ranges (inclusive), blank line, available IDs
    /// </summary>
    private readonly string input;
    internal DaySeven(bool IsTest = false)
    {
        Title = IsTest ? "Day 7 Test" : "Day 7 Actual";
        var rawPath = "./Inputs/DaySeven.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
        input = IsTest
            ? """
            .......S.......
            ...............
            .......^.......
            ...............
            ......^.^......
            ...............
            .....^.^.^.....
            ...............
            ....^.^...^....
            ...............
            ...^.^...^.^...
            ...............
            ..^...^.....^..
            ...............
            .^.^.^.^.^...^.
            ...............
            """
            : File.ReadAllText(filePath);
    }

    internal override string Title { get; init; }
    internal const char BEAM = '|';
    internal const char SPLITTER = '^';
    internal override string SolvePartOne()
    {
        var lines = input.Split(Environment.NewLine);
        int splitCount = 0;
        List<int> currentBeamColumns = [lines[0].IndexOf('S')]; //first beam is always row 1 under S

        for (int row = 2/*start at index 2 because 1 is always the first beam*/; row < lines.Length; row++)
        {
            if (row % 2 == 0) //row with splitters
            {
                HashSet<int> newBeamColumns = [];
                foreach (var col in currentBeamColumns)
                {
                    if (lines[row][col] == SPLITTER)
                    {
                        newBeamColumns.Add(col - 1);
                        newBeamColumns.Add(col + 1);
                        splitCount++;
                    }
                    else
                    {
                        newBeamColumns.Add(col);//beam continues straight down
                    }
                }
                currentBeamColumns = [.. newBeamColumns];
            }
        }

        return splitCount.ToString();
    }

    internal override string SolvePartTwo()
    {
        //each space has a value of all the beams that enter it
        //a beam's value in a space is equal to the number of ways to get there
        //answer is the sum of bottom row values
        //another way to think of it is each splitter adds the incoming value to the left and right
        //probably should look up binary tree calculations and this would be easier lol

        var lines = input.Split(Environment.NewLine);
        var length = lines[0].Length;
        var grid = new long[lines.Length][];
        grid[0] = new long[length];
        grid[0][lines[0].IndexOf('S')] = 1;

        for (int row = 1; row < lines.Length; row++)
        {
            grid[row] = new long[length];
            for (int col = 0; col < length; col++)
            {
                var incomingStrength = grid[row - 1][col];
                if (incomingStrength == 0)
                { continue; }

                if (lines[row][col] == SPLITTER)
                {
                    grid[row][col - 1] += incomingStrength;
                    grid[row][col + 1] += incomingStrength;
                }
                else
                {
                    grid[row][col] += incomingStrength;
                }
            }
        }

        return grid[^1].Sum().ToString();
    }
}