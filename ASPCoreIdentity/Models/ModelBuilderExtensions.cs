using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreIdentity.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee
                    {
                        Id = 2,
                        Name = "Mary",
                        Department = Dept.IT,
                        Email = "mary@pragimtech.com"
                    },
                    new Employee
                    {
                        Id = 3,
                        Name = "John",
                        Department = Dept.HR,
                        Email = "john@pragimtech.com"
                    }
                );
        }
    }
}
