using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //initialize data
            builder.Entity<ToDo>().HasData(
                new ToDo
                {
                    Id = 1,
                    Title = "Analyze requirement",
                    Description = "Get requirement details and create estimation",
                    AssignedTo = "nail",
                    ExpiryDate = new DateTime(2020, 8, 19),
                    PercentComplete = 0
                }
            );

            builder.Entity<ToDo>().HasData(
                new ToDo
                {
                    Id = 2,
                    Title = "Develop restful API",
                    Description = "Create new restful api project",
                    AssignedTo = "nail",
                    ExpiryDate = new DateTime(2020, 8, 25),
                    PercentComplete = 0
                }
            );

            builder.Entity<ToDo>().HasData(
                new ToDo
                {
                    Id = 3,
                    Title = "Unit test",
                    Description = "Unit test the restful api",
                    AssignedTo = "nail",
                    ExpiryDate = new DateTime(2020, 8, 21),
                    PercentComplete = 0
                }
            );
        }
    }
}
