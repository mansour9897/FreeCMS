namespace FreeCMS.Common.Utilities
{
    public class VideoNullValidator:FileNullValidator {}

    public class VideoMimeTypeValidator:FileMimeTypeValidator
    {
        private static readonly List<string> _mimeTypes = new List<string>{"video/mp4","video/avi"};
        public VideoMimeTypeValidator()
            :base(_mimeTypes){}
        public VideoMimeTypeValidator(List<string> mimeTypes)
            :base(mimeTypes)
        {}
    }
    public class VideoExtensionValidator:FileExtensionValidator
    {
        private static readonly List<string> _extensions = new List<string>{".mp4",".avi"};
        public VideoExtensionValidator()
            :base(_extensions){}
        public VideoExtensionValidator(List<string> extensions)
            :base(extensions){}
    }
    public class VideoSizeValidator:FileSizeValidator
    {
        private static readonly int _defaultVideoFileSize = 50000 * 1024;
        public VideoSizeValidator()
            :base(_defaultVideoFileSize){}
        public VideoSizeValidator(int desiredSize)
            :base(desiredSize){}
    }
    public class VideoFileChecker:BaseFileChecker
    {
        private readonly int _defaultVideoFileSize = 50000 * 1024;
        public VideoFileChecker()
        {
            Validators.Add(new VideoNullValidator());
            Validators.Add(new VideoSizeValidator(_defaultVideoFileSize));
            Validators.Add(new VideoMimeTypeValidator());
            Validators.Add(new VideoExtensionValidator());
        }
        public VideoFileChecker(int fileSize)
        {
            Validators.Add(new VideoNullValidator());
            Validators.Add(new VideoSizeValidator(fileSize));
            Validators.Add(new VideoMimeTypeValidator());
            Validators.Add(new VideoExtensionValidator());
        }
    }
}