using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using AutoMapper;
using FreeCMS.Extensions.Attributes;
using Microsoft.AspNetCore.Authorization;
using FreeCMS.DomainModels.System;
using FreeCMS.Areas.Core.ViewModels;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت شبکه های اجتماعی","سیستم")]
    public class SocialNetworkController:Controller
    {
        #region variables 
        private readonly ISocialNetworkService _socialService;
        private readonly IMapper _mapper;
        #endregion

        #region constructors 
        public SocialNetworkController(ISocialNetworkService socialService,IMapper mapper)
        {
            this._socialService = socialService;
            this._mapper = mapper;
        }
        #endregion

        #region actions
        [ActionInfo("فهرست شبکه های اجتماعی","مشاهده همه شبکه های موجود")]
        public IActionResult Index(int? page)
        {
            var socials = _socialService.List().OrderByDescending(s => s.CreationDate).ToList();
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(socials.ToPagedList(pageNumber,pageSize));
        }
        [ActionInfo("افزودن شبکه جدید","افزودن یک شبکه اجتماعی جدید")]
        public IActionResult Create()
        {
            NewSocialNetwork vm = new NewSocialNetwork();
            vm.Display = false;
            vm.IsShareable = false;
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NewSocialNetwork model)
        {
            if(ModelState.IsValid)
            {
                if(model.Display && string.IsNullOrEmpty(model.Address))
                {
                    ModelState.AddModelError("Address","وارد کردن آدرس الزامی است.");
                    return View(model);
                }
                if(model.IsShareable && string.IsNullOrEmpty(model.ShareAddress))
                {
                    ModelState.AddModelError("ShareAddress","وارد کردن آدرس اشتراک گذاری الزامی است.");
                    return View(model);
                }
                SocialNetwork socialNetwork = _mapper.Map<SocialNetwork>(model);
                _socialService.Add(socialNetwork);
                return RedirectToAction("Index","SocialNetwork",new{area="Core"});
            }
            return View(model);
        }
        [ActionInfo("جزییات شبکه اجتماعی","مشاهده جزییات شبکه اجتماعی")]
        public IActionResult Details(int id)
        {
            SocialNetwork socialNetwork = _socialService.FindById(id);
            if(socialNetwork == null)
                return NotFound();
            return View(socialNetwork);
        }
        [ActionInfo("ویرایش شبکه اجتماعی","ویرایش اطلاعات شبکه اجتماعی")]
        public IActionResult Edit(int id)
        {
            SocialNetwork socialNetwork = _socialService.FindById(id);
            if(socialNetwork == null)
                return NotFound();
            EditableSocialNetwork editableSocial = _mapper.Map<EditableSocialNetwork>(socialNetwork);
            return View(editableSocial);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditableSocialNetwork model)
        {
            if(ModelState.IsValid)
            {
                if(model.Display && string.IsNullOrEmpty(model.Address))
                {
                    ModelState.AddModelError("Address","وارد کردن آدرس الزامی است.");
                    return View(model);
                }
                if(model.IsShareable && string.IsNullOrEmpty(model.ShareAddress))
                {
                    ModelState.AddModelError("ShareAddress","وارد کردن آدرس اشتراک گذاری الزامی است.");
                    return View(model);
                }
                SocialNetwork socialNetwork = _mapper.Map<SocialNetwork>(model);
                _socialService.Update(socialNetwork);
                return RedirectToAction("Index","SocialNetwork",new{area="Core"});
            }
            return View(model);
        }
        [ActionInfo("حذف شبکه اجتماعی","حذف شبکه اجتماعی")]
        public IActionResult Delete(int id)
        {
            SocialNetwork socialNetwork = _socialService.FindById(id);
            if(socialNetwork == null)
                return NotFound();
            return View(socialNetwork);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            SocialNetwork socialNetwork = _socialService.FindById(id);
            if(socialNetwork != null)
            {
                _socialService.Delete(socialNetwork);
            }
            return RedirectToAction("Index","SocialNetwork",new{area="Core"});
        }
        #endregion
    }
}