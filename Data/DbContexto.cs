using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsStudio.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NailsStudio.Data
{
    public class DbContexto : IdentityDbContext<Usuario>
    {
        public DbContexto (DbContextOptions<DbContexto> options) : base(options) {}

        public DbSet<Servico> Servicos => Set<Servico>();
        public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Agendamento>()
            .HasOne(a => a.Usuario)
            .WithMany(u => u.Agendamentos)
            .HasForeignKey(a=>a.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Servico>()
            .Property(s=>s.Preco)
            .HasPrecision(10,2);
        }
    }
}