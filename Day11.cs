namespace AdventOfCode2023;

public class Day11 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day11");

		for (int i = 0; i < data.Count; i++) {
			if (data[i].Count(f => f == '.') == data[i].Length) {
				data.Insert(i, data[i]);
				i++;
			}
		}

		for (int i = 0; i < data[0].Length; i++) {
			int countDot = 0;
			int j = 0;
			for (; j < data.Count; j++) {
				if (data[j][i] == '.') {
					countDot++;
				}
			}

			if (j == countDot) {
				j = 0;
				for (; j < data.Count; j++) {
					data[j] = data[j].Insert(i, ".");
				}

				i++;
			}
		}

		List<Tuple<int, int>> galaxies = new();

		for (int i = 0; i < data.Count; i++) {
			for (int j = 0; j < data[i].Length; j++) {
				if (data[i][j] == '#') {
					galaxies.Add(new(i, j));
				}
			}
		}

		int count = 0;
		for (int i = 0; i < galaxies.Count; i++) {
			for (int j = i + 1; j < galaxies.Count; j++) {
				count += int.Abs(galaxies[i].Item1 - galaxies[j].Item1) +
				         int.Abs(galaxies[i].Item2 - galaxies[j].Item2);
			}
		}

		return count;
	}

	public static ulong Part2() {
		const int expansion = 1_000_000;
		List<string> data = GeneralFuncs.ReadFile("day11");
		List<int> colWeights = new();
		List<int> rowWeights = data.Select(t => t.Count(f => f == '.') == t.Length ? expansion : 1).ToList();

		for (int i = 0; i < data[0].Length; i++) {
			int countDot = 0;
			int j = 0;
			for (; j < data.Count; j++) {
				if (data[j][i] == '.') {
					countDot++;
				}
			}

			colWeights.Add(j == countDot ? expansion : 1);
		}

		List<Tuple<int, int>> galaxies = new();

		for (int i = 0; i < data.Count; i++) {
			for (int j = 0; j < data[i].Length; j++) {
				if (data[i][j] == '#') {
					galaxies.Add(new(i, j));
				}
			}
		}

		ulong count = 0;
		for (int i = 0; i < galaxies.Count; i++) {
			for (int j = i + 1; j < galaxies.Count; j++) {
				count +=  (ulong)(int.Abs(rowWeights.Slice(0, galaxies[j].Item1).Sum() - rowWeights.Slice(0, galaxies[i].Item1).Sum()) +
				                  int.Abs(colWeights.Slice(0, galaxies[j].Item2).Sum() - colWeights.Slice(0, galaxies[i].Item2).Sum()));
			}
		}

		return count;
	}
}