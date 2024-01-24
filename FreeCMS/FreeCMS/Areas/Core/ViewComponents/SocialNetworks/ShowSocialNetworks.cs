using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Core.ViewComponents.SocialNetworks
{
	public class ShowSocialNetworks : ViewComponent
	{
		private readonly ISocialNetworkService _socialNetworkService;
		public ShowSocialNetworks(ISocialNetworkService socialNetworkService)
		{
			this._socialNetworkService = socialNetworkService;
		}
		public IViewComponentResult Invoke()
		{
			var socialNetworks = this._socialNetworkService.List().Where(s => s.Display == true).ToList();
			return View("~/Areas/Core/Views/Shared/Components/ShowSocialNetworks.cshtml", socialNetworks);
		}
	}
}