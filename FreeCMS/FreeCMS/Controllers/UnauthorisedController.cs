using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace FreeCMS.Controllers
{
    [Authorize]
    [Route("/Unauthorised")]
    public class UnauthorisedController:Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
    
}