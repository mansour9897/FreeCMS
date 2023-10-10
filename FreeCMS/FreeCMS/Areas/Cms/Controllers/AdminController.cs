using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
