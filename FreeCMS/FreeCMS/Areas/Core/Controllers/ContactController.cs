using FreeCMS.Extensions.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using X.PagedList;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت پیام ها","سیستم")]
    public class ContactController:Controller
    {
        #region variables ...
		private readonly IContactMessageService _contactService;
        #endregion

        #region constructors ...
        public ContactController(IContactMessageService contactService)
        {
			this._contactService = contactService;
        }
        #endregion

        #region actions ...
        [ActionInfo("مشاهده پیام ها","مشاهده پیام های ارسالی کاربران")]
        public IActionResult Index(int? page)
        {
            var messages = _contactService.GetAll().OrderByDescending(m => m.CreateDate);
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(messages.ToPagedList(pageNumber,pageSize));
        }
        [ActionInfo("مشاهده پیام","مشاهده پیام بصورت کامل")]
        public IActionResult Details(int id)
        {
            var message = _contactService.FindById(id);
            if(message == null)
                return NotFound();
            return View(message);
        }
        [ActionInfo("حذف پیام","حذف پیام")]
        public IActionResult Delete(int id)
        {
            var message = _contactService.FindById(id);
            if(message == null)
                return NotFound();
            return View(message);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            _contactService.Remove(id);
            return RedirectToAction("Index","Contact",new {area="Core"});
        }
		#endregion
	}
}