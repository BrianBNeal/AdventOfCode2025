using AdventOfCode2025.Common;

namespace AdventOfCode2025;

internal class DayEight : Problem {
	private readonly string input;
	private readonly int connectionThreshold;
	private readonly Dictionary<JunctionBox, JunctionBox> circuits = new();
	private readonly Dictionary<JunctionBox, int> ranks = new();

	internal DayEight(bool IsTest = false) {
		Title = IsTest ? "Day 8 Test" : "Day 8 Actual";
		connectionThreshold = IsTest ? 10 : 1000;

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

	internal override string SolvePartOne() {
		//find minimum spanning tree of junction boxes
		//KRUSKAL'S ALGORITHM!!! found this searching Udemy graph theory course

		/*
		 found this on geeksforgeeks, I suspect it will need tweaking
		 since I am not looping until everything is in one circuit
		 and because I am not using simple int arrays for edges

		 public static int KruskalsMST(int V, int[][] edges) {

			Array.Sort(edges, (e1, e2) => e1[2].CompareTo(e2[2]));

			// Traverse edges in sorted order
			DSU dsu = new DSU(V);
			int cost = 0, count = 0;

			foreach (var e in edges) {
				int x = e[0], y = e[1], w = e[2];

				// Make sure that there is no cycle
				if (dsu.Find(x) != dsu.Find(y)) {
					dsu.Union(x, y);
					cost += w;
					if (++count == V - 1) break;
				}
			}
			return cost;
		}

		 // Disjoint set union data structure
		class DSU {
			private int[] parent, rank;

			public DSU(int n) {
				parent = new int[n];
				rank = new int[n];
				for (int i = 0; i < n; i++) {
					parent[i] = i;
					rank[i] = 1;
				}
			}

			public int Find(int i) {
				if (parent[i] != i) {
					parent[i] = Find(parent[i]);
				}
				return parent[i];
			}

			public void Union(int x, int y) {
				int s1 = Find(x);
				int s2 = Find(y);
				if (s1 != s2) {
					if (rank[s1] < rank[s2]) {
						parent[s1] = s2;
					} else if (rank[s1] > rank[s2]) {
						parent[s2] = s1;
					} else {
						parent[s2] = s1;
						rank[s1]++;
					}
				}
			}
		}

		*/
		var boxes = input.ToJunctionBoxes();
		var edges = boxes
			.CalculateAllEdges()
			.OrderBy(e => e.Weight)
			.ToList();
		foreach (var box in boxes) {
			circuits[box] = box;
			ranks[box] = 1;
		}
		var connectionsMade = 0;

		foreach (var edge in edges) {
			connectionsMade++;
			Union(edge.A, edge.B);
			if (connectionsMade >= connectionThreshold) {
				break;
			}
		}

		return boxes
			.GroupBy(box => Find(box))
			.OrderByDescending(g => g.Count())
			.Take(3)
			.Aggregate(1, (acc, circuit) => acc *= circuit.Count())
			.ToString();
	}

	internal override string SolvePartTwo() {
		var boxes = input.ToJunctionBoxes();
		var edges = boxes
			.CalculateAllEdges()
			.OrderBy(e => e.Weight)
			.ToList();
		foreach (var box in boxes) {
			circuits[box] = box;
			ranks[box] = 1;
		}

		Connection finalConnection = default;
		foreach (var edge in edges) {
			Union(edge.A, edge.B);
			var rootCount = boxes.Select(box => Find(box)).Distinct().Count();
			
			if (rootCount == 1) {
				finalConnection = edge;
				break;
			}
		}
		
		return (finalConnection.A.X * finalConnection.B.X).ToString();
	}

	internal JunctionBox Find(JunctionBox box) {

		if (!circuits.TryGetValue(box, out var found)) {
			circuits[box] = box;
			ranks[box] = 1;
			return box;
		}

		if (found != box) {
			circuits[box] = Find(circuits[box]);
		}

		return circuits[box];
	}

	internal void Union(JunctionBox A, JunctionBox B) {
		var circuitA = Find(A);
		var circuitB = Find(B);
		if (circuitA != circuitB) {
			if (ranks[circuitA] < ranks[circuitB]) {
				circuits[circuitA] = circuitB;
			} else if (ranks[circuitA] > ranks[circuitB]) {
				circuits[circuitB] = circuitA;
			} else {
				circuits[circuitB] = circuitA;
				ranks[circuitA]++;
			}
		}
	}
}

// disjoint set union data structure
// is for cycle detection in kruskal's algorithm
// It does this by keeping track of connected components
// and merging them when a new edge is added
// If an edge connects two nodes that are already in the same component, it creates a cycle
// and is therefore not added to the MST
// Makes me wonder if our ClientTree lookups would benefit from this

internal record struct JunctionBox(double X, double Y, double Z);

internal record struct Connection(JunctionBox A, JunctionBox B, double Weight);

internal static class DayEightExtensions {

	extension(string str) {
		internal List<JunctionBox> ToJunctionBoxes() =>
			str.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
				.Select(line => {
					var parts = line.Split(',');
					return new JunctionBox(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
				}).ToList();
	}

	extension(List<JunctionBox> boxes) {
		internal IEnumerable<Connection> CalculateAllEdges() =>
			boxes.SelectMany((a, i) =>
				boxes.Skip(i + 1)
					.Select(b => new Connection(a, b, a.DistanceTo(b)))
			);
	}

	extension(JunctionBox point) {
		internal double DistanceTo(JunctionBox other) {
			var x = point.X - other.X;
			var y = point.Y - other.Y;
			var z = point.Z - other.Z;
			return Math.Sqrt(x * x + y * y + z * z);
		}
	}
}