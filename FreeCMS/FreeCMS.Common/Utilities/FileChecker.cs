using Microsoft.AspNetCore.Http;
namespace FreeCMS.Common.Utilities
{
    #region result structures and classes ...
    public enum CheckFileStatus
    {
        Succeeded,
        Failed
    }
    public enum ValidateFileStatus
    {
        Succeeded,
        Failed
    }
    public interface ICheckFileResult
    {
        CheckFileStatus Status { get; set; }
        List<string> Errors { get; set; }
    }
    public interface IValidateFileResult
    {
        ValidateFileStatus Status { get; set;}
        string Error { get; set;}
    }
    public class CheckFileResult:ICheckFileResult
    {
        public CheckFileResult()
        {
            this.Status = CheckFileStatus.Failed;
            this.Errors = new List<string>();
        }
        public CheckFileStatus Status { get; set; }
        public List<string> Errors { get; set; }
        
    }
    public class ValidateFileResult: IValidateFileResult
    {
        public ValidateFileResult()
        {
            this.Status = ValidateFileStatus.Failed;
            this.Error = "";
        }
        public ValidateFileStatus Status { get; set;}
        public string Error { get; set;}
    }
    #endregion

    #region  interfaces ...
    public interface IFileChecker
    {
        ICheckFileResult Check(IFormFile file);
        List<IFileValidator> Validators { get; set;}
    }
    public interface IFileValidator
    {
        IValidateFileResult Validate(IFormFile file);
    }
    #endregion

    #region base classes ...
    public class FileNullValidator:IFileValidator
    {
        public IValidateFileResult Validate(IFormFile file)
        {
            IValidateFileResult result = new ValidateFileResult();
            if(file == null || file.Length == 0)
            {
                result.Error = "فایل انتخاب نشده است.";
                return result;
            }
            result.Status = ValidateFileStatus.Succeeded;
            return result;
        }
    }
    public class FileMimeTypeValidator:IFileValidator
    {
        private protected List<string> MimeTypes { get; set;}
        public FileMimeTypeValidator()
        {
            MimeTypes = new List<string>();
        }
        public FileMimeTypeValidator(List<string> mimeTypes)
        {
            MimeTypes = mimeTypes;
        }
        public IValidateFileResult Validate(IFormFile file)
        {
            IValidateFileResult result = new ValidateFileResult();
            string fileMimeType = file.ContentType.ToLower();
            foreach (var mimeType in MimeTypes)
            {
                if(fileMimeType == mimeType)
                {
                    result.Status = ValidateFileStatus.Succeeded;
                    return result;
                }
            }
            result.Error = "فایل با فرمت " + this.MimeTypes.ToSplittedPersianItems() + " مجاز می باشد.";
            return result;
        }
    }
    public class FileExtensionValidator:IFileValidator
    {
        private protected List<string> Extensions { get; set;}
        public FileExtensionValidator()
        {
            Extensions = new List<string>();
        }
        public FileExtensionValidator(List<string> extensions)
        {
            this.Extensions = extensions;
        }
        public IValidateFileResult Validate(IFormFile file)
        {
            IValidateFileResult result = new ValidateFileResult();
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            foreach (var extension in this.Extensions)
            {
                if(fileExtension == extension)
                {
                    result.Status = ValidateFileStatus.Succeeded;
                    return result;
                }
            }
            result.Error = "فایل با این پسوند " + this.Extensions.ToSplittedPersianItems() + " مجاز می باشد.";
            return result;
        }
    }
    public class FileSizeValidator:IFileValidator
    {
        private protected  int DesiredSize { get; set;}
        public FileSizeValidator()
        {
            DesiredSize = 0;
        }
        public FileSizeValidator(int imageSize)
        {
            DesiredSize = imageSize;
        }
        public IValidateFileResult Validate(IFormFile file)
        {
            IValidateFileResult result = new ValidateFileResult();
            long imageSize = file.Length;
            if(imageSize >= DesiredSize)
            {
                result.Error = "فایل بزرگ است.";
                return result;
            }
            result.Status = ValidateFileStatus.Succeeded;
            return result;
        }
    }
    public class BaseFileChecker:IFileChecker
    {
        public List<IFileValidator> Validators { get; set;}
        public BaseFileChecker()
        {
            Validators  = new List<IFileValidator>();
        }
        public ICheckFileResult Check(IFormFile file)
        {
            ICheckFileResult result = new CheckFileResult();
            foreach (var imageValidator in Validators)
            {
                IValidateFileResult validationResult = imageValidator.Validate(file);
                if(validationResult.Status == ValidateFileStatus.Failed)
                {
                   
                    result.Errors.Add(validationResult.Error);
                    return result;
                }
                
            }
            result.Status = CheckFileStatus.Succeeded;
            return result;
        }
    }
    #endregion

    #region factory class ....
    public static class FileCheckerFactory
    {
        public static IFileChecker GetDefaultImageChecker()
        {
            return new ImageFileChecker();
        }
        public static IFileChecker GetDefaultZipChecker()
        {
            return new ZipFileChecker();
        }
        public static IFileChecker GetDefaultAudioChecker()
        {
            return new AudioFileChecker();
        }
        public static IFileChecker GetDefaultVideoChecker()
        {
            return new VideoFileChecker();
        }
    }
    #endregion

    #region extensions ...
    public static class ListExtensions
    {
        public static string ToSplittedPersianItems(this List<string> items)
        {
            string result = "";
            foreach(string item in items)
            {
                result += item + " ،";
            }
            result = result.TrimEnd('،');
            return result;
        }
    }
    #endregion
}