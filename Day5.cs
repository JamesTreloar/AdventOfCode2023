using System.Net;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day5 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day5");
		List<long> seeds = new();
		List<Tuple<long, long, long>> seedToSoil = new();
		List<Tuple<long, long, long>> soilToFertiliser = new();
		List<Tuple<long, long, long>> fertiliserToWater = new();
		List<Tuple<long, long, long>> waterToLight = new();
		List<Tuple<long, long, long>> lightToTemp = new();
		List<Tuple<long, long, long>> tempToHumidity = new();
		List<Tuple<long, long, long>> humidityToLoc = new();
		string last = "";
		data.ForEach(delegate(string s) {
			if (s.Length == 0) {
			} else if (s.StartsWith("seeds: ")) {
				List<string> temp = s.Replace("seeds: ", "").Split(" ").ToList();
				seeds.AddRange(temp.Select(long.Parse));
			} else if (!IsDigit(s[0])) {
				last = s;
			} else {
				List<long> temp = new();
				temp.AddRange(s.Split(" ").Select(long.Parse));
				switch (last) {
					case "seed-to-soil map:":
						seedToSoil.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "soil-to-fertilizer map:":
						soilToFertiliser.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "fertilizer-to-water map:":
						fertiliserToWater.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "water-to-light map:":
						waterToLight.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "light-to-temperature map:":
						lightToTemp.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "temperature-to-humidity map:":
						tempToHumidity.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "humidity-to-location map:":
						humidityToLoc.Add(new(temp[0], temp[1], temp[2]));
						break;
				}
			}
		});
		long lowest = long.MaxValue;
		foreach (long seed in seeds) {
			long soil = Convert(seed, seedToSoil);
			long fertiliser = Convert(soil, soilToFertiliser);
			long water = Convert(fertiliser, fertiliserToWater);
			long light = Convert(water, waterToLight);
			long temperature = Convert(light, lightToTemp);
			long humidity = Convert(temperature, tempToHumidity);
			long location = Convert(humidity, humidityToLoc);

			if (location < lowest) {
				lowest = location;
			}
		}

		return (int)lowest;
	}

	private static long Convert(long source, List<Tuple<long, long, long>> conversionRule) {
		foreach (var tuple in conversionRule) {
			if (source >= tuple.Item2 && source <= (tuple.Item2 + tuple.Item3 - 1)) {
				return tuple.Item1 + (source - tuple.Item2);
			}
		}

		return source;
	}
	
	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day5");
		List<long> seeds = new();
		List<Tuple<long, long, long>> seedToSoil = new();
		List<Tuple<long, long, long>> soilToFertiliser = new();
		List<Tuple<long, long, long>> fertiliserToWater = new();
		List<Tuple<long, long, long>> waterToLight = new();
		List<Tuple<long, long, long>> lightToTemp = new();
		List<Tuple<long, long, long>> tempToHumidity = new();
		List<Tuple<long, long, long>> humidityToLoc = new();
		string last = "";
		data.ForEach(delegate(string s) {
			if (s.Length == 0) {
			} else if (s.StartsWith("seeds: ")) {
				List<string> temp = s.Replace("seeds: ", "").Split(" ").ToList();
				seeds.AddRange(temp.Select(long.Parse));
			} else if (!IsDigit(s[0])) {
				last = s;
			} else {
				List<long> temp = new();
				temp.AddRange(s.Split(" ").Select(long.Parse));
				switch (last) {
					case "seed-to-soil map:":
						seedToSoil.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "soil-to-fertilizer map:":
						soilToFertiliser.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "fertilizer-to-water map:":
						fertiliserToWater.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "water-to-light map:":
						waterToLight.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "light-to-temperature map:":
						lightToTemp.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "temperature-to-humidity map:":
						tempToHumidity.Add(new(temp[0], temp[1], temp[2]));
						break;
					case "humidity-to-location map:":
						humidityToLoc.Add(new(temp[0], temp[1], temp[2]));
						break;
				}
			}
		});

		// long lowest = long.MaxValue;
		// for (int i = 0; i < seeds.Count; i += 2) {
		// 	long start = seeds[i];
		// 	long length = seeds[i + 1];
		// 	for (long j = start; j < start + length; j++) {
		// 		long seed = j;
		// 		long soil = Convert(seed, seedToSoil);
		// 		long fertiliser = Convert(soil, soilToFertiliser);
		// 		long water = Convert(fertiliser, fertiliserToWater);
		// 		long light = Convert(water, waterToLight);
		// 		long temperature = Convert(light, lightToTemp);
		// 		long humidity = Convert(temperature, tempToHumidity);
		// 		long location = Convert(humidity, humidityToLoc);
		//
		// 		if (location < lowest) {
		// 			lowest = location;
		// 		}
		// 	}
		// }

		long lowest = long.MaxValue;
		humidityToLoc.Sort((x, y) => x.Item1.CompareTo(y.Item1));
		foreach (var tuple in humidityToLoc) {
			if (tuple.Item1 > lowest) {
				break;
			}
			for (long location = tuple.Item1; location < tuple.Item1 + tuple.Item3; location++) {
				long humidity = DeConvert(location, humidityToLoc);
				long temperature = DeConvert(humidity, tempToHumidity);
				long light = DeConvert(temperature, lightToTemp);
				long water = DeConvert(light, waterToLight);
				long fertiliser = DeConvert(water, fertiliserToWater);
				long soil = DeConvert(fertiliser, soilToFertiliser);
				long seed = DeConvert(soil, seedToSoil);
				if (SeedPresent(seed, seeds)) {
					lowest = location;
					break;
				}
			}
		}

		for (long location = humidityToLoc[0].Item1 - 1; location >= 0; location--) {
			long humidity = DeConvert(location, humidityToLoc);
			long temperature = DeConvert(humidity, tempToHumidity);
			long light = DeConvert(temperature, lightToTemp);
			long water = DeConvert(light, waterToLight);
			long fertiliser = DeConvert(water, fertiliserToWater);
			long soil = DeConvert(fertiliser, soilToFertiliser);
			long seed = DeConvert(soil, seedToSoil);
			if (SeedPresent(seed, seeds)) {
				lowest = location;
			}
		}
		
		
		return (int)lowest;
	}

	private static long DeConvert(long dest, List<Tuple<long, long, long>> conversionRule) {
		foreach (var tuple in conversionRule) {
			if (tuple.Item1 > dest) {
				continue;
			}
			if (tuple.Item1 + tuple.Item3 < dest) {
				continue;
			} else {
				return dest - tuple.Item1 + tuple.Item2;
			}
		}

		return dest;
	}

	private static bool SeedPresent(long seed, IReadOnlyList<long> seeds) {
		for (int i = 0; i < seeds.Count; i += 2) {
			if (seed > seeds[i] && seed < seeds[i] + seeds[i + 1]) {
				return true;
			}
		}

		return false;
	} 

	private static bool IsDigit(char c) {
		return c is '0' or '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9';
	}
}