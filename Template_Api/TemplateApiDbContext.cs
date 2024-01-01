using Core.Database.EF.Concrate;
using Microsoft.EntityFrameworkCore;

namespace Template_Api
{
    public class TemplateApiDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public TemplateApiDbContext(DbContextOptions<AknDbContext> options) : base(options)
        {
        }
    }
}
