using FreeCMS.DomainModels.Cms;
using FreeCMS.DomainModels.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FreeCMS.DAL
{
	public class FreeCMSContext : IdentityDbContext<ApplicationUser, Role, int>
	{
		public FreeCMSContext(DbContextOptions<FreeCMSContext> options)
		: base(options)
		{
		}

		public DbSet<Gallery> Galleries { get; set; }
		public DbSet<GalleryItem> GalleryItems { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<Page> Pages { get; set; }
		public DbSet<Topic> Topics { get; set; }
		public DbSet<PostTopic> PostTopics { get; set; }
		public DbSet<Slide> Slides { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			builder.Entity<PostTopic>().HasKey(pt => new { pt.PostId, pt.TopicId });

		}
	}
}
