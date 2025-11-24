using SistemaAgendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SistemaAgendamento.Infrastructure.Data
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
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Sala> Salas { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.RotinasPermitidas)
                .WithMany(r => r.UsuariosPermitidos)
                .UsingEntity(j => j.ToTable("UsuarioRotinaPermissao"));

            modelBuilder.Entity<Agendamento>()
                .HasOne(a => a.Usuario)
                .WithMany()
                .HasForeignKey(a => a.UsuarioId);

            modelBuilder.Entity<Solicitacao>()
            .HasOne(s => s.Solicitante)
            .WithMany()
            .HasForeignKey(s => s.SolicitanteId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitacao>()
            .HasOne(s => s.Solicitado)
            .WithMany()
            .HasForeignKey(s => s.SolicitadoId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Solicitacao>()
            .HasOne(s => s.Agendamento)
            .WithMany()
            .HasForeignKey(s => s.AgendamentoId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}