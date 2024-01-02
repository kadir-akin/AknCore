using Core.Database.EF.Concrate;
using Microsoft.EntityFrameworkCore;

namespace Template_Api
{
    public class TemplateApiDbContext : AknDbContext
    {
        public DbSet<UserEntity> User { get; set; }
        public TemplateApiDbContext(DbContextOptions<AknDbContext> options) : base(options)
        {
        }
    }
}
