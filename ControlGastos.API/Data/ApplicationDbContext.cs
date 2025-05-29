using Microsoft.EntityFrameworkCore;
using ControlGastos.Core.Entities;

namespace ControlGastos.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TipoGasto> TiposGasto { get; set; }
        public DbSet<FondoMonetario> FondosMonetarios { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<GastoEncabezado> GastoEncabezados { get; set; }
        public DbSet<GastoDetalle> GastoDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // relaciones y restricciones
            modelBuilder.Entity<GastoEncabezado>()
                .HasMany(e => e.Detalles)
                .WithOne(d => d.GastoEncabezado)
                .HasForeignKey(d => d.GastoEncabezadoId);

            modelBuilder.Entity<GastoDetalle>()
                .HasOne(d => d.TipoGasto)
                .WithMany()
                .HasForeignKey(d => d.TipoGastoId);

          
        }
    }
}