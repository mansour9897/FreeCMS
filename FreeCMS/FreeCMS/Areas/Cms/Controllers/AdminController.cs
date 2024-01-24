using FreeCMS.Attributes;
using FreeCMS.Extensions.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	[Area("CMS")]
    [Route("CMS/[controller]/[action]")]
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت وبلاگ","وبلاگ")]
    public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
