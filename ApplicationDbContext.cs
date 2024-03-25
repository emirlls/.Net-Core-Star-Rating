using Degerlendirme.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Degerlendirme.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Rating> Ratings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EMIR\\MSSQLSERVER01;Initial Catalog=Ratings;Integrated Security=True;Encrypt=False");
        }
 

    }
}
