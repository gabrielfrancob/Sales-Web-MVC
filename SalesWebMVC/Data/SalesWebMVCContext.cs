using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Models;

namespace SalesWebMVC.Data
{
    public class SalesWebMVCContext : DbContext
    {
        public SalesWebMVCContext(DbContextOptions<SalesWebMVCContext> options) : base(options) { }
        public DbSet<Department> Department { get; set; } = default!;
        public DbSet<Seller> Seller { get; set; } = default!;
        public DbSet<SalesRecord> SalesRecords { get; set; } = default!;

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Department>().HasData(
        //     new Department { Id = 1, Name = "Computadores" },
        //     new Department { Id = 2, Name = "Eletronicos" },
        //     new Department { Id = 3, Name = "Moda" },
        //     new Department { Id = 4, Name = "Livros" });

        //    Department d1 = new Department(1, "Computers");

        //    Department d2 = new Department(2, "Electronics");

        //    Department d3 = new Department(3, "Fashion");

        //    Department d4 = new Department(4, "Books");

        //    modelBuilder.Entity<Seller>().HasData(
        //        new Seller { Id = 1, Name = "Bob Brown", Email = "bob@gmail.com", BirthDate = new DateTime(1998, 4, 21), BaseSalary = 1000.0, Department = d1 },
        //        new Seller { 2, "Maria Green", "maria@gmail.com", DateTime(1979, 12, 31), 3500.0, 2 },
        //        new Seller { 3, "Alex Grey", "alex@gmail.com", DateTime(1988, 1, 15), 2200.0, 1 },
        //        new Seller { 4, "Martha Red", "martha@gmail.com", DateTime(1993, 11, 30), 3000.0, 4 },
        //        new Seller { 5, "Donald Blue", "donald@gmail.com", DateTime(2000, 1, 9), 4000.0, 3 },
        //        new Seller { 6, "Alex Pink", "bob@gmail.com", DateTime(1997, 3, 4), 3000.0, 2 }
        //        );
        //}
    }
}
