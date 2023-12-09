using System.Text.RegularExpressions;

namespace AdventOfCode2023; 

public class Day9 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day9");
		int sum = 0;
		foreach (string s in data) {
			List<List<int>> allNums = new();
			List<int> nums = s.Split(" ").Select(int.Parse).ToList();
			allNums.Add(nums);
			while (!CheckLastList(allNums)) {
				List<int> newNums = new();
				List<int> temp = allNums.Last();
				for (int i = 1; i < temp.Count; i++) {
					newNums.Add(temp[i] - temp[i-1]);
				}
				allNums.Add(newNums);
			}

			for (int i = allNums.Count - 2; i >= 0; i--) {
				allNums[i].Add(allNums[i].Last() + allNums[i+1].Last());
			}

			sum += allNums[0].Last();
		}
		
		return sum;
	}

	private static bool CheckLastList(List<List<int>> nums) {
		return nums.Last().Count(i => i == 0) == nums.Last().Count;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day9");
		int sum = 0;
		foreach (string s in data) {
			List<List<int>> allNums = new();
			List<int> nums = s.Split(" ").Select(int.Parse).ToList();
			allNums.Add(nums);
			while (!CheckLastList(allNums)) {
				List<int> newNums = new();
				List<int> temp = allNums.Last();
				for (int i = 1; i < temp.Count; i++) {
					newNums.Add(temp[i] - temp[i-1]);
				}
				allNums.Add(newNums);
			}

			for (int i = allNums.Count - 2; i >= 0; i--) {
				allNums[i].Insert(0, allNums[i][0] - allNums[i+1][0]);
			}

			sum += allNums[0][0];
		}
		
		return sum;
	}
}