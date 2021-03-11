using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Models
{
    public class CompanyDBContext : IdentityDbContext<ApplicationUser>
    {

        public CompanyDBContext(DbContextOptions<CompanyDBContext> options) : base(options)
        {
            Database.EnsureCreated();

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=DESKTOP-SB3UMDG;Initial Catalog=CompanyDB;Integrated Security= True");
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
        //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   {
        ////      optionsBuilder.UseSqlServer(@"Server=DESKTOP-SB3UMDG;Initial Catalog=CompanyDB;Integrated Security= True");
        //       base.OnConfiguring(optionsBuilder);
        //   }


        public DbSet<Employee> Employees { get; set; }
    }
}
