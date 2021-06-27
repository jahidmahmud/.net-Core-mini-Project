using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Data
{
    public class BookStoreContext:DbContext
    {

        public BookStoreContext(DbContextOptions<BookStoreContext> option):base(option)
        {

        }
        public DbSet<Books> Books { set; get; }
        public DbSet<Language> Language { set; get; }
        public DbSet<BookGalary> BookGalary { set; get; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Sequrity=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
