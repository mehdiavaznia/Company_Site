using Company_site.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Company_Site.Infrastructure.Data
{
    public class DataBaseContext : IdentityDbContext<User,Role,string>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>(p => p.HasKey(p => new {p.ProviderKey,p.LoginProvider }));

            builder.Entity<IdentityUserRole<string>>(p => p.HasKey(p => new { p.UserId, p.RoleId }));
            builder.Entity<IdentityUserToken<string>>(p => p.HasKey(p => new { p.UserId, p.LoginProvider }));
        }
    }
}
