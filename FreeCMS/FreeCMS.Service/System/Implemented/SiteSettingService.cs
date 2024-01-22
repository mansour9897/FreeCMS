using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class SiteSettingService:ISiteSettingService
    {
        private readonly ISettingService _settingService;
        private readonly string _settingsType = "SiteSettings" ;
        private readonly string _siteNameSetting = "SiteName";
        private readonly string _siteLogoName = "SiteLogo";
        private readonly string _siteEmailSetting = "SiteEmail";
        private readonly string _siteAddressSetting = "SiteAddress";
        private readonly string _siteAboutUsSetting = "SiteAboutUs";
        private readonly string _siteTelephoneSetting = "SiteTelephone";
        private readonly string _siteSloganSetting = "SiteSlogan";
        private readonly string _siteFaxSetting = "SiteFax";
        private readonly string _defaultSiteLogo = "/images/weboicon.jpg";
        public SiteSettingService(ISettingService settingService)
        {
            this._settingService = settingService;
        }
        public string GetSiteName()
        {
            return _settingService.GetByName(_siteNameSetting,_settingsType).Value;
        }
        public string GetSiteLogo()
        {
            string siteLogo = _settingService.GetByName(_siteLogoName,_settingsType).Value;
            return string.IsNullOrEmpty(siteLogo)? _defaultSiteLogo : siteLogo;
        }
        public string GetSiteEmail() =>  _settingService.GetByName(_siteEmailSetting,_settingsType).Value;
        public string GetSiteAddress() =>  _settingService.GetByName(_siteAddressSetting,_settingsType).Value;
        public string GetSiteTelephone() =>  _settingService.GetByName(_siteTelephoneSetting,_settingsType).Value;
        public string GetSiteFax() =>  _settingService.GetByName(_siteFaxSetting,_settingsType).Value;
        public string GetSiteAboutUs() =>  _settingService.GetByName(_siteAboutUsSetting,_settingsType).Value;
        
    }
    
}