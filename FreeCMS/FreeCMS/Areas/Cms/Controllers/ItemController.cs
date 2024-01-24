using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels.Item;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
    [Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت آیتم ها", "وبلاگ")]
    public class ItemController : Controller
	{
		#region  variables
		private readonly string _areaName = "CMS";
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		private readonly IGalleryItemService _gitemService;
		private readonly IGalleryService _galleryService;
		#endregion

		#region constructors
		public ItemController(ILogger<ItemController> logger, IMapper mapper, IGalleryItemService galleryItemService,
			IGalleryService galleryService)
		{
			_logger = logger;
			_mapper = mapper;
			_gitemService = galleryItemService;
			_galleryService = galleryService;
		}
		#endregion

		#region actions
		//[ActionInfo("مشاهده همه تصاویر", "فهرست تصاویر")]
		public IActionResult Images(int? page)
		{
			GalleryType itemType = GalleryType.Image;
			ViewBag.Type = itemType;
			var items = _gitemService.GetByGalleryType(itemType).OrderByDescending(i => i.CreationDate).ToList();
			int pageSize = 5;
			int pageNumber = page ?? 1;
			return View(items.ToPagedList(pageNumber, pageSize));
		}

		//[ActionInfo("مشاهده همه ویدئوها", "فهرست ویدئوها")]
		public IActionResult Videos(int? page)
		{
			GalleryType itemType = GalleryType.Video;
			ViewBag.Type = itemType;
			var items = _gitemService.GetByGalleryType(itemType).OrderByDescending(i => i.CreationDate).ToList();
			int pageSize = 5;
			int pageNumber = page ?? 1;
			return View(items.ToPagedList(pageNumber, pageSize));
		}

		//[ActionInfo("ایجاد آیتم جدید", "آیتم جدید")]
		public IActionResult Create(GalleryType type)
		{
			ViewBag.Type = type;
			SetupGalleryList(0, type);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NewItem model)
		{
			ViewBag.Type = model.Type;
			if (ModelState.IsValid)
			{
				GalleryItem item = _mapper.Map<GalleryItem>(model);
				_gitemService.Add(item);
				return RedirectToIndex(model.Type);
			}
			SetupGalleryList(model.GalleryId, model.Type);
			return View(model);
		}

		//[ActionInfo("مشاهده جزییات یک آیتم", "جزییات آیتم")]
		public IActionResult Details(int id)
		{
			GalleryItem item = _gitemService.FindById(id);
			item.Gallery = _galleryService.FindById(item.GalleryId);
			if (item == null)
				return NotFound();
			return View(item);
		}

		//[ActionInfo("ویرایش اطلاعات آیتم", "ویرایش آیتم")]
		public IActionResult Edit(int id)
		{
			GalleryItem item = _gitemService.FindById(id);
			if (item == null)
				return NotFound();
			SetupGalleryList(item.GalleryId, item.Gallery.Type);
			EditableItem editableItem = _mapper.Map<EditableItem>(item);
			return View(editableItem);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EditableItem model)
		{
			if (ModelState.IsValid)
			{
				GalleryItem item = _mapper.Map<GalleryItem>(model);
				_gitemService.Update(item);
				return RedirectToIndex(model.Type);
			}
			SetupGalleryList(model.GalleryId, model.Type);
			return View(model);
		}

		//[ActionInfo("حذف آیتم", "حذف آیتم")]
		public IActionResult Delete(int id)
		{
			GalleryItem item = _gitemService.FindById(id);
			if (item == null)
				return NotFound();
			return View(item);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(int id)
		{
			GalleryItem item = _gitemService.FindById(id);
			if (item == null)
				return NotFound();
			GalleryType type = item.Gallery.Type;
			_gitemService.Delete(item);
			return RedirectToIndex(type);
		}
		#endregion

		#region methods
		private IActionResult RedirectToIndex(GalleryType type)
		{
			if (type == GalleryType.Image)
				return RedirectToAction("Images", "Item", new { area = _areaName });
			return RedirectToAction("Videos", "Item", new { area = _areaName });
		}

		private void SetupGalleryList(int galleryId, GalleryType type)
		{
			ViewBag.GalleryId = new SelectList(_galleryService.GetByType(type), "Id", "Name");
		}
		#endregion
	}
}
