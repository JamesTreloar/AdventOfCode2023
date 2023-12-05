namespace AdventOfCode2023;

public class Day3 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day3");
		int sum = 0;
		int line = 0;
		data.ForEach(delegate(string s) {
			bool valid = false;
			int current = 0;
			for (int i = 0; i < s.Length; i++) {
				if (s[i] == '.' || IsSymbol(s[i])) {
					if (valid) {
						sum += current;
					}

					current = 0;
					valid = false;
				} else if (IsDigit(s[i])) {
					if (i != 0 && IsSymbol(data[line][i - 1])) {
						valid = true;
					} else if (i != s.Length - 1 && IsSymbol(data[line][i + 1])) {
						valid = true;
					}
					if (line > 0) {
						if (IsSymbol(data[line - 1][i])) {
							valid = true;
						} else if (i != 0 && IsSymbol(data[line - 1][i - 1])) {
							valid = true;
						} else if (i != s.Length - 1 && IsSymbol(data[line - 1][i + 1])) {
							valid = true;
						}
					}
					if (line < data.Count - 1) {
						if (IsSymbol(data[line + 1][i])) {
							valid = true;
						} else if (i != 0 && IsSymbol(data[line + 1][i - 1])) {
							valid = true;
						} else if (i != s.Length - 1 && IsSymbol(data[line + 1][i + 1])) {
							valid = true;
						}
					}

					current = current * 10 + (s[i] - 48);
				}
			}
			if (valid) {
				sum += current;
			}
			line++;
		});
		return sum;
	}

	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day3");
		int line = 0;
		Dictionary<int, List<int>> asteriskAdjacent = new();
		data.ForEach(delegate(string s) {
			bool valid = false;
			int current = 0;
			HashSet<Tuple<int, int>> asterisks = new();
			for (int i = 0; i < s.Length; i++) {
				if (s[i] == '.' || IsSymbol(s[i])) {
					if (valid) {
						asteriskAdjacent =  AddAsterisk(asteriskAdjacent, asterisks, current);
						asterisks = new();
					}

					current = 0;
					valid = false;
				} else if (IsDigit(s[i])) {
					if (i != 0 && data[line][i - 1] == '*') {
						valid = true;
						asterisks.Add(new(line, i - 1));
					} else if (i != s.Length - 1 && data[line][i + 1] == '*') {
						valid = true;
						asterisks.Add(new(line, i + 1));
					}
					if (line > 0) {
						if (data[line - 1][i] == '*') {
							valid = true;
							asterisks.Add(new(line - 1, i));
						} else if (i != 0 && data[line - 1][i - 1] == '*') {
							valid = true;
							asterisks.Add(new(line - 1, i - 1));
						} else if (i != s.Length - 1 && data[line - 1][i + 1] == '*') {
							valid = true;
							asterisks.Add(new(line - 1, i + 1));
						}
					}
					if (line < data.Count - 1) {
						if (data[line + 1][i] == '*') {
							valid = true;
							asterisks.Add(new(line + 1, i));
						} else if (i != 0 && data[line + 1][i - 1] == '*') {
							valid = true;
							asterisks.Add(new(line + 1, i - 1));
						} else if (i != s.Length - 1 && data[line + 1][i + 1] == '*') {
							valid = true;
							asterisks.Add(new(line + 1, i + 1));
						}
					}

					current = current * 10 + (s[i] - 48);
				}
			}
			if (valid) {
				asteriskAdjacent =  AddAsterisk(asteriskAdjacent, asterisks, current);
			}
			line++;
		});

		return asteriskAdjacent.Select(keyValuePair => keyValuePair.Value).Where(list => list.Count == 2).Sum(list => list[0] * list[1]);
	}

	private static bool IsDigit(char c) {
		return c is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9';
	}

	private static bool IsSymbol(char c) {
		return c != '.' && !IsDigit(c);
	}
	
	private static Dictionary<int, List<int>> AddAsterisk (Dictionary<int, List<int>> asteriskAdjacent, HashSet<Tuple<int, int>> asterisks, int current) {
		foreach (var asterisk in asterisks) {
			int key = asterisk.Item1 * 149 + asterisk.Item2 * 151;
			if (!asteriskAdjacent.ContainsKey(key)) {
				List<int> ints = new();
				asteriskAdjacent.Add(key, ints);
			}
			asteriskAdjacent[key].Add(current);
		}

		return asteriskAdjacent;
	}
}