namespace AdventOfCode2023; 

public class GeneralFuncs {
	public static List<String> ReadFile(String name) {
		try {
			var res = new List<String>();
			using (var sr = new StreamReader($"data/{name}.txt")) { 
				while (sr.ReadLine() is { } line) {
					res.Add(line);
				}
				return res;
			}
		} catch (IOException e) {
			Console.WriteLine(e.Message);
			return new List<string>();
		}
	}
}