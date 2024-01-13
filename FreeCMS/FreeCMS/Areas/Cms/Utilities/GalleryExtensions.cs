using FreeCMS.DomainModels.Cms;

namespace FreeCMS.Areas.Cms.Utilities
{
	public static class GalleryExtensions
	{
		public static string GalleryTypeToString(this Gallery gallery)
		{
			if (gallery.Type == GalleryType.Image)
				return "تصویر";
			return "ویدئو";
		}
	}
}
