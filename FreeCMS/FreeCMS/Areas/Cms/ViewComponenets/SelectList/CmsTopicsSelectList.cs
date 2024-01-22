using FreeCMS.Attributes;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Webo.Cms.ViewComponents
{
    [SelectList("دسته ها",0)]
    public class CmsTopicsSelectList:ViewComponent
    {
        private readonly ITopicService _topicService;
        public CmsTopicsSelectList(ITopicService topicService)
        {
            this._topicService = topicService;
        }
        public IViewComponentResult Invoke()
        {
            var topics = _topicService.GetAll().OrderByDescending(t => t.CreationDate).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/SelectLists/CmsTopicsSelectList.cshtml",topics);
        }
    }
}