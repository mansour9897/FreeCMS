using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class PGController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
