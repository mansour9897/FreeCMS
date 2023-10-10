using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class PostController1 : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
