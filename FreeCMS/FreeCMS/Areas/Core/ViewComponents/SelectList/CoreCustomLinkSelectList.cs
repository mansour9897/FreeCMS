using Microsoft.AspNetCore.Mvc;
namespace FreeCMS.Areas.Core.ViewComponents.SelectList
{
    public class CoreCustomLinkSelectList:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/SelectList/CoreCustomLinkSelectList.cshtml");
        }
    }
}