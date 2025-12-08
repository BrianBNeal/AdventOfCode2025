using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayEight : Problem
{
    private readonly string input;
    internal DayEight(bool IsTest = false)
    {
        Title = IsTest ? "Day 8 Test" : "Day 8 Actual";
        var rawPath = "./Inputs/DayEight.txt";
        var filePath = File.Exists(rawPath) ? rawPath : "./Inputs/TEMPLATE.txt";
        input = IsTest
            ? """
            162,817,812
            57,618,57
            906,360,560
            592,479,940
            352,342,300
            466,668,158
            542,29,236
            431,825,988
            739,650,466
            52,470,668
            216,146,977
            819,987,18
            117,168,530
            805,96,715
            346,949,466
            970,615,88
            941,993,340
            862,61,35
            984,92,344
            425,690,689
            """
            : File.ReadAllText(filePath);
    }

    internal override string Title { get; init; }

    internal override string SolvePartOne()
    {


        return "NOT IMPLEMENTED";
    }

    internal override string SolvePartTwo()
    {
        return "NOT IMPLEMENTED";
    }
}

internal record struct Point3D(int X, int Y, int Z);

internal static class DayEightExtensions
{
    extension(IEnumerable<Point3D> boxes)
    {
        internal (Point3D, Point3D) GetClosestPair()
        {
            Point3D closestA = new(0, 0, 0);
            Point3D closestB = new(0, 0, 0);
            var closestDistance = double.MaxValue;
            var junctionBoxes = boxes.ToList();
            for (int i = 0; i < junctionBoxes.Count; i++)
            {
                for (int j = i + 1; j < junctionBoxes.Count; j++)
                {
                    var distance = junctionBoxes[i].DistanceTo(junctionBoxes[j]);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestA = junctionBoxes[i];
                        closestB = junctionBoxes[j];
                    }
                }
            }
            return (closestA, closestB);
        }
    }

    extension(Point3D point)
    {
        internal double DistanceTo(Point3D other)
        {
            var x = point.X - other.X;
            var y = point.Y - other.Y;
            var z = point.Z - other.Z;
            return Math.Sqrt(x * x + y * y + z * z);
        }
    }
}