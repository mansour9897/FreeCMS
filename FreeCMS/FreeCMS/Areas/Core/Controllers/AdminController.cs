using Microsoft.AspNetCore.Mvc;
namespace Webo.Core.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    //[WeboAuthorize]
    //[ControllerInfo("مدیریت")]
    public class AdminController:Controller
    {
        public AdminController(/*ICommentService commentService,IContactMessageService contactMessageService*/)
        {
        }

        //[ActionInfo("مشاهده پیشخوان","دسترسی به بخش های اصلی سایت مانند کاربران، پلاگین ها و ... ")]
        public IActionResult Index()
        {
            return View();
        }
    }
    
}