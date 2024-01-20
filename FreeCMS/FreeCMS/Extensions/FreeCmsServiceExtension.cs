using FreeCMS.DomainModels.System;
using FreeCMS.Repository.CMS.Abstraction;
using FreeCMS.Repository.CMS.Implemented;
using FreeCMS.Repository.System;
using FreeCMS.Service.CMS.Abstraction;
using FreeCMS.Service.CMS.Implemented;
using FreeCMS.Service.System;
using FreeCMS.Service.System.Abstraction;
using FreeCMS.Service.System.Implemented;

namespace FreeCMS.Extensions
{
    public static class FreeCmsServiceExtension
    {
        public static void ConfigFreeCmsServices(this IServiceCollection services)
        {
            services.AddScoped<IGalleryRepository, GalleryRepository>();
            services.AddScoped<IGalleryService, GalleryService>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostTopicRepository, PostTopicRepository>();
            services.AddScoped<IGalleryItemRepository, GalleryItemRepository>();
            services.AddScoped<IGalleryItemService, GalleryItemService>();
            services.AddScoped<FreeCmsRoleManager>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IRolePermissionServices, RolePermissionService>();
            services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
            services.AddScoped<IContactMessageService, ContactMessageService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();

        }
    }
}
