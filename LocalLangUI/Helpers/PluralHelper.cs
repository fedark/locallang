namespace LocalLangUI.Helpers
{
	public static class PluralHelper
	{
		public static string GetCountedText(int count, string single, string plural2to4, string plural)
		{
			return (count % 100, count % 10) switch
			{
				(1, _) => single,
				( >= 2 and <= 4, _) => plural2to4,
				( > 4 and <= 20, _) => plural,
				(_, 1) => single,
				(_, >= 2 and <= 4) => plural2to4,
				_ => plural,
			};
		}
	}
}
