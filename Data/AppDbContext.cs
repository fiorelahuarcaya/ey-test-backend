using Microsoft.EntityFrameworkCore;
using ey_technical_test.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ey_technical_test.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Proveedor> Proveedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}