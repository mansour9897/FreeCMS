namespace FreeCMS.Service.System.Abstraction
{
    public interface ISiteSettingService
    {
        string GetSiteName();
        string GetSiteLogo();
        string GetSiteEmail();
        string GetSiteAddress();
        string GetSiteTelephone();
        string GetSiteFax();
        string GetSiteAboutUs();
    }
    
}