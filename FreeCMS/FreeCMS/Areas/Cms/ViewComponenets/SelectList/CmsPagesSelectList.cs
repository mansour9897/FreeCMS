using FreeCMS.Attributes;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace Webo.Cms.ViewComponents
{
    [SelectList("برگه ها",4)]
    public class CmsPagesSelectList:ViewComponent
    {
        private readonly IPageService _pageService;
        public CmsPagesSelectList(IPageService pageService)
        {
            this._pageService = pageService;
        }
        public IViewComponentResult Invoke()
        {
            var pages = _pageService.GetAll().Where(p => p.IsPublished == true)
                .OrderByDescending(t => t.CreationDate).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/SelectLists/CmsPagesSelectList.cshtml",pages);
        }
    }
}