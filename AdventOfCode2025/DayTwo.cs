using System.Text.RegularExpressions;
using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayTwo : Problem {
	private readonly string input;

	public DayTwo(bool IsTest = false) {
		input = IsTest
			? "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
			: File.ReadAllText("./Inputs/DayTwo.txt");
	}



	public override string Title => "Day 2";
	private const string pattern = @"(\w+)\1+";

	public override string SolvePartOne() {
		return input.Split(",")
			.Aggregate((long)0, (acc, rangeString) => {
				var bounds = rangeString.Split('-');
				var ids = CreateRange(long.Parse(bounds[0]), long.Parse(bounds[1]));
				acc += ids.Where(id => Regex.IsMatch(id.ToString(), pattern)).Select(x => { Console.WriteLine(x);return x; }).Sum();
				return acc;
			})
			.ToString();




		//create ranges and concat
		//distinct
		//find only invalid ones
		//sum
	}

	public override string SolvePartTwo() {
		return "ERROR";
	}

	private IEnumerable<long> CreateRange(long start, long end) {
		while (start <= end) {
			yield return start;
			start++;
		}
	}
}
