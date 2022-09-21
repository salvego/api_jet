using api_jet.Data.Mappings;
using api_jet.Models;
using Microsoft.EntityFrameworkCore;

namespace api_jet.Data
{
    public class JetDataContext : DbContext
    {
        public JetDataContext(DbContextOptions<JetDataContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
        }

    }
}