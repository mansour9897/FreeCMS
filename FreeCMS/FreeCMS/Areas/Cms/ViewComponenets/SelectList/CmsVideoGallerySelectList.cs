using FreeCMS.Attributes;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace Webo.Cms.ViewComponents
{
    [SelectList("گالری ویدئو",3)]
    public class CmsVideoGallerySelectList:ViewComponent
    {
        private readonly IGalleryService _galleryService;
        public CmsVideoGallerySelectList(IGalleryService galleryService)
        {
            this._galleryService = galleryService;
        }
        public IViewComponentResult Invoke()
        {
            var galleries = _galleryService.GetByType(GalleryType.Video).OrderByDescending(g => g.CreationDate).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/SelectLists/CmsVideoGallerySelectList.cshtml",galleries);
        }
    }
}