namespace FreeCMS.Common.Utilities
{
    public class ZipNullValidator:FileNullValidator{}

    public class ZipMimeTypeValidator:FileMimeTypeValidator
    {
        public ZipMimeTypeValidator()
        {
            MimeTypes = new List<string>(){"application/zip", "application/octet-stream", "application/x-zip-compressed", "multipart/x-zip"};
        }
        public ZipMimeTypeValidator(List<string> mimeTypes)
        {
            MimeTypes = mimeTypes;
        }
    }
    public class ZipExtensionValidator:FileExtensionValidator
    {
        public ZipExtensionValidator()
        {
            Extensions = new List<string>{".zip"};
        }
        public ZipExtensionValidator(List<string> extensions)
        {
            this.Extensions = extensions;
        }
    }
    public class ZipFileChecker:BaseFileChecker
    {
        public ZipFileChecker()
        {
            Validators.Add(new ZipNullValidator());
            Validators.Add(new ZipMimeTypeValidator());
            Validators.Add(new ZipExtensionValidator());
        }
    }
    
}