using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
namespace FreeCMS.Areas.Core.ViewComponents.SelectList
{
    public class ShowSelectLists:ViewComponent
    {
        private readonly ISelectListService _selectService;
        public ShowSelectLists(ISelectListService selectService)
        {
            this._selectService = selectService;
        }
        public IViewComponentResult Invoke()
        {
            var selectLists = _selectService.List().GroupBy(s => s.PluginName)
                .Select(g => g.OrderBy(s => s.Priority))
                .OrderBy(g => g.First().Priority).SelectMany(e => e).Select(i => i.Name).ToList();
            
            return View("~/Views/Shared/Components/SelectList/ShowSelectLists.cshtml",selectLists);
        }
    }
}