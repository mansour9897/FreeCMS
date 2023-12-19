namespace FreeCMS.Areas.Cms.Utilities
{
	public static class MetaKeywordsExtensions
	{
		public static string ToMetaKeywords(this string viewKeywords)
		{
			return viewKeywords.Replace('+', ',');
		}
		public static string ToViewMetaKeywords(this string keywords)
		{
			return keywords.Replace(',', '+');
		}
	}
}
