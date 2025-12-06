using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayOne : Problem {
	private const int POSITION_MIN = 0;
	private const int POSITION_MAX = 99;
	private const int RANGE_SIZE = 100;
	private const int STARTING_POSITION = 50;

	private readonly Rotation[] moves;

	public DayOne(bool IsTest = false) {
		moves = IsTest
			? [.. """
			  L68
			  L30
			  R48
			  L5
			  R60
			  L55
			  L1
			  L99
			  R14
			  L82
			"""
				.Split(Environment.NewLine, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
				.Select(s => new Rotation([.. s]))]
			: [.. File.ReadAllLines("./Inputs/DayOne.txt").Select(s => new Rotation([.. s]))];
	}

	public override string Title => "Day 1";

	public override string SolvePartOne() {
		int position = STARTING_POSITION;
		return moves
			.Aggregate(0, (timesLandingOnZero, current) => {
				position = current.Direction switch {
					TurnDirection.Left => position - current.Distance,
					TurnDirection.Right => position + current.Distance,
					_ => throw new NotImplementedException(),
				};

				while (position < POSITION_MIN) {
					position += RANGE_SIZE;
				}

				while (position > POSITION_MAX) {
					position -= RANGE_SIZE;
				}

				if (position == 0) {
					timesLandingOnZero++;
				}

				return timesLandingOnZero;
			}).ToString();
	}

	public override string SolvePartTwo() {//6223
		int position = STARTING_POSITION;
		return moves.Aggregate(0, (timesPointingAtZero, current) => {
			int distanceFromZero = position == 0
				? RANGE_SIZE
				: current.Direction switch {
					TurnDirection.Left => position,
					TurnDirection.Right => RANGE_SIZE - position,
					_ => throw new NotImplementedException(),
				};

			//each full rotation will pass zero
			int fullRotations = current.Distance / RANGE_SIZE;
			timesPointingAtZero += fullRotations;

			// the remainder might cause it to pass zero again, find new position and see if it's out of bounds
			int remainder = current.Distance % RANGE_SIZE;
			var newPosition = current.Direction switch {
				TurnDirection.Left => position - remainder,
				TurnDirection.Right => position + remainder,
				_ => throw new NotImplementedException(),
			};

			if (newPosition < POSITION_MIN) {
				newPosition += RANGE_SIZE;
			} else if (newPosition > POSITION_MAX) {
				newPosition -= RANGE_SIZE;
			}

			if (remainder != 0 && remainder >= distanceFromZero) {
				timesPointingAtZero++;
			}
			
			position = newPosition;
			return timesPointingAtZero;
		}).ToString();
	}
}
