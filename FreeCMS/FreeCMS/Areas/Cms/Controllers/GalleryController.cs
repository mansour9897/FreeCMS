using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels;
using FreeCMS.Attributes;
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
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت گالری ها", "وبلاگ")]
    public class GalleryController : Controller
	{
		#region variables
		private readonly string _areaName = "CMS";
		private readonly ILogger _logger;
		private readonly IMapper _mapper;
		private readonly IGalleryService _galleryService;
		#endregion

		#region constructors
		public GalleryController(ILogger<GalleryController> logger, IMapper mapper, IGalleryService albumService)
		{
			_logger = logger;
			_mapper = mapper;
			_galleryService = albumService;
		}
		#endregion

		#region actions
		[ActionInfo("مشاهده همه گالری ها", "فهرست گالری ها")]
		public IActionResult Index(int? page)
		{
			var albums = _galleryService.GetAll().OrderByDescending(a => a.CreationDate).ToList();
			int pageSize = 5;
			int pageNumber = page ?? 1;
			return View(albums.ToPagedList(pageNumber, pageSize));
		}

		[ActionInfo("ایجاد گالری جدید", "گالری جدید")]
		public IActionResult Create()
		{
			SetupGallertType(GalleryType.Image);
			return View();
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NewGallery model)
		{
			SetupGallertType(model.Type);
			if (ModelState.IsValid)
			{
				//check existence of gallery
				if (_galleryService.Exists(model.Name, model.Type))
				{
					ModelState.AddModelError("Name", "گالری با این نام و نوع وجود دارد.");
					return View(model);
				}
				Gallery gallery = _mapper.Map<Gallery>(model);
				_galleryService.Add(gallery);
				return RedirectToAction("Index", "Gallery", new { area = _areaName });
			}
			return View(model);
		}
		
		public IActionResult Details(int id)
		{
			Gallery gallery = _galleryService.FindById(id);
			if (gallery == null)
				return NotFound();
			return View(gallery);
		}

		[ActionInfo("ویرایش اطلاعات گالری", "ویرایش گالری")]
		public IActionResult Edit(int id)
		{
			Gallery gallery = _galleryService.FindById(id);
			if (gallery == null)
				return NotFound();
			EditableGallery editableGallery = _mapper.Map<EditableGallery>(gallery);
			return View(editableGallery);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EditableGallery model)
		{
			if (ModelState.IsValid)
			{
				if (_galleryService.Exists(model.Id, model.Name, model.Type))
				{
					ModelState.AddModelError("Name", "گالری با این نام و نوع وجود دارد.");
					return View(model);
				}
				Gallery gallery = _mapper.Map<Gallery>(model);
				_galleryService.Update(gallery);
				return RedirectToAction("Index", "Gallery", new { area = _areaName });
			}
			return View(model);
		}

		[ActionInfo("حذف گالری", "حذف گالری")]
		public IActionResult Delete(int id)
		{
			Gallery gallery = _galleryService.FindById(id);
			if (gallery == null)
				return NotFound();
			return View(gallery);
		}
		
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(int id)
		{
			Gallery gallery = _galleryService.FindById(id);
			if (gallery == null)
				return NotFound();
			_galleryService.Delete(gallery);
			return RedirectToAction("Index", "Gallery", new { area = _areaName });
		}
		#endregion

		#region methods
		private void SetupGallertType(GalleryType type)
		{
			ViewBag.Type = new SelectList(Enum.GetValues(typeof(GalleryType)).Cast<GalleryType>()
			.Select(v => new SelectListItem
			{
				Text = v.ToString(),
				Value = ((int)v).ToString(),
				Selected = (v == type)
			}).ToList(), "Value", "Text", type);

		}
		#endregion
	}
}
