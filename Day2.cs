using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day2 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day2");
		int sum = 0;
		int count = 1;
		data.ForEach(delegate(string s) {
			bool allowed = true;
			s = Regex.Replace(s, "Game \\d+: ", "");
			string[] split = s.Split("; ");
			foreach (string s1 in split) {
				string[] innerSplit = s1.Split(", ");
				foreach (string s2 in innerSplit) {
					int a = int.Parse(s2.Split(' ')[0]);
					if (s2.Contains("blue")) {
						if (a > 14) {
							allowed = false;
							break;
						}
					} else if (s2.Contains("red")) {
						if (a > 12) {
							allowed = false;
							break;
						}
					} else if (s2.Contains("green")) {
						if (a > 13) {
							allowed = false;
							break;
						}
					}
				}
			}

			if (allowed) {
				sum += count;
			}

			count++;
		});

		return sum;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day2");
		int sum = 0;
		data.ForEach(delegate(string s) {
			s = Regex.Replace(s, "Game \\d+: ", "");
			string[] split = s.Split("; ");
			int green = 0;  int red = 0;  int blue = 0;
			foreach (string s1 in split) {
				string[] innerSplit = s1.Split(", ");
				foreach (string s2 in innerSplit) {
					int a = int.Parse(s2.Split(' ')[0]);
					if (s2.Contains("blue")) {
						if (a > blue) {
							blue = a;
						}
					} else if (s2.Contains("red")) {
						if (a > red) {
							red = a;
						}
					} else if (s2.Contains("green")) {
						if (a > green) {
							green = a;
						}
					}
				}
			}

			sum += (red * blue * green);
		});

		return sum;
	}
}