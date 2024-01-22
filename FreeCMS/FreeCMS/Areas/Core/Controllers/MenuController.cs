using FreeCMS.Areas.Core.ViewModels.Menu;
using FreeCMS.DomainModels.System;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("مدیریت منوها","سیستم")]
    public class MenuController:Controller
    {
        #region variables
        private readonly  IMenuService _menuService;
        private readonly IMenuItemService _itemService;
        #endregion

        #region constructors 
        public MenuController(IMenuService menuService,IMenuItemService itemService)
        {
            this._menuService = menuService;
            this._itemService = itemService;
        }
        #endregion

        #region actions
        [ActionInfo("فهرست منوها","مشاهده همه منوها عمومی")]
        public IActionResult Index(int? page)
        {
            var publicMenus = _menuService.GetAllMenuTypes().Where(m => m.IsPrivate == false).ToList();
            int pageSize = 5;
            int pageNumber = page ?? 1;
            return View(publicMenus.ToPagedList(pageNumber,pageSize));
        }
        [ActionInfo("مدیریت منو","مدیریت منو")]
        public IActionResult Edit(int id)
        {
            var menu =_menuService.GetMenuTypeById(id);
            if(menu == null)
                return NotFound();
            menu.MenuItems = _itemService.GetAllItems(menu.Name);
            return View(menu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditableMenu model)
        {
            UpdateMenu(model);  
            //remove all menu items
            _itemService.RemoveByMenuId(model.Id);
            AddMenuItems(model.Id,model.Text,model.Links,model.CssClasses,model.OpenInNewTab,
            model.LinkIsEnabled,model.Parents);
            return RedirectToAction("Index","Menu",new {area = "Core"});
        }

        
        //[ActionInfo("منوی جدید", "ساخت منوی جدید")]
        //public IActionResult Create()
        //{
        //    var menu = new Menu();
        //    return View(menu);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Menu model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _menuService.Add(model);
        //        return RedirectToAction("Index", "Menu", new { area = "Core" });
        //    }
        //    return View(model);
        //}
        #endregion

        #region  methods
        private void UpdateMenu(EditableMenu menu)
        {
            var targetMenu = _menuService.GetMenuTypeById(menu.Id);
            targetMenu.DisplayText = menu.DisplayText;
            targetMenu.CssClass = menu.CssClass;
            targetMenu.Description = menu.Description;
            _menuService.Update(targetMenu);
        } 
        private void AddMenuItems(int menuId,List<string> texts,List<string> Links,List<string> cssClasses,
            List<bool> openInNewTab,List<bool> linkIsEditable,List<string> parents)
        {
            if(linkIsEditable != null && linkIsEditable.Count > 0)
            {
                Dictionary<int,Guid> parentsDict = new Dictionary<int, Guid>();
                int itemsCount = linkIsEditable.Count;
                for (int i = 0; i < itemsCount; i++)
                {
                    MenuItem item = new MenuItem();
                    item.MenuId = menuId;
                    item.Id = Guid.NewGuid();
                    item.CssClass = cssClasses[i];
                    item.Url = Links[i];
                    item.LinkIsEditable = linkIsEditable[i];
                    item.OpenInNewWindow = openInNewTab[i];
                    item.Text = texts[i];
                    item.ParentId = SetParentId(parents[i],parentsDict);
                    item.Name = item.Id.ToString();
                    _itemService.Add(item);
                    parentsDict.Add(i,item.Id);
                }
            }
        }
        private Guid? SetParentId(string inputParentId,Dictionary<int,Guid> parentsDict)
        {
            if(string.IsNullOrEmpty(inputParentId) || inputParentId == "null")
                return null;
            if(parentsDict.ContainsKey(Int32.Parse(inputParentId)))
            {
                return parentsDict[Int32.Parse(inputParentId)];
            }
            return null;
        }
        #endregion
    }
}