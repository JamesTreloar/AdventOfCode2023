using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day1 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day1");
		int sum = 0;
		data.ForEach(delegate(string s) {
			s = Regex.Replace(s, "[a-zA-Z]", "");
			string temp = "";
			temp += s[0];
			temp += s[^1];
			sum += int.Parse(temp);
		});
		return sum;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day1");
		int sum = 0;
		data.ForEach(delegate(string s) {
			var replacements = new Dictionary<string, string> {
				{ "oneight", "18" }, { "twone", "21" }, { "eightwo", "82" }, { "one", "1" }, { "two", "2" },
				{ "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" }, { "seven", "7" }, { "eight", "8" },
				{ "nine", "9" }
			};
			s = replacements.Aggregate(s,
				(current, replacement) => current.Replace(replacement.Key, replacement.Value));
			s = Regex.Replace(s, "[a-z_]", "");
			string temp = "";
			temp += s[0];
			temp += s[^1];
			sum += int.Parse(temp);
		});
		return sum;
	}
}