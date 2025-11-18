using Agendamento.Core.Entities;
using Entities = Agendamento.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Sistema> Sistemas { get; set; }
        public DbSet<Modulo> Modulos { get; set; }
        public DbSet<Rotina> Rotinas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Entities.Agendamento> Agendamentos { get; set; }
        public DbSet<RegraDisponibilidade> RegrasDisponibilidade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.RotinasPermitidas)
                .WithMany(r => r.UsuariosPermitidos)
                .UsingEntity(j => j.ToTable("UsuarioRotinaPermissao"));

            modelBuilder.Entity<Entities.Agendamento>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<RegraDisponibilidade>()
    .HasOne(r => r.Usuario)
    .WithMany()
    .HasForeignKey(r => r.UsuarioId);
        }
    }
}