using System;
using System.Collections.Generic;
using System.Text;
using static AdventOfCode2025.DayOne;

namespace AdventOfCode2025.Common;

internal static class InputOperations {
	public static Rotation[] ReadInput(string fileName) {
		return [.. File.ReadAllLines(fileName)
			.Select(line => line.Trim())
			.Select(line => new Rotation([.. line]))];
	}

	
}
