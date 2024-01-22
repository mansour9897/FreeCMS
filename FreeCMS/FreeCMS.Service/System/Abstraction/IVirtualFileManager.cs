using Microsoft.AspNetCore.Http;

namespace FreeCMS.Service.System.Abstraction
{
    public interface IFileStorageHandler
    {
        bool CanSaveFile(IFormFile file);
        string Save(IFormFile file,string rootPath,string sectionPath,string fileName);
        string BaseAddress { get; set; }
        List<string> Extentions { get; set;}
    }
    public interface IVirtualFileManager
    {
        string Save(IFormFile file,string sectionName);
        bool Update(IFormFile file,string fileAddress);    
        bool Delete(string fileAddress);
    }
    public interface IRandomFileNameGenerator
    {
        string Generate();
    }
}