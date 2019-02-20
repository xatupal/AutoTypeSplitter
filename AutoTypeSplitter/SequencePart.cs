using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoTypeSplitter
{
	public class SequencePart
	{
		public const string PlaceholderPrefix = "{SPLIT";
		public const string PlaceholderMessage = PlaceholderPrefix + ":";
		public const string Placeholder = PlaceholderPrefix + "}";

		public string Message { get; private set; }
		public string Sequence { get; private set; }

		private SequencePart(string sequence)
		{
			if (sequence.StartsWith(Placeholder))
			{
				Sequence = sequence.Remove(0, Placeholder.Length);
			}
			else if (sequence.StartsWith(PlaceholderMessage))
			{
				var placeholderEnd = sequence.IndexOf("}", StringComparison.Ordinal);
				var messageLength = placeholderEnd - Placeholder.Length;
				Message = sequence.Substring(PlaceholderMessage.Length, messageLength);
				Sequence = sequence.Substring(placeholderEnd + 1);
			}
			else
			{
				Sequence = sequence;
			}
		}

		public static SequencePart[] Create(string sequence)
		{
			var sequenceParts = Regex.Split(sequence, @"(?=\" + PlaceholderPrefix + ")");
			return sequenceParts.Select(x => new SequencePart(x)).ToArray();
		}
	}
}