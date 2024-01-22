using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [Authorize]
    [ControllerInfo("آمار بازدید","سیستم")]
    public class AnalyticController:Controller
    {
        #region variables 
        private readonly IViewObjectService _viewService;
        #endregion

        #region constructors 
        public AnalyticController(IViewObjectService viewService)
        {
            this._viewService = viewService;
        }
        #endregion

        #region actions
        [ActionInfo("آمار بازدید","مشاهده همه اطلاعات آمار بازدید")]
        public IActionResult Index()
        {
            //delete all views before three month to decrease database volume
            _viewService.Delete(v => v.Date.Date < DateTime.Now.AddDays(-30));
            return View();
        }
        #endregion
    }
}