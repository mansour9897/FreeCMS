using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Repository.CMS.Implemented;
using FreeCMS.Service.CMS.Abstraction;
using FreeCMS.Service.CMS.Implemented;

namespace FreeCMS.Extensions
{
	public static class FreeCmsServiceExtension
	{
		public static void ConfigFreeCmsServices(this IServiceCollection services)
		{
			services.AddScoped<IGalleryRepository, GalleryRepository>();
			services.AddScoped<IGalleryService, GalleryService>();
			services.AddScoped<IPageRepository, PageRepository>();
			services.AddScoped<IPageService, PageService>();
		}
	}
}
