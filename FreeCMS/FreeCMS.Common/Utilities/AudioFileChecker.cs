namespace FreeCMS.Common.Utilities
{
    public class AudioNullValidator:FileNullValidator {}

    public class AudioMimeTypeValidator:FileMimeTypeValidator
    {
        private static readonly List<string> _mimeTypes = new List<string>{"audio/mp3","audio/mpeg","audio/vnd.wav"};
        public AudioMimeTypeValidator()
            :base(_mimeTypes){}
        public AudioMimeTypeValidator(List<string> mimeTypes)
            :base(mimeTypes)
        {}
    }
    public class AudioExtensionValidator:FileExtensionValidator
    {
        private static readonly List<string> _extensions = new List<string>{".mp3",".wav"};
        public AudioExtensionValidator()
            :base(_extensions){}
        public AudioExtensionValidator(List<string> extensions)
            :base(extensions){}
    }
    public class AudioSizeValidator:FileSizeValidator
    {
        private static readonly int _defaultAudioFileSize = 5000 * 1024;
        public AudioSizeValidator()
            :base(_defaultAudioFileSize){}
        public AudioSizeValidator(int desiredSize)
            :base(desiredSize){}
    }
    public class AudioFileChecker:BaseFileChecker
    {
        private readonly int _defaultAudioFileSize = 5000 * 1024;
        public AudioFileChecker()
        {
            Validators.Add(new AudioNullValidator());
            Validators.Add(new AudioSizeValidator(_defaultAudioFileSize));
            Validators.Add(new AudioMimeTypeValidator());
            Validators.Add(new AudioExtensionValidator());
        }
        public AudioFileChecker(int fileSize)
        {
            Validators.Add(new AudioNullValidator());
            Validators.Add(new AudioSizeValidator(fileSize));
            Validators.Add(new AudioMimeTypeValidator());
            Validators.Add(new AudioExtensionValidator());
        }
    }
}