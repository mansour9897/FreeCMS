using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FreeCMS.DomainModels.System
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string ShareAddress { get; set;}
        public bool IsShareable { get; set; }
        public bool Display { get; set; }
    }
    public class SocialNetworkConfig:IEntityTypeConfiguration<SocialNetwork>
    {
        public void Configure(EntityTypeBuilder<SocialNetwork> builder)
        {
            builder.ToTable("Core_SocialNetworks");
        }
    }
}