using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Database.EF.Concrate
{
    public class AknDbContext :DbContext
    {
        public AknDbContext(DbContextOptions<AknDbContext> options) :base(options)
        {

        }
    }
}
