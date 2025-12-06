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
            123 328  51 64 
             45 64  387 23 
              6 98  215 314
            *   +   *   +  
            """
            : File.ReadAllText("./Inputs/DaySix.txt");
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne()
    {
        var splitLines = input.Split(Environment.NewLine)
            .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        var operandsPerProblem = splitLines.Count() - 1;
        var problemCount = splitLines.First().Length;
        var retVal = 0L;
        for (int i = 0; i < problemCount; i++)
        {
            var result = 0L;
            var myOperator = splitLines.Last().ElementAt(i);
            for (int j = 0; j < operandsPerProblem; j++)
            {
                var operand = long.Parse(splitLines.ElementAt(j).ElementAt(i));
                switch (myOperator)
                {
                    case "+":
                    case "*" when result is 0:
                        result += operand;
                        break;
                    case "*":
                        result *= operand;
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }
            retVal += result;
        }

        return retVal.ToString();
    }

    internal override string SolvePartTwo()
    {
        var lines = input.Split(Environment.NewLine);
        var retVal = 0L;
        var operands = new List<long>();
        var currentOperand = "";
        char myOperator = ' ';
        for (int i = lines.First().Length - 1; i >= 0; i--)
        {

            for (var j = 0; j < lines.Length; j++)
            {
                var currentChar = lines[j][i];
                if (currentChar is ' ')
                {
                    continue;
                }
                else if (char.IsDigit(currentChar))
                {
                    currentOperand += currentChar;
                }
                else
                {
                    myOperator = currentChar;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentOperand))
            {
                operands.Add(long.Parse(currentOperand));
                currentOperand = "";
            }
            else
            {
                operands.Clear();
            }

            if (myOperator != ' ')
            {

                retVal += myOperator switch
                {
                    '+' => operands.Sum(),
                    '*' => operands.Aggregate(1L, (acc, val) => acc * val),
                    _ => throw new InvalidOperationException(),
                };
                myOperator = ' ';
            }
        }

        return retVal.ToString();
    }
}

internal record MathProblem(IEnumerable<int> Operands, char Operator);