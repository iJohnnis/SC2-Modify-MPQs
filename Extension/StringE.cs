using System;

/// <summary> Starting with i means that the method uses the EqualsCompareOptions. </summary>
public static class StringE {
	public static StringComparison EqualsCompareOptions = StringComparison.InvariantCultureIgnoreCase;
	/// <summary> Checks whether or not the string Equals any of the following string.
	/// The comparison options if defined by the EqualsCompareOptions variable. </summary>
	public static bool EqualsAny(this string self, params string[] strings) {
		foreach (string s in strings)
			if (self.Equals(s, EqualsCompareOptions))
				return true;
		return false;
	}
	public static bool iEquals(this string self, string second) {
		return self.Equals(second, EqualsCompareOptions);
	}

	#region Quote
	public static string Enquote(this string self) {
		if (!self.StartsWith("\"")) self = "\"" + self;
		if (!self.EndsWith("\"")) return self + "\"";
		return self;
	}
	public static string Dequote(this string self) {
		int startIndex = 0;
		int length = self.Length;
		while (self[startIndex] == '\"') startIndex++;
		while (self[length - 1] == '"') length--;
		return self.Substring(startIndex, length - startIndex);
	}
	#endregion
}

