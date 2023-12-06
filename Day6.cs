using System.Text.RegularExpressions;

namespace AdventOfCode2023; 

public class Day6 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day6");
		int mul = 1;
		List<int> durations = new();
		durations.AddRange(data[0].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
		List<int> distances = new();
		distances.AddRange(data[1].Split(":")[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

		for (int i = 0; i < durations.Count; i++) {
			int count = 0;
			for (int j = 0; j <= durations[i]; j++) {
				int time = j * (durations[i] - j);
				if (time > distances[i]) {
					count++;
				}
			}
			mul *= count;

		}
		
		return mul;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day6");
		int count = 0;
		ulong duration = ulong.Parse(data[0].Replace(" ", "").Split(":")[1]);
		ulong distance = ulong.Parse(data[1].Replace(" ", "").Split(":")[1]);
		for (ulong i = 0; i < duration; i++) {
			ulong time = i * (duration - i);
			if (time > distance) {
				count++;
			}
		}
		return count;
	}
}