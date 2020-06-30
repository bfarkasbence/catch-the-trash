using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using catch_the_trash.Models;

namespace catch_the_trash.Data
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<catch_the_trash.Models.User> User { get; set; }

        public DbSet<catch_the_trash.Models.Report> Report { get; set; }
    }
}
