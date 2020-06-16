using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using catch_the_trash.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace catch_the_trash.Data
{
    public class UserContext : IdentityDbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<catch_the_trash.Models.User> User { get; set; }
    }
}
