using FreeCMS.Attributes;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Webo.Cms.ViewComponents
{
    [SelectList("نوشته ها",1)]
    public class CmsPostsSelectList:ViewComponent
    {
        private readonly IPostService _postService;
        public CmsPostsSelectList(IPostService postService)
        {
            this._postService = postService;
        }
        public IViewComponentResult Invoke()
        {
            var posts = _postService.GetAll().Where(p => p.IsPublished == true).OrderByDescending(t => t.CreationDate).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/SelectLists/CmsPostsSelectList.cshtml",posts);
        }
    }
}