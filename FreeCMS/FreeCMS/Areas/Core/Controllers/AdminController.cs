using FreeCMS.Attributes;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace Webo.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت","سیستم")]
    public class AdminController:Controller
    {
        private readonly ICommentService _commentService;
        private readonly IContactMessageService _contactService;
        public AdminController(ICommentService commentService, IContactMessageService contactMessageService)
        {
            this._commentService = commentService;
            this._contactService = contactMessageService;
        }
        [ActionInfo("مشاهده پیشخوان", "دسترسی به بخش های اصلی سایت مانند کاربران، پلاگین ها و ... ")]
        public IActionResult Index()
        {
            return View();
        }
        [ActionInfo("پیشخوان اصلی", " مشاهده پیشخوان اصلی")]
        public IActionResult Dashboard()
        {
            ViewBag.CommentsCount = _commentService.GetAll().Count;
            ViewBag.MessagesCount = _contactService.GetAll().Count;
            return View();
        }
    }
    
}