using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayNine : Problem {
	/// <summary>
	/// fresh ID ranges (inclusive), blank line, available IDs
	/// </summary>
	private readonly string input;
	internal DayNine(bool IsTest = false) {
		Title = IsTest ? "Day 9 Test" : "Day 9 Actual";
		var rawPath = "./Inputs/DayNine.txt";
		var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
		input = IsTest
			? """
            7,1
            11,1
            11,7
            9,7
            9,5
            2,5
            2,3
            7,3
            """
			: File.ReadAllText(filePath);
	}

	internal override string Title { get; init; }

	internal override string SolvePartOne() {
		var redTiles = input
			.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
			.Select(line => line.Split(',').Select(long.Parse).ToArray())
			.Select(x => (x: x[0], y: x[1]))
			.ToArray();
		
		return redTiles
			.SelectMany(
				(t1, i) => redTiles
					.Skip(i + 1)
					.Select(t2 => computeArea(t1, t2)))
			.DefaultIfEmpty(0L)
			.Max()
			.ToString();
	}

	internal override string SolvePartTwo() {
		//depth-first search?
		return "NOT IMPLEMENTED";
	}

	private long computeArea((long x, long y) t1, (long x, long y) t2) {
		var width = Math.Abs(t2.x - t1.x) + 1;
		var height = Math.Abs(t2.y - t1.y) + 1;
		return width * height;
	}
}