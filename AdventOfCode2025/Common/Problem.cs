using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2025.Common;

internal abstract class Problem {
	public abstract string Title { get; }
	public abstract int SolvePartOne();
	public abstract int SolvePartTwo();
}
