using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebSovelluksetFinal.Models;

namespace WebSovelluksetFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<HouseType>().ToTable("HouseType");
            builder.Entity<Tilaus>().ToTable("Order");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<WebSovelluksetFinal.Models.Tilaus> Orders { get; set; }
        public DbSet<WebSovelluksetFinal.Models.ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<WebSovelluksetFinal.Models.HouseType> HouseTypes { get; set; }
    }
}

