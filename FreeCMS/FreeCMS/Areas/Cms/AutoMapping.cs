using FreeCMS.Areas.Cms.ViewModels.Item;
using FreeCMS.Areas.Cms.ViewModels.Page;
using FreeCMS.Areas.Cms.ViewModels.Post;
using FreeCMS.Areas.Cms.ViewModels.Topic;
using FreeCMS.Areas.Cms.ViewModels;
using FreeCMS.DomainModels.Cms;
using AutoMapper;
using FreeCMS.Areas.Cms.Utilities;
using FreeCMS.Areas.Cms.ViewModels.Slide;

namespace FreeCMS.Areas.Cms
{
	public class AutoMapping:Profile
	{
		public AutoMapping()
		{
			//topic maps
			CreateMap<Topic, NewTopic>().ReverseMap();
			CreateMap<Topic, EditableTopic>().ReverseMap();
			//post maps
			CreateMap<NewPost, Post>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));
			CreateMap<Post, EditablePost>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToViewMetaKeywords()));
			CreateMap<EditablePost, Post>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));

			//gallery maps
			CreateMap<NewGallery, Gallery>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));
			CreateMap<Gallery, EditableGallery>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToViewMetaKeywords()));
			CreateMap<EditableGallery, Gallery>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));

			//gallery item maps
			CreateMap<NewItem, GalleryItem>();
			CreateMap<GalleryItem, EditableItem>()
				.ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Gallery.Type));
			CreateMap<EditableItem, GalleryItem>();

			//page items
			CreateMap<NewPage, Page>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));
			CreateMap<Page, EditablePage>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToViewMetaKeywords()));
			CreateMap<EditablePage, Page>()
				.ForMember(dest => dest.MetaKeywords,
				opt => opt.MapFrom(src => src.MetaKeywords.ToMetaKeywords()));

            CreateMap<NewSlide, Slide>();
            CreateMap<Slide, EditableSlide>().ReverseMap();

        }

    }
}
