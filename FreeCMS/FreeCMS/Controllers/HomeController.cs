using FreeCMS.DomainModels.System;
using FreeCMS.Models;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UAParser;

namespace FreeCMS.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IViewObjectService _viewService;
        private readonly Parser _uaParser;
        private readonly string extensionRegex = @"(^(.*\.css$)$)|(^(.*\.js$)$)|(^(.*\.png$)$)|(^(.*\.jpg$)$)|(^(.*\.gif$)$)|(^(.*\.pdf$)$)";
        private readonly string staticFilesRegex = @"(^\/favicon(-.*|.ico$))|(^\/robots.txt$)|(^\/rss.xml$)";
        private readonly string pathRegex = @"(^\/RichFilemanager\/.*)|(^\/css\/.*)|(^\/fonts\/.*)|(^\/images\/.*)|(^\/js\/.*)|(^\/lib\/.*)";
        public HomeController(ILogger<HomeController> logger,IViewObjectService viewObjectService)
        {
            _logger = logger;
            _viewService = viewObjectService;
            _uaParser = Parser.GetDefault();
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Analytic")]
        [HttpPost]
        public void Analytic(string page, string referrer)
        {
            //if path includes static files do not store the request
            if (Regex.IsMatch(Request.Path, extensionRegex, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(Request.Path, staticFilesRegex, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(Request.Path, pathRegex, RegexOptions.IgnoreCase))
            {
                return;
            }
            string visitorCookieName = "VisitorId";
            Guid visitorId = Guid.NewGuid();
            try
            {
                //get visitor id 
                if (Request.Cookies.ContainsKey(visitorCookieName))
                {
                    visitorId = Guid.Parse(Request.Cookies[visitorCookieName]);
                }
                string ipAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                string requestScheme = Request.Scheme + "://";
                string withoutScheme = page.Replace(requestScheme, "");
                string purePage = withoutScheme.Replace(Request.Host.ToString(), "");
                string path = purePage;
                string query = Request.QueryString.ToString();
                referrer = string.IsNullOrEmpty(referrer) ? "" : referrer;
                string userAgent = Request.Headers[HeaderNames.UserAgent];
                string browser = "Other";
                string os = "Other";
                if (!String.IsNullOrEmpty(userAgent))
                {
                    ClientInfo clientInfo = _uaParser.Parse(userAgent);
                    if (clientInfo != null)
                    {
                        if (!string.IsNullOrEmpty(clientInfo.UA.Family))
                            browser = clientInfo.UA.Family;
                        if (!string.IsNullOrEmpty(clientInfo.OS.Family))
                            os = clientInfo.OS.Family;
                    }
                }
                ViewObject viewObject = new ViewObject(visitorId, ipAddress, path, query, browser, os, referrer);
                _viewService.Insert(viewObject);
            }
            catch (Exception e)
            {
                _logger.LogError($"Some error in analytics middleware. {e.Message}");

            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}