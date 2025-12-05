namespace AdventOfCode2025.Common;

public record struct Rotation(char[] Raw) {
	public readonly TurnDirection Direction => Raw[0] == 'L' ? TurnDirection.Left : TurnDirection.Right;
	public readonly int Distance => int.Parse(Raw.AsSpan()[1..]);
}
