using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day8 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day8");
		string path = data[0];
		data.RemoveAt(0);
		data.RemoveAt(0);
		int sum = 0;
		Dictionary<string, Tuple<string, string>> directions = new();
		data.ForEach(delegate(string s) {
			string[] split = s.Split(" = ");
			string[] dests = split[1].Replace("(", "").Replace(")", "").Split(", ");
			directions[split[0].Trim()] = new(dests[0], dests[1]);
		});

		string current = "AAA";
		int pathPos = 0;
		while (current != "ZZZ") {
			if (path[pathPos] == 'L') {
				current = directions[current].Item1;
			} else {
				current = directions[current].Item2;
			}

			pathPos++;
			sum++;

			if (pathPos >= path.Length) {
				pathPos = 0;
			}
		}

		return sum;
	}

	public static ulong Part2() {
		List<string> data = GeneralFuncs.ReadFile("day8");
		string path = data[0];
		data.RemoveAt(0);
		data.RemoveAt(0);
		ulong sum = 0;
		Dictionary<string, Tuple<string, string>> directions = new();
		List<string> currents = new();
		data.ForEach(delegate(string s) {
			string[] split = s.Split(" = ");
			string[] dests = split[1].Replace("(", "").Replace(")", "").Split(", ");
			if (split[0][2] == 'A') {
				currents.Add(split[0]);
			}

			directions[split[0].Trim()] = new(dests[0], dests[1]);
		});

		List<ulong> dists = new();
		for (int i = 0; i < currents.Count; i++) {
			int pathPos = 0;
			sum = 0;
			string current = currents[i];
			while (current[2] != 'Z') {
				if (path[pathPos] == 'L') {
					current = directions[current].Item1;
				} else {
					current = directions[current].Item2;
				}

				pathPos++;
				sum++;

				if (pathPos >= path.Length) {
					pathPos = 0;
				}
			}

			dists.Add(sum);
		}

		sum = dists[0];
		for (int i = 1; i < dists.Count; i++) {
			sum = LCM(sum, dists[i]);
		}

		return sum;
	}

	static ulong GCD(ulong a, ulong b) {
		while (b != 0) {
			ulong temp = b;
			b = a % b;
			a = temp;
		}

		return a;
	}

	static ulong LCM(ulong a, ulong b) {
		return (a / GCD(a, b)) * b;
	}
}