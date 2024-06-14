using Microsoft.EntityFrameworkCore;

namespace Eleven.Data.Entidad
{
    public partial class RepositoryPatternContext : DbContext
    {
        public RepositoryPatternContext()
        {

        }

        public RepositoryPatternContext(DbContextOptions<RepositoryPatternContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; } = null!;

        //public virtual DbSet<ValReturn<int>> ValReturn { get; set; } = null!;
        //public virtual DbSet<Product> Product { get; set; } = null!;
        //public virtual DbSet<Order> Order { get; set; } = null!;
        //public virtual DbSet<OrderDetails> OrderDetails { get; set; } = null!;
        //public virtual DbSet<OrderAgregado> OrderAgregado { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ValReturn<bool>>().HasNoKey();
            //modelBuilder.Entity<ValReturn<DateTime>>().HasNoKey();
            //modelBuilder.Entity<ValReturn<string>>().HasNoKey();

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdSIN)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            //modelBuilder.Entity<Product>(entity =>
            //{
            //    entity.Property(e => e.Description)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //    entity.Property(e => e.Name)
            //        .IsRequired()
            //        .HasMaxLength(50);

            //    entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            //});

            //modelBuilder.Entity<OrderAgregado>(entity =>
            //{
            //    entity.HasNoKey();
            //    entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            //});
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
