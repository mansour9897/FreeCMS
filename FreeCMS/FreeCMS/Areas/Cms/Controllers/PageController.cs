using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels.Page;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Service.CMS.Abstraction;
using FreeCMS.Service.Filters;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
    [Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    public class PageController : Controller
	{
		#region variables
		private readonly string _areaName = "CMS";
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IPageService _pageService;
		#endregion

		#region constructors
		public PageController(IPageService pageService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
		{
			this._pageService = pageService;
			this._mapper = mapper;
			this._httpContextAccessor = httpContextAccessor;
		}
		#endregion

		#region actions
		//[ActionInfo("مشاهده همه برگه ها", "فهرست برگه ها")]
		public IActionResult Index(int? page)
		{
			var pages = _pageService.GetAll().OrderByDescending(t => t.CreationDate); ;
			int pageSize = 5;
			int pageNumber = page ?? 1;
			return View(pages.ToPagedList(pageNumber, pageSize));
		}

		//[ActionInfo("ایجاد برگه جید", "ایجاد برگه")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(NewPage model)
		{
			if (ModelState.IsValid)
			{
				if (_pageService.PageExists(model.Title))
				{
					ModelState.AddModelError("Title", "برگه ای با این  عنوان وجود دارد.");
					return View(model);
				}
				var page = _mapper.Map<Page>(model);

				//set autorid
				if (_httpContextAccessor.HttpContext is not null)
				{
					var authorId = _httpContextAccessor.HttpContext.User.GetId();
					page.AuthorId = authorId;
				}
				_pageService.Add(page);
				return RedirectToAction("Index", "Page", new { area = _areaName });
			}
			return View(model);
		}

		//[ActionInfo("مشاهده جزییات برگه", "مشاهده برگه")]
		public IActionResult Details(int id)
		{
			Page page = _pageService.FindById(id);
			if (page == null)
			{
				return NotFound();
			}
			return View(page);
		}

		//[ActionInfo("ویرایش اطلاعات برگه", "ویرایش برگه")]
		public IActionResult Edit(int id)
		{
			Page page = _pageService.FindById(id);
			if (page == null)
			{
				return NotFound();
			}
			EditablePage editablePage = _mapper.Map<EditablePage>(page);
			return View(editablePage);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(EditablePage model)
		{
			if (ModelState.IsValid)
			{
				if (_pageService.PageExists(model.Title, model.Id))
				{
					ModelState.AddModelError("Title", "برگه ای با این  عنوان وجود دارد.");
					return View(model);
				}
				var page = _mapper.Map<Page>(model);
				_pageService.Update(page);
				return RedirectToAction("Index", "Page", new { area = _areaName });
			}
			return View(model);
		}

		//[ActionInfo("حذف برگه", "حذف برگه")]
		public IActionResult Delete(int id)
		{
			Page page = _pageService.FindById(id);
			if (page == null)
			{
				return NotFound();
			}
			return View(page);
		}
		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult ConfirmDelete(int id)
		{
			Page page = _pageService.FindById(id);
			if (page == null)
			{
				return NotFound();
			}
			_pageService.Delete(page);
			return RedirectToAction("Index", "Page", new { area = _areaName });
		}
		#endregion
	}
}
