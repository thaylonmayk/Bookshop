using Bookshop.Domain.Entities;
using Bookshop.Domain.Entities.View;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.Infra.Data.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }

        public virtual DbSet<LivroEntity> Livros { get; set; }
        public virtual DbSet<AutorEntity> Autores { get; set; }
        public virtual DbSet<AssuntoEntity> Assuntos { get; set; }
        public virtual DbSet<LivroAutorEntity> LivrosAutores { get; set; }
        public virtual DbSet<LivroAssuntoEntity> LivrosAssuntos { get; set; }
        public virtual DbSet<LivroAutorAssuntoView> LivrosAutoresAssuntos { get; set; }
        public virtual DbSet<CanalEntity> Canais { get; set; }
        public virtual DbSet<CanalPrecoEntity> CanalPrecos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LivroAutorEntity>()
                .HasKey(la => new { la.CodLivro, la.CodAutor });

            modelBuilder.Entity<LivroAutorEntity>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAutores)
                .HasForeignKey(la => la.CodLivro);

            modelBuilder.Entity<LivroAutorEntity>()
                .HasOne(la => la.Autor)
                .WithMany()
                .HasForeignKey(la => la.CodAutor);

            modelBuilder.Entity<LivroAssuntoEntity>()
                .HasKey(la => new { la.CodLivro, la.CodAssunto });

            modelBuilder.Entity<LivroAssuntoEntity>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivroAssuntos)
                .HasForeignKey(la => la.CodLivro);

            modelBuilder.Entity<LivroAssuntoEntity>()
                .HasOne(la => la.Assunto)
                .WithMany()
                .HasForeignKey(la => la.CodAssunto);

            modelBuilder.Entity<LivroEntity>()
                .Property(e => e.CreatedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<LivroEntity>()
                .Property(e => e.LastModifiedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<AutorEntity>()
                .Property(e => e.CreatedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<AutorEntity>()
                .Property(e => e.LastModifiedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<AssuntoEntity>()
                .Property(e => e.CreatedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<AssuntoEntity>()
                .Property(e => e.LastModifiedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<LivroAutorAssuntoView>()
                .ToView("vw_livros_autores_assuntos")
                .HasKey(e => new { e.LivroCod, e.AutorCod, e.AssuntoCod });

            modelBuilder.Entity<CanalPrecoEntity>()
                .HasOne(la => la.Canais)
                .WithMany(l => l.CanalPrecos)
                .HasForeignKey(la => la.CodCanal);

            modelBuilder.Entity<CanalPrecoEntity>()
                .HasOne(la => la.Livros)
                .WithMany(l => l.CanalPrecos)
                .HasForeignKey(la => la.CodLivro);

            modelBuilder.Entity<CanalPrecoEntity>()
                .Property(e => e.CreatedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));

            modelBuilder.Entity<CanalEntity>()
                .Property(e => e.CreatedDate)
                .HasConversion(v => v.UtcDateTime, v => new DateTimeOffset(v));
        }
    }
}