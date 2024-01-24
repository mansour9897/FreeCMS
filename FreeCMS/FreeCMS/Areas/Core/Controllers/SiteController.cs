using FreeCMS.Areas.Core.ViewModels.Site;
using FreeCMS.Attributes;
using FreeCMS.Common.Utilities;
using FreeCMS.DomainModels.System;
using FreeCMS.Extensions;
using FreeCMS.Extensions.Attributes;
using FreeCMS.Service.System.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeCMS.Areas.Core.Controllers
{
    [Area("Core")]
    [Route("Core/[controller]/[action]")]
    [FreeCmsAuthorize]
    [ControllerInfo("مدیریت سایت","سیستم")]
    public class SiteController:Controller
    {
        #region variables ...
        private readonly ISettingService _settingService;
        private readonly IFileChecker _iconChecker;
        private readonly IWebHostEnvironment _host;
        private readonly ISiteSettingService _siteSettingService;
        private readonly IVirtualFileManager _vfm;
        private readonly string _settingsType = "SiteSettings" ;
        private readonly string _commentSettings = "CommentSettings";
        private readonly string _siteNameSetting = "SiteName";
        private readonly string _siteEmailSetting = "SiteEmail";
        private readonly string _siteAddressSetting = "SiteAddress";
        private readonly string _siteAboutUsSetting = "SiteAboutUs";
        private readonly string _siteTelephoneSetting = "SiteTelephone";
        private readonly string _siteLogoSetting = "SiteLogo";
        private readonly string _siteSloganSetting = "SiteSlogan";
        private readonly string _siteFaxSetting = "SiteFax";
        private readonly string _siteLogoName = "Sitelogo";
        private readonly string _siteLogoFolder = "images";
        private readonly int _logoSize = 50;
        private readonly string _autoVerifySetting = "AutoVerify";
        private readonly string _storageAreaName = "Settings";
        #endregion

        #region constructors ...
        public SiteController(ISettingService settingService,IWebHostEnvironment host,
            ISiteSettingService siteSettingService,IVirtualFileManager vfm)
        {
            this._settingService = settingService;
            this._iconChecker = BuildFileChecker();
            this._host = host;
            this._siteSettingService = siteSettingService;
            this._vfm = vfm;
        }
        #endregion

        #region actions ...
        [ActionInfo("تنظیمات سایت","دسترسی به تنظمیات سایت، شبکه های اجتماعی، لوگو و ...")]
        public IActionResult Index()
        {
            return View();
        }
        
        [ActionInfo("ویرایش اطلاعات سایت","ویرایش نام، ایمیل، تلفن، آدرس، درباره ما و ...")]
        public IActionResult Settings()
        {
            return View(GetSiteSettings());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Settings(SiteSettingsVm model)
        {
            if(ModelState.IsValid)
            {
                SetSiteSettings(model);
                return RedirectToAction("Index","Site",new{area="Core"});
            }
            return View(model);
        }
        [ActionInfo("ویرایش لوگو سایت","ویرایش لوگو سایت")]
        public IActionResult Logo()
        {
            ViewBag.LogoAddress = _siteSettingService.GetSiteLogo();
            ViewBag.SiteImageSize = _logoSize;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logo(IFormFile file)
        {   
            string logoAddress = _siteSettingService.GetSiteLogo();
            ViewBag.LogoAddress = logoAddress;
            ViewBag.SiteImageSize = _logoSize;
            ICheckFileResult checkResult = _iconChecker.Check(file);
            if(checkResult.Status == CheckFileStatus.Succeeded)
            {
                try
                {
                    if(string.IsNullOrEmpty(logoAddress) || !_vfm.Update(file,logoAddress))
                    {
                        logoAddress = _vfm.Save(file,_storageAreaName);
                    }
                    SetSettingValue(_siteLogoSetting,logoAddress);
                    return RedirectToAction("Index","Site",new{area="Core"});
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty,"در هنگام بارگذاری تصویر مشکلی وجود دارد. دوباره تلاش کنید.");
                    return View();
                }

            }
            foreach(var error in checkResult.Errors)
            {
                ModelState.AddModelError(string.Empty,error);
            }
            return View();
        }
        public IActionResult Comments()
        {
            return View(GetCommentSettings());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Comments(CommentSettingsVm model)
        {
            if(ModelState.IsValid)
            {
                SetCommentSettings(model);
                return RedirectToAction("Index","Site",new{area="Core"});
            }
            return View(model);
        }
        #endregion

        #region methods ...
        private CommentSettingsVm GetCommentSettings()
        {
            CommentSettingsVm vm = new CommentSettingsVm();
            var commentSettings = _settingService.GetByType(_commentSettings);
            vm.AutoVerify = GetSettingByName(commentSettings,_autoVerifySetting).Value.ToBoolean();
            return vm;
        }
        private void SetCommentSettings(CommentSettingsVm model)
        {
            SetSettingValue(_commentSettings,_autoVerifySetting,model.AutoVerify.ToString());
        }
        private SiteSettingsVm GetSiteSettings()
        {
            SiteSettingsVm vm = new SiteSettingsVm();
            var siteSettings = _settingService.GetByType(_settingsType);
            vm.Name = GetSettingByName(siteSettings,_siteNameSetting).Value;
            vm.Email = GetSettingByName(siteSettings,_siteEmailSetting).Value;
            vm.Telephone = GetSettingByName(siteSettings,_siteTelephoneSetting).Value;
            vm.Address = GetSettingByName(siteSettings,_siteAddressSetting).Value;
            vm.Slogan = GetSettingByName(siteSettings,_siteSloganSetting).Value;
            vm.Fax = GetSettingByName(siteSettings,_siteFaxSetting).Value;
            vm.AboutUs = GetSettingByName(siteSettings,_siteAboutUsSetting).Value;
            return vm;
        }
        private Setting GetSettingByName(List<Setting> list,string name)
        {
            return list.Where(s => s.Name == name).FirstOrDefault();
        } 
        private void SetSiteSettings(SiteSettingsVm model)
        {
            SetSettingValue(_siteNameSetting,model.Name);
            SetSettingValue(_siteEmailSetting,model.Email);
            SetSettingValue(_siteTelephoneSetting,model.Telephone);
            SetSettingValue(_siteAddressSetting,model.Address);
            SetSettingValue(_siteSloganSetting,model.Slogan);
            SetSettingValue(_siteFaxSetting,model.Fax);
            SetSettingValue(_siteAboutUsSetting,model.AboutUs);
        }
        private void SetSettingValue(string name,string value)
        {
            Setting s = _settingService.GetByName(name,_settingsType);
            if(s != null)
            {
                s.Value = value;
                _settingService.UpdateSetting(s);
            }
        }
        private void SetSettingValue(string type,string name,string value)
        {
            Setting s = _settingService.GetByName(name,type);
            if(s != null)
            {
                s.Value = value;
                _settingService.UpdateSetting(s);
            }
        }
        private IFileChecker BuildFileChecker()
        {
             return new IconFileChecker(_logoSize * 1000);
        }
        #endregion
        
    }
    public class IconFileChecker:BaseFileChecker
    {
        private readonly int _defaultImageSize = 50 * 1000;
        public IconFileChecker()
        {
            Validators.Add(new ImageNullValidator());
            Validators.Add(new ImageSizeValidator(_defaultImageSize));
            Validators.Add(new ImageMimeTypeValidator());
            Validators.Add(new ImageExtensionValidator());
        }
        public IconFileChecker(int fileSize)
        {
            Validators.Add(new ImageNullValidator());
            Validators.Add(new ImageSizeValidator(fileSize));
            Validators.Add(new ImageMimeTypeValidator());
            Validators.Add(new ImageExtensionValidator());
        }
    }
}