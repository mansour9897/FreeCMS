namespace FreeCMS.Common.Utilities
{
    public class ImageNullValidator:FileNullValidator{}

    public class ImageMimeTypeValidator:FileMimeTypeValidator
    {
        public ImageMimeTypeValidator()
        {
            MimeTypes = new List<string>(){"image/jpg","image/jpeg","image/pjpeg","image/gif","image/x-png","image/png"};
        }
        public ImageMimeTypeValidator(List<string> mimeTypes)
        {
            MimeTypes = mimeTypes;
        }
    }
    
    public class ImageExtensionValidator:FileExtensionValidator
    {
        public ImageExtensionValidator()
        {
            Extensions = new List<string>{".jpg",".png",".gif",".jpeg"};
        }
        public ImageExtensionValidator(List<string> extensions)
        {
            this.Extensions = extensions;
        }
    }
    public class ImageSizeValidator:FileSizeValidator
    {
        private readonly int _defaultImageSize = 500 * 1000;
        public ImageSizeValidator()
        {
            DesiredSize = _defaultImageSize;
        }
        public ImageSizeValidator(int imageSize)
        {
            DesiredSize = imageSize;
        }
    }
    public class ImageFileChecker:BaseFileChecker
    {
        public ImageFileChecker()
        {
            Validators.Add(new ImageNullValidator());
            Validators.Add(new ImageSizeValidator());
            Validators.Add(new ImageMimeTypeValidator());
            Validators.Add(new ImageExtensionValidator());
        }
        public ImageFileChecker(int maxFileSize)
        {
            Validators.Add(new ImageNullValidator());
            Validators.Add(new ImageSizeValidator(maxFileSize));
            Validators.Add(new ImageMimeTypeValidator());
            Validators.Add(new ImageExtensionValidator());
        }
    }
}