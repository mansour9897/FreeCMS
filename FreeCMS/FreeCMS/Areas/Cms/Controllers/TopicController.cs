using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Cms.Controllers
{
	public class TopicController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
