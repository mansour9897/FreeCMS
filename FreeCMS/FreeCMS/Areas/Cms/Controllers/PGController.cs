using AutoMapper;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class PGController : Controller
	{
		#region variables
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		private readonly IGalleryService _galleryService;
		private readonly IGalleryItemService _itemService;

		#endregion

		#region constructors
		public PGController(ILogger<PGController> logger, IMapper mapper, IGalleryService galleryService,
			IGalleryItemService itemService)
		{
			_logger = logger;
			_mapper = mapper;
			_galleryService = galleryService;
			_itemService = itemService;
		}
		#endregion

		#region actions
		public IActionResult Images(int? page)
		{
			var galleries = GetGalleries(GalleryType.Image);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(galleries.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult Videos(int? page)
		{
			var galleries = GetGalleries(GalleryType.Video);
			int pageSize = 10;
			int pageNumber = page ?? 1;
			return View(galleries.ToPagedList(pageNumber, pageSize));
		}
		public IActionResult View(int id)
		{
			Gallery gallery = _galleryService.FindById(id);
			if (gallery == null)
				return NotFound();
			return View(gallery);
		}
		#endregion

		#region methods
		private List<Gallery> GetGalleries(GalleryType? type)
		{
			List<Gallery> result = new List<Gallery>();
			if (type != null)
			{
				result = _galleryService.GetByType(type.Value);
			}
			else
			{
				result = _galleryService.GetAll();
			}
			return result.OrderByDescending(g => g.CreationDate).ToList();
		}
		#endregion
	}
}
