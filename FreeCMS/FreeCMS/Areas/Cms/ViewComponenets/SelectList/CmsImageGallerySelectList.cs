using FreeCMS.Attributes;
using FreeCMS.DomainModels.Cms;
using FreeCMS.Service.CMS.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace FreeCMS.Areas.Cms.ViewComponents.SelectList
{
    [SelectList("گالری تصویر",2)]
    public class CmsImageGallerySelectList:ViewComponent
    {
        private readonly IGalleryService _galleryService;
        public CmsImageGallerySelectList(IGalleryService galleryService)
        {
            this._galleryService = galleryService;
        }
        public IViewComponentResult Invoke()
        {
            var images = _galleryService.GetByType(GalleryType.Image).OrderByDescending(g => g.CreationDate).ToList();
            return View("~/Areas/CMS/Views/Shared/Components/SelectLists/CmsImageGallerySelectList.cshtml",images);
        }
    }
}