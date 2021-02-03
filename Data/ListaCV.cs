using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Website___Ricardo.Models;

namespace Website___Ricardo.Data
{
    public class ListaCV : DbContext
    {
        public ListaCV(DbContextOptions<ListaCV> options)
            : base(options) { }
             protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargo>() // Lado N
                .HasOne(p => p.Empresa) // uma empresa tem vários cargos
                .WithMany(c => c.Cargos) // que por sua vez tem vá
                .HasForeignKey(p => p.EmpresaId) // chave estrangeira
                .OnDelete(DeleteBehavior.Restrict); // não permitir o cascade delete
                   
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Website___Ricardo.Models.Cargo> Cargo { get; set; }

        public DbSet<Website___Ricardo.Models.Empresa> Empresa { get; set; }

        public DbSet<Website___Ricardo.Models.Cliente> Cliente { get; set; }
    }
}

