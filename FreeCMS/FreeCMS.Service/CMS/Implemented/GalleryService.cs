using FreeCMS.Common.Service;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Service.CMS.Abstraction;

namespace FreeCMS.Service.CMS.Implemented
{
	public class GalleryService : BaseService<Gallery, int>, IGalleryService
	{
		public GalleryService(IGalleryRepository repository)
			: base(repository) { }

		public override Gallery Add(Gallery gallery)
		{
			gallery.CreationDate = DateTime.Now;
			return base.Add(gallery);
		}
		public List<Gallery> GetByType(GalleryType type)
		{
			return this.List(a => a.Type == type).ToList();
		}
		public Gallery FindByNameAndType(string name, GalleryType type)
		{
			return this.List(g => g.Name == name && g.Type == type).FirstOrDefault();
		}
		public bool Exists(string name, GalleryType type)
		{
			if (FindByNameAndType(name, type) != null)
				return true;
			return false;
		}
		public bool Exists(int id, string name, GalleryType type)
		{
			if (this.List(g => g.Id != id && g.Name == name && g.Type == type).FirstOrDefault() != null)
				return true;
			return false;
		}
	}
}
