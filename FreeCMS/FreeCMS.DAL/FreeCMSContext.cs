using FreeCMS.DomainModels.Cms;
using FreeCMS.DomainModels.Identity;
using FreeCMS.DomainModels.System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace FreeCMS.DAL
{
	public class FreeCMSContext : IdentityDbContext<ApplicationUser, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
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
		public DbSet<ContactMessage> ContactMessages { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<SocialNetwork> SocialNetworks { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>().HasMany(e => e.UserRoles)
				.WithOne(e => e.User)
				.HasForeignKey(ur => ur.UserId)
				.IsRequired();

			builder.Entity<Permission>().ToTable("Permissions");

			builder.Entity<PostTopic>().HasKey(pt => new { pt.PostId, pt.TopicId });
			
			builder.Entity<Role>().HasMany(e => e.UserRoles)
				.WithOne(e => e.Role)
				.HasForeignKey(ur => ur.RoleId)
				.IsRequired();

			builder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

			builder.Entity<RolePermission>().HasOne(rp => rp.Role)
				.WithMany(r => r.RolePermissions)
				.HasForeignKey(rp => rp.RoleId);

			builder.Entity<RolePermission>().HasOne(rp => rp.Permission)
				.WithMany(p => p.RolePermissions)
				.HasForeignKey(rp => rp.PermissionId);
		}
	}
}
