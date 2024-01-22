using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using FreeCMS.Service.System.Abstraction;

namespace FreeCMS.Service.System.Implemented
{
    public class VirtualFileManager : IVirtualFileManager
    {
        #region  variables ...
        private readonly IRandomFileNameGenerator _nameGenerator;
        private readonly string _webRootPath;
        #endregion

        #region constructors ...
        public VirtualFileManager(IWebHostEnvironment evn)
        {
            this.StorageHandlers = new List<IFileStorageHandler>();
            this._nameGenerator = new GuidNameGenerator();
            this._webRootPath = evn.WebRootPath;
            this.StorageHandlers.Add(new ImageFileHandler());
            this.StorageHandlers.Add(new VideoFileHandler());
            this.StorageHandlers.Add(new AudioFileHandler());
            this.StorageHandlers.Add(new OtherFileHandler());
        }
        #endregion

        #region  properties ...
        public List<IFileStorageHandler> StorageHandlers { get; set; }
        #endregion
        #region methods ...
        public string Save(IFormFile file, string sectionName)
        {
            if (string.IsNullOrEmpty(sectionName))
                throw new ArgumentNullException(nameof(sectionName));
            if (file == null || file.Length == 0)
                throw new ArgumentNullException(nameof(file));
            string fileName = this._nameGenerator.Generate() + Path.GetExtension(file.FileName).ToLower();
            string sectionPath = Path.Combine("files", sectionName);
            string sectionDirectoryPath = Path.Combine(this._webRootPath, sectionPath);
            //check existence of section directory
            if (!Directory.Exists(sectionDirectoryPath))
            {
                Directory.CreateDirectory(sectionDirectoryPath);
            }
            foreach (var storageHandler in this.StorageHandlers)
            {
                //check existence of sundirectories
                string storageHandlerPath = Path.Combine(sectionDirectoryPath, storageHandler.BaseAddress);
                if (!Directory.Exists(storageHandlerPath))
                {
                    Directory.CreateDirectory(storageHandlerPath);
                }
                if (storageHandler.CanSaveFile(file))
                {
                    return storageHandler.Save(file, this._webRootPath, sectionPath, fileName);
                }
            }
            return "";
        }
        public bool Update(IFormFile file, string fileAddress)
        {
            try
            {
                string saveFilePath = Path.Combine(this._webRootPath, fileAddress.TrimStart('/').Replace("/", @"\"));
                if (File.Exists(saveFilePath))
                {
                    using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                    {
                        file.CopyTo(fileStream);
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Delete(string fileAddress)
        {
            string filePath = Path.Combine(this._webRootPath, fileAddress.TrimStart('/').Replace("/", @"\"));
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
    #region vfm internal classes ...
    public class GuidNameGenerator : IRandomFileNameGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
    public class DocumentFileHandler : IFileStorageHandler
    {
        public DocumentFileHandler()
        {
            this.BaseAddress = "documents";
            this.Extentions = new List<string> { ".pdf", ".doc", ".html" };
        }
        public string BaseAddress { get; set; }
        public List<string> Extentions { get; set; }
        public bool CanSaveFile(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (this.Extentions.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        public string Save(IFormFile file, string rootPath, string sectionPath, string fileName)
        {
            try
            {
                string relativeFilePath = Path.Combine(sectionPath, BaseAddress, fileName);
                string saveFilePath = Path.Combine(rootPath, relativeFilePath);
                using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return Path.Combine("/", relativeFilePath.Replace(@"\", "/"));
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
    public class AudioFileHandler : IFileStorageHandler
    {
        public AudioFileHandler()
        {
            this.BaseAddress = "audio";
            this.Extentions = new List<string> { ".mp3" };
        }
        public string BaseAddress { get; set; }
        public List<string> Extentions { get; set; }
        public bool CanSaveFile(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (this.Extentions.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        public string Save(IFormFile file, string rootPath, string sectionPath, string fileName)
        {
            try
            {
                string relativeFilePath = Path.Combine(sectionPath, BaseAddress, fileName);
                string saveFilePath = Path.Combine(rootPath, relativeFilePath);
                using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return Path.Combine("/", relativeFilePath.Replace(@"\", "/"));
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
    public class VideoFileHandler : IFileStorageHandler
    {
        public VideoFileHandler()
        {
            this.BaseAddress = "video";
            this.Extentions = new List<string> { ".mp4", ".avi" };
        }
        public string BaseAddress { get; set; }
        public List<string> Extentions { get; set; }
        public bool CanSaveFile(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (this.Extentions.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        public string Save(IFormFile file, string rootPath, string sectionPath, string fileName)
        {
            try
            {
                string relativeFilePath = Path.Combine(sectionPath, BaseAddress, fileName);
                string saveFilePath = Path.Combine(rootPath, relativeFilePath);
                using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return Path.Combine("/", relativeFilePath.Replace(@"\", "/"));
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
    public class ImageFileHandler : IFileStorageHandler
    {
        public ImageFileHandler()
        {
            this.BaseAddress = "images";
            this.Extentions = new List<string> { ".jpg", ".png", ".gif", ".jpeg" };
        }
        public string BaseAddress { get; set; }
        public List<string> Extentions { get; set; }
        public bool CanSaveFile(IFormFile file)
        {
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (this.Extentions.Contains(fileExtension))
            {
                return true;
            }
            return false;
        }
        public string Save(IFormFile file, string rootPath, string sectionPath, string fileName)
        {
            try
            {
                string relativeFilePath = Path.Combine(sectionPath, BaseAddress, fileName);
                string saveFilePath = Path.Combine(rootPath, relativeFilePath);
                using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return Path.Combine("/", relativeFilePath.Replace(@"\", "/"));
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
    public class OtherFileHandler : IFileStorageHandler
    {
        public OtherFileHandler()
        {
            this.BaseAddress = "others";
        }
        public string BaseAddress { get; set; }
        public List<string> Extentions { get; set; }
        public bool CanSaveFile(IFormFile file)
        {
            return true;
        }
        public string Save(IFormFile file, string rootPath, string sectionPath, string fileName)
        {
            try
            {
                string relativeFilePath = Path.Combine(sectionPath, BaseAddress, fileName);
                string saveFilePath = Path.Combine(rootPath, relativeFilePath);
                using (var fileStream = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                return Path.Combine("/", relativeFilePath.Replace(@"\", "/"));
            }
            catch (Exception)
            {

                return "";
            }
        }
    }
    #endregion
}