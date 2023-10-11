using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class PostController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
