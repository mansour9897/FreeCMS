using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
