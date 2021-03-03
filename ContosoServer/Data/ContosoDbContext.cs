using ContosoServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoServer.Data
{
    public class ContosoDbContext :DbContext
    {
        public ContosoDbContext(DbContextOptions<ContosoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("tblCourse");
            modelBuilder.Entity<Student>().ToTable("tblStudent");

            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }


        public DbSet<Course> dbCourse { get; set; }
        public DbSet<Student> dbStudent { get; set; }
    }

}
