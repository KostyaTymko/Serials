using SiteSerials.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteSerials.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Serial> Serials { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
