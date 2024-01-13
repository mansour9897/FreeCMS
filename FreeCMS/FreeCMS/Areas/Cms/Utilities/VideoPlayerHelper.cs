using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    public static class VideoPlayerHelper
    {
        public static IHtmlContent DisplayVideo(this IHtmlHelper helper,string video)
        {
            if(video.Contains("www.aparat.com"))
            {
                int index = video.LastIndexOf("/v/");
                string videoUniqueCode = video.Substring(index + 3,5);
                string aparatFrame = @"<style>.h_iframe-aparat_embed_frame{position:relative;}.h_iframe-aparat_embed_frame .ratio"+
                    @"{display:block;width:100%;height:auto;}.h_iframe-aparat_embed_frame iframe{position:absolute;top:0;left:0;width:100%;height:100%;}"+
                    @"</style><div class=""h_iframe-aparat_embed_frame""><span style=""display: block;padding-top: 57%""></span>"+
                    @"<iframe src=""https://www.aparat.com/video/video/embed/videohash/"+ 
                    videoUniqueCode +
                    @"/vt/frame"" allowFullScreen=""true"" webkitallowfullscreen=""true"" mozallowfullscreen=""true""></iframe></div>";
                
                return helper.Raw(aparatFrame);
            }
            string localVideo = @"<video controls width=""100%""><source src=""{0}"" type=""video/mp4"" ></video>";
            return helper.Raw(string.Format(localVideo,video));
        }
    }
}