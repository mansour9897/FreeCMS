using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.ViewComponents.Menu
{
    public class ShowMenu:ViewComponent
    {
        private readonly IMenuService _menuService;
        public ShowMenu(IMenuService menuService)
        {
            this._menuService = menuService;
        }
        public IViewComponentResult Invoke(string name)
        {
            var menu = _menuService.GetMenuTypeByName(name);
            return View("~/Views/Shared/Components/Menu/ShowMenu.cshtml",menu);
        }
    }
}