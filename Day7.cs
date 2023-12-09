using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class Day7 {
	public static int Part1() {
		List<string> data = GeneralFuncs.ReadFile("day7");
		int sum = 0;
		data.Sort(CompareHandP1);
		int count = 1;
		foreach (string s in data) {
			sum += count * int.Parse(s.Split(" ")[1]);
			count++;
		}

		return sum;
	}

	public static int Part2() {
		List<string> data = GeneralFuncs.ReadFile("day7");
		int sum = 0;
		data.Sort(CompareHandP2);
		int count = 1;
		foreach (string s in data) {
			sum += count * int.Parse(s.Split(" ")[1]);
			count++;
		}

		return sum;
	}

	private static int CompareHandP1(string h1, string h2) {
		string h1Cards = h1.Split(" ")[0];
		string h2Cards = h2.Split(" ")[0];
		if (h1Cards == h2Cards) {
			return 0;
		}

		HandTypes t1 = GetTypeP1(h1Cards.Split(" ")[0]);
		HandTypes t2 = GetTypeP1(h2Cards.Split(" ")[0]);
		if (t1 > t2) {
			return -1;
		} else if (t1 < t2) {
			return 1;
		} else {
			List<char> points = new() { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };
			for (int i = 0; i < h1.Length; i++) {
				int t = CompareCard(h1Cards[i], h2Cards[i], points);
				if (t != 0) {
					return t;
				}
			}
		}

		return 0;
	}

	private static HandTypes GetTypeP1(string hand) {
		Dictionary<char, int> cards = cardCounts(hand);

		int pair = 0;
		int triple = 0;
		foreach (KeyValuePair<char, int> keyValuePair in cards) {
			switch (keyValuePair.Value) {
				case 5:
					return HandTypes.Five;
				case 4:
					return HandTypes.Four;
				case 3: {
					triple++;
					if (pair == 1) {
						return HandTypes.Full;
					}

					break;
				}
				case 2: {
					pair++;
					if (triple == 1) {
						return HandTypes.Full;
					}

					break;
				}
			}
		}

		if (triple == 1) {
			return HandTypes.Three;
		}

		return pair switch {
			2 => HandTypes.TwoPair,
			1 => HandTypes.OnePair,
			_ => HandTypes.High
		};
	}

	private static int CompareHandP2(string h1, string h2) {
		string h1Cards = h1.Split(" ")[0];
		string h2Cards = h2.Split(" ")[0];
		if (h1Cards == h2Cards) {
			return 0;
		}

		HandTypes t1 = GetTypeP2(h1Cards);
		HandTypes t2 = GetTypeP2(h2Cards);
		if (t1 > t2) {
			return -1;
		} else if (t1 < t2) {
			return 1;
		} else {
			List<char> points = new() { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };
			for (int i = 0; i < h1Cards.Length; i++) {
				int t = CompareCard(h1Cards[i], h2Cards[i], points);
				if (t != 0) {
					return t;
				}
			}
		}

		return 0;
	}

	private static HandTypes GetTypeP2(string hand) {
		Dictionary<char, int> cards = cardCounts(hand);

		int pair = 0;
		int triple = 0;
		
		int jVal = 0;
		if (cards.TryGetValue('J', out int card)) {
			jVal = card;
		}
		
		foreach (KeyValuePair<char, int> keyValuePair in cards) {
			if (keyValuePair.Key == 'J') {
				if (keyValuePair.Value == 5) {
					return HandTypes.Five;
				}
				continue;
			}

			int compare = keyValuePair.Value + jVal;
			if (compare == 5) {
				return HandTypes.Five;
			} else if (compare == 4) {
				return HandTypes.Four;
			} else if (keyValuePair.Value == 3) {
				triple++;
				if (pair == 1) {
					return HandTypes.Full;
				}
			} else if (keyValuePair.Value == 2) {
				pair++;
				if (triple == 1) {
					return HandTypes.Full;
				}
			}
		}

		if (triple == 1) {
			return HandTypes.Three;
		}

		return pair switch {
			2 when jVal == 1 => HandTypes.Full,
			2 => HandTypes.TwoPair,
			1 when jVal == 1 => HandTypes.Three,
			1 => HandTypes.OnePair,
			_ => jVal switch {
				2 => HandTypes.Three,
				1 => HandTypes.OnePair,
				_ => HandTypes.High
			}
		};
	}

	private static Dictionary<char, int> cardCounts(string hand) {
		Dictionary<char, int> cards = new();
		foreach (char c in hand) {
			if (cards.ContainsKey(c)) {
				cards[c]++;
			} else {
				cards[c] = 1;
			}
		}

		return cards;
	}
	
	private static int CompareCard(char c1, char c2, List<char> points) {
		int p1 = points.IndexOf(c1);
		int p2 = points.IndexOf(c2);
		if (p1 == p2) {
			return 0;
		}

		if (p1 < p2) {
			return 1;
		}

		return -1;
	}
}

public enum HandTypes {
	Five,
	Four,
	Full,
	Three,
	TwoPair,
	OnePair,
	High
}