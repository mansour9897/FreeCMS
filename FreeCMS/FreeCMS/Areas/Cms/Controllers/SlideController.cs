using AutoMapper;
using FreeCMS.Areas.Cms.ViewModels.Slide;
using FreeCMS.Attributes;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FreeCMS.Areas.Cms.Controllers
{
    [Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت اسلاید ها", "وبلاگ")]
    public class SlideController : Controller
    {
        #region variables 
        private readonly IMapper _mapper;
        private readonly ISlideService _slideService;
        private readonly string _areaName = "CMS";
        #endregion

        #region constructors
        public SlideController(IMapper mapper, ISlideService slideService)
        {
            _mapper = mapper;
            _slideService = slideService;
        }
        #endregion

        #region actions
        [ActionInfo("فهرست اسلاید ها", "مشاهده فهرست اسلاید ها")]
        public IActionResult Index(int? page)
        {
            var slides = _slideService.GetAll().OrderBy(m => m.ShowPriority).ToList();
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(slides.ToPagedList(pageNumber, pageSize));
        }
        
        [ActionInfo("افزودن اسلاید", "ایجاد اسلاید جدید")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewSlide model)
        {
            if (ModelState.IsValid)
            {
                Slide slide = _mapper.Map<Slide>(model);
                _slideService.Add(slide);
                return RedirectToAction("Index", "Slide", new { area = _areaName });
            }
            return View(model);
        }
        
        [ActionInfo("ویرایش اسلاید", "ویرایش اطلاعات اسلاید")]
        public IActionResult Edit(int id)
        {
            Slide slide = _slideService.FindById(id);
            if (slide == null)
                return NotFound();
            EditableSlide editableSlide = _mapper.Map<EditableSlide>(slide);
            return View(editableSlide);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditableSlide model)
        {
            if (ModelState.IsValid)
            {
                Slide slide = _mapper.Map<Slide>(model);
                _slideService.Update(slide);
                return RedirectToAction("Index", "Slide", new { area = _areaName });
            }
            return View(model);
        }
        
        [ActionInfo("جزییات اسلاید", "نمایش جزییات اسلاید")]
        public IActionResult Details(int id)
        {
            Slide slide = _slideService.FindById(id);
            if (slide == null)
                return NotFound();
            return View(slide);
        }
        
        [ActionInfo("حذف اسلاید", "حذف اسلاید")]
        public IActionResult Delete(int id)
        {
            Slide slide = _slideService.FindById(id);
            if (slide == null)
                return NotFound();
            return View(slide);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            Slide slide = _slideService.FindById(id);
            if (slide == null)
                return NotFound();
            _slideService.Delete(id);
            return RedirectToAction("Index", "Slide", new { area = _areaName });
        }
        #endregion

        #region methods
        private IActionResult RedirectToIndex()
        {
            return RedirectToAction("Index", "Slide", new { area = "CMS" });
        }
        #endregion
    }

}
