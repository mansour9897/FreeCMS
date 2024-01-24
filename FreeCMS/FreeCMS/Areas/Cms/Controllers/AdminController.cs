using FreeCMS.Extensions.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
    [Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت وبلاگ","وبلاگ")]
    public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
