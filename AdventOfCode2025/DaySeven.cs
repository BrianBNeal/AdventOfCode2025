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
            : File.ReadAllText("./Inputs/DaySeven.txt");
    }

    internal override string Title { get; init; }
    internal const char BEAM = '|';
    internal const char SPLITTER = '^';
    internal override string SolvePartOne()
    {
        //find S
        //extend
        //find splits in next line(only the ones with a beam above)
        //split(count)
        //extend (always a fully blank line after a split line)
        //find next splits, etc
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
        return "NOT IMPLEMENTED";
    }
}

