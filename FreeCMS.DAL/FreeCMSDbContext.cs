using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreeCMS.DAL;

public class FreeCMSDbContext : IdentityDbContext
{
    public FreeCMSDbContext(DbContextOptions<FreeCMSDbContext> options)
        : base(options)
    {
    }
}
