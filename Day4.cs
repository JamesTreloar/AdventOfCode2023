using System.Text.RegularExpressions;

namespace AdventOfCode2023; 

public class Day4 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day4");
		int sum = 0;
		data.ForEach(delegate(string s) {
			s = Regex.Replace(s, "Card \\d+: ", "").Replace("  ", " ");
			int score = 0;
			string[] groups = s.Split('|');
			string[] winningNumbers = groups[0].Trim().Split(" ");
			string[] currentNumbers = groups[1].Trim().Split(" ");
			foreach (string currentNumber in currentNumbers) {
				if (winningNumbers.Contains(currentNumber)) {
					if (score == 0) {
						score = 1;
					} else {
						score *= 2;
					}
				}
			}

			sum += score;
		});
		return sum;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day4");
		Dictionary<int, int> copies = new();
		int current = 1;
		data.ForEach(delegate(string s) {
			s = Regex.Replace(s, "Card \\d+: ", "").Replace("  ", " ");
			string[] groups = s.Split('|');
			string[] winningNumbers = groups[0].Trim().Split(" ");
			string[] currentNumbers = groups[1].Trim().Split(" ");
			int score = currentNumbers.Count(currentNumber => winningNumbers.Contains(currentNumber));

			int currentCopies = 1;
			
			if (copies.ContainsKey(current)) {
				copies[current]++;
				currentCopies = copies[current];
			} else {
				copies.Add(current, 1);
			}

			for (int i = current + 1; i < current + score + 1; i++) {
				if (copies.ContainsKey(i)) {
					copies[i] += currentCopies;
				} else {
					copies.Add(i, currentCopies);
				}
			}
			
			current++;
		});
		return copies.Sum(keyValuePair => keyValuePair.Value);
	}
}