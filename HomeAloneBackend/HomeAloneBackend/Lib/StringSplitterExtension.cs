using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeAloneBackend.Lib
{
	public static class StringSplitterExtension
    {
		public static IList<string> SplitKeepDelimiters(
			this string s,
			char delimiter,
			StringSplitOptions splitOptions)
		{
			var tokens = new List<string>();
			int idx = 0;
			for (int i = 0; i < s.Length; ++i)
			{
				if (delimiter == s[i])
				{
					tokens.Add(s[idx..i]);
					idx = i;
				}
			}

			tokens.Add(s.Substring(idx));

			if (splitOptions == StringSplitOptions.RemoveEmptyEntries)
			{
				tokens = tokens
					.Where(token => !string.IsNullOrWhiteSpace(token) && token != delimiter.ToString())
					.ToList();
			}

			return tokens;
		}
	}
}

